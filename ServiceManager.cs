using Serilog;
using System;
using System.Threading;

namespace EventViewer.TestHarness
{
    internal class ServiceManager
    {
        EventViewerLogger _eventViewerLogger;

        ManualResetEvent _shutdownEvent = new ManualResetEvent(false);
        Thread _thread;

        public ServiceManager()
        {
            _eventViewerLogger = new EventViewerLogger();

            Log.Logger = new LoggerConfiguration()
                        .WriteTo.EventLog("EventViewer.TestHarness", "Application")
                        .CreateLogger();
        }

        internal void Start()
        {
            _thread = new Thread(DoWork)
            {
                IsBackground = true
            };
            _thread.Start();

            _eventViewerLogger.LogInformationToCustomLog("Hello Adrian!, starting service & logging it from EventViewerLogger!");
            Log.Information("Hello Adrian, starting service & logging it from Serilog!");
        }

        internal void Stop()
        {
            _eventViewerLogger.LogInformationToCustomLog("Hello Adrian!, stopping the service & logging it from EventViewerLogger!");
            Log.Information("Goodbye, stopping service & logging it from Serilog!");

            _shutdownEvent.Set();
            if (!_thread.Join(3000))
            { // give the thread 3 seconds to stop
                _thread.Abort();
            }
        }

        void DoWork()
        {
            while (!_shutdownEvent.WaitOne(1000)) // 1 second
            {
                string msg = string.Format("Service running @ {0} and all is good, logged from EventViewerLogger!", DateTime.Now);

                _eventViewerLogger.LogInformationToCustomLog(msg);                
                Log.Information(string.Format("Windows Event Log from Serilog @ {0} and all is good, logged from Serilog!", DateTime.Now));
            }
        }
    }
}