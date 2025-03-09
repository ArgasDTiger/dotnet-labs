using System;
using System.Text.Json.Serialization;

namespace Lab_6
{
    [Serializable]
    public class Team : INameAndCopy, IComparable<Team>
    {
        protected string _organization;
        protected int _registrationNumber;

        // Default constructor for JSON deserialization
        public Team() : this("Стандартна команда", 1)
        {
        }

        [JsonConstructor]
        public Team(string organization, int registrationNumber)
        {
            _organization = organization;
            _registrationNumber = registrationNumber;
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
            get => _organization;
            set => _organization = value;
        }

        public virtual object DeepCopy()
        {
            return new Team(_organization, _registrationNumber);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (Team)obj;
            return _organization == other._organization &&
                   _registrationNumber == other._registrationNumber;
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
            return HashCode.Combine(_organization, _registrationNumber);
        }

        public override string ToString()
        {
            return $"Організація: {_organization}, Реєстрація: {_registrationNumber}";
        }

        public int CompareTo(Team? other)
        {
            return other == null ? 1 : _registrationNumber.CompareTo(other._registrationNumber);
        }
    }
}