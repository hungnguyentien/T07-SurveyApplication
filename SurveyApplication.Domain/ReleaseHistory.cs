using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SurveyApplication.Domain
{
    [Description("Bảng lịch sử phát hành")]
    public class ReleaseHistory
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Description("Khóa chính")]
        public int ID { get; set; }

        public int IDChannel { get; set; } = 0;

        [Required]
        [MaxLength(250)]
        [Description("Phiên bản")]
        public string BuildNumber { get; set; }

        [Required]
        [MaxLength(150)]
        [Description("Mã migration")]
        public string MigrationId { get; set; }

        [Required]
        [Description("Ngày phát hành")]
        public DateTime ReleaseDate { get; set; }

        [MaxLength(20)]
        [Description("Mã khách hàng")]
        public string CustomerCode { get; set; }

        [MaxLength(30)]
        [Description("Môi trường triển khai")]
        public string Env { get; set; }

        [MaxLength(500)]
        [Description("Mô tả")]
        public string Description { get; set; }

        [Description("Trạng thái")]
        public int Status { get; set; } = 1;
    }
}
