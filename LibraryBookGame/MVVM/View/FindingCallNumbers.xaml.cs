using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace LibraryBookGame.MVVM.View
{
    public partial class FindingCallNumbers : UserControl
    {
        //----------------------------------------Declarations----------------------------------------//
        private List<TreeNode> treeNodes = new List<TreeNode>();
        private List<string> flattenedList = new List<string>();
        private List<string> quizOptions = new List<string>();
        private string correctAnswer;

        // Starting level
        private int currentLevel = 1;
        private string currentQuestion;

        // Initialize the score variable
        private int score = 0; 


        //Variables for the timer
        private int remainingSeconds = 30;
        private DispatcherTimer timer;

        //----------------------------------------Declarations----------------------------------------//

        public class TreeNode
        {
            public string Name { get; set; }
            public List<TreeNode> Children { get; set; } = new List<TreeNode>();
        }

        public FindingCallNumbers()
        {
            InitializeComponent();

            string filePath = @"D:\Repository\LibraryBookGame\LibraryBookGame\Text\DataCallNumbers.txt";

            definitionListView.Loaded += (sender, e) => definitionListView.SelectionChanged += definitionListView_SelectionChanged;

            ReadDataAndPopulateTree(filePath);

            FlattenTreeForListView(treeNodes);
            StartQuiz();

            InitializeTimer();
        }

        //----------------------------------------Timer Methods----------------------------------------//

        private void InitializeTimer()
        {
            timer = new DispatcherTimer(DispatcherPriority.Normal);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (remainingSeconds > 0)
            {
                remainingSeconds--;
                timerLabel.Content = remainingSeconds.ToString();
            }
            else
            {
                timer.Stop();
                MessageBox.Show("Oh no! Your time has run out. Your score is " + score);

                RestartTimer();

            }
        }

        private void RestartTimer()
        {

            if (timer.IsEnabled)
            {
                timer.Stop();
            }
            remainingSeconds = 30;
            timerLabel.Content = remainingSeconds.ToString();
        }


        //----------------------------------------Timer Methods----------------------------------------//





        //----------------------------------------Tree----------------------------------------//
        private void ReadDataAndPopulateTree(string filePath)
        {
            try
            {
                var lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');

                    TreeNode node = new TreeNode
                    {
                        Name = parts[0],
                    };

                    treeNodes.Add(node);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FlattenTreeForListView(List<TreeNode> nodes)
        {
            foreach (var node in nodes)
            {
                flattenedList.Add(node.Name);
                FlattenTreeForListView(node.Children);
            }
        }

        //----------------------------------------Tree----------------------------------------//


        //----------------------------------------Displaying Call Numbers----------------------------------------//

        private void DisplayFilteredEntries()
        {
            wordListView.Items.Clear();
            definitionListView.Items.Clear();

            foreach (var entry in flattenedList)
            {
                if (entry.StartsWith("(3)"))
                {
                    wordListView.Items.Add(RemoveNumericPrefix(entry));
                }
                else
                {
                    definitionListView.Items.Add(RemoveNumericPrefix(entry));
                }
            }
        }

        private string RemoveNumericPrefix(string entry)
        {
            var match = Regex.Match(entry, @"\((\d+)\)\s*(.+)");

            if (match.Success)
            {
                string numericPart = match.Groups[3].Value.Trim();
                string description = match.Groups[2].Value.Trim();

                return $"{description}";
            }

            return entry.Trim();
        }

        private string GetRandomQuestion()
        {
            var thirdLevelEntries = flattenedList.Where(entry => entry.StartsWith("(3)")).ToList();
            Random rand = new Random();
            return thirdLevelEntries[rand.Next(thirdLevelEntries.Count)];
        }

        private void DisplayAnswerOptions(string currentQuestion)
        {
            List<string> options = GetAnswerOptions(currentQuestion);

            definitionListView.Items.Clear();
            foreach (var option in options.OrderBy(opt => opt))
            {
                definitionListView.Items.Add(RemoveNumericPrefix(option));
            }
        }

        private List<string> GetAnswerOptions(string currentQuestion)
        {
            quizOptions.Clear();

            quizOptions.Add(currentQuestion);

            var random = new Random();
            var incorrectOptions = flattenedList.Where(entry => entry.StartsWith("(1)")).Except(quizOptions).OrderBy(x => random.Next()).Take(3);
            quizOptions.AddRange(incorrectOptions);

            quizOptions = quizOptions.OrderBy(x => random.Next()).ToList();

            correctAnswer = currentQuestion;

            return quizOptions;
        }

        private void OptionSelected(string selectedOption)
        {
            string cleanedSelectedOption = RemoveNumericPrefix(selectedOption);
            string cleanedCorrectAnswer = RemoveNumericPrefix(correctAnswer);

            if (cleanedSelectedOption == cleanedCorrectAnswer)
            {
                MessageBox.Show("Correct!");
                score += 5;
                ScoreLabel.Content = "Score: " + score;

                if (currentLevel < 3)
                {
                    currentLevel++;
                    currentQuestion = GetRandomQuestion();
                    wordListView.Items.Clear();
                    wordListView.Items.Add(RemoveNumericPrefix(currentQuestion));
                    DisplayAnswerOptions(currentQuestion);
                }
                else
                {
                    MessageBox.Show("Congratulations! You completed the question.");
                    StartQuiz();
                }
            }
            else
            {
                MessageBox.Show($"Incorrect! The correct answer is: {correctAnswer}. Please try again.");
                score -= 10;
                ScoreLabel.Content = "Score: " + score;
            }
        }

        private void definitionListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (definitionListView.SelectedItem != null)
            {
                string selectedOption = definitionListView.SelectedItem.ToString();
                OptionSelected(selectedOption);
            }
        }

        //----------------------------------------Displaying Call Numbers----------------------------------------//




        //----------------------------------------Buttons----------------------------------------//
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartQuiz();

            //Starts Timer
            timer.Start();

            score = 0;
            ScoreLabel.Content = "Score: " + score;
        }

        private void StartQuiz()
        {
            quizOptions.Clear();
            correctAnswer = string.Empty;

            DisplayFilteredEntries();

            currentQuestion = GetRandomQuestion();

            wordListView.Items.Clear();
            wordListView.Items.Add(RemoveNumericPrefix(currentQuestion));

            DisplayAnswerOptions(currentQuestion);
        }

        

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            currentLevel = 1;
            StartQuiz();
        }

        private void HowToPlayButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("How to Play:\nSelect the correct 3rd level call number to progress through levels and complete the question.");
        }

        //----------------------------------------Buttons----------------------------------------//
    }
}


