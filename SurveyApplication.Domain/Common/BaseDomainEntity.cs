using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Domain.Common
{
    public abstract class BaseDomainEntity
    {
        public int Id { get; set; }
        public int? ActiveFlag { get; set; } = 1;
        public int? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
    }
}
