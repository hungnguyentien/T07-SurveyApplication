namespace SurveyApplication.Application.DTOs.Role
{
    public class UpdateRoleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<MatrixPermission> MatrixPermission { get; set; }
    }
}
