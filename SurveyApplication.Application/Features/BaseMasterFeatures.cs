using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features;

public class BaseMasterFeatures
{
    protected ISurveyRepositoryWrapper _surveyRepo;

    protected BaseMasterFeatures(ISurveyRepositoryWrapper surveyRepository)
    {
        _surveyRepo = surveyRepository;
    }
}