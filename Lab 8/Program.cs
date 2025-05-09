using System.Collections.Concurrent;
using System.Threading.Tasks.Dataflow;
using Lab_8;

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

var results = new ConcurrentDictionary<string, int>();

var loadFileBlock = new TransformBlock<string, (ProgressReport FileInfo, List<string> Lines)>(
    filePath => 
    {
        string fileName = Path.GetFileName(filePath);
        var lines = new List<string>();
        
        try
        {
            lines = File.ReadAllLines(filePath).ToList();
            int totalLines = lines.Count;
            
            Console.WriteLine($"Starting to process {fileName} with {totalLines} lines");
            
            return (new ProgressReport
            {
                FileName = filePath,
                CurrentLine = 0,
                TotalLines = totalLines
            }, lines);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing {fileName}: {ex.Message}");
            return (new ProgressReport
            {
                FileName = filePath,
                CurrentLine = 0,
                TotalLines = 0
            }, lines);
        }
    },
    new ExecutionDataflowBlockOptions
    {
        MaxDegreeOfParallelism = Environment.ProcessorCount
    });

var processBlock = new TransformBlock<(ProgressReport FileInfo, List<string> Lines), FileSearchResult>(
    data => 
    {
        var (fileInfo, lines) = data;
        FileSearchResult result = ProcessFileContent(fileInfo, lines, searchString, results);
        Console.WriteLine($"{Path.GetFileName(result.FileName)}: Search completed, {result.OccurrencesCount} occurrences found");
        return result;
    },
    new ExecutionDataflowBlockOptions
    {
        MaxDegreeOfParallelism = Environment.ProcessorCount
    });

var completionBlock = new ActionBlock<FileSearchResult>(
    _ => { },
    new ExecutionDataflowBlockOptions
    {
        MaxDegreeOfParallelism = 1
    });

loadFileBlock.LinkTo(processBlock, new DataflowLinkOptions { PropagateCompletion = true });
processBlock.LinkTo(completionBlock, new DataflowLinkOptions { PropagateCompletion = true });

foreach (var file in textFiles)
{
    loadFileBlock.Post(file);
}

loadFileBlock.Complete();

await completionBlock.Completion;

Console.WriteLine("\nFinished!!! Results:");
foreach (var result in results.OrderByDescending(r => r.Value))
{
    Console.WriteLine($"{Path.GetFileName(result.Key)}: {result.Value} occurrences");
}
return;

FileSearchResult ProcessFileContent(ProgressReport fileInfo, List<string> lines, string searchString, ConcurrentDictionary<string, int> results)
{
    string fileName = Path.GetFileName(fileInfo.FileName);
    int occurrences = 0;
    int processedLines = 0;
    int lastReportedPercentage = -1;
    var lastProgressUpdate = DateTime.MinValue;
    
    int totalLines = lines.Count;
    
    if (totalLines == 0)
    {
        Console.WriteLine($"{fileName} is empty");
        results[fileInfo.FileName] = 0;
        return new FileSearchResult 
        { 
            FileName = fileInfo.FileName,
            OccurrencesCount = 0,
            TotalLines = 0
        };
    }
    
    foreach (var line in lines)
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
    
    results[fileInfo.FileName] = occurrences;
    
    return new FileSearchResult
    {
        FileName = fileInfo.FileName,
        OccurrencesCount = occurrences,
        TotalLines = totalLines
    };
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