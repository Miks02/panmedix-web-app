namespace PanMedix.Exceptions.User;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException(string message = "Korisnik sa navedenom e-mail adresom već postoji") : base(message)
    {

    }
}