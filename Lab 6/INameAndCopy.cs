namespace Lab_6;

public interface INameAndCopy
{
    string Name { get; set; }
    object DeepCopy();
}
