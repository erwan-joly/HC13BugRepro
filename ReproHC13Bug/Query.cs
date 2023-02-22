using ReproHC13Bug;
using System.ComponentModel.DataAnnotations;

public class Query
{
    public bool ValidateInt(InputValue inputValue) => true;
}

public abstract class InputValue
{
    protected InputValue(string email)
    {
        Email = email;
    }

    [EmailAddress]
    [Custom]
    public string Email { get; set; }
}