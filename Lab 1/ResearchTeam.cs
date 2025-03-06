namespace Lab_1;

public class ResearchTeam
{
    private string _researchTopic;
    private string _organization;
    private int _registrationNumber;
    private TimeFrame _timeFrame;
    private Paper[]? _publications;

    public ResearchTeam(string topic, string org, int regNumber, TimeFrame time)
    {
        ResearchTopic = topic;
        Organization = org;
        RegistrationNumber = regNumber;
        TimeFrame = time;
        Publications = [];
    }

    public ResearchTeam() 
        : this("Теорiя штучного iнтелекту", "OpenAI", 1, TimeFrame.Year)
    {
    }

    public string ResearchTopic { get => _researchTopic; init => _researchTopic = value; }
    public string Organization { get => _organization; init => _organization = value; }
    public int RegistrationNumber { get => _registrationNumber; init => _registrationNumber = value; }
    public TimeFrame TimeFrame { get => _timeFrame; init => _timeFrame = value; }
    public Paper[] Publications { get => _publications; set => _publications = value; }

    public Paper? LastPublication => Publications?.MaxBy(p => p.PublicationDate);

    public bool this[TimeFrame frame] => _timeFrame == frame;

    public void AddPapers(params Paper[] papers)
    {
        if (papers == null || papers.Length == 0) return;
        if (Publications == null || Publications.Length == 0)
        {
            Publications = papers;
            return;
        }
        int oldLength = _publications.Length;
        Array.Resize(ref _publications, oldLength + papers.Length);
        Array.Copy(papers, 0, _publications, oldLength, papers.Length);
    }

    public override string ToString()
    {
        return $"Дослiдження: {ResearchTopic}\n" +
               $"Органiзацiя: {Organization}\nРеєстрацiя: {RegistrationNumber}\nTimeframe: {TimeFrame}\nПублiкацiї:\n{string.Join("\n", Publications.Select(p => "- " + p))}";
    }

    public string ToShortString()
    {
        return $"Дослiдження: {ResearchTopic}\nОрганiзацiя: {Organization}\nРеєстрацiя: {RegistrationNumber}\nTimeframe: {TimeFrame}";
    }
}