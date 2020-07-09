using System;

namespace Chronometer.Models
{
    /// <summary>
    /// EventArgs class for the timer model.
    /// </summary>
    public class TimerModelEventArgs : EventArgs
    {
        /// <summary>
        /// Enum Status of Chronometer
        /// </summary>
        public enum Status
        {
            NotSpecified,
            Stopped,
            Paused,
            Started,
            Running,
            Reset
        }

        public Status State { get; set; } = Status.NotSpecified;

        /// <summary>
        /// Event Args status of Chronometer
        /// </summary>
        /// <param name="state"></param>
        public TimerModelEventArgs(Status state)
        {
            State = state;
        }

    }
}
