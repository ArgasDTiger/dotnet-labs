using System.Threading.Tasks.Dataflow;

string? directoryPath = null;

while (string.IsNullOrEmpty(directoryPath))
{
    Console.WriteLine("Enter not nul and not empty directory with txt files:");
    directoryPath = Console.ReadLine();
}

if (!Path.IsPathRooted(directoryPath))
{
    directoryPath = Path.Combine(Directory.GetCurrentDirectory(), directoryPath);
}

if (!Directory.Exists(directoryPath))
{
    Console.WriteLine($"Directory {directoryPath} is not found.");
    return;
}

string? searchString = null;

while (string.IsNullOrEmpty(searchString))
{
    Console.WriteLine("Enter the search string:");
    searchString = Console.ReadLine();
}
var textFiles = Directory.GetFiles(directoryPath, "*.txt");
if (textFiles.Length == 0)
{
    Console.WriteLine("No text files was found in the directory.");
    return;
}

Console.WriteLine("Max degree: " + Environment.ProcessorCount);


Console.WriteLine($"Found {textFiles.Length} text files\n");

var results = new Dictionary<string, int>();

var searchBlock = new TransformBlock<string, (string FilePath, int Count)>(
    filePath => SearchInFile(filePath, searchString, results),
    new ExecutionDataflowBlockOptions
    {
        MaxDegreeOfParallelism = Environment.ProcessorCount
    });

var printBlock = new ActionBlock<(string FilePath, int Count)>(
    _ => { });

searchBlock.LinkTo(printBlock, new DataflowLinkOptions { PropagateCompletion = true });

foreach (var file in textFiles)
{
    searchBlock.Post(file);
}

searchBlock.Complete();
await printBlock.Completion;

Console.WriteLine("\nFinished!!! Results:");
foreach (var result in results)
{
    Console.WriteLine($"{Path.GetFileName(result.Key)}: {result.Value} occurrences");
}
return;

(string, int) SearchInFile(string filePath, string searchString,  Dictionary<string, int> results)
{
    string fileName = Path.GetFileName(filePath);
    int occurrences = 0;
    int processedLines = 0;
    int lastReportedPercentage = -1;
    var lastProgressUpdate = DateTime.MinValue;
    
    try
    {
        var totalLines = File.ReadLines(filePath).Count();
        
        if (totalLines == 0)
        {
            Console.WriteLine($"{fileName} is empty");
            results[filePath] = 0;
            return (filePath, 0);
        }
        
        using (var reader = new StreamReader(filePath))
        {
            while (reader.ReadLine() is { } line)
            {
                processedLines++;
                
                int lineOccurrences = CountOccurrences(line, searchString);
                occurrences += lineOccurrences;
                
                int currentPercentage = (int)((double)processedLines / totalLines * 100);
                if ((currentPercentage != lastReportedPercentage && 
                    (DateTime.Now - lastProgressUpdate).TotalSeconds >= 0.05) ||
                    processedLines == totalLines)
                {
                    Console.WriteLine($"{fileName}: processed {currentPercentage}%");
                    lastReportedPercentage = currentPercentage;
                    lastProgressUpdate = DateTime.Now;
                }
            }
        }
        
        results[filePath] = occurrences;
        
        return (filePath, occurrences);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error processing {fileName}: {ex.Message}");
        results[filePath] = -1;
        return (filePath, -1);
    }
}

int CountOccurrences(string? source, string searchString)
{
    int count = 0;
    int position = 0;
    
    while (source != null && (position = source.IndexOf(searchString, position, StringComparison.OrdinalIgnoreCase)) != -1)
    {
        count++;
        position += searchString.Length;
    }
    
    return count;
}