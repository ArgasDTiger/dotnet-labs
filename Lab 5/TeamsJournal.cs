using System.Text;

namespace Lab_5;

public class TeamsJournal
{
    private List<TeamsJournalEntry> _entries = [];

    public List<TeamsJournalEntry> Entries
    {
        get => _entries;
        set => _entries = value;
    }

    public void TeamEventHandler(object source, TeamListHandlerEventArgs args)
    {
        var entry = new TeamsJournalEntry(
            args.CollectionName,
            args.ChangeType,
            args.ElementNumber
        );
        
        Entries.Add(entry);
    }

    public override string ToString()
    {
        if (Entries.Count == 0)
        {
            return "Journal is empty";
        }

        var sb = new StringBuilder();
        sb.AppendLine("Teams Journal Entries:");
        
        for (int i = 0; i < Entries.Count; i++)
        {
            sb.AppendLine($"{i + 1}. {Entries[i]}");
        }
        
        return sb.ToString();
    }
}