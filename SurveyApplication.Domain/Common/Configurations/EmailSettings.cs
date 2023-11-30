﻿namespace SurveyApplication.Domain.Common.Configurations;

public class EmailSettings
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string SecretKey { get; set; }
    public string LinkKhaoSat { get; set; }
    public string DomainKhaoSat { get; set; }
    public string LinkDoiMatKhau { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string CcMail { get; set; }
    public bool IsSendMailBo { get; set; }
}