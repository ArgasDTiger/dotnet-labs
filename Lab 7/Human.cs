namespace Lab_7;

public abstract class Human : IHasName
{
    private readonly string _name = string.Empty;
    
    public string Name 
    { 
        get => _name;
        protected init => _name = value ?? throw new ArgumentNullException(nameof(value)); 
    }
    
    public abstract bool IsMale();
    
    public string GetName()
    {
        return Name;
    }
}
