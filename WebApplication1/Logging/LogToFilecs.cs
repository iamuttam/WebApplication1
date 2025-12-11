namespace WebApplication1.Logging
{
    public class LogToFilecs : ILogs
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("LogToFilecs");
        }
    }
}
