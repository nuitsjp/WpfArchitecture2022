namespace AdventureWorks;

public partial struct Dollar
{
    public static Dollar operator +(Dollar z, Dollar w)
    {
        return new Dollar(z.value + w.value);
    }

    public static Dollar operator -(Dollar z, Dollar w)
    {
        return new Dollar(z.value - w.value);
    }

    public static Dollar operator *(Dollar z, int quantity)
    {
        return new Dollar(z.value * quantity);
    }

    public static Dollar operator /(Dollar z, int quantity)
    {
        return new Dollar(z.value / quantity);
    }
    public static Dollar operator *(Dollar z, TaxRate taxRate)
    {
        return new Dollar(z.value * taxRate.AsPrimitive() / 100);
    }
}

public partial struct DollarPerGram
{
    public static Dollar operator *(DollarPerGram rate, Gram gram)
    {
        return new Dollar(rate.value * gram.AsPrimitive());
    }

}