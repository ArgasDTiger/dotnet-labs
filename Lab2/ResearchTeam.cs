using System.Collections;

namespace Lab2;

public class ResearchTeam : Team
{
    private string _researchTopic;
    private TimeFrame _timeFrame;
    private ArrayList? _members;
    private ArrayList? _publications;

    public ResearchTeam(string topic, string org, int regNumber, TimeFrame time)
        : base(org, regNumber)
    {
        ResearchTopic = topic;
        TimeFrame = time;
        Members = new ArrayList();
        Publications = new ArrayList();
    }

    public ResearchTeam()
    {
        ResearchTopic = "Теорiя штучного iнтелекту";
        TimeFrame = TimeFrame.Year;
        Members = new ArrayList();
        Publications = new ArrayList();
    }
    public string ResearchTopic { get => _researchTopic; init => _researchTopic = value; }
    public TimeFrame TimeFrame { get => _timeFrame; init => _timeFrame = value; }
    public ArrayList? Publications
    { 
        get => _publications; 
        set => _publications = value; 
    }

    public ArrayList? Members
    {
        get => _members;
        set => _members = value;
    }

    public Paper? LastPublication
    {
        get
        {
            if (Publications == null || Publications.Count == 0)
                return null;
            
            return Publications.Cast<Paper>().MaxBy(p => p.PublicationDate);
        }
    }

    public Team Team
    {
        get => this;
        set
        {
            Organization = value.Organization;
            RegistrationNumber = value.RegistrationNumber;
        }
    }

    public void AddPapers(params Paper[] papers)
    {
        Publications ??= [];

        Publications.AddRange(papers);
    }

    public override object DeepCopy()
    {
        var copy = new ResearchTeam(ResearchTopic, Organization, RegistrationNumber, TimeFrame);
        
        if (Members is not null)
        {
            copy.Members = new ArrayList();
            foreach (Person person in Members)
            {
                copy.Members.Add(person.DeepCopy());
            }
        }

        if (Publications == null) return copy;
        copy.Publications = new ArrayList();
        foreach (Paper paper in Publications)
        {
            copy.Publications.Add(paper.DeepCopy());
        }

        return copy;
    }

    public IEnumerable<Person> MembersWithoutPublications()
    {
        if (Members == null || Publications == null)
            yield break;

        foreach (Person member in Members)
        {
            bool hasPublication = false;
            foreach (Paper pub in Publications)
            {
                if (pub.Author == member)
                {
                    hasPublication = true;
                    break;
                }
            }
            if (!hasPublication)
                yield return member;
        }
    }

    public IEnumerable<Paper> GetPublicationsInLastSpecifiedYears(int n)
    {
        if (Publications == null)
            yield break;

        DateTime minusNDate = DateTime.Now.AddYears(-n);
        foreach (Paper pub in Publications)
        {
            if (pub.PublicationDate >= minusNDate)
                yield return pub;
        }
    }

    public override string ToString()
    {
        string membersStr = Members == null ? "Відсутні" : 
            string.Join("\n", Members.Cast<Person>().Select(m => "- " + m));
        string publicationsStr = Publications == null ? "Відсутні" : 
            string.Join("\n", Publications.Cast<Paper>().Select(p => "- " + p));

        return $"Дослiдження: {ResearchTopic}\nОрганiзацiя: {Organization}\nРеєстрацiя: {RegistrationNumber}\nTimeframe: {TimeFrame}\nУчасники:\n{membersStr}\nПублікації:\n{publicationsStr}";
    }

    public string ToShortString()
    {
        return $"Дослiдження: {ResearchTopic}\nОрганiзацiя: {Organization}\nРеєстрацiя: {RegistrationNumber}\nTimeframe: {TimeFrame}";
    }
}