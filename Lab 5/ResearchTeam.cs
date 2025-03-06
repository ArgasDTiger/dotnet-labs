namespace Lab_5;

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
        _topic = topic;
        _timeFrame = timeFrame;
        _participants = new List<Person>();
        _publications = new List<Paper?>();
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

    public Team Team
    {
        get => new Team(_organization, _registrationNumber);
    }

    public void AddParticipants(params Person[] persons)
    {
        _participants.AddRange(persons);
    }

    public void AddPapers(params Paper[] papers)
    {
        _publications.AddRange(papers);
    }

    public Paper? LatestPublication
    {
        get
        {
            return _publications.Count == 0 ? null : _publications.OrderByDescending(p => p?.PublicationDate).First();
        }
    }

    public bool this[TimeFrame timeFrame] => _timeFrame == timeFrame;

    public override object DeepCopy()
    {
        ResearchTeam copy = new ResearchTeam(_organization, _registrationNumber, _topic, _timeFrame);
        
        foreach (Person person in _participants)
        {
            copy._participants.Add((Person)person.DeepCopy());
        }
        
        foreach (Paper? paper in _publications)
        {
            if (paper is not null)
            {
                copy._publications.Add((Paper)paper.DeepCopy());
            }
        }
        
        return copy;
    }

    public override string ToString()
    {
        string result = $"{base.ToString()}, Тема: {_topic}, Тривалість: {_timeFrame}\n";
        
        result += "Учасники:\n";
        if (_participants.Count > 0)
        {
            foreach (Person person in _participants)
            {
                result += $"- {person}\n";
            }
        }
        else
        {
            result += "- Немає учасників\n";
        }
        
        result += "Публікації:\n";
        if (_publications.Count > 0)
        {
            foreach (Paper? paper in _publications)
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
        return $"{base.ToString()}, Тема: {_topic}, Тривалість: {_timeFrame}, Кількість учасників: {_participants.Count}, Кількість публікацій: {_publications.Count}";
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