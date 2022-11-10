namespace AdventureWorks;

public partial struct Money
{
    public static Money operator +(Money z, Money w)
    {
        return new Money(z.value + w.value);
    }

    public static Money operator -(Money z, Money w)
    {
        return new Money(z.value - w.value);
    }

    public static Money operator *(Money z, int quantity)
    {
        return new Money(z.value * quantity);
    }

    public static Money operator /(Money z, int quantity)
    {
        return new Money(z.value / quantity);
    }
}