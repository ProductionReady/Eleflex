using System;
using System.Timers;
using System.Web.Hosting;

namespace Eleflex.Web
{
    /// <summary>
    /// Represents an object used to perform background processes in the web application. We should use QueueBackgroundWorkItem (QBWI), however as of right now, Azure does not support .NET 4.5.2.
    /// </summary>
    public class EleflexWebProcess : IRegisteredObject
    {

        protected object _lock = new object();
        protected bool _stop;
        protected Timer _timer;
        protected Action _process;

        public EleflexWebProcess(double interval, Action process)
        {            
            HostingEnvironment.RegisterObject(this);
            _process = process;
            _timer = new Timer();
            _timer.Elapsed += _timer_Elapsed;
            _timer.Interval = interval;
            _timer.Start();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (_lock)
            {                
                if (_stop)
                {
                    return;
                }
                _process();
            }
        }

        public void Stop(bool immediate)
        {
            lock (_lock)
            {
                _stop = true;
            }
            HostingEnvironment.UnregisterObject(this);
        }

    }
}
