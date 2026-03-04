namespace Event_Management_System.MiddleWares.CustomeException
{
    public class BadRequestException(string message) : Exception(message)
    {
    }
}
