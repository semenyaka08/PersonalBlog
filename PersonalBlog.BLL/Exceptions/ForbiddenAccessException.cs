namespace PersonalBlog.BLL.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException(string message) : base(message) { }
    
    public string Details { get; set; } = string.Empty;
}