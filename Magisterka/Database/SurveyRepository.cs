
namespace Magisterka.Database
{
    using Magisterka.Models;

    public class SurveyRepository : GenericRepository<SurveyModel>
    {
        public SurveyRepository(Entities context)
            : base(context)
        {
        }

        public void Save(SurveyModel survey)
        {
            var clientCode = this._repositories.ClientCodeRepository.GetByID(survey.Id);

            survey.ClientCode = clientCode;

            this.UpdateCreate(survey);
        }
    }
}