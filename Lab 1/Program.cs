using Lab_1;

Console.WriteLine("Введiть цiле номер рядкiв, а потiм стовпцiв, роздiлних пробiлом, комою або крапкою з комою:");
string inputLine = Console.ReadLine();
string[] input = inputLine.Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

int nRows = int.Parse(input[0]);
int nColumns = int.Parse(input[1]);
var nRowsColumns = nRows * nColumns;

var oneDimensional = new Paper[nRowsColumns];
var twoDimensional = new Paper[nRows, nColumns];

int acc = 0, maxRows = 0;
while (acc < nRowsColumns)
{
    maxRows++;
    acc += maxRows;
}

var jaggedArray = new Paper[maxRows][];
for (int i = 0; i < maxRows - 1; i++)
{
    jaggedArray[i] = new Paper[i + 1];
}

jaggedArray[maxRows - 1] = new Paper[maxRows - (acc - nRowsColumns)];


for (int i = 0; i < nRowsColumns; i++)
{
    oneDimensional[i] = new Paper();
}

for (int i = 0; i < nRows; i++)
{
    for (int j = 0; j < nColumns; j++)
    {
        twoDimensional[i, j] = new Paper();
    }
}

for (int i = 0; i < jaggedArray.Length; i++)
{
    for (int j = 0; j < jaggedArray[i].Length; j++)
    {
        jaggedArray[i][j] = new Paper();
    }
}

int startTime = Environment.TickCount;
for (int i = 0; i < nRowsColumns; i++)
{
    oneDimensional[i].Author.BirthYear = 2000;
}
int oneDimTime = Environment.TickCount - startTime;

startTime = Environment.TickCount;
for (int i = 0; i < nRows; i++)
{
    for (int j = 0; j < nColumns; j++)
    {
        twoDimensional[i, j].Author.BirthYear = 2000;
    }
}
int twoDimTime = Environment.TickCount - startTime;

startTime = Environment.TickCount;
for (int i = 0; i < jaggedArray.Length; i++)
{
    for (int j = 0; j < jaggedArray[i].Length; j++)
    {
        jaggedArray[i][j].Author.BirthYear = 2000;
    }
}
int jaggedTime = Environment.TickCount - startTime;

Console.WriteLine("\nЧас:");
Console.WriteLine($"1-вимiрний: {oneDimTime}");
Console.WriteLine($"2-вимiрний: {twoDimTime}");
Console.WriteLine($"Зубчатий: {jaggedTime}");

var team = new ResearchTeam
{
    ResearchTopic = "Пошук розв'язку нової задачi Кошi",
    Organization = "ЧНУ"
};
Console.WriteLine("\nToShortString:");
Console.WriteLine(team.ToShortString());

Console.WriteLine("\nTimeFrame iндекс:");
Console.WriteLine($"Year: {team[TimeFrame.Year]}");
Console.WriteLine($"TwoYears: {team[TimeFrame.TwoYears]}");
Console.WriteLine($"Long: {team[TimeFrame.Long]}");

Console.WriteLine("\nToString");
Console.WriteLine(team.ToString());

var paper1 = new Paper("Держава", new Person("Платон", "Платон", new DateTime(350, 5, 5)), 
    new DateTime(360, 1, 1));
var paper2 = new Paper("Енеїда", new Person("Iван", "Котляревський", new DateTime(1769, 9, 9)), 
    new DateTime(1842, 6, 1));
team.AddPapers(paper1, paper2);

Console.WriteLine("\nПiсля додавання видань:");
Console.WriteLine(team.ToString());

Console.WriteLine("\nОстання публiкацiя:");
Console.WriteLine(team.LastPublication?.ToString() ?? "Публiкацiї вiдсутнi");