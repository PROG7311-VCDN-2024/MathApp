namespace MathApp.Utils
{
    public sealed class AuthLogger
    {
        private static readonly AuthLogger instance = new AuthLogger();
        private readonly StreamWriter fileWriter;

        static AuthLogger() { }

        private AuthLogger()
        {
            fileWriter = new StreamWriter("auth_errors.log", true);
            fileWriter.AutoFlush = true;
        }

        public static AuthLogger Instance
        {
            get
            {
                return instance;
            }
        }

        public void LogError(string errorMessage)
        {
            string logMessage = $"{DateTime.Now} - ERROR: {errorMessage}";
            fileWriter.WriteLine(logMessage);
        }
    }
}
