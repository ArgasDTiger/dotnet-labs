namespace Lab3;

public interface INameAndCopy
{
    string Name { get; set; }
    object DeepCopy();
}
