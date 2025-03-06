
using System.Collections.Immutable;
using Lab_4;

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
    new Paper(".NET", team1.Participants[0], new DateTime(2023, 1, 10)),
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

Console.WriteLine("\n\nЧастина 3: Тестування часу операцій для різних типів колекцій");
Console.WriteLine("=================================================");

TestCollectionOperations(collectionSize);

Console.WriteLine("\nПрограма завершила роботу.");

void TestCollectionOperations(int size)
{
    Console.WriteLine($"\nТестування операцій на колекціях розміром {size}:");
    
    var data = new List<ResearchTeam>();
    for (int i = 0; i < size; i++)
    {
        data.Add(TestCollections.GenerateResearchTeam(i));
    }
    
    Console.WriteLine("\n1. Час створення колекцій:");
    
    int startTime = Environment.TickCount;
    var standardList = new List<ResearchTeam>(data);
    int standardListTime = Environment.TickCount - startTime;
    Console.WriteLine($"- List<ResearchTeam>: {standardListTime} мс");
    
    startTime = Environment.TickCount;
    var immutableList = ImmutableList.CreateRange(data);
    int immutableListTime = Environment.TickCount - startTime;
    Console.WriteLine($"- ImmutableList<ResearchTeam>: {immutableListTime} мс");
    
    var keyValuePairs = data.ToDictionary(team => team.Team);
    
    startTime = Environment.TickCount;
    var standardDict = new Dictionary<Team, ResearchTeam>(keyValuePairs);
    int standardDictTime = Environment.TickCount - startTime;
    Console.WriteLine($"- Dictionary<Team, ResearchTeam>: {standardDictTime} мс");
    
    startTime = Environment.TickCount;
    var immutableDict = ImmutableDictionary.CreateRange(keyValuePairs);
    int immutableDictTime = Environment.TickCount - startTime;
    Console.WriteLine($"- ImmutableDictionary<Team, ResearchTeam>: {immutableDictTime} мс");
    
    startTime = Environment.TickCount;
    var sortedDict = new SortedDictionary<Team, ResearchTeam>(keyValuePairs, new TeamComparer());
    int sortedDictTime = Environment.TickCount - startTime;
    Console.WriteLine($"- SortedDictionary<Team, ResearchTeam>: {sortedDictTime} мс");
    
    startTime = Environment.TickCount;
    var sortedList = new SortedList<Team, ResearchTeam>(keyValuePairs, new TeamComparer());
    int sortedListTime = Environment.TickCount - startTime;
    Console.WriteLine($"- SortedList<Team, ResearchTeam>: {sortedListTime} мс");
    
    Console.WriteLine("\n2. Час додавання елементу:");
    
    var newTeam = TestCollections.GenerateResearchTeam(size + 1);
    
    startTime = Environment.TickCount;
    standardList.Add(newTeam);
    int addToStandardListTime = Environment.TickCount - startTime;
    Console.WriteLine($"- List<ResearchTeam>.Add: {addToStandardListTime} мс");
    
    startTime = Environment.TickCount;
    var newImmutableList = immutableList.Add(newTeam);
    int addToImmutableListTime = Environment.TickCount - startTime;
    Console.WriteLine($"- ImmutableList<ResearchTeam>.Add: {addToImmutableListTime} мс");
    
    var newTeamKey = newTeam.Team;
    
    startTime = Environment.TickCount;
    standardDict.Add(newTeamKey, newTeam);
    int addToStandardDictTime = Environment.TickCount - startTime;
    Console.WriteLine($"- Dictionary<Team, ResearchTeam>.Add: {addToStandardDictTime} мс");
    
    startTime = Environment.TickCount;
    var newImmutableDict = immutableDict.Add(newTeamKey, newTeam);
    int addToImmutableDictTime = Environment.TickCount - startTime;
    Console.WriteLine($"- ImmutableDictionary<Team, ResearchTeam>.Add: {addToImmutableDictTime} мс");
    
    startTime = Environment.TickCount;
    sortedDict.Add(newTeamKey, newTeam);
    int addToSortedDictTime = Environment.TickCount - startTime;
    Console.WriteLine($"- SortedDictionary<Team, ResearchTeam>.Add: {addToSortedDictTime} мс");
    
    var anotherNewTeam = TestCollections.GenerateResearchTeam(size + 2);
    var anotherNewTeamKey = anotherNewTeam.Team;
    
    startTime = Environment.TickCount;
    sortedList.Add(anotherNewTeamKey, anotherNewTeam);
    int addToSortedListTime = Environment.TickCount - startTime;
    Console.WriteLine($"- SortedList<Team, ResearchTeam>.Add: {addToSortedListTime} мс");
    
    Console.WriteLine("\n3. Час пошуку елементу (середина колекції):");
    
    int middleIndex = size / 2;
    var middleTeam = data[middleIndex];
    var middleTeamKey = middleTeam.Team;
    
    startTime = Environment.TickCount;
    bool containsInStandardList = standardList.Contains(middleTeam);
    int findInStandardListTime = Environment.TickCount - startTime;
    Console.WriteLine($"- List<ResearchTeam>.Contains: {findInStandardListTime} мс ({containsInStandardList})");
    
    startTime = Environment.TickCount;
    bool containsInImmutableList = immutableList.Contains(middleTeam);
    int findInImmutableListTime = Environment.TickCount - startTime;
    Console.WriteLine($"- ImmutableList<ResearchTeam>.Contains: {findInImmutableListTime} мс ({containsInImmutableList})");
    
    startTime = Environment.TickCount;
    bool containsKeyInStandardDict = standardDict.ContainsKey(middleTeamKey);
    int findKeyInStandardDictTime = Environment.TickCount - startTime;
    Console.WriteLine($"- Dictionary<Team, ResearchTeam>.ContainsKey: {findKeyInStandardDictTime} мс ({containsKeyInStandardDict})");
    
    startTime = Environment.TickCount;
    bool containsKeyInImmutableDict = immutableDict.ContainsKey(middleTeamKey);
    int findKeyInImmutableDictTime = Environment.TickCount - startTime;
    Console.WriteLine($"- ImmutableDictionary<Team, ResearchTeam>.ContainsKey: {findKeyInImmutableDictTime} мс ({containsKeyInImmutableDict})");
    
    startTime = Environment.TickCount;
    bool containsKeyInSortedDict = sortedDict.ContainsKey(middleTeamKey);
    int findKeyInSortedDictTime = Environment.TickCount - startTime;
    Console.WriteLine($"- SortedDictionary<Team, ResearchTeam>.ContainsKey: {findKeyInSortedDictTime} мс ({containsKeyInSortedDict})");
    
    startTime = Environment.TickCount;
    bool containsKeyInSortedList = sortedList.ContainsKey(middleTeamKey);
    int findKeyInSortedListTime = Environment.TickCount - startTime;
    Console.WriteLine($"- SortedList<Team, ResearchTeam>.ContainsKey: {findKeyInSortedListTime} мс ({containsKeyInSortedList})");
    
    Console.WriteLine("\n4. Час видалення елементу (середина колекції):");
    
    startTime = Environment.TickCount;
    bool removedFromStandardList = standardList.Remove(middleTeam);
    int removeFromStandardListTime = Environment.TickCount - startTime;
    Console.WriteLine($"- List<ResearchTeam>.Remove: {removeFromStandardListTime} мс ({removedFromStandardList})");
    
    startTime = Environment.TickCount;
    var newImmutableListAfterRemove = immutableList.Remove(middleTeam);
    int removeFromImmutableListTime = Environment.TickCount - startTime;
    Console.WriteLine($"- ImmutableList<ResearchTeam>.Remove: {removeFromImmutableListTime} мс (результат: {!immutableList.Equals(newImmutableListAfterRemove)})");
    
    startTime = Environment.TickCount;
    bool removedFromStandardDict = standardDict.Remove(middleTeamKey);
    int removeFromStandardDictTime = Environment.TickCount - startTime;
    Console.WriteLine($"- Dictionary<Team, ResearchTeam>.Remove: {removeFromStandardDictTime} мс ({removedFromStandardDict})");
    
    startTime = Environment.TickCount;
    var newImmutableDictAfterRemove = immutableDict.Remove(middleTeamKey);
    int removeFromImmutableDictTime = Environment.TickCount - startTime;
    Console.WriteLine($"- ImmutableDictionary<Team, ResearchTeam>.Remove: {removeFromImmutableDictTime} мс (результат: {!immutableDict.Equals(newImmutableDictAfterRemove)})");
    
    startTime = Environment.TickCount;
    bool removedFromSortedDict = sortedDict.Remove(middleTeamKey);
    int removeFromSortedDictTime = Environment.TickCount - startTime;
    Console.WriteLine($"- SortedDictionary<Team, ResearchTeam>.Remove: {removeFromSortedDictTime} мс ({removedFromSortedDict})");
    
    startTime = Environment.TickCount;
    bool removedFromSortedList = sortedList.Remove(middleTeamKey);
    int removeFromSortedListTime = Environment.TickCount - startTime;
    Console.WriteLine($"- SortedList<Team, ResearchTeam>.Remove: {removeFromSortedListTime} мс ({removedFromSortedList})");
    
    Console.WriteLine("\nАналіз результатів:");
    Console.WriteLine("--------------------");
    
    Console.WriteLine("Найшвидше створення:");
    var creationTimes = new List<(string, int)>
    {
        ("List<ResearchTeam>", standardListTime),
        ("ImmutableList<ResearchTeam>", immutableListTime),
        ("Dictionary<Team, ResearchTeam>", standardDictTime),
        ("ImmutableDictionary<Team, ResearchTeam>", immutableDictTime),
        ("SortedDictionary<Team, ResearchTeam>", sortedDictTime),
        ("SortedList<Team, ResearchTeam>", sortedListTime)
    };
    var fastestCreation = creationTimes.MinBy(t => t.Item2);
    Console.WriteLine($"- {fastestCreation.Item1}: {fastestCreation.Item2} мс");
    
    Console.WriteLine("Найшвидше додавання:");
    List<(string, int)> additionTimes = new List<(string, int)>
    {
        ("List<ResearchTeam>.Add", addToStandardListTime),
        ("ImmutableList<ResearchTeam>.Add", addToImmutableListTime),
        ("Dictionary<Team, ResearchTeam>.Add", addToStandardDictTime),
        ("ImmutableDictionary<Team, ResearchTeam>.Add", addToImmutableDictTime),
        ("SortedDictionary<Team, ResearchTeam>.Add", addToSortedDictTime),
        ("SortedList<Team, ResearchTeam>.Add", addToSortedListTime)
    };
    var fastestAddition = additionTimes.MinBy(t => t.Item2);
    Console.WriteLine($"- {fastestAddition.Item1}: {fastestAddition.Item2} мс");
    
    Console.WriteLine("Найшвидший пошук:");
    var searchTimes = new List<(string, int)>
    {
        ("List<ResearchTeam>.Contains", findInStandardListTime),
        ("ImmutableList<ResearchTeam>.Contains", findInImmutableListTime),
        ("Dictionary<Team, ResearchTeam>.ContainsKey", findKeyInStandardDictTime),
        ("ImmutableDictionary<Team, ResearchTeam>.ContainsKey", findKeyInImmutableDictTime),
        ("SortedDictionary<Team, ResearchTeam>.ContainsKey", findKeyInSortedDictTime),
        ("SortedList<Team, ResearchTeam>.ContainsKey", findKeyInSortedListTime)
    };
    var fastestSearch = searchTimes.MinBy(t => t.Item2);
    Console.WriteLine($"- {fastestSearch.Item1}: {fastestSearch.Item2} мс");
    
    Console.WriteLine("Найшвидше видалення:");
    var removalTimes = new List<(string, int)>
    {
        ("List<ResearchTeam>.Remove", removeFromStandardListTime),
        ("ImmutableList<ResearchTeam>.Remove", removeFromImmutableListTime),
        ("Dictionary<Team, ResearchTeam>.Remove", removeFromStandardDictTime),
        ("ImmutableDictionary<Team, ResearchTeam>.Remove", removeFromImmutableDictTime),
        ("SortedDictionary<Team, ResearchTeam>.Remove", removeFromSortedDictTime),
        ("SortedList<Team, ResearchTeam>.Remove", removeFromSortedListTime)
    };
    var fastestRemoval = removalTimes.MinBy(t => t.Item2);
    Console.WriteLine($"- {fastestRemoval.Item1}: {fastestRemoval.Item2} мс");
    
    Console.WriteLine("\nРезультааат:");
    Console.WriteLine("-------------------");
    
    var avgTimeByCollection = new Dictionary<string, double>
    {
        ["Standard List"] = (standardListTime + addToStandardListTime + findInStandardListTime + removeFromStandardListTime) / 4.0,
        ["Immutable List"] = (immutableListTime + addToImmutableListTime + findInImmutableListTime + removeFromImmutableListTime) / 4.0,
        ["Standard Dictionary"] = (standardDictTime + addToStandardDictTime + findKeyInStandardDictTime + removeFromStandardDictTime) / 4.0,
        ["Immutable Dictionary"] = (immutableDictTime + addToImmutableDictTime + findKeyInImmutableDictTime + removeFromImmutableDictTime) / 4.0,
        ["Sorted Dictionary"] = (sortedDictTime + addToSortedDictTime + findKeyInSortedDictTime + removeFromSortedDictTime) / 4.0,
        ["Sorted List"] = (sortedListTime + addToSortedListTime + findKeyInSortedListTime + removeFromSortedListTime) / 4.0
    };

    var mostEfficient = avgTimeByCollection.MinBy(kvp => kvp.Value);
    Console.WriteLine($"Найефективніша колекція: {mostEfficient.Key} з середнім часом виконання операцій {mostEfficient.Value:F2} мс");
}

int GetCollectionSizeFromUser()
{
    int size = 1000;
    bool validInput = false;
    
    while (!validInput)
    {
        Console.Write("Введіть розмір масиву: ");
        string? input = Console.ReadLine();
        
        if (int.TryParse(input, out int inputSize) && inputSize > 0)
        {
            size = inputSize;
            validInput = true;
        }
        else
        {
            Console.WriteLine("Некоректне значення. Будь ласка, введіть додатне ціле число.");
        }
    }
    
    return size;
}