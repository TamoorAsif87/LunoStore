namespace Ordering.ValueObjects;

public record Address
{
    public string FirstName { get; } = default!;
    public string LastName { get; } = default!;
    public string EmailAddress { get; } = default!;
    public string AddressLine { get; } = default!;
    public string Country { get; } = default!;
    public string ZipCode { get; } = default!;
    public string City { get; } = default!;
    public string State { get; } = default!;

    protected Address() { }

    private Address(string firstName,string lastName,string emailAddress,string addressLine,string country,string city,string state,string zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        AddressLine = addressLine;
        Country = country;
        City = city;
        State = state;
        ZipCode = zipCode;

    }

    public static Address Of(string firstName, string lastName, string emailAddress, string addressLine, string country, string city, string state, string zipCode)
    {
        return new Address(firstName, lastName, emailAddress, addressLine, country, city, state, zipCode);
    }
}
