﻿namespace SurveyApplication.Domain.Common.Configurations;

public class BackupRestoreConfiguration
{
    public string DatabaseNames { get; set; }
    public string NamejobBackupDb { get; set; }
    public string DirBackupDb { get; set; }
    public string DirBackupServer { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}