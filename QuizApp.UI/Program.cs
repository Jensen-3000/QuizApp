using QuizApp.Logic;
using System.Text.Json;


while (true)
{
    ChooseQuiz();
};


void RunQuiz(List<QuizModel> quiz)
{
    int quizCorrectAnswers = 0;

    foreach (var currentQuestion in quiz)
    {
        DisplayQuestions(currentQuestion, quiz);

        string answer = Console.ReadLine().ToLower();

        Console.WriteLine();

        if (IsAnswerCorrect(currentQuestion, answer))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Korrekt!");
            Console.ResetColor();
            quizCorrectAnswers++;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Forkert :(");
            Console.ResetColor();
            Console.Write("Det rigtige svar var: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(currentQuestion.Answer);
            Console.ResetColor();
        }

        Console.WriteLine();
        DisplayFact(currentQuestion.Fact);
        ClickToContinue();
    }

    Console.WriteLine($"Du fik {quizCorrectAnswers} ud af {quiz.Count} rigtige!");
    ClickToContinue();
}

bool IsAnswerCorrect(QuizModel question, string userAnswer)
{
    if (userAnswer == question.Answer.ToLower())
        return true;

    if (int.TryParse(userAnswer, out int intAnswer)
        && intAnswer > 0
        && intAnswer <= question.Options.Length
        && question.Answer == question.Options[intAnswer - 1])
        return true;

    return false;
}

void DisplayQuestions(QuizModel questions, List<QuizModel> quiz)
{
    int questionNum = 1;
    Console.WriteLine($"Spørgsmål {questionNum++} / {quiz.Count}");
    Console.WriteLine();
    Console.WriteLine($"{questions.Question}");


    int num = 1;
    foreach (var option in questions.Options)
    {
        Console.WriteLine($"{num++}. {option}");
    }

    Console.WriteLine();
    Console.Write("Indtast tal eller svar: ");
}

void DisplayFact(string[] facts)
{
    foreach (var fact in facts)
    {
        Console.WriteLine(fact);
    }
}

(List<QuizModel>, string) ReadQuizFile(string filePath)
{
    try
    {
        var text = File.ReadAllText(filePath);
        var quizContainer = JsonSerializer.Deserialize<QuizCollection>(text);
        return (quizContainer.Questions, null);
    }
    catch (Exception ex)
    {
        return (null, $"Der skete en fejl: {ex.Message}");
    }
}

void ChooseQuiz()
{
    Console.WriteLine("Vælg en Quiz\n");

    var availableQuizzes = Directory.GetFiles("Quizzes", "*.json");
    if (availableQuizzes.Length <= 0)
    {

        Console.WriteLine("Der blev ikke fundet nogle Quiz'.");
        return;
    }

    int availableQuizzesNum = 1;

    foreach (var file in availableQuizzes)
    {
        Console.WriteLine($"{availableQuizzesNum++}. {Path.GetFileNameWithoutExtension(file)}");
    }

    if (int.TryParse(Console.ReadLine(), out int quizChoice) &&
        quizChoice > 0 && quizChoice <= availableQuizzes.Length)
    {
        var selectedQuizPath = availableQuizzes[quizChoice - 1];
        var (quizQuestions, errorMessage) = ReadQuizFile(selectedQuizPath);

        if (!string.IsNullOrEmpty(errorMessage))
        {
            Console.WriteLine(errorMessage);
            return;
        }
        Console.Clear();
        RunQuiz(quizQuestions);
    }
    else
    {
        Console.WriteLine("Prøv igen...");
    }
}

void ClickToContinue()
{
    Console.WriteLine("\n\nTryk på en vilkårlig tast for at fortsætte...");
    Console.ReadKey();
    Console.Clear();
}