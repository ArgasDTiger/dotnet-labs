using System.Collections.Immutable;
namespace Lab_4;

public class TestCollections
{
    private List<Team> _teams;
    private List<string> _teamNames;
    private Dictionary<Team, ResearchTeam> _teamDictionary;
    private Dictionary<string, ResearchTeam> _teamNameDictionary;
    
    private ImmutableList<Team> _immutableTeams;
    private ImmutableList<string> _immutableTeamNames;
    private ImmutableDictionary<Team, ResearchTeam> _immutableTeamDictionary;
    private ImmutableDictionary<string, ResearchTeam> _immutableTeamNameDictionary;
    
    private SortedList<Team, ResearchTeam> _sortedTeamList;
    private SortedList<string, ResearchTeam> _sortedTeamNameList;
    private SortedDictionary<Team, ResearchTeam> _sortedTeamDictionary;
    private SortedDictionary<string, ResearchTeam> _sortedTeamNameDictionary;
    
    private int _size;

    public TestCollections(int size)
    {
        _size = size;
        
        _teams = new List<Team>();
        _teamNames = new List<string>();
        _teamDictionary = new Dictionary<Team, ResearchTeam>();
        _teamNameDictionary = new Dictionary<string, ResearchTeam>();
        
        var immutableTeamsBuilder = ImmutableList.CreateBuilder<Team>();
        var immutableTeamNamesBuilder = ImmutableList.CreateBuilder<string>();
        var immutableTeamDictionaryBuilder = ImmutableDictionary.CreateBuilder<Team, ResearchTeam>();
        var immutableTeamNameDictionaryBuilder = ImmutableDictionary.CreateBuilder<string, ResearchTeam>();
        
        _sortedTeamList = new SortedList<Team, ResearchTeam>(new TeamComparer());
        _sortedTeamNameList = new SortedList<string, ResearchTeam>();
        _sortedTeamDictionary = new SortedDictionary<Team, ResearchTeam>(new TeamComparer());
        _sortedTeamNameDictionary = new SortedDictionary<string, ResearchTeam>();

        for (int i = 0; i < size; i++)
        {
            ResearchTeam team = GenerateResearchTeam(i);
            string teamString = team.Team.ToString();
            
            _teams.Add(team.Team);
            _teamNames.Add(teamString);
            _teamDictionary.Add(team.Team, team);
            _teamNameDictionary.Add(teamString, team);
            
            immutableTeamsBuilder.Add(team.Team);
            immutableTeamNamesBuilder.Add(teamString);
            immutableTeamDictionaryBuilder.Add(team.Team, team);
            immutableTeamNameDictionaryBuilder.Add(teamString, team);
            
            _sortedTeamList.Add(team.Team, team);
            _sortedTeamNameList.Add(teamString, team);
            _sortedTeamDictionary.Add(team.Team, team);
            _sortedTeamNameDictionary.Add(teamString, team);
        }
        
        _immutableTeams = immutableTeamsBuilder.ToImmutable();
        _immutableTeamNames = immutableTeamNamesBuilder.ToImmutable();
        _immutableTeamDictionary = immutableTeamDictionaryBuilder.ToImmutable();
        _immutableTeamNameDictionary = immutableTeamNameDictionaryBuilder.ToImmutable();
    }

    public static ResearchTeam GenerateResearchTeam(int index)
    {
        string[] organizations = ["Профспілка", "Організація 1", "Організація 2", "Організація 3", "Організація 4"];
        string[] topics = ["Тема 1", "Тема 2", "Тема 3", "Тема 4", "Тема 5"];
        TimeFrame[] timeFrames = [TimeFrame.Year, TimeFrame.TwoYears, TimeFrame.Long];

        int orgIndex = index % organizations.Length;
        int topicIndex = index % topics.Length;
        int timeFrameIndex = index % timeFrames.Length;

        var team = new ResearchTeam(
            organizations[orgIndex], 
            334 + index, 
            topics[topicIndex], 
            timeFrames[timeFrameIndex]
        );

        int participantCount = 1 + index % 4;
        for (int i = 0; i < participantCount; i++)
        {
            team.AddParticipants(new Person(
                $"Ім'я{i}",
                $"Прізвище{i}",
                new DateTime(1990 + i, 1 + i % 12, 1 + i % 28)
            ));
        }

        int publicationCount = 1 + index % 5;
        for (int i = 0; i < publicationCount; i++)
        {
            team.AddPapers(new Paper(
                $"Публікація {i}",
                new Person(),
                DateTime.Now.AddDays(-100 * i)
            ));
        }

        return team;
    }

    public void SearchElementsAndMeasureTime()
    {
        const int firstIndex = 0;
        int middleIndex = _size / 2;
        int lastIndex = _size - 1;
        int notExistingIndex = _size + 1;

        var firstTeam = _teams[firstIndex];
        var middleTeam = _teams[middleIndex];
        var lastTeam = _teams[lastIndex];
        var notExistingTeam = GenerateResearchTeam(notExistingIndex).Team;

        string firstName = _teamNames[firstIndex];
        string middleName = _teamNames[middleIndex];
        string lastName = _teamNames[lastIndex];
        string notExistingName = notExistingTeam.ToString();

        var firstResearchTeam = _teamDictionary[firstTeam];
        var middleResearchTeam = _teamDictionary[middleTeam];
        var lastResearchTeam = _teamDictionary[lastTeam];
        var notExistingResearchTeam = GenerateResearchTeam(notExistingIndex);

        Console.WriteLine("\nВимірювання часу пошуку елементів у колекціях:");
        Console.WriteLine("=================================================");
        
        Console.WriteLine("\n--- СТАНДАРТНІ КОЛЕКЦІЇ ---");
        MeasureStandardCollectionsSearchTime("Перший елемент", firstTeam, firstName, firstResearchTeam);
        MeasureStandardCollectionsSearchTime("Середній елемент", middleTeam, middleName, middleResearchTeam);
        MeasureStandardCollectionsSearchTime("Останній елемент", lastTeam, lastName, lastResearchTeam);
        MeasureStandardCollectionsSearchTime("Неіснуючий елемент", notExistingTeam, notExistingName, notExistingResearchTeam);
        
        Console.WriteLine("\n--- IMMUTABLE КОЛЕКЦІЇ ---");
        MeasureImmutableCollectionsSearchTime("Перший елемент", firstTeam, firstName, firstResearchTeam);
        MeasureImmutableCollectionsSearchTime("Середній елемент", middleTeam, middleName, middleResearchTeam);
        MeasureImmutableCollectionsSearchTime("Останній елемент", lastTeam, lastName, lastResearchTeam);
        MeasureImmutableCollectionsSearchTime("Неіснуючий елемент", notExistingTeam, notExistingName, notExistingResearchTeam);
        
        Console.WriteLine("\n--- SORTED КОЛЕКЦІЇ ---");
        MeasureSortedCollectionsSearchTime("Перший елемент", firstTeam, firstName, firstResearchTeam);
        MeasureSortedCollectionsSearchTime("Середній елемент", middleTeam, middleName, middleResearchTeam);
        MeasureSortedCollectionsSearchTime("Останній елемент", lastTeam, lastName, lastResearchTeam);
        MeasureSortedCollectionsSearchTime("Неіснуючий елемент", notExistingTeam, notExistingName, notExistingResearchTeam);
    }

    private void MeasureStandardCollectionsSearchTime(string elementDescription, Team team, string teamName, ResearchTeam researchTeam)
    {
        Console.WriteLine($"\nПошук (стандартні колекції): {elementDescription}");
        Console.WriteLine("-------------------");

        int startTime = Environment.TickCount;
        bool containsTeam = _teams.Contains(team);
        int endTime = Environment.TickCount - startTime;
        Console.WriteLine($"List<Team>.Contains: {endTime} мс ({containsTeam})");

        startTime = Environment.TickCount;
        bool containsTeamName = _teamNames.Contains(teamName);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"List<string>.Contains: {endTime} мс ({containsTeamName})");

        startTime = Environment.TickCount;
        bool containsTeamKey = _teamDictionary.ContainsKey(team);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"Dictionary<Team, ResearchTeam>.ContainsKey: {endTime} мс ({containsTeamKey})");

        startTime = Environment.TickCount;
        bool containsTeamNameKey = _teamNameDictionary.ContainsKey(teamName);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"Dictionary<string, ResearchTeam>.ContainsKey: {endTime} мс ({containsTeamNameKey})");

        startTime = Environment.TickCount;
        bool containsResearchTeam = _teamDictionary.ContainsValue(researchTeam);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"Dictionary<Team, ResearchTeam>.ContainsValue: {endTime} мс ({containsResearchTeam})");

        startTime = Environment.TickCount;
        bool containsResearchTeamByName = _teamNameDictionary.ContainsValue(researchTeam);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"Dictionary<string, ResearchTeam>.ContainsValue: {endTime} мс ({containsResearchTeamByName})");
    }
    
    private void MeasureImmutableCollectionsSearchTime(string elementDescription, Team team, string teamName, ResearchTeam researchTeam)
    {
        Console.WriteLine($"\nПошук (immutable колекції): {elementDescription}");
        Console.WriteLine("-------------------");

        int startTime = Environment.TickCount;
        bool containsTeam = _immutableTeams.Contains(team);
        int endTime = Environment.TickCount - startTime;
        Console.WriteLine($"ImmutableList<Team>.Contains: {endTime} мс ({containsTeam})");

        startTime = Environment.TickCount;
        bool containsTeamName = _immutableTeamNames.Contains(teamName);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"ImmutableList<string>.Contains: {endTime} мс ({containsTeamName})");

        startTime = Environment.TickCount;
        bool containsTeamKey = _immutableTeamDictionary.ContainsKey(team);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"ImmutableDictionary<Team, ResearchTeam>.ContainsKey: {endTime} мс ({containsTeamKey})");

        startTime = Environment.TickCount;
        bool containsTeamNameKey = _immutableTeamNameDictionary.ContainsKey(teamName);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"ImmutableDictionary<string, ResearchTeam>.ContainsKey: {endTime} мс ({containsTeamNameKey})");

        startTime = Environment.TickCount;
        bool containsResearchTeam = _immutableTeamDictionary.Values.Contains(researchTeam);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"ImmutableDictionary<Team, ResearchTeam>.Values.Contains: {endTime} мс ({containsResearchTeam})");

        startTime = Environment.TickCount;
        bool containsResearchTeamByName = _immutableTeamNameDictionary.Values.Contains(researchTeam);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"ImmutableDictionary<string, ResearchTeam>.Values.Contains: {endTime} мс ({containsResearchTeamByName})");
    }
    
    private void MeasureSortedCollectionsSearchTime(string elementDescription, Team team, string teamName, ResearchTeam researchTeam)
    {
        Console.WriteLine($"\nПошук (sorted колекції): {elementDescription}");
        Console.WriteLine("-------------------");

        int startTime = Environment.TickCount;
        bool containsSortedTeamKey = _sortedTeamList.ContainsKey(team);
        int endTime = Environment.TickCount - startTime;
        Console.WriteLine($"SortedList<Team, ResearchTeam>.ContainsKey: {endTime} мс ({containsSortedTeamKey})");

        startTime = Environment.TickCount;
        bool containsSortedTeamNameKey = _sortedTeamNameList.ContainsKey(teamName);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"SortedList<string, ResearchTeam>.ContainsKey: {endTime} мс ({containsSortedTeamNameKey})");
        
        startTime = Environment.TickCount;
        bool containsSortedDictionaryTeamKey = _sortedTeamDictionary.ContainsKey(team);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"SortedDictionary<Team, ResearchTeam>.ContainsKey: {endTime} мс ({containsSortedDictionaryTeamKey})");

        startTime = Environment.TickCount;
        bool containsSortedDictionaryTeamNameKey = _sortedTeamNameDictionary.ContainsKey(teamName);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"SortedDictionary<string, ResearchTeam>.ContainsKey: {endTime} мс ({containsSortedDictionaryTeamNameKey})");

        startTime = Environment.TickCount;
        bool containsSortedTeamValue = _sortedTeamList.ContainsValue(researchTeam);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"SortedList<Team, ResearchTeam>.ContainsValue: {endTime} мс ({containsSortedTeamValue})");

        startTime = Environment.TickCount;
        bool containsSortedTeamNameValue = _sortedTeamNameList.ContainsValue(researchTeam);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"SortedList<string, ResearchTeam>.ContainsValue: {endTime} мс ({containsSortedTeamNameValue})");
        
        startTime = Environment.TickCount;
        bool containsSortedDictionaryTeamValue = _sortedTeamDictionary.Values.Contains(researchTeam);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"SortedDictionary<Team, ResearchTeam>.Values.Contains: {endTime} мс ({containsSortedDictionaryTeamValue})");

        startTime = Environment.TickCount;
        bool containsSortedDictionaryTeamNameValue = _sortedTeamNameDictionary.Values.Contains(researchTeam);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"SortedDictionary<string, ResearchTeam>.Values.Contains: {endTime} мс ({containsSortedDictionaryTeamNameValue})");
    }
}