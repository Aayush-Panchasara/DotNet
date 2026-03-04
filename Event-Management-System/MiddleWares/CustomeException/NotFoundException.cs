namespace Event_Management_System.MiddleWares.CustomeException
{
    public class NotFoundException(string message) : Exception(message)
    {

    }
}
