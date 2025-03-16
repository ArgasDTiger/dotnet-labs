using Lab3;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Частина 1: Тестування класу ResearchTeamCollection");
Console.WriteLine("=================================================");

ResearchTeamCollection collection = new ResearchTeamCollection();
collection.AddDefaults();

ResearchTeam team1 = new ResearchTeam("ЧНУ", 789, "Комп'ютерні науки", TimeFrame.Year);
team1.AddParticipants(
    new Person("Андрій", "Дорош", new DateTime(1985, 5, 15)),
    new Person("Марія", "Коваленко", new DateTime(1990, 3, 20))
);
team1.AddPapers(
    new Paper(",NET", team1.Participants[0], new DateTime(2023, 1, 10)),
    new Paper("PHP", team1.Participants[1], new DateTime(2023, 5, 22))
);

ResearchTeam team2 = new ResearchTeam("Львівська політехніка", 456, "Бази даних", TimeFrame.TwoYears);
team2.AddParticipants(
    new Person("Олег", "Сидоренко", new DateTime(1982, 10, 8)),
    new Person("Наталія", "Шевченко", new DateTime(1988, 7, 12)),
    new Person("Андрій", "Мельник", new DateTime(1995, 2, 3))
);
team2.AddPapers(
    new Paper("NoSQL", team2.Participants[0], new DateTime(2022, 11, 5)),
    new Paper("Тестування", team2.Participants[1], new DateTime(2023, 3, 15)),
    new Paper("Англійська мова", team2.Participants[2], new DateTime(2023, 8, 10))
);

collection.AddResearchTeams(team1, team2);

Console.WriteLine(collection);

Console.WriteLine("\nСортування за реєстраційним номером:");
collection.SortByRegistrationNumber();
Console.WriteLine(collection.ToShortList());

Console.WriteLine("\nСортування за темою дослідження:");
collection.SortByTopic();
Console.WriteLine(collection.ToShortList());

Console.WriteLine("\nСортування за кількістю публікацій:");
collection.SortByPublicationCount();
Console.WriteLine(collection.ToShortList());

Console.WriteLine($"\nМінімальний реєстраційний номер: {collection.MinRegistrationNumber}");

Console.WriteLine("\nПроекти тривалістю TwoYears:");
foreach (var team in collection.TwoYearProjects)
{
    Console.WriteLine(team.ToShortString());
}

Console.WriteLine("\nГрупування за кількістю учасників (2):");
var teamsWith2Participants = collection.NGroup(2);
foreach (var team in teamsWith2Participants)
{
    Console.WriteLine(team.ToShortString());
}

Console.WriteLine("\n\nЧастина 2: Тестування класу TestCollections");
Console.WriteLine("=================================================");

int collectionSize = GetCollectionSizeFromUser();
TestCollections testCollections = new TestCollections(collectionSize);
testCollections.SearchElementsAndMeasureTime();

Console.WriteLine("\nПрограма завершила роботу.");
return;

int GetCollectionSizeFromUser()
{
    int size = 0;
    bool validInput = false;

    while (!validInput)
    {
        Console.Write("\nВведіть розмір колекцій для тестування: ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out size) && size > 0)
        {
            validInput = true;
        }
        else
        {
            Console.WriteLine("Помилка! Введіть додатне ціле число.");
        }
    }

    return size;
}