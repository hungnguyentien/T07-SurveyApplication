using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.Exceptions;
using SurveyApplication.Application.Features.CauHoi.Requests.Commands;
using SurveyApplication.Application.Features.DonVis.Requests.Commands;
using SurveyApplication.Application.Responses;
using SurveyApplication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.CauHoi.Handlers.Commands
{
    public class DeleteCauHoiCommandHandler : IRequestHandler<DeleteCauHoiCommand, BaseCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICauHoiRepository _cauHoiRepository;
        public DeleteCauHoiCommandHandler(IMapper mapper, ICauHoiRepository cauHoiRepository)
        {
            _mapper = mapper;
            _cauHoiRepository = cauHoiRepository;
        }

        public async Task<BaseCommandResponse> Handle(DeleteCauHoiCommand request, CancellationToken cancellationToken)
        {
            var lstCauHoi = await _cauHoiRepository.GetByIds(x => request.Ids.Contains(x.Id));
            if (lstCauHoi == null || lstCauHoi.Count == 0)
                throw new NotFoundException(nameof(CauHoi), request.Ids);

            await _cauHoiRepository.Deletes(lstCauHoi);
            return new BaseCommandResponse("Xóa thành công!");
        }
    }
}
