namespace ProFilePOC2.Domain.Exceptions;

public class DuplicateNameException : Exception
{
    public DuplicateNameException(string message) : base(message)
    {
        
    }
}