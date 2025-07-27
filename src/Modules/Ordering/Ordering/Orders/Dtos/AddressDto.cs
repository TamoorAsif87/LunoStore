    namespace Ordering.Orders.Dtos;

    public record AddressDto(

        string FirstName, 
        string LastName, 
        string EmailAddress, 
        string AddressLine, 
        string Country,
        string ZipCode, 
        string City, 
        string State 

        );
