using EFRelationships.Data;

namespace EFRelationships.Web.Models
{
    public class QuestionViewModel
    {
        public List<Question> Questions { get; set; } = new List<Question>();
        public Question Question { get; set; } = new Question();
        public User User{ get; set; }
    }
}
