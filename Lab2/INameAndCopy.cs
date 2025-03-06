namespace Lab2;

public interface INameAndCopy
{
    string Name { get; set; }
    object DeepCopy();
}
