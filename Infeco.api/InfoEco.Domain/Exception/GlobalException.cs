namespace InfoEco.Domain.Exceptions
{
    public class GlobalException : Exception
    {
        public GlobalException(string message)
            : base(message)
        {
        }
    }
}