using System;

namespace BBConfigurator.Worker
{
    public delegate void ActionOccurredHandler(object sender, ActionEventArgs e);

    public class ActionEventArgs : EventArgs
    {
        public enum ActionEnum
        {
            ProcessStarted,
            ProcessStopped,
            BlackBoxRestarted,
            ExceptionHappen
        }

        public ActionEnum Action { get; set; }

        public String Message { get; set; }
    }
}