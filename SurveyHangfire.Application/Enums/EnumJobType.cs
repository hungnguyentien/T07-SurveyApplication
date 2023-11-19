using System.ComponentModel;

namespace Hangfire.Application.Enums;

public class EnumJobType
{
    public enum Type
    {
        [Description("Fire-and-forget jobs")] Enqueue = 1,
        [Description("Delayed jobs")] Delayed = 2,
        [Description("Recurring jobs")] Recurring = 3,
        [Description("Continuations")] Continuation = 4
    }
}