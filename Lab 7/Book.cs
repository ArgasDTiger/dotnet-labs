namespace Lab_7;

public class Book : IHasName
{
    public string Name { get; set; }
    
    public Book()
    {
        Name = "Book";
    }
}