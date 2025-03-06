namespace Lab3;

public class Paper
{
    public string Title { get; init; }
    public Person Author { get; init; }
    public DateTime PublicationDate { get; init; }

    public Paper(string title, Person author, DateTime publicationDate)
    {
        Title = title;
        Author = author;
        PublicationDate = publicationDate;
    }

    public Paper() : this("Курсова", new Person(), DateTime.Now)
    {
    }

    public virtual object DeepCopy()
    {
        return new Paper(
            Title,
            (Person)Author.DeepCopy(),
            PublicationDate
        );
    }

    public override string ToString()
    {
        return $"'{Title}' написав(-ла) {Author.ToShortString()}, опублiковано {PublicationDate:d}";
    }
}