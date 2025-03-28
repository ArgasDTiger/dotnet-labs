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
    
    private readonly int _size;

    public TestCollections(int size)
    {
        Size = size;
        
        Teams = [];
        TeamNames = [];
        TeamDictionary = new Dictionary<Team, ResearchTeam>();
        TeamNameDictionary = new Dictionary<string, ResearchTeam>();
        
        var immutableTeamsBuilder = ImmutableList.CreateBuilder<Team>();
        var immutableTeamNamesBuilder = ImmutableList.CreateBuilder<string>();
        var immutableTeamDictionaryBuilder = ImmutableDictionary.CreateBuilder<Team, ResearchTeam>();
        var immutableTeamNameDictionaryBuilder = ImmutableDictionary.CreateBuilder<string, ResearchTeam>();
        
        SortedTeamList = new SortedList<Team, ResearchTeam>(new TeamComparer());
        SortedTeamNameList = new SortedList<string, ResearchTeam>();
        SortedTeamDictionary = new SortedDictionary<Team, ResearchTeam>(new TeamComparer());
        SortedTeamNameDictionary = new SortedDictionary<string, ResearchTeam>();

        for (int i = 0; i < size; i++)
        {
            ResearchTeam team = GenerateResearchTeam(i);
            string teamString = team.Team.ToString();
            
            Teams.Add(team.Team);
            TeamNames.Add(teamString);
            TeamDictionary.Add(team.Team, team);
            TeamNameDictionary.Add(teamString, team);
            
            immutableTeamsBuilder.Add(team.Team);
            immutableTeamNamesBuilder.Add(teamString);
            immutableTeamDictionaryBuilder.Add(team.Team, team);
            immutableTeamNameDictionaryBuilder.Add(teamString, team);
            
            SortedTeamList.Add(team.Team, team);
            SortedTeamNameList.Add(teamString, team);
            SortedTeamDictionary.Add(team.Team, team);
            SortedTeamNameDictionary.Add(teamString, team);
        }
        
        ImmutableTeams = immutableTeamsBuilder.ToImmutable();
        ImmutableTeamNames = immutableTeamNamesBuilder.ToImmutable();
        ImmutableTeamDictionary = immutableTeamDictionaryBuilder.ToImmutable();
        ImmutableTeamNameDictionary = immutableTeamNameDictionaryBuilder.ToImmutable();
    }

    public List<Team> Teams
    {
        get => _teams; 
        init => _teams = value;
    }

    private List<string> TeamNames
    {
        get => _teamNames; 
        init => _teamNames = value;
    }

    private Dictionary<Team, ResearchTeam> TeamDictionary
    {
        get => _teamDictionary; 
        init => _teamDictionary = value;
    }

    public Dictionary<string, ResearchTeam> TeamNameDictionary
    {
        get => _teamNameDictionary; 
        init => _teamNameDictionary = value;
    }

    public ImmutableList<Team> ImmutableTeams
    {
        get => _immutableTeams;
        init => _immutableTeams = value;
    }

    public ImmutableList<string> ImmutableTeamNames
    {
        get => _immutableTeamNames; 
        init => _immutableTeamNames = value;
    }

    public ImmutableDictionary<Team, ResearchTeam> ImmutableTeamDictionary
    {
        get => _immutableTeamDictionary;
        init => _immutableTeamDictionary = value;
    }

    public ImmutableDictionary<string, ResearchTeam> ImmutableTeamNameDictionary
    {
        get => _immutableTeamNameDictionary;
        init => _immutableTeamNameDictionary = value;
    }

    public SortedList<Team, ResearchTeam> SortedTeamList
    {
        get => _sortedTeamList; 
        init => _sortedTeamList = value;
    }

    public SortedList<string, ResearchTeam> SortedTeamNameList
    {
        get => _sortedTeamNameList; 
        init => _sortedTeamNameList = value;
    }

    public SortedDictionary<Team, ResearchTeam> SortedTeamDictionary
    {
        get => _sortedTeamDictionary;
        init => _sortedTeamDictionary = value;
    }

    public SortedDictionary<string, ResearchTeam> SortedTeamNameDictionary
    {
        get => _sortedTeamNameDictionary; 
        init => _sortedTeamNameDictionary = value;
    }
    
    private int Size
    {
        get => _size;
        init
        {
            if (value <= 0)
                throw new ArgumentException("Size must be greater than zero.");
            _size = value;
        }
    }

    public static ResearchTeam GenerateResearchTeam(int index)
    {
        var team = ResearchTeam.Create(index);

        int participantCount = 1 + index % 4;
        for (int i = 0; i < participantCount; i++)
        {
            team.AddParticipants(Person.Create(index));
        }

        int publicationCount = 1 + index % 5;
        for (int i = 0; i < publicationCount; i++)
        {
            team.AddPapers(Paper.Create(i));
        }

        return team;
    }

    public void SearchElementsAndMeasureTime()
    {
        const int firstIndex = 0;
        int middleIndex = Size / 2;
        int lastIndex = Size - 1;
        int notExistingIndex = Size + 1;

        var firstTeam = ResearchTeam.Create(firstIndex);
        var middleTeam = ResearchTeam.Create(middleIndex);
        var lastTeam = ResearchTeam.Create(lastIndex);
        var notExistingTeam = GenerateResearchTeam(notExistingIndex).Team;

        string firstName = TeamNames[firstIndex];
        string middleName = TeamNames[middleIndex];
        string lastName = TeamNames[lastIndex];
        string notExistingName = notExistingTeam.ToString();

        var firstResearchTeam = TeamDictionary[firstTeam];
        var middleResearchTeam = TeamDictionary[middleTeam];
        var lastResearchTeam = TeamDictionary[lastTeam];
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
        bool containsTeam = Teams.Contains(team);
        int endTime = Environment.TickCount - startTime;
        Console.WriteLine($"List<Team>.Contains: {endTime} мс ({containsTeam})");

        startTime = Environment.TickCount;
        bool containsTeamName = TeamNames.Contains(teamName);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"List<string>.Contains: {endTime} мс ({containsTeamName})");

        startTime = Environment.TickCount;
        bool containsTeamKey = TeamDictionary.ContainsKey(team);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"Dictionary<Team, ResearchTeam>.ContainsKey: {endTime} мс ({containsTeamKey})");

        startTime = Environment.TickCount;
        bool containsTeamNameKey = TeamNameDictionary.ContainsKey(teamName);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"Dictionary<string, ResearchTeam>.ContainsKey: {endTime} мс ({containsTeamNameKey})");

        startTime = Environment.TickCount;
        bool containsResearchTeam = TeamDictionary.ContainsValue(researchTeam);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"Dictionary<Team, ResearchTeam>.ContainsValue: {endTime} мс ({containsResearchTeam})");

        startTime = Environment.TickCount;
        bool containsResearchTeamByName = TeamNameDictionary.ContainsValue(researchTeam);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"Dictionary<string, ResearchTeam>.ContainsValue: {endTime} мс ({containsResearchTeamByName})");
    }
    
    private void MeasureImmutableCollectionsSearchTime(string elementDescription, Team team, string teamName, ResearchTeam researchTeam)
    {
        Console.WriteLine($"\nПошук (immutable колекції): {elementDescription}");
        Console.WriteLine("-------------------");

        int startTime = Environment.TickCount;
        bool containsTeam = ImmutableTeams.Contains(team);
        int endTime = Environment.TickCount - startTime;
        Console.WriteLine($"ImmutableList<Team>.Contains: {endTime} мс ({containsTeam})");

        startTime = Environment.TickCount;
        bool containsTeamName = ImmutableTeamNames.Contains(teamName);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"ImmutableList<string>.Contains: {endTime} мс ({containsTeamName})");

        startTime = Environment.TickCount;
        bool containsTeamKey = ImmutableTeamDictionary.ContainsKey(team);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"ImmutableDictionary<Team, ResearchTeam>.ContainsKey: {endTime} мс ({containsTeamKey})");

        startTime = Environment.TickCount;
        bool containsTeamNameKey = ImmutableTeamNameDictionary.ContainsKey(teamName);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"ImmutableDictionary<string, ResearchTeam>.ContainsKey: {endTime} мс ({containsTeamNameKey})");

        startTime = Environment.TickCount;
        bool containsResearchTeam = ImmutableTeamDictionary.Values.Contains(researchTeam);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"ImmutableDictionary<Team, ResearchTeam>.Values.Contains: {endTime} мс ({containsResearchTeam})");

        startTime = Environment.TickCount;
        bool containsResearchTeamByName = ImmutableTeamNameDictionary.Values.Contains(researchTeam);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"ImmutableDictionary<string, ResearchTeam>.Values.Contains: {endTime} мс ({containsResearchTeamByName})");
    }
    
    private void MeasureSortedCollectionsSearchTime(string elementDescription, Team team, string teamName, ResearchTeam researchTeam)
    {
        Console.WriteLine($"\nПошук (sorted колекції): {elementDescription}");
        Console.WriteLine("-------------------");

        int startTime = Environment.TickCount;
        bool containsSortedTeamKey = SortedTeamList.ContainsKey(team);
        int endTime = Environment.TickCount - startTime;
        Console.WriteLine($"SortedList<Team, ResearchTeam>.ContainsKey: {endTime} мс ({containsSortedTeamKey})");

        startTime = Environment.TickCount;
        bool containsSortedTeamNameKey = SortedTeamNameList.ContainsKey(teamName);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"SortedList<string, ResearchTeam>.ContainsKey: {endTime} мс ({containsSortedTeamNameKey})");
        
        startTime = Environment.TickCount;
        bool containsSortedDictionaryTeamKey = SortedTeamDictionary.ContainsKey(team);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"SortedDictionary<Team, ResearchTeam>.ContainsKey: {endTime} мс ({containsSortedDictionaryTeamKey})");

        startTime = Environment.TickCount;
        bool containsSortedDictionaryTeamNameKey = SortedTeamNameDictionary.ContainsKey(teamName);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"SortedDictionary<string, ResearchTeam>.ContainsKey: {endTime} мс ({containsSortedDictionaryTeamNameKey})");

        startTime = Environment.TickCount;
        bool containsSortedTeamValue = SortedTeamList.ContainsValue(researchTeam);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"SortedList<Team, ResearchTeam>.ContainsValue: {endTime} мс ({containsSortedTeamValue})");

        startTime = Environment.TickCount;
        bool containsSortedTeamNameValue = SortedTeamNameList.ContainsValue(researchTeam);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"SortedList<string, ResearchTeam>.ContainsValue: {endTime} мс ({containsSortedTeamNameValue})");
        
        startTime = Environment.TickCount;
        bool containsSortedDictionaryTeamValue = SortedTeamDictionary.Values.Contains(researchTeam);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"SortedDictionary<Team, ResearchTeam>.Values.Contains: {endTime} мс ({containsSortedDictionaryTeamValue})");

        startTime = Environment.TickCount;
        bool containsSortedDictionaryTeamNameValue = SortedTeamNameDictionary.Values.Contains(researchTeam);
        endTime = Environment.TickCount - startTime;
        Console.WriteLine($"SortedDictionary<string, ResearchTeam>.Values.Contains: {endTime} мс ({containsSortedDictionaryTeamNameValue})");
    }
}