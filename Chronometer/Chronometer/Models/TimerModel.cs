namespace Chronometer.Models
{
    internal class TimerModel : TimerModelBase
    {

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}", Chrono.Hours.ToString("D2"),
                Chrono.Minutes.ToString("D2"), Chrono.Seconds.ToString("D2"));
        }
    }
}
