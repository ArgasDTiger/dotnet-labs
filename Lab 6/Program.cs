using Lab_6;


Console.OutputEncoding = System.Text.Encoding.UTF8;
    
ResearchTeam originalTeam = new ResearchTeam(
    "Київський Політехнічний Інститут",
    12345,
    "Дослідження штучного інтелекту",
    TimeFrame.TwoYears
);
    
originalTeam.AddPapers(
    new Paper("Розвиток нейронних мереж", new Person("Іван", "Ковальчук", new DateTime(1985, 5, 15)), new DateTime(2023, 3, 10)),
    new Paper("Машинне навчання в медицині", new Person("Олена", "Петренко", new DateTime(1990, 8, 22)), new DateTime(2023, 5, 5))
);
    
// Create a full copy using serialization
ResearchTeam copiedTeam = (ResearchTeam)originalTeam.DeepCopy();
    
// Display original and copy
Console.WriteLine("=== Оригінальний об'єкт: ===");
Console.WriteLine(originalTeam);
    
Console.WriteLine("=== Копія об'єкту: ===");
Console.WriteLine(copiedTeam);
    
// 2. Ask user to enter filename
Console.WriteLine("\n=== Робота з файлами ===");
Console.Write("Введіть ім'я файлу для збереження/завантаження даних: ");
string filename = Console.ReadLine();
    
if (!File.Exists(filename))
{
    Console.WriteLine($"Файл '{filename}' не знайдено. Cтворюю новий файл...");
    originalTeam.Save(filename);
    Console.WriteLine("Файл успішно створено та збережено дані.");
}
else
{
    Console.WriteLine($"Файл '{filename}' існує. Завантажую дані...");
    if (originalTeam.Load(filename))
    {
        Console.WriteLine("Дані успішно завантажено.");
    }
    else
    {
        Console.WriteLine("Помилка при завантаженні даних.");
    }
}
    
// 3. Display the ResearchTeam object
Console.WriteLine("\n=== Поточний стан об'єкту: ===");
Console.WriteLine(originalTeam);
    
// 4. Call AddFromConsole and Save
Console.WriteLine("\n=== Додавання нової публікації ===");
if (originalTeam.AddFromConsole())
{
    Console.WriteLine("Збереження змін у файлі...");
    if (originalTeam.Save(filename))
    {
        Console.WriteLine("Зміни успішно збережено.");
    }
    else
    {
        Console.WriteLine("Помилка при збереженні змін.");
    }
}
    
Console.WriteLine("\n=== Оновлений стан об'єкту: ===");
Console.WriteLine(originalTeam);
    
// 5. Call static Load, AddFromConsole, and static Save
Console.WriteLine("\n=== Використання статичних методів ===");
    
if (ResearchTeam.Load(filename, originalTeam))
{
    Console.WriteLine("Дані успішно завантажено за допомогою статичного методу.");
}
else
{
    Console.WriteLine("Помилка при завантаженні даних за допомогою статичного методу.");
}
    
Console.WriteLine("\n=== Додавання нової публікації (статичний цикл) ===");
if (originalTeam.AddFromConsole())
{
    Console.WriteLine("Збереження змін у файлі за допомогою статичного методу...");
    if (ResearchTeam.Save(filename, originalTeam))
    {
        Console.WriteLine("Зміни успішно збережено за допомогою статичного методу.");
    }
    else
    {
        Console.WriteLine("Помилка при збереженні змін за допомогою статичного методу.");
    }
}
    
// 6. Display the ResearchTeam object
Console.WriteLine("\n=== Фінальний стан об'єкту: ===");
Console.WriteLine(originalTeam);