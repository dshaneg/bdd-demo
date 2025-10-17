using Reqnroll;

[Binding]
public class BankAccountHooks
{
    // can filter based on tags.
    // note that the unfiltered hook will also run for tagged scenarios.
    [BeforeScenario("@slow")]
    public static void BeforeSlowScenario()
    {
        Console.WriteLine("Setting up for a slow scenario");
    }

    [BeforeScenario]
    public static void BeforeScenario()
    {
        Console.WriteLine("BeforeScenario");
    }

    [AfterScenario]
    public static void AfterScenario()
    {
        Console.WriteLine("AfterScenario");
    }

    [BeforeFeature]
    public static void BeforeFeature()
    {
        Console.WriteLine("BeforeFeature");
    }

    [AfterFeature]
    public static void AfterFeature()
    {
        Console.WriteLine("AfterFeature");
    }
}