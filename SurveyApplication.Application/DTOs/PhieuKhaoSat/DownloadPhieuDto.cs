namespace SurveyApplication.Application.DTOs.PhieuKhaoSat;

public class DownloadPhieuDto
{
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] FileContents { get; set; }
}