namespace Lab3;

public class TestCollections
{
    private List<Team> _teams;
    private Dictionary<string, ResearchTeam> _teamNameDictionary;
    private Dictionary<Team, ResearchTeam> _teamDictionary;
    private List<string> _teamNames;
    private int _size;
    
    public TestCollections(int size)
    {
        Size = size;
        Teams = [];
        TeamNames = [];
        TeamDictionary = new Dictionary<Team, ResearchTeam>();
        TeamNameDictionary = new Dictionary<string, ResearchTeam>();

        for (int i = 0; i < Size; i++)
        {
            ResearchTeam team = GenerateResearchTeam(i);
            Teams.Add(team.Team);
            string teamString = team.Team.ToString();
            TeamNames.Add(teamString);
            TeamDictionary.Add(team.Team, team);
            TeamNameDictionary.Add(teamString, team);
        }
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

    private int Size
    {
        get => _size;
        init
        {
            if (value < 0)
                throw new ArgumentException("Size must be greater than zero.");
            _size = value;
        }
    }

    private static ResearchTeam GenerateResearchTeam(int index)
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
        
        MeasureSearchTime("Перший елемент", firstTeam, firstName, firstResearchTeam);
        MeasureSearchTime("Середній елемент", middleTeam, middleName, middleResearchTeam);
        MeasureSearchTime("Останній елемент", lastTeam, lastName, lastResearchTeam);
        MeasureSearchTime("Неіснуючий елемент", notExistingTeam, notExistingName, notExistingResearchTeam);
    }

    private void MeasureSearchTime(string elementDescription, Team team, string teamName, ResearchTeam researchTeam)
    {
        Console.WriteLine($"\nПошук: {elementDescription}");
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
}