using System.Text.Json;

namespace Lab_6
{
    [Serializable]
    public class ResearchTeam : Team, IComparable<ResearchTeam>, IComparer<ResearchTeam>
    {
        private string _topic;
        private TimeFrame _timeFrame;
        private List<Person> _participants;
        private List<Paper?> _publications;

        public ResearchTeam(string organization, int registrationNumber, string topic, TimeFrame timeFrame)
            : base(organization, registrationNumber)
        {
            _topic = topic;
            _timeFrame = timeFrame;
            _participants = new List<Person>();
            _publications = new List<Paper?>();
        }

        public ResearchTeam() : this("Стандартна організація", 1, "Стандартна тема", TimeFrame.Year)
        {
        }

        public string Topic
        {
            get => _topic;
            set => _topic = value;
        }

        public TimeFrame TimeFrame
        {
            get => _timeFrame;
            set => _timeFrame = value;
        }

        public List<Person> Participants
        {
            get => _participants;
            set => _participants = value;
        }

        public List<Paper?> Publications
        {
            get => _publications;
            set => _publications = value;
        }

        public Team Team => new Team(_organization, _registrationNumber);

        public void AddParticipants(params Person[] persons)
        {
            _participants.AddRange(persons);
        }

        public void AddPapers(params Paper[] papers)
        {
            _publications.AddRange(papers);
        }

        public Paper? LatestPublication
        {
            get
            {
                return _publications.Count == 0 ? null : _publications.OrderByDescending(p => p?.PublicationDate).First();
            }
        }

        public bool this[TimeFrame timeFrame] => _timeFrame == timeFrame;

        public new object DeepCopy()
        {
            try
            {
                // Use JSON serialization for deep copy
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    // Set up preserving references to handle circular references
                    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
                };
                
                string jsonString = JsonSerializer.Serialize(this, options);
                return JsonSerializer.Deserialize<ResearchTeam>(jsonString, options)!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating deep copy: {ex.Message}");
                // Fall back to manual deep copy if serialization fails
                ResearchTeam copy = new ResearchTeam(_organization, _registrationNumber, _topic, _timeFrame);
                
                foreach (Person person in _participants)
                {
                    copy._participants.Add((Person)person.DeepCopy());
                }
                
                foreach (Paper? paper in _publications)
                {
                    if (paper is not null)
                    {
                        copy._publications.Add((Paper)paper.DeepCopy());
                    }
                }
                
                return copy;
            }
        }

        public bool Save(string? filename)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
                };
                
                string jsonString = JsonSerializer.Serialize(this, options);
                File.WriteAllText(filename, jsonString);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
                return false;
            }
        }

        public bool Load(string? filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("File does not exist!");
                return false;
            }

            try
            {
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
                };
                
                string jsonString = File.ReadAllText(filename);
                ResearchTeam? temp = JsonSerializer.Deserialize<ResearchTeam>(jsonString, options);

                if (temp == null) return false;
                _organization = temp._organization;
                _registrationNumber = temp._registrationNumber;
                _topic = temp._topic;
                _timeFrame = temp._timeFrame;
                _participants = temp._participants;
                _publications = temp._publications;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading from file: {ex.Message}");
                return false;
            }
        }

        public bool AddFromConsole()
        {
            Console.WriteLine("Додавання нової публікації");
            Console.WriteLine("Введіть дані у форматі: 'Назва публікації;Ім'я автора;Прізвище автора;Дата народження (DD.MM.YYYY);Дата публікації (DD.MM.YYYY)'");
            Console.WriteLine("Приклад: 'Наукова стаття;Іван;Петренко;01.01.1990;15.06.2023'");
            
            string? input = Console.ReadLine();

            try
            {
                string[]? parts = input?.Split(';');
                if (parts != null && parts.Length != 5)
                {
                    throw new FormatException("Неправильна кількість параметрів");
                }

                if (parts != null)
                {
                    string title = parts[0].Trim();
                    string firstName = parts[1].Trim();
                    string lastName = parts[2].Trim();
                    DateTime birthDate = DateTime.Parse(parts[3].Trim());
                    DateTime publicationDate = DateTime.Parse(parts[4].Trim());
                
                    Person author = new Person(firstName, lastName, birthDate);
                    Paper paper = new Paper(title, author, publicationDate);
                
                    _publications.Add(paper);
                }

                Console.WriteLine("Публікацію успішно додано!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при додаванні публікації: {ex.Message}");
                return false;
            }
        }

        public static bool Save(string? filename, ResearchTeam? team)
        {
            if (team == null)
            {
                Console.WriteLine("Team object is null!");
                return false;
            }

            return team.Save(filename);
        }

        public static bool Load(string? filename, ResearchTeam? team)
        {
            if (team is null)
            {
                Console.WriteLine("Team object is null!");
                return false;
            }

            return team.Load(filename);
        }

        public override string ToString()
        {
            string result = $"{base.ToString()}, Тема: {_topic}, Тривалість: {_timeFrame}\n";
            
            result += "Учасники:\n";
            if (_participants.Count > 0)
            {
                foreach (Person person in _participants)
                {
                    result += $"- {person}\n";
                }
            }
            else
            {
                result += "- Немає учасників\n";
            }
            
            result += "Публікації:\n";
            if (_publications.Count > 0)
            {
                foreach (Paper? paper in _publications)
                {
                    result += $"- {paper}\n";
                }
            }
            else
            {
                result += "- Немає публікацій\n";
            }
            
            return result;
        }

        public virtual string ToShortString()
        {
            return $"{base.ToString()}, Тема: {_topic}, Тривалість: {_timeFrame}, Кількість учасників: {_participants.Count}, Кількість публікацій: {_publications.Count}";
        }

        public int CompareTo(ResearchTeam? other)
        {
            return other == null ? 1 : RegistrationNumber.CompareTo(other.RegistrationNumber);
        }
        
        public int Compare(ResearchTeam? x, ResearchTeam? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            return string.CompareOrdinal(x.Topic, y.Topic);
        }
    }
}