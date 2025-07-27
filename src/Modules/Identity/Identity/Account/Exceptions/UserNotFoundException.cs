using Shared.Exceptions;

namespace Identity.Account.Exceptions;

public class UserNotFoundException:NotFoundException
{
    public UserNotFoundException(string message):base(message)
    {

    }
  
}
