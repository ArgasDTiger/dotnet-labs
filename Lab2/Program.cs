using Lab2;

Console.WriteLine("=== Тестування Team ===");
var team1 = new Team("Команда A", 1);
var team2 = new Team("Команда A", 1);

Console.WriteLine($"team1 == team2: {team1 == team2}");
Console.WriteLine($"Однаковi посилання: {ReferenceEquals(team1, team2)}");
Console.WriteLine($"Hash team1: {team1.GetHashCode()}");
Console.WriteLine($"Hash team2: {team2.GetHashCode()}");

Console.WriteLine("\n=== Тестування виключення ===");
try
{
    team1.RegistrationNumber = 0;
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Помилка: {ex.Message}");
}

Console.WriteLine("\n=== Тестування ResearchTeam ===");
var research = new ResearchTeam(
    "Розслiдування",
    "Лабораторiя",
    123,
    TimeFrame.TwoYears
);

var person1 = new Person("Пан", "Коцький", new DateTime(1997, 12, 1));
var person2 = new Person("Володимир", "Василь", new DateTime(2014, 2, 2));
research.Members?.Add(person1);
research.Members?.Add(person2);

var paper1 = new Paper("Основи Пайтона", person1, DateTime.Now.AddYears(-1));
var paper2 = new Paper("Платформи корпоративних систем", person1, DateTime.Now.AddYears(-3));
research.AddPapers(paper1, paper2);

Console.WriteLine("Перший ResearchTeam:");
Console.WriteLine(research.ToString());

Console.WriteLine("\nTeam:");
Console.WriteLine(research.Team.ToString());

Console.WriteLine("\n=== Тестування DeepCopy ===");
var copy = (ResearchTeam)research.DeepCopy();
research.Organization = "Лабораторiя";
research.Members?.RemoveAt(0);

copy.Organization = "Testing.";

Console.WriteLine("Перший варiант пiсля копiювання:");
Console.WriteLine(research.ToString());
Console.WriteLine("\nКопiя:");
Console.WriteLine(copy.ToString());

Console.WriteLine("\n=== Учасники з публiкацiями ===");
foreach (Person person in copy.MembersWithoutPublications())
{
    Console.WriteLine(person.ToString());
}

Console.WriteLine("\n=== Публiкацiї за останнi 2 роки ===");
foreach (Paper paper in copy.GetPublicationsInLastSpecifiedYears(2))
{
    Console.WriteLine(paper.ToString());
}