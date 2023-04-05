using UnitGenerator;

namespace AdventureWorks.Business;

/// <summary>
/// Date without time
/// </summary>
[UnitOf(typeof(DateTime))]
public partial struct Date
{
    public static Date Today => (Date) DateTime.Today;
    public static Date operator +(Date date, Days days)
        => new(date.value.AddDays(days.AsPrimitive()));

    public static Date operator -(Date date, Days days)
        => new(date.value.AddDays(days.AsPrimitive() * -1));
}