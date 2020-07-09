using System;
using System.Windows.Threading;

namespace Chronometer.Models
{
    abstract class TimerModelBase : ITimerModel
    {
        #region Fields
        /// <summary>
        /// The underlying timer
        /// </summary>
        private readonly DispatcherTimer _chrono = new DispatcherTimer();
        #endregion

        #region Constructors

        /// <summary>
        /// Create a timer into Ctor
        /// </summary>
        public TimerModelBase()
        {
            _chrono.Interval = TimeSpan.FromSeconds(1);
            _chrono.Tick += (sender, e) => OnDispatcherTimerTick();
            Reset();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Return the timer interval that we're using.
        /// </summary>
        public TimeSpan TickIntrvl => _chrono.Interval;

        /// <summary>
        /// Return the time remaining.
        /// </summary>
        public TimerState Status { get; set; }

        /// <summary>
        /// Return the time.
        /// </summary>
        public TimeSpan Chrono { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Start the Chronometer.
        /// </summary>
        public void Start()
        {
            _chrono.Start();
            OnStarted();
        }

        /// <summary>
        /// Pause the Chronometer.
        /// </summary>
        public void Pause()
        {
            _chrono.Stop();
            OnPaused();
        }

        /// <summary>
        /// Stop the Chronometer.
        /// </summary>
        public void Stop()
        {
            _chrono.Stop();
            OnStopped();
        }

        /// <summary>
        /// Stop and reset.
        /// </summary>
        public void Reset()
        {
            Chrono = TimeSpan.Zero;
        }

        #endregion

        #region Event Handlers
        /// <summary>
        /// Handle the ticking of the system timer.
        /// Add interval to Timer
        /// </summary>
        private void OnDispatcherTimerTick()
        {
            Chrono = Chrono + TickIntrvl;
            OnTickEvent();
        }
        #endregion

        #region Events
        public event EventHandler<TimerModelEventArgs> TickEvent;
        public event EventHandler<TimerModelEventArgs> StartedEvent;
        public event EventHandler<TimerModelEventArgs> PausedEvent;
        public event EventHandler<TimerModelEventArgs> StoppedEvent;
        #endregion

        #region  OnStopped
        /// <summary>
        /// Trigger Stopped event.
        /// </summary>
        private void OnStopped()
        {
            Status = TimerState.Stoped;

            if (StoppedEvent != null)
            {
                StoppedEvent(this, new TimerModelEventArgs(TimerModelEventArgs.Status.Stopped));
            }
        }
        #endregion

        #region  OnPaused
        /// <summary>
        /// Trigger Paused event.
        /// </summary>
        private void OnPaused()
        {
            Status = TimerState.Paused;

            if (StoppedEvent != null)
            {
                PausedEvent(this, new TimerModelEventArgs(TimerModelEventArgs.Status.Paused));
            }
        }
        #endregion

        #region OnStarted
        /// <summary>
        /// Trigger Started event.
        /// </summary>
        private void OnStarted()
        {
            Status = TimerState.Running;

            if (StartedEvent != null)
            {
                StartedEvent(this, new TimerModelEventArgs(TimerModelEventArgs.Status.Started));
            }
        }
        #endregion

        #region OnTick
        /// <summary>
        /// Trigger Tick event.
        /// </summary>
        private void OnTickEvent()
        {
            Status = TimerState.Running;

            if (TickEvent != null)
            {
                TickEvent(this, new TimerModelEventArgs(TimerModelEventArgs.Status.Running));
            }
        }
        #endregion

    }
}
