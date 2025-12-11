namespace WebApplication1.Logging
{
    public class LogToDB : ILogs
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("LogToDB");
        }
    }
}
