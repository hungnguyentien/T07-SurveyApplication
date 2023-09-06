using SurveyApplication.Domain.Interfaces.Persistence;

namespace SurveyApplication.Application.Features
{
    public  class BaseMasterFeatures
    {
        protected ISurveyRepositoryWrapper _surveyRepo;
        public BaseMasterFeatures(ISurveyRepositoryWrapper surveyRepository)
        {
            _surveyRepo = surveyRepository;
        }
    }
}
