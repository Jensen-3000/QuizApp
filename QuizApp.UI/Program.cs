﻿using QuizApp.Logic;
using System.Reflection;
using System.Text.Json;


var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"QuizQuestions.json");

var text = File.ReadAllText(path);
var quiz = JsonSerializer.Deserialize<List<Quiz>>(text);

int quizNum = 1;
int quizCorrectAnswers = 0;

foreach (var item in quiz)
{
    Console.WriteLine($"Spørgsmål {quizNum++} / {quiz.Count}");
    Console.WriteLine(item.Question);


    int num = 1;
    foreach (var item2 in item.Options)
    {
        Console.WriteLine($"{num++}. {item2}");
    }

    Console.Write("Indtast svar: ");
    string answer = Console.ReadLine().ToLower();
    var success = int.TryParse(answer, out int intAnswer);


    Console.WriteLine();
    if (answer == item.Answer.ToLower() || true == success && item.Answer == item.Options[intAnswer-1])
    {
        Console.WriteLine("Rigtigt!");
        quizCorrectAnswers++;
    }

    else
    {
        Console.WriteLine("Forkert :(");
        Console.Write("Det rigtige svar er: ");
        Console.WriteLine(item.Answer);
    }

    Console.WriteLine();
    foreach (var item3 in item.Fact)
    {
        Console.WriteLine(item3);
    }

    Console.WriteLine("\n\nTryk på en vilkårlig tast for at fortsætte...");
    Console.ReadKey();
    Console.Clear();
}

Console.WriteLine($"Du fik {quizCorrectAnswers} ud af {quiz.Count} rigtige!");
