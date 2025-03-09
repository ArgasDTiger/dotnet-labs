namespace Lab_7;

[Couple(Pair = "Student", Probability = 0.7, ChildType = "Girl")]
[Couple(Pair = "Botan", Probability = 0.3, ChildType = "SmartGirl")]
public class Girl : Human
{
    public Girl()
    {
        Name = "Girl";
    }
    
    public override bool IsMale()
    {
        return false;
    }
}