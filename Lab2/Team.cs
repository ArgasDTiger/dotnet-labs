namespace Lab2;

public class Team : INameAndCopy
{
    protected string _organization;
    protected int _registrationNumber;

    public Team(string organization, int registrationNumber)
    {
        Organization = organization;
        RegistrationNumber = registrationNumber;
    }

    public Team() : this("Стандартна команда", 1)
    {
    }

    public string Organization
    {
        get => _organization;
        set => _organization = value;
    }

    public int RegistrationNumber
    {
        get => _registrationNumber;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Число має бути додатнім");
            _registrationNumber = value;
        }
    }

    public string Name
    {
        get => Organization;
        set => Organization = value;
    }

    public virtual object DeepCopy() => (Team)MemberwiseClone();

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var other = (Team)obj;
        return Organization == other.Organization &&
               RegistrationNumber == other.RegistrationNumber;
    }

    public static bool operator ==(Team? left, Team? right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(Team? left, Team? right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Organization, RegistrationNumber);
    }

    public override string ToString()
    {
        return $"Організація: {Organization}, Реєстрація: {RegistrationNumber}";
    }
}