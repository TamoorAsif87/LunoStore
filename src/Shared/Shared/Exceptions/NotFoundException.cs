﻿namespace Shared.Exceptions;

public class NotFoundException:Exception
{
    public NotFoundException(string message):base(message)
    {
        
    }

    public NotFoundException(string name, object key):base($"Entity \" {name} \" not found with key:{key}")
    {
        
    }
}
