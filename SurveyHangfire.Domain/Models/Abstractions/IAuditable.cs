using System;

namespace Hangfire.Domain.Models.Abstractions
{
    public interface IAuditable
    {
        int? CreatedBy { get; set; }

        DateTime? CreateDate { get; set; }

        DateTime? UpdatedDate { get; set; }

        int? UpdatedBy { get; set; }
    }
}
