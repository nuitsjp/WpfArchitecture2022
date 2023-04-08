namespace AdventureWorks;

public static class Environments
{
    public static string GetEnvironmentVariable(string variable, string defaultValue)
    {
        return Environment.GetEnvironmentVariable(variable, EnvironmentVariableTarget.Process)
               ?? Environment.GetEnvironmentVariable(variable, EnvironmentVariableTarget.User)
               ?? Environment.GetEnvironmentVariable(variable, EnvironmentVariableTarget.Machine)
               ?? defaultValue;
    }

}