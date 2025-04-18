namespace Lab_7;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class CoupleAttribute : Attribute
{
    public string Pair { get; init; }
    public double Probability { get; init; }
    public string ChildType { get; init; }
    
    public CoupleAttribute() { }
    
    public CoupleAttribute(string pair, double probability, string childType)
    {
        Pair = pair;
        Probability = probability;
        ChildType = childType;
    }
}