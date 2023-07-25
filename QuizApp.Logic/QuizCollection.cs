using System.Text.Json.Serialization;

namespace QuizApp.Logic
{
    public class QuizCollection
    {
        [JsonPropertyName("questions")]
        public List<QuizModel> Questions { get; set; }
    }
}
