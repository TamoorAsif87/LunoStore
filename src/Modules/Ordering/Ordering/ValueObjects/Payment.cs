namespace Ordering.ValueObjects;

public record Payment
{
    public string? CardName { get; }
    public string CardNumber { get; } = default!;
    public DateOnly Expiration { get; } = default!;
    public string CVV { get; } = default!;

    protected Payment() { }

    private Payment(string cardName,string cardNumber,DateOnly expiration,string cvv)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        Expiration = expiration;
        CVV = cvv;

    }

    public static Payment Of(string cardName, string cardNumber, DateOnly expiration, string cvv)
    {
        return new Payment(cardName, cardNumber, expiration, cvv);
    }
}
