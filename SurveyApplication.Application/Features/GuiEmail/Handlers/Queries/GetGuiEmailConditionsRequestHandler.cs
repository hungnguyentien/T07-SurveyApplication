using AutoMapper;
using MediatR;
using SurveyApplication.Application.Contracts.Persistence;
using SurveyApplication.Application.DTOs.GuiEmail;
using SurveyApplication.Application.Features.GuiEmails.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Features.GuiEmails.Handlers.Queries
{
   
    public class GetGuiEmailConditionsRequestHandler : IRequestHandler<GetGuiEmailConditionsRequest, List<GuiEmailDto>>
    {
        private readonly IGuiEmailRepository _guiEmailRepository;
        private readonly IMapper _mapper;
        public GetGuiEmailConditionsRequestHandler(IGuiEmailRepository guiEmailRepository, IMapper mapper)
        {
            _guiEmailRepository = guiEmailRepository;
            _mapper = mapper;
        }

        public async Task<List<GuiEmailDto>> Handle(GetGuiEmailConditionsRequest request, CancellationToken cancellationToken)
        {
            var guiEmails = await _guiEmailRepository.GetByConditions(request.PageIndex, request.PageSize, x => string.IsNullOrEmpty(request.Keyword) || !string.IsNullOrEmpty(x.TieuDe) && x.TieuDe.Contains(request.Keyword), x => x.Created);
            return _mapper.Map<List<GuiEmailDto>>(guiEmails);
        }
    }
}
