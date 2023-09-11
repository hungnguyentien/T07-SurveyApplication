using System;
using Hangfire.Domain.Models.Abstractions;

namespace Hangfire.Domain.Models.Abstractions
{
    public class Auditable : IAuditable
    {
        public int? CreatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
