namespace AdventureWorks;

public partial struct Gram
{
    public static Gram operator *(Gram gram, int quantity)
        => new(gram.value * quantity);

}