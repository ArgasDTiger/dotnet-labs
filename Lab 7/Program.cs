using System.Reflection;
using Lab_7;

Random random = new Random();

Type[] humanTypes =
[
    typeof(Student),
    typeof(Botan),
    typeof(Girl),
    typeof(PrettyGirl),
    typeof(SmartGirl)
];

ConsoleKeyInfo keyInfo;

do
{
    Console.WriteLine("\n=== Testing Couples ===");

    for (int i = 0; i < 2; i++)
    {
        try
        {
            Type firstType = humanTypes[random.Next(humanTypes.Length)];
            Type secondType = humanTypes[random.Next(humanTypes.Length)];
            
            Human human1 = CreateHumanInstance(firstType);
            Human human2 = CreateHumanInstance(secondType);
            
            Console.WriteLine($"\nCouple attempt: {human1.GetType().Name} and {human2.GetType().Name}");
            
            try
            {
                IHasName? result = Couple(human1, human2);

                Console.WriteLine(result != null
                    ? $"Result: {result.GetType().Name} with name {result.Name}"
                    : "No match occurred");
            }
            catch (SameGenderException ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
            catch (CouplingException ex)
            {
                Console.WriteLine($"Coupling error: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    Console.WriteLine("\nPress Enter for next couple, Q to quit, or 1/0 to quit");
    keyInfo = Console.ReadKey(true);

} while (keyInfo.Key != ConsoleKey.Q && keyInfo.Key != ConsoleKey.D1 && keyInfo.Key != ConsoleKey.D0);

return;

IHasName? Couple(Human first, Human second)
{
    ArgumentNullException.ThrowIfNull(first);

    ArgumentNullException.ThrowIfNull(second);

    if (first.IsMale() == second.IsMale())
    {
        throw new SameGenderException($"Both humans are of the same gender: {first.GetType().Name} and {second.GetType().Name}");
    }
    
    var firstAttributes = first.GetType()
        .GetCustomAttributes<CoupleAttribute>(true)
        .ToArray();
        
    if (firstAttributes.Length == 0)
    {
        throw new CouplingException($"{first.GetType().Name} has no Couple attributes defined");
    }
    
    string secondTypeName = second.GetType().Name;
    CoupleAttribute? matchingAttribute = firstAttributes
        .FirstOrDefault(attr => string.Equals(attr.Pair, secondTypeName, StringComparison.Ordinal));
    
    if (matchingAttribute == null)
    {
        Console.WriteLine($"{first.GetType().Name} doesn't have an attribute for {secondTypeName}");
        return null;
    }
    
    if (string.IsNullOrEmpty(matchingAttribute.ChildType))
    {
        throw new CouplingException($"ChildType is not set in the Couple attribute for {first.GetType().Name} and {secondTypeName}");
    }
    
    bool firstLikesSecond = random.NextDouble() <= matchingAttribute.Probability;
    Console.WriteLine($"Does {first.GetType().Name} like {secondTypeName}? {firstLikesSecond}");
    
    if (!firstLikesSecond)
    {
        return null;
    }
    
    var secondAttributes = second.GetType()
        .GetCustomAttributes<CoupleAttribute>(true)
        .ToArray();
        
    if (secondAttributes.Length == 0)
    {
        throw new CouplingException($"{secondTypeName} has no Couple attributes defined");
    }
    
    string firstTypeName = first.GetType().Name;
    CoupleAttribute? secondMatchingAttribute = secondAttributes
        .FirstOrDefault(attr => string.Equals(attr.Pair, firstTypeName, StringComparison.Ordinal));
    
    if (secondMatchingAttribute == null)
    {
        Console.WriteLine($"{secondTypeName} doesn't have an attribute for {firstTypeName}");
        return null;
    }
    
    bool secondLikesFirst = random.NextDouble() <= secondMatchingAttribute.Probability;
    Console.WriteLine($"Does {secondTypeName} like {firstTypeName}? {secondLikesFirst}");
    
    if (!secondLikesFirst)
    {
        return null;
    }
    
    string childName = second.Name;
    try
    {
        MethodInfo? nameMethod = second
            .GetType()
            .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .FirstOrDefault(m => m.ReturnType == typeof(string) && m.GetParameters().Length == 0);
            
        if (nameMethod != null)
        {
            string? methodResult = nameMethod.Invoke(second, null) as string;
            if (!string.IsNullOrEmpty(methodResult))
            {
                childName = methodResult;
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error getting name: {ex.Message}");
    }
    
    string childTypeName = matchingAttribute.ChildType;
    
    Type? childType = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(a => a.GetTypes())
        .FirstOrDefault(t => t.Name == childTypeName);
    
    if (childType == null)
    {
        throw new CouplingException($"Child type not found: {childTypeName}");
    }
    
    if (!typeof(IHasName).IsAssignableFrom(childType))
    {
        throw new CouplingException($"Child type {childTypeName} does not implement IHasName interface");
    }
    
    object? childObj = Activator.CreateInstance(childType);
    if (childObj == null)
    {
        throw new CouplingException($"Failed to create an instance of {childTypeName}");
    }
    
    IHasName child = (IHasName)childObj;
    
    PropertyInfo? nameProperty = childType.GetProperty("Name");
    if (nameProperty != null && nameProperty.CanWrite)
    {
        nameProperty.SetValue(child, childName);
    }
    else
    {
        Console.WriteLine($"Warning: Cannot set Name property on {childTypeName}");
    }
    
    PropertyInfo? patronymicProperty = childType.GetProperty("Patronymic");
    if (patronymicProperty != null && patronymicProperty.CanWrite)
    {
        string suffix = first.IsMale() ? "ович" : "івна";
        string patronymic = first.Name + suffix;
        patronymicProperty.SetValue(child, patronymic);
    }
    
    return child;
}

Human CreateHumanInstance(Type humanType)
{
    ArgumentNullException.ThrowIfNull(humanType);

    if (!typeof(Human).IsAssignableFrom(humanType))
    {
        throw new ArgumentException($"Type {humanType.Name} is not a Human", nameof(humanType));
    }
    
    object? instance = Activator.CreateInstance(humanType);
    if (instance == null)
    {
        throw new InvalidOperationException($"Failed to create an instance of {humanType.Name}");
    }
    
    return (Human)instance;
}