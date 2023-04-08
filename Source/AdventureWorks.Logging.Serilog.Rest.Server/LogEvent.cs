﻿namespace AdventureWorks.Logging.Serilog.Rest.Server;

public class LogEvent
{
    public DateTime Timestamp { get; set; }

    public string Level { get; set; }

    public string RenderedMessage { get; set; }
}