namespace ProFilePOC2.Application.Common.Exceptions;

public class UsernameConflictedException : Exception
{
    public UsernameConflictedException() : base("Username already exists!")
    {
        
    }
}