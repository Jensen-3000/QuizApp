using QuizApp.Logic;
using System.Text.Json;


var (quiz, errorMessage) = ReadQuizFile();

if (!string.IsNullOrEmpty(errorMessage))
{
    Console.WriteLine(errorMessage);
    return;
}


int quizNum = 1;
int quizCorrectAnswers = 0;

foreach (var currentQuestion in quiz)
{
    DisplayQuestions(currentQuestion);

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

    Console.WriteLine("\n\nTryk på en vilkårlig tast for at fortsætte...");
    Console.ReadKey();
    Console.Clear();
}

Console.WriteLine($"Du fik {quizCorrectAnswers} ud af {quiz.Count} rigtige!");


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

void DisplayQuestions(QuizModel questions)
{
    Console.WriteLine($"Spørgsmål {quizNum++} / {quiz.Count}");
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

(List<QuizModel>, string) ReadQuizFile()
{
    try
    {
        var text = File.ReadAllText("QuizQuestions.json");
        var quizContainer = JsonSerializer.Deserialize<QuizCollection>(text);
        return (quizContainer.Questions, null);
    }
    catch (Exception ex)
    {
        return (null, $"Der skete en fejl: {ex.Message}");
    }
}