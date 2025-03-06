namespace Lab_5;

public class TeamsJournalEntry
{
    public string CollectionName { get; init; }
    public string EventInfo { get; init; }
    public int ElementNumber { get; init; }

    public TeamsJournalEntry(string collectionName, string eventInfo, int elementNumber)
    {
        CollectionName = collectionName;
        EventInfo = eventInfo;
        ElementNumber = elementNumber;
    }

    public override string ToString()
    {
        return $"Collection: {CollectionName}, Event: {EventInfo}, Element Number: {ElementNumber}";
    }
}
