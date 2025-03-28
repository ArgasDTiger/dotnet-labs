using System.Text;

namespace Lab3;

public sealed class ResearchTeamCollection
{
    private List<ResearchTeam> _researchTeams = [];
    public List<ResearchTeam> ResearchTeams
    {
        get => _researchTeams;
        private set => _researchTeams = value;
    }

    public void AddDefaults()
    {
        // _researchTeams.Add(new ResearchTeam());
        ResearchTeams.Add(new ResearchTeam("Організація 1", 123, "Штучний інтелект", TimeFrame.TwoYears));
        ResearchTeams.Add(new ResearchTeam("Організація 2", 456, "Квантові обчислення", TimeFrame.Long));
    }

    public void AddResearchTeams(params ResearchTeam[]? teams)
    {
        if (teams is null || teams.Length == 0)
        {
            return;
        }

        ResearchTeams ??= [];
        ResearchTeams.AddRange(teams);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Список дослідницьких команд:");
        
        foreach (var team in _researchTeams)
        {
            sb.AppendLine(team.ToString());
            sb.AppendLine("----------");
        }
        
        return sb.ToString();
    }

    public string ToShortList()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Короткий список дослідницьких команд:");
        
        foreach (var team in _researchTeams)
        {
            sb.AppendLine(team.ToShortString());
            sb.AppendLine("----------");
        }
        
        return sb.ToString();
    }

    public void SortByRegistrationNumber()
    {
        ResearchTeams.Sort((x, y) => x.CompareTo(y));
    }

    public void SortByTopic()
    {
        ResearchTeams.Sort(new ResearchTeam());
    }

    public void SortByPublicationCount()
    {
        ResearchTeams.Sort(new PublicationComparer());
    }

    public int MinRegistrationNumber
    {
        get
        {
            return ResearchTeams.Count > 0 
                ? ResearchTeams.Min(team => team.RegistrationNumber) 
                : 0;
        }
    }

    public IEnumerable<ResearchTeam> TwoYearProjects
    {
        get
        {
            return ResearchTeams.Where(team => team[TimeFrame.TwoYears]);
        }
    }

    public List<ResearchTeam> NGroup(int participantCount)
    {
        return ResearchTeams
            .Where(team => team.Participants.Count == participantCount)
            .ToList();
    }
}