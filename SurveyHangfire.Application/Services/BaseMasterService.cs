using SurveyApplication.Domain.Interfaces.Persistence;

namespace Hangfire.Application.Services;

public class BaseMasterService
{
    protected ISurveyRepositoryWrapper _surveyRepo;

    protected BaseMasterService(ISurveyRepositoryWrapper surveyRepository)
    {
        _surveyRepo = surveyRepository;
    }
}