namespace Lab_5;

public class TeamListHandlerEventArgs : EventArgs
{
    public string CollectionName { get; init; }
    public string ChangeInfo { get; init; }
    public int ElementNumber { get; init; }

    public TeamListHandlerEventArgs(string collectionName, string changeInfo, int elementNumber)
    {
        CollectionName = collectionName;
        ChangeInfo = changeInfo;
        ElementNumber = elementNumber;
    }

    public override string ToString()
    {
        return $"Collection: {CollectionName}, Change: {ChangeInfo}, Element Number: {ElementNumber}";
    }
}

public delegate void TeamListHandler(object source, TeamListHandlerEventArgs args);