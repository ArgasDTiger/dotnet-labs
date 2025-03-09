namespace Lab_7;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class CoupleAttribute : Attribute
{
    public string Pair { get; set; }
    public double Probability { get; set; }
    public string ChildType { get; set; }
    
    // Default constructor
    public CoupleAttribute() { }
    
    // Constructor with all parameters
    public CoupleAttribute(string pair, double probability, string childType)
    {
        Pair = pair;
        Probability = probability;
        ChildType = childType;
    }
}