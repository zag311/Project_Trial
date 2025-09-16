using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace Recraft.Pages
{
    public partial class QuizPage : ContentPage
    {
        private class QuizQuestion
        {
            public string QuestionText { get; set; }
            public string[] Choices { get; set; }
            public int CorrectIndex { get; set; }
        }

        private List<QuizQuestion> allQuestions = new List<QuizQuestion>
        {
            new QuizQuestion { QuestionText = "What is the best way to recycle plastic?", Choices = new[] { "Throw in trash", "Burn it", "Reuse or send to recycling center", "Bury it" }, CorrectIndex = 2 },
            new QuizQuestion { QuestionText = "How long does it take for paper to decompose?", Choices = new[] { "2 weeks", "2 months", "2 years", "200 years" }, CorrectIndex = 1 },
            new QuizQuestion { QuestionText = "What is composting?", Choices = new[] { "Burning trash", "Decomposing organic waste", "Throwing food away", "Recycling metal" }, CorrectIndex = 1 },
            new QuizQuestion { QuestionText = "Which material is not recyclable?", Choices = new[] { "Glass", "Aluminum", "Styrofoam", "Paper" }, CorrectIndex = 2 },
            new QuizQuestion { QuestionText = "What does 'eco-friendly' mean?", Choices = new[] { "Harmful to nature", "Safe for the environment", "Expensive", "Plastic-free" }, CorrectIndex = 1 },
            new QuizQuestion { QuestionText = "Which one is a renewable energy source?", Choices = new[] { "Coal", "Natural gas", "Wind", "Plastic" }, CorrectIndex = 2 },
            new QuizQuestion { QuestionText = "How can you save water at home?", Choices = new[] { "Keep tap running", "Take shorter showers", "Water lawn daily", "Flush often" }, CorrectIndex = 1 }
        };

        private List<QuizQuestion> quizQuestions;
        private int currentQuestionIndex = 0;
        private int score = 0;

        public QuizPage()
        {
            InitializeComponent();
            StartNewQuiz();
        }

        private void StartNewQuiz()
        {
            score = 0;
            currentQuestionIndex = 0;
            // Select 5 random questions for the quiz
            quizQuestions = allQuestions.OrderBy(q => Guid.NewGuid()).Take(5).ToList();

            // Ensure all quiz-related elements are visible when starting a new quiz
            OptionA.IsVisible = true;
            OptionB.IsVisible = true;
            OptionC.IsVisible = true;
            OptionD.IsVisible = true;
            NextButton.IsVisible = true;
            ResultButtons.IsVisible = false; // Hide "Retry" button until quiz is complete

            ShowQuestion();
        }

        private void ShowQuestion()
        {
            if (currentQuestionIndex < quizQuestions.Count)
            {
                // Display the current question
                var q = quizQuestions[currentQuestionIndex];

                QuestionNumberLabel.Text = $"Question {currentQuestionIndex + 1} of {quizQuestions.Count}";
                QuestionLabel.Text = q.QuestionText;

                // Assign choices to the Content property of RadioButtons
                OptionA.Content = q.Choices[0];
                OptionB.Content = q.Choices[1];
                OptionC.Content = q.Choices[2];
                OptionD.Content = q.Choices[3];

                // Uncheck all radio buttons for the new question
                OptionA.IsChecked = false;
                OptionB.IsChecked = false;
                OptionC.IsChecked = false;
                OptionD.IsChecked = false;
            }
            else
            {
                // Quiz is complete
                QuestionNumberLabel.Text = "Quiz Complete!";
                QuestionLabel.Text = $"Your Score: {score} / {quizQuestions.Count}";

                // Hide the entire options container (the boxes)
                
                OptionA.IsVisible = false;
                OptionB.IsVisible = false;
                OptionC.IsVisible = false;
                OptionD.IsVisible = false;
                // Hide the Next button and show the Result buttons
                NextButton.IsVisible = false;
                ResultButtons.IsVisible = true;

            }
        }

        private void OnNextClicked(object sender, EventArgs e)
        {
            var selected = GetSelectedAnswerIndex();

            if (selected == -1)
            {
                // User hasn't selected an answer, prompt them
                DisplayAlert("Selection Required", "Please choose an option before proceeding.", "OK");
                return;
            }

            // Check if the selected answer is correct and update score
            if (selected == quizQuestions[currentQuestionIndex].CorrectIndex)
            {
                score++;
            }

            currentQuestionIndex++; // Move to the next question
            ShowQuestion(); // Display the next question or the final results
        }

        private int GetSelectedAnswerIndex()
        {
            // Determine which radio button is selected
            if (OptionA.IsChecked) return 0;
            if (OptionB.IsChecked) return 1;
            if (OptionC.IsChecked) return 2;
            if (OptionD.IsChecked) return 3;
            return -1; // No answer selected
        }

        private void OnRetryClicked(object sender, EventArgs e)
        {
            StartNewQuiz(); // Restart the quiz
        }
    }
}