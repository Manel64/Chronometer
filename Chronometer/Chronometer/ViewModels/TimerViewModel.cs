using Chronometer.Core;
using Chronometer.Core.ICommands;
using Chronometer.Models;
using System;
using System.Media;
using System.Windows.Input;

namespace Chronometer.ViewModels
{
    public class TimerViewModel : ViewModelBase
    {
        #region Fields
        private readonly ITimerModel _Chrono = new TimerModel();
        #endregion

        #region Constructors
        /// <summary>
        /// Construct a new timer view model.
        /// </summary>
        public TimerViewModel()
        {
            //  add event handlers
            AddEventHandlers();
            //  bind the commands to their respective actions of 
            BindCommands();
        }
        #endregion

        #region Properties
        #region TimerValue
        /// <summary>
        /// The value of the timer as a string.
        /// </summary>
        private string timerValue = "00:00:00";
        public string TimerValue
        {
            get => timerValue;
            set
            {
                if (timerValue != value)
                {
                    timerValue = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion
        #region Text Start Button
        /// <summary>
        /// The content of Start Button
        /// </summary>
        private string startText = "Start";
        public string StartText
        {
            get => startText;
            set
            {
                if (startText != value)
                {
                    startText = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #endregion

        #region Commands

        public ICommand StartTimerCommand { get; private set; }
        public ICommand PauseTimerCommand { get; private set; }
        public ICommand StopTimerCommand { get; private set; }

        #endregion

        #region Binding Commands

        private void BindCommands()
        {
            StartTimerCommand = new RelayCommand(() => StartTimerExecute(), CanStartTimerExecute);
            PauseTimerCommand = new RelayCommand(() => PauseTimerExecute(), CanPauseTimerExecute);
            StopTimerCommand = new RelayCommand(() => StopTimerExecute(), CanStopTimerExecute);
        }
        #endregion

        #region Settings Commands

        #region Start Command
        /// <summary>
        /// Start the timer
        /// </summary>
        private void StartTimerExecute()
        {
            StartText = "Start";
            _Chrono.Start();
        }

        /// <summary>
        /// can we start timer?
        /// </summary>
        private bool CanStartTimerExecute()
        {
            return _Chrono.Status != TimerState.Running;
        }

        #endregion

        #region Pause Command

        /// <summary>
        /// Stop the underlying timer.
        /// </summary>
        private void PauseTimerExecute()
        {
            StartText = "Restart";
            _Chrono.Pause();
        }

        /// <summary>
        /// can we Pause timer?
        /// </summary>
        private bool CanPauseTimerExecute()
        {
            return _Chrono.Status == TimerState.Running;
        }

        #endregion

        #region Stop Command

        /// <summary>
        /// Stop the underlying timer.
        /// </summary>
        private void StopTimerExecute()
        {
            StartText = "Star";
            _Chrono.Reset();
            _Chrono.Stop();
        }

        /// <summary>
        /// can we stop timer?
        /// </summary>
        private bool CanStopTimerExecute()
        {
            return (_Chrono.Status == TimerState.Running || _Chrono.Status == TimerState.Paused) && (_Chrono.Status != TimerState.Stoped);
        }

        #endregion

        #region Reset Command

        /// <summary>
        /// Reset the timer and update corresponding values.
        /// </summary>
        private void ResetTimerExecute()
        {
            _Chrono.Reset();
            UpdateTimerValues();
        }
        #endregion

        #endregion Setting Commands

        #region Methods
        /// <summary>
        /// Update the timer view model.
        /// </summary>
        /// <param name="t"></param>
        private void UpdateTimer(TimerModelEventArgs e)
        {
            UpdateTimerValues();
        }
        /// <summary>
        /// Update the timer view model.
        /// </summary>
        /// <param name="t"></param>
        private void UpdateTimerValues()
        {
            TimeSpan t = _Chrono.Chrono;
            TimerValue = string.Format("{0}:{1}:{2}", t.Hours.ToString("D2"),
                t.Minutes.ToString("D2"), t.Seconds.ToString("D2"));
        }

        /// <summary>
        /// Add the event handlers.
        /// </summary>
        private void AddEventHandlers()
        {
            _Chrono.TickEvent += (sender, e) => OnTick(sender, e);
            _Chrono.StartedEvent += (sender, e) => OnStarted(sender, e);
            _Chrono.PausedEvent += (sender, e) => OnPaused(sender, e);
            _Chrono.StoppedEvent += (sender, e) => OnStopped(sender, e);
        }
        #endregion

        #region Event handlers
        /// <summary>
        /// Fires when the timer ticks.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTick(object sender, TimerModelEventArgs e)
        {
            UpdateTimer(e);
        }

        /// <summary>
        /// Fires when the timer starts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnStarted(object sender, TimerModelEventArgs e)
        {
            UpdateTimer(e);
            SystemSounds.Beep.Play();
        }

        /// <summary>
        /// Fires when the timer paused.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPaused(object sender, TimerModelEventArgs e)
        {
            UpdateTimer(e);
            SystemSounds.Beep.Play();
        }

        /// <summary>
        /// Fires when the timer stops.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnStopped(object sender, TimerModelEventArgs e)
        {
            UpdateTimer(e);
            SystemSounds.Beep.Play();
        }

        /// <summary>
        /// Fires when the timer resets.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReset(object sender, TimerModelEventArgs e)
        {
            UpdateTimer(e);
            SystemSounds.Beep.Play();
        }
        #endregion

    }
}
