namespace AdventureWorks;

public partial struct RevisionNumber
{
    public static readonly RevisionNumber Unregistered = new(short.MinValue);
}