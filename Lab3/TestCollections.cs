namespace Lab3;

public class TestCollections
{
    private List<Team> _teams;
    private List<string> _teamNames;
    private Dictionary<Team, ResearchTeam> _teamDictionary;
    private Dictionary<string, ResearchTeam> _teamNameDictionary;
    private int _size;

    public TestCollections(int size)
    {
        _size = size;
        _teams = new List<Team>();
        _teamNames = new List<string>();
        _teamDictionary = new Dictionary<Team, ResearchTeam>();
        _teamNameDictionary = new Dictionary<string, ResearchTeam>();

        for (int i = 0; i < size; i++)
        {
            ResearchTeam team = GenerateResearchTeam(i);
            _teams.Add(team.Team);
            string teamString = team.Team.ToString();
            _teamNames.Add(teamString);
            _teamDictionary.Add(team.Team, team);
            _teamNameDictionary.Add(teamString, team);
        }
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
}