namespace Lab_5;

public interface INameAndCopy
{
    string Name { get; set; }
    object DeepCopy();
}
