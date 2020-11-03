using Microsoft.Azure.WebJobs;

namespace AdlsClientTestV3DI
{
    public interface IMyLoggerProvider
    {
        void TestMethod();
        void WriteLogProvider(string logMessage, ExecutionContext context);
    }
}