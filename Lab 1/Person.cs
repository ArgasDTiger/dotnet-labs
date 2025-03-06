namespace Lab_1;

public class Person
{
    private string _firstName;
    private string _lastName;
    private DateTime _birthDate;

    public Person(string firstName, string lastName, DateTime birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
    }

    public Person() : this("Дід", "Мороз", new DateTime(2005, 5, 5))
    {
    }

    public string FirstName { get => _firstName; init => _firstName = value; }
    public string LastName { get => _lastName; init => _lastName = value; }
    public DateTime BirthDate { get => _birthDate; init => _birthDate = value; }
    
    public int BirthYear
    {
        get => _birthDate.Year;
        set => _birthDate = _birthDate.AddYears(value - _birthDate.Year);
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName}, born {BirthDate:d}";
    }

    public virtual string ToShortString()
    {
        return $"{FirstName} {LastName}";
    }
}
