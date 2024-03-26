namespace HIOF.Hast.HARB.Framework
{
    /// <summary>
    /// LogEntry is used to keep time and event separated for easy sorting and processing.
    /// </summary>
    /// <param name="time">Time of event.</param>
    /// <param name="message">Event to be logged.</param>
    public readonly struct LogEntry(DateTime time, string message)
    {
        public DateTime Time { get; } = time;
        public string Message { get; } = message;
        public override readonly string ToString()
        {
            return $"{Time} - {Message}";
        }
    }
}
