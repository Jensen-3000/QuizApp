using System.Text.Json.Serialization;

namespace QuizApp.Logic
{
    public class Quiz
    {
        [JsonPropertyName("question")]
        public string Question { get; set; }

        [JsonPropertyName("options")]
        public string[] Options { get; set; }

        [JsonPropertyName("answer")]
        public string Answer { get; set; }

        [JsonPropertyName("fact")]
        public string[] Fact { get; set; }
    }
}