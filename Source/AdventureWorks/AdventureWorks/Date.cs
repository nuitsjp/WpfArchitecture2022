namespace AdventureWorks;

public partial struct Date
{
    public static Date operator +(Date date, Days days)
        => new(date.value.AddDays(days.AsPrimitive()));

    public static Date operator -(Date date, Days days)
        => new(date.value.AddDays(days.AsPrimitive() * -1));
}