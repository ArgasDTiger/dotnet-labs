namespace Lab_4;

public class TeamComparer : IComparer<Team>
{
    public int Compare(Team? x, Team? y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;

        // Спочатку порівнюємо за реєстраційним номером
        int registrationComparison = x.RegistrationNumber.CompareTo(y.RegistrationNumber);
        if (registrationComparison != 0)
            return registrationComparison;
            
        // Якщо номери однакові, порівнюємо за назвою організації
        return string.Compare(x.Organization, y.Organization, StringComparison.Ordinal);
    }
}