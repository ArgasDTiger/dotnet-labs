using Lab_5;

var collection1 = new ResearchTeamCollection("First Collection");
var collection2 = new ResearchTeamCollection("Second Collection");

var journal1 = new TeamsJournal();
var journal2 = new TeamsJournal();

collection1.ResearchTeamAdded += journal1.TeamEventHandler;
collection1.ResearchTeamInserted += journal1.TeamEventHandler;

collection1.ResearchTeamAdded += journal2.TeamEventHandler;
collection1.ResearchTeamInserted += journal2.TeamEventHandler;
collection2.ResearchTeamAdded += journal2.TeamEventHandler;
collection2.ResearchTeamInserted += journal2.TeamEventHandler;

Console.WriteLine("Adding default teams to collections:");
collection1.AddDefaults();
collection2.AddDefaults();

var team1 = new ResearchTeam("New Organization 1", 789, "Machine Learning", TimeFrame.Year);
var team2 = new ResearchTeam("New Organization 2", 1011, "Data Mining", TimeFrame.Long);

team1.AddParticipants(new Person("Bohdan", "Trypadush", new DateTime(1985, 5, 20)));
team2.AddParticipants(new Person("Roman", "Lupu", new DateTime(1990, 10, 15)));

team1.AddPapers(new Paper("System Programming", team1.Participants[0], new DateTime(2023, 3, 10)));

Console.WriteLine("\nAdding new teams to collections:");
collection1.AddResearchTeams(team1);
collection2.AddResearchTeams(team2);

Console.WriteLine("\nInserting team at position 0 in collection1:");
var team3 = new ResearchTeam("Inserted Organization", 1213, "Blockchain", TimeFrame.TwoYears);
collection1.InsertAt(0, team3);

Console.WriteLine("\nInserting team at non-existing position 10 in collection2:");
var team4 = new ResearchTeam("Appended Organization", 1415, "Cryptography", TimeFrame.Long);
collection2.InsertAt(10, team4);

Console.WriteLine("\nRemoving team at position 1 from collection1:");
collection1.Remove(1);

Console.WriteLine("\n" + collection1.ToShortList());
Console.WriteLine("\n" + collection2.ToShortList());

Console.WriteLine("\nJournal 1 (subscribed to collection1 only):");
Console.WriteLine(journal1);

Console.WriteLine("\nJournal 2 (subscribed to both collections):");
Console.WriteLine(journal2);