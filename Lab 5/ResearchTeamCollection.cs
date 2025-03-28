using System.Text;

namespace Lab_5;

public sealed class ResearchTeamCollection
{
    private List<ResearchTeam> _researchTeams = [];
    public List<ResearchTeam> ResearchTeams
    {
        get => _researchTeams;
        private set => _researchTeams = value;
    }
        
    public string CollectionName { get; init; }
    
    public event TeamListHandler? ResearchTeamAdded;
    public event TeamListHandler? ResearchTeamInserted;
    
    public ResearchTeamCollection(string collectionName)
    {
        CollectionName = collectionName;
    }
    
    public ResearchTeam this[int index]
    {
        get
        {
            if (index >= 0 && index < ResearchTeams.Count)
            {
                return ResearchTeams[index];
            }
            throw new IndexOutOfRangeException($"Index {index} is out of range");
        }
        
        init
        {
            if (index >= 0 && index < ResearchTeams.Count)
            {
                ResearchTeams[index] = value;
            }
            else
            {
                throw new IndexOutOfRangeException($"Index {index} is out of range");
            }
        }
    }
    
    public void InsertAt(int j, ResearchTeam researchTeam)
    {
        if (j >= 0 && j < ResearchTeams.Count)
        {
            ResearchTeams.Insert(j, researchTeam);
            
            ResearchTeamInserted?.Invoke(
                this,
                new TeamListHandlerEventArgs(
                    CollectionName,
                    "Team inserted into collection",
                    j
                )
            );
        }
        else
        {
            ResearchTeams.Add(researchTeam);
            
            ResearchTeamAdded?.Invoke(
                this,
                new TeamListHandlerEventArgs(
                    CollectionName,
                    "Team added to collection",
                    ResearchTeams.Count - 1
                )
            );
        }
    }
    
    public void AddDefaults()
    {
        ResearchTeams.Add(new ResearchTeam("Організація 1", 123, "Штучний інтелект", TimeFrame.TwoYears));
        ResearchTeams.Add(new ResearchTeam("Організація 2", 456, "Квантові обчислення", TimeFrame.Long));
        
        for (int i = ResearchTeams.Count - 2; i < ResearchTeams.Count; i++)
        {
            ResearchTeamAdded?.Invoke(
                this,
                new TeamListHandlerEventArgs(
                    CollectionName,
                    "Team added to collection",
                    i
                )
            );
        }
    }

    public void AddResearchTeams(params ResearchTeam[] teams)
    {
        int startIndex = _researchTeams.Count;
        ResearchTeams.AddRange(teams);
        
        for (int i = startIndex; i < ResearchTeams.Count; i++)
        {
            ResearchTeamAdded?.Invoke(
                this,
                new TeamListHandlerEventArgs(
                    CollectionName,
                    "Team added to collection",
                    i
                )
            );
        }
    }
    
    public bool Remove(int index)
    {
        if (index >= 0 && index < ResearchTeams.Count)
        {
            _researchTeams.RemoveAt(index);
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Collection Name: {CollectionName}");
        sb.AppendLine("Список дослідницьких команд:");
        
        foreach (var team in ResearchTeams)
        {
            sb.AppendLine(team.ToString());
            sb.AppendLine("----------");
        }
        
        return sb.ToString();
    }

    public string ToShortList()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Collection Name: {CollectionName}");
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
        return _researchTeams
            .Where(team => team.Participants.Count == participantCount)
            .ToList();
    }
}