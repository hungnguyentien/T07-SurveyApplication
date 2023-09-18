using MediatR;
using SurveyApplication.Domain.Common.Responses;

namespace SurveyApplication.Application.Features.Accounts.Requests.Commands
{
    public class RoleCommand : IRequest<BaseCommandResponse>
    {
        public string Name { get; set; }
        public List<MatrixPermission> MatrixPermission { get; set; }
    }

    public class MatrixPermission
    {
        public int Module { get; set; }
        public List<int> LstPermission { get; set; }
    }
}
