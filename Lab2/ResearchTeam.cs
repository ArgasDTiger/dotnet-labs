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
        _researchTopic = topic;
        _timeFrame = time;
        Members = new ArrayList();
        Publications = new ArrayList();
    }

    public ResearchTeam()
    {
        _researchTopic = "Теорiя штучного iнтелекту";
        _timeFrame = TimeFrame.Year;
        Members = new ArrayList();
        Publications = new ArrayList();
    }

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
        get => new Team(_organization, _registrationNumber);
        set
        {
            _organization = value.Organization;
            _registrationNumber = value.RegistrationNumber;
        }
    }

    public void AddPapers(params Paper[] papers)
    {
        Publications ??= [];

        Publications.AddRange(papers);
    }

    public override object DeepCopy()
    {
        var copy = new ResearchTeam(_researchTopic, _organization, _registrationNumber, _timeFrame);
        
        if (_members is not null)
        {
            copy._members = new ArrayList();
            foreach (Person person in _members)
            {
                copy._members.Add(person.DeepCopy());
            }
        }

        if (_publications == null) return copy;
        copy._publications = new ArrayList();
        foreach (Paper paper in _publications)
        {
            copy._publications.Add(paper.DeepCopy());
        }

        return copy;
    }

    public IEnumerable<Person> MembersWithoutPublications()
    {
        if (_members == null || _publications == null)
            yield break;

        foreach (Person member in _members)
        {
            bool hasPublication = false;
            foreach (Paper pub in _publications)
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
        if (_publications == null)
            yield break;

        DateTime minusNDate = DateTime.Now.AddYears(-n);
        foreach (Paper pub in _publications)
        {
            if (pub.PublicationDate >= minusNDate)
                yield return pub;
        }
    }

    public override string ToString()
    {
        string membersStr = _members == null ? "Відсутні" : 
            string.Join("\n", _members.Cast<Person>().Select(m => "- " + m));
        string publicationsStr = _publications == null ? "Відсутні" : 
            string.Join("\n", _publications.Cast<Paper>().Select(p => "- " + p));

        return $"Дослiдження: {_researchTopic}\nОрганiзацiя: {_organization}\nРеєстрацiя: {_registrationNumber}\nTimeframe: {_timeFrame}\nУчасники:\n{membersStr}\nПублікації:\n{publicationsStr}";
    }

    public string ToShortString()
    {
        return $"Дослiдження: {_researchTopic}\nОрганiзацiя: {Organization}\nРеєстрацiя: {RegistrationNumber}\nTimeframe: {_timeFrame}";
    }
}