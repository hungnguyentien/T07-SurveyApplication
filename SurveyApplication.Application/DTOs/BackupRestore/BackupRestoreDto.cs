using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.DTOs.BackupRestore
{
    public class BackupRestoreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Capacity { get; set; }
        public string TimeBackup { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
