using System.Text;

namespace Lab_5;

public class TeamsJournal
{
    private readonly List<TeamsJournalEntry> _entries = [];

    public void TeamEventHandler(object source, TeamListHandlerEventArgs args)
    {
        var entry = new TeamsJournalEntry(
            args.CollectionName,
            args.ChangeInfo,
            args.ElementNumber
        );
        
        _entries.Add(entry);
    }

    public override string ToString()
    {
        if (_entries.Count == 0)
        {
            return "Journal is empty";
        }

        var sb = new StringBuilder();
        sb.AppendLine("Teams Journal Entries:");
        
        for (int i = 0; i < _entries.Count; i++)
        {
            sb.AppendLine($"{i + 1}. {_entries[i]}");
        }
        
        return sb.ToString();
    }
}