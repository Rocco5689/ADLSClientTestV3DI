using System;
using Microsoft.Azure.DataLake.Store;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AdlsClientTestV3DI
{

    public class Function1
    {
        private readonly ILogger _logger;
        private readonly IMyLoggerProvider _otherClass;
        private AdlsClient _adlsClient;

        public Function1(ILogger<Function1> logger, IMyLoggerProvider otherClass, IAdlsClientClass adlsClass)
        {
            _logger = logger;
            _otherClass = otherClass;
            _adlsClient = ((AdlsClientClass)adlsClass).adlsClient;
        }

        [FunctionName("Function1")]
        public void Run([TimerTrigger("%timerSchedule%")]TimerInfo myTimer, ILogger log, ExecutionContext context)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            _adlsClient.CreateDirectory("testDir");

            // Passing in the logger Initiated by the Function Scope
            WriteLogFunction("Test Log From Function!!!", log, context);

            // Passing in the logger Initiated by the Host Scope of the MyLoggerProvider
            _otherClass.WriteLogProvider("Test Log From Logger Provider!!!", context);

            _otherClass.TestMethod();
            
        }

        public void WriteLogFunction(string logMessage, ILogger log, ExecutionContext context)
        {
            var path = String.Empty;

            try
            {
                path = Environment.GetEnvironmentVariable("HOME");

                if (path == null)
                {
                    path = String.Empty;
                }

                if (!path.ToLower().Contains("home"))
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                }
                
                if (path.ToLower().Contains("home"))
                {
                    path = path + @"\Logfiles";
                }
            }
            catch(Exception exc)
            {
                log.LogError(exc.Message);
            }

            log.LogInformation($"Log Path used by Function: {path}");

            System.IO.File.AppendAllText(path + @"\" + "TestLoggingFile.txt", $"{DateTime.Now} - InvocId: {context.InvocationId} - {logMessage}\n");
        }
    }
}
