using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace LibraryBookGame.MVVM.View
{
    public partial class IdentifyingAreas : UserControl
    {
        private string selectedWord;
        private string selectedDefinition;
        private bool isCallNumberMode = true;
        private int score = 0; // Initialize the score variable

        //Variables for the timer
        private int remainingSeconds = 30;
        private DispatcherTimer timer;

        private Dictionary<string, string> wordDefinitionPairs = new Dictionary<string, string>
        {
            { "000", "Generalities" },
            { "100", "Philosophy" },
            { "200", "Religion" },
            { "300", "Social Sciences" },
            { "400", "Language" },
            { "500", "Natural Sciences and Mathematics" },
            { "600", "Technology" },
            { "700", "Arts and Recreation" },
            { "800", "Literature" },
            { "900", "History and Geography" }
        };

        private List<string> callNumbers;
        private List<string> matchingDefinitions;
        private List<string> allDefinitions;


        public IdentifyingAreas()
        {
            InitializeComponent();
            InitializeTimer();

        }

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

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame();

            //Starts Timer
            timer.Start();

            score = 0;
            ScoreLabel.Content = "Score: " + score;

        }

        private void InitializeGame()
        {
            callNumbers = GetRandomCallNumbers(4); //Gets 4 random call numbers that are stored in the wordDefinitionPairs dictionary
            matchingDefinitions = GetMatchingDefinitions(callNumbers); //Gets 4 matching definitions that are stored in the wordDefinitionPairs dictionary
            allDefinitions = GetRandomDefinitions(7); //Gets 7 additional random definitions that are stored in the wordDefinitionPairs dictionary
            Shuffle(allDefinitions); //Shuffles all definitions so that they are not displayed next to their corresponding call number

            //Populates Both wordListView && definitionListView
            wordListView.ItemsSource = callNumbers;
            definitionListView.ItemsSource = allDefinitions;

            //Handles all item selection within wordListView
            wordListView.SelectionChanged += (sender, e) =>
            {
                if (wordListView.SelectedItem != null)
                {
                    selectedWord = wordListView.SelectedItem.ToString();
                    CheckMatch();
                }
            };

            //Handles all item selection within definitionListView
            definitionListView.SelectionChanged += (sender, e) =>
            {
                if (definitionListView.SelectedItem != null)
                {
                    selectedDefinition = definitionListView.SelectedItem.ToString();
                    CheckMatch();
                }
            };


            //Randomizes between displaying call numbers or their definitions in the 
            isCallNumberMode = new Random().Next(2) == 0;
            //Refreshes the listView to show either call numbers vs definitions or definitions vs call numbers
            RefreshListViews();

   
        }

        private void RefreshListViews()
        {

            if (isCallNumberMode)
            {
                wordListView.ItemsSource = callNumbers;
                definitionListView.ItemsSource = allDefinitions;
            }
            else
            {
                //Re-initialize for displaying call numbers in definitionListView and answers in wordListView
                callNumbers = GetRandomCallNumbers(7); 
                matchingDefinitions = GetMatchingDefinitions(callNumbers); 
                allDefinitions = GetRandomDefinitions(4); 
                Shuffle(allDefinitions); 
                wordListView.ItemsSource = allDefinitions;
                definitionListView.ItemsSource = callNumbers;
            }
        }

        //Chat-GPT Helped me here to remove correctly matched pair from data
        private void RemoveMatchedPair()
        {
            callNumbers.Remove(selectedWord);
            matchingDefinitions.Remove(selectedDefinition);
            allDefinitions.Remove(selectedDefinition);
        }

        

        //Gets random call numbers to be displayed in wordListView
        private List<string> GetRandomCallNumbers(int count)
        {
            Random random = new Random();
            var callNumbersList = wordDefinitionPairs.Keys.ToList();
            Shuffle(callNumbersList);
            return callNumbersList.Take(count).ToList();
        }

        private List<string> GetMatchingDefinitions(List<string> callNumbers)
        {
            return callNumbers.Select(cn => wordDefinitionPairs[cn]).ToList();
        }

        //Gets random answers to be displayed in definitionListView
        private List<string> GetRandomDefinitions(int count)
        {
            Random random = new Random();
            var definitionsList = wordDefinitionPairs.Values.ToList();
            Shuffle(definitionsList);
            return definitionsList.Take(count).ToList();
        }


        //Shuffling algo
        private void Shuffle<T>(IList<T> list)
        {
            Random random = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private void CheckMatch()
        {
            if (!string.IsNullOrEmpty(selectedWord) && !string.IsNullOrEmpty(selectedDefinition))
            {
                if (isCallNumberMode)
                {
                    if (wordDefinitionPairs.ContainsKey(selectedWord) && wordDefinitionPairs[selectedWord] == selectedDefinition)
                    {
                        MessageBox.Show("You got it correct!");
                        RemoveMatchedPair();

                        score += 10;
                        ScoreLabel.Content = "Score: " + score;

                        if (callNumbers.Count == 0)
                        {
                            MessageBox.Show("Congratulations! You've completed the game.");
                            InitializeGame();
                           
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sorry, that's not correct.");
                        score -= 5;
                        ScoreLabel.Content = "Score: " + score;
                    }
                }
                else
                {
                    if (wordDefinitionPairs.ContainsKey(selectedDefinition) && wordDefinitionPairs[selectedDefinition] == selectedWord)
                    {
                        MessageBox.Show("You got it correct!");
                        RemoveMatchedPair();

                        score += 10;
                        ScoreLabel.Content = "Score: " + score;

                        if (allDefinitions.Count == 0)
                        {
                            MessageBox.Show("Congratulations! You've completed the game.");
                            InitializeGame();
                            
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sorry, that's not correct.");
                        score -= 5;
                        ScoreLabel.Content = "Score: " + score;
                    }
                }

                selectedWord = null;
                selectedDefinition = null;
                wordListView.SelectedItem = null;
                definitionListView.SelectedItem = null;
            }
        }

        private void RestartGame()
        {
            InitializeGame();
        }


        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to restart?", "Restart Game", MessageBoxButton.YesNo);

            // If Yes selected then both ListBoxes are cleared. If No is selected, nothing is cleared from either ListBox.

            if (result == MessageBoxResult.Yes)
            {
                

                //Restarts Countdown from 30 seconds
                RestartTimer();

                InitializeGame();

                // Enables/Disables the Restart and Start buttons
                RestartButton.IsEnabled = true;
                StartButton.IsEnabled = true;

                // Resets the score to 0 and updates the score label
                score = 0;
                ScoreLabel.Content = "Score: " + score;
            }
        }

        private void HowToPlayButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Click on the call number generated and select its matching description to score points. " +
                "\n\nKeep track of your time as you only have 30 seconds to complete the game!", "Start Game",
            MessageBoxButton.OK);
        }
    }
}

