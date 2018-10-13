using Topshelf;

namespace EventViewer.TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            var rc = HostFactory.Run(x =>
            {
                x.Service<ServiceManager>(s =>
                {
                    s.ConstructUsing(name => new ServiceManager());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                

                x.SetDescription("EventViewer test harness");
                x.SetDisplayName("EventViewer test harness");
                x.SetServiceName("EventViewer test harness");
            });
        }
    }
}
