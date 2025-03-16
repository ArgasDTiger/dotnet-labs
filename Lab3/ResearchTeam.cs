namespace Lab3;

using System;
using System.Collections.Generic;
using System.Linq;

public class ResearchTeam : Team, IComparable<ResearchTeam>, IComparer<ResearchTeam>
{
    private string _topic;
    private TimeFrame _timeFrame;
    private List<Person> _participants;
    private List<Paper?> _publications;

    public ResearchTeam(string organization, int registrationNumber, string topic, TimeFrame timeFrame)
        : base(organization, registrationNumber)
    {
        Topic = topic;
        TimeFrame = timeFrame;
        Participants = [];
        Publications = [];
    }

    public ResearchTeam() : this("Стандартна організація", 1, "Стандартна тема", TimeFrame.Year)
    {
    }

    public string Topic
    {
        get => _topic;
        set => _topic = value;
    }

    public TimeFrame TimeFrame
    {
        get => _timeFrame;
        set => _timeFrame = value;
    }

    public List<Person> Participants
    {
        get => _participants;
        set => _participants = value;
    }

    public List<Paper?> Publications
    {
        get => _publications;
        set => _publications = value;
    }

    public Team Team => this;

    public void AddParticipants(params Person[] persons)
    {
        Participants.AddRange(persons);
    }

    public void AddPapers(params Paper[] papers)
    {
        Publications.AddRange(papers);
    }

    public Paper? LatestPublication
    {
        get
        {
            return Publications.Count == 0 ? null : Publications.OrderByDescending(p => p?.PublicationDate).First();
        }
    }

    public bool this[TimeFrame timeFrame] => TimeFrame == timeFrame;

    public override object DeepCopy()
    {
        ResearchTeam copy = new ResearchTeam(Organization, RegistrationNumber, Topic, TimeFrame);
        
        foreach (Person person in Participants)
        {
            copy.Participants.Add((Person)person.DeepCopy());
        }
        
        foreach (Paper? paper in Publications)
        {
            if (paper is not null)
            {
                copy.Publications.Add((Paper)paper.DeepCopy());
            }
        }
        
        return copy;
    }

    public override string ToString()
    {
        string result = $"{base.ToString()}, Тема: {Topic}, Тривалість: {TimeFrame}\n";
        
        result += "Учасники:\n";
        if (Participants.Count > 0)
        {
            foreach (Person person in Participants)
            {
                result += $"- {person}\n";
            }
        }
        else
        {
            result += "- Немає учасників\n";
        }
        
        result += "Публікації:\n";
        if (Publications.Count > 0)
        {
            foreach (Paper? paper in Publications)
            {
                result += $"- {paper}\n";
            }
        }
        else
        {
            result += "- Немає публікацій\n";
        }
        
        return result;
    }

    public virtual string ToShortString()
    {
        return $"{base.ToString()}, Тема: {Topic}, Тривалість: {TimeFrame}, Кількість учасників: {Participants.Count}, Кількість публікацій: {Publications.Count}";
    }

    public int CompareTo(ResearchTeam? other)
    {
        return other == null ? 1 : RegistrationNumber.CompareTo(other.RegistrationNumber);
    }
    
    public int Compare(ResearchTeam? x, ResearchTeam? y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;

        return string.CompareOrdinal(x.Topic, y.Topic);
    }

}