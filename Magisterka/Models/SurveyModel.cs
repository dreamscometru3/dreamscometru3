namespace Magisterka.Models
{
    public class SurveyModel : BaseModel
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }

        public bool TheoryGame { get; set; }
        public string Why { get; set; }

        public virtual ClientCodeModel ClientCode { get; set; }
    }
}