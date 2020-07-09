using System;

namespace Chronometer.Models
{
    public interface ITimerModel
    {
        /// <summary>
        /// Return Chronometer value
        /// </summary>
        TimeSpan Chrono { get; set; }

        /// <summary>
        /// Start Timer
        /// </summary>
        void Start();

        /// <summary>
        /// Pause Timer
        /// </summary>
        void Pause();

        /// <summary>
        /// Stop and reset timer
        /// </summary>
        void Stop();

        /// <summary>
        /// Reset timmer
        /// </summary>
        void Reset();

        /// <summary>
        /// Return the timer interval that we're using.
        /// </summary>
        TimeSpan TickIntrvl { get; }

        /// <summary>
        /// Tick event
        /// </summary>
        event EventHandler<TimerModelEventArgs> TickEvent;

        /// <summary>
        /// Timer started event
        /// </summary>
        event EventHandler<TimerModelEventArgs> StartedEvent;

        /// <summary>
        /// Timer Paused event
        /// </summary>
        event EventHandler<TimerModelEventArgs> PausedEvent;

        /// <summary>
        /// Timer stopped event
        /// </summary>
        event EventHandler<TimerModelEventArgs> StoppedEvent;
    }
}
