using MovieStoreWebAPI.Utilities.Logger.Abstract;

namespace MovieStoreWebAPI.Utilities.Logger.Concrete
{
    public class ConsoleLogger:ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine("[ConsoleLogger] - " + message);
        }
    }
}
