using System.Text;

namespace Lab_4;

public sealed class ResearchTeamCollection
{
    private readonly List<ResearchTeam> _researchTeams;

    public ResearchTeamCollection()
    {
        _researchTeams = new List<ResearchTeam>();
    }

    public void AddDefaults()
    {
        _researchTeams.Add(new ResearchTeam());
        _researchTeams.Add(new ResearchTeam("Організація 1", 123, "Штучний інтелект", TimeFrame.TwoYears));
        _researchTeams.Add(new ResearchTeam("Організація 2", 456, "Квантові обчислення", TimeFrame.Long));
    }

    public void AddResearchTeams(params ResearchTeam[] teams)
    {
        _researchTeams.AddRange(teams);
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
        _researchTeams.Sort();
    }

    public void SortByTopic()
    {
        _researchTeams.Sort(new ResearchTeam());
    }

    public void SortByPublicationCount()
    {
        _researchTeams.Sort(new PublicationComparer());
    }

    public int MinRegistrationNumber
    {
        get
        {
            return _researchTeams.Count > 0 
                ? _researchTeams.Min(team => team.RegistrationNumber) 
                : 0;
        }
    }

    public IEnumerable<ResearchTeam> TwoYearProjects
    {
        get
        {
            return _researchTeams.Where(team => team[TimeFrame.TwoYears]);
        }
    }

    public List<ResearchTeam> NGroup(int participantCount)
    {
        return _researchTeams
            .Where(team => team.Participants.Count == participantCount)
            .ToList();
    }
}