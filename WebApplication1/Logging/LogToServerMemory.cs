namespace WebApplication1.Logging
{
    public class LogToServerMemory :ILogs
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Log To Server Memory");
        }
    }
}
