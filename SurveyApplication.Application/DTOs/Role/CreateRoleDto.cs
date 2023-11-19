namespace SurveyApplication.Application.DTOs.Role;

public class CreateRoleDto
{
    public string Name { get; set; }
    public List<MatrixPermission> MatrixPermission { get; set; }
}

public class MatrixPermission
{
    public int Module { get; set; }
    public string NameModule { get; set; }
    public List<LstPermission> LstPermission { get; set; }
}

public class LstPermission
{
    public string Name { get; set; }
    public int Value { get; set; }
}