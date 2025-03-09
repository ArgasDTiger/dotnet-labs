namespace Lab_7;

[Couple(Pair = "Student", Probability = 1.0, ChildType = "PrettyGirl")]
[Couple(Pair = "Botan", Probability = 0.1, ChildType = "PrettyGirl")]
public class PrettyGirl : Human
{
    public PrettyGirl()
    {
        Name = "PrettyGirl";
    }
    
    public override bool IsMale()
    {
        return false;
    }
}