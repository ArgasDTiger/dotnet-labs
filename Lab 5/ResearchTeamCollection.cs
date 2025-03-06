using System.Text;

namespace Lab_5;

public sealed class ResearchTeamCollection
{
    private readonly List<ResearchTeam> _researchTeams = [];
    
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
            if (index >= 0 && index < _researchTeams.Count)
            {
                return _researchTeams[index];
            }
            throw new IndexOutOfRangeException($"Index {index} is out of range");
        }
        
        init
        {
            if (index >= 0 && index < _researchTeams.Count)
            {
                _researchTeams[index] = value;
            }
            else
            {
                throw new IndexOutOfRangeException($"Index {index} is out of range");
            }
        }
    }
    
    public void InsertAt(int j, ResearchTeam researchTeam)
    {
        if (j >= 0 && j < _researchTeams.Count)
        {
            // Insert before element j
            _researchTeams.Insert(j, researchTeam);
            
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
            _researchTeams.Add(researchTeam);
            
            // Raise the added event
            ResearchTeamAdded?.Invoke(
                this,
                new TeamListHandlerEventArgs(
                    CollectionName,
                    "Team added to collection",
                    _researchTeams.Count - 1
                )
            );
        }
    }
    
    public void AddDefaults()
    {
        _researchTeams.Add(new ResearchTeam("Організація 1", 123, "Штучний інтелект", TimeFrame.TwoYears));
        _researchTeams.Add(new ResearchTeam("Організація 2", 456, "Квантові обчислення", TimeFrame.Long));
        
        for (int i = _researchTeams.Count - 2; i < _researchTeams.Count; i++)
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
        _researchTeams.AddRange(teams);
        
        for (int i = startIndex; i < _researchTeams.Count; i++)
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
        if (index >= 0 && index < _researchTeams.Count)
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
        _researchTeams.Sort((x, y) => x.CompareTo(y));
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