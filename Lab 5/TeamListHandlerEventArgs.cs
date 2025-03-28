namespace Lab_5;

public class TeamListHandlerEventArgs : EventArgs
{
    public string CollectionName { get; init; }
    public string ChangeType { get; init; }
    public int ElementNumber { get; init; }

    public TeamListHandlerEventArgs(string collectionName, string changeType, int elementNumber)
    {
        CollectionName = collectionName;
        ChangeType = changeType;
        ElementNumber = elementNumber;
    }

    public override string ToString()
    {
        return $"Collection: {CollectionName}, Change: {ChangeType}, Element Number: {ElementNumber}";
    }
}

public delegate void TeamListHandler(object source, TeamListHandlerEventArgs args);