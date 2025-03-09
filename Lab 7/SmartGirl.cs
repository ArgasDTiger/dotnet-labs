namespace Lab_7;

[Couple(Pair = "Student", Probability = 0.2, ChildType = "Girl")]
[Couple(Pair = "Botan", Probability = 0.5, ChildType = "Book")]
public class SmartGirl : Human
{
    public SmartGirl()
    {
        Name = "SmartGirl";
    }
    
    public override bool IsMale()
    {
        return false;
    }
}