using System.Text;

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

    public void AddParticipants(params Person[]? persons)
    {
        if (persons is null || persons.Length == 0)
        {
            return;
        }

        Participants ??= [];
        Participants.AddRange(persons);
    }

    public void AddPapers(params Paper[]? papers)
    {
        if (papers is null || papers.Length == 0)
        {
            return;
        }

        Publications ??= [];
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
        var result = new StringBuilder();
        result.AppendLine($"{base.ToString()}, Тема: {Topic}, Тривалість: {TimeFrame}");
            
        result.AppendLine("Учасники:");
        if (Participants.Count > 0)
        {
            foreach (Person person in Participants)
            {
                result.AppendLine($"- {person}");
            }
        }
        else
        {
            result.AppendLine("- Немає учасників");
        }
        
        result.AppendLine("Публікації:");
        if (Publications.Count > 0)
        {
            foreach (Paper? paper in Publications)
            {
                result.AppendLine($"- {paper}");
            }
        }
        else
        {
            result.AppendLine("- Немає публікацій");
        }
        
        return result.ToString();
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

    public static ResearchTeam Create(int index)
    {
        string[] topics = ["Тема 1", "Тема 2", "Тема 3", "Тема 4", "Тема 5"];
        TimeFrame[] timeFrames = [TimeFrame.Year, TimeFrame.TwoYears, TimeFrame.Long];

        int topicIndex = index % topics.Length;
        int timeFrameIndex = index % timeFrames.Length;

        var team = new ResearchTeam(
            "Організація " + index, 
            index, 
            topics[topicIndex], 
            timeFrames[timeFrameIndex]
        );

        int participantCount = 1 + index % 4;
        for (int i = 0; i < participantCount; i++)
        {
            team.AddParticipants(Person.Create(i));
        }

        int publicationCount = 1 + index % 5;
        for (int i = 0; i < publicationCount; i++)
        {
            team.AddPapers(Paper.Create(i));
        }

        return team;
    }
}