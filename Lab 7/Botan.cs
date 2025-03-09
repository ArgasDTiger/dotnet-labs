namespace Lab_7;


[Couple(Pair = "Girl", Probability = 0.7, ChildType = "SmartGirl")]
[Couple(Pair = "PrettyGirl", Probability = 0.1, ChildType = "PrettyGirl")]
[Couple(Pair = "SmartGirl", Probability = 0.8, ChildType = "Book")]
public class Botan : Human
{
    public Botan() // Fixed constructor name
    {
        Name = "Botan";
    }
    
    public override bool IsMale()
    {
        return true;
    }
}
