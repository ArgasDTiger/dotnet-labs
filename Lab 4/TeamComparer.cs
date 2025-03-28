namespace Lab_4;

public class TeamComparer : IComparer<Team>
{
    public int Compare(Team? x, Team? y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;

        int registrationComparison = x.RegistrationNumber.CompareTo(y.RegistrationNumber);
        return registrationComparison != 0 ? registrationComparison : string.Compare(x.Organization, y.Organization, StringComparison.Ordinal);
    }
}