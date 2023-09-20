using AutoMapper;
using MediatR;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.Accounts.Requests.Commands;
using SurveyApplication.Domain.Common.Responses;
using SurveyApplication.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.Accounts.Handlers.Commands
{
    public class DeleteAccountCommandHandler : BaseMasterFeatures, IRequestHandler<DeleteAccountCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;

        public DeleteAccountCommandHandler(ISurveyRepositoryWrapper surveyRepository, IMapper mapper) : base(surveyRepository)
        {
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var lstLoaiHinh = await _surveyRepo.Account.GetByIds(x => request.Ids.Contains(x.Id));

            if (lstLoaiHinh == null || lstLoaiHinh.Count == 0)
                throw new NotFoundException(nameof(Accounts), request.Ids);

            foreach (var item in lstLoaiHinh)
                item.Deleted = true;

            await _surveyRepo.Account.UpdateAsync(lstLoaiHinh);
            await _surveyRepo.SaveAync();
            return new BaseCommandResponse("Xóa thành công!");
        }
    }
}
