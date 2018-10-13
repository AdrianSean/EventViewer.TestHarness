using System.Diagnostics;

namespace EventViewer.TestHarness
{
    internal class EventViewerLogger
    {
        /// <summary>
        /// Add an entry under Application log
        /// </summary>
        /// <param name="message"></param>
        internal void LogInformationToApplication(string message) {

            using (var eventLog = new EventLog("Application")) {
                eventLog.Source = "EventViewer.TestHarness";
                eventLog.WriteEntry(message, EventLogEntryType.Information);
            }
        }

        /// <summary>
        /// Add a custom log and write entry to it
        /// </summary>
        /// <param name="message"></param>
        internal void LogInformationToCustomLog(string message) {

            const string source = "r";
            const string log = "LLL";
            // Create the source, if it does not already exist.
            if (!EventLog.SourceExists(source,"."))
            {
                // An event log source should not be created and immediately used.
                // There is a latency time to enable the source, it should be created
                // prior to executing the application that uses the source.
                // Execute this sample a second time to use the new source.
                var eventSOurceCreationData = new EventSourceCreationData(source, log);
                EventLog.CreateEventSource(eventSOurceCreationData);
               
                return;
            }

            // Create an EventLog instance and assign its source.
            using (var myLog = new EventLog(log)) {
                myLog.Source = source;
                myLog.WriteEntry("Writing to event log.");
                myLog.Close();                
            }
        }
    }
}