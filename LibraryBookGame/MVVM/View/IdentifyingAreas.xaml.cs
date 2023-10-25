using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LibraryBookGame.MVVM.View
{
    public partial class IdentifyingAreas : UserControl
    {
        private string selectedWord;
        private string selectedDefinition;

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
            InitializeGame();
        }

        private void InitializeGame()
        {
            callNumbers = GetRandomCallNumbers(4); //Gets 4 random call numbers that are stored in the wordDefinitionPairs dictionary
            matchingDefinitions = GetMatchingDefinitions(callNumbers); //Get 4 matching definitions that are stored in the wordDefinitionPairs dictionary
            allDefinitions = GetRandomDefinitions(7); //Gets 7 additional random definitions that are stored in the wordDefinitionPairs dictionary
            Shuffle(allDefinitions); //Shuffles all definitions so that they are not displayed next to their corresponding call number

            //Populate ListViews
            wordListView.ItemsSource = callNumbers;
            definitionListView.ItemsSource = allDefinitions;

            //Handle item selection
            wordListView.SelectionChanged += (sender, e) =>
            {
                if (wordListView.SelectedItem != null)
                {
                    selectedWord = wordListView.SelectedItem.ToString();
                    CheckMatch();
                }
            };

            definitionListView.SelectionChanged += (sender, e) =>
            {
                if (definitionListView.SelectedItem != null)
                {
                    selectedDefinition = definitionListView.SelectedItem.ToString();
                    CheckMatch();
                }
            };
        }

        private void RemoveMatchedPair()
        {
            //Removes matched pair from the data sources
            callNumbers.Remove(selectedWord);
            matchingDefinitions.Remove(selectedDefinition);
            allDefinitions.Remove(selectedDefinition);
        }

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
                //Check if the selected definition matches the word
                if (wordDefinitionPairs.ContainsKey(selectedWord) && wordDefinitionPairs[selectedWord] == selectedDefinition)
                {
                    MessageBox.Show("You got it correct!");
                    RemoveMatchedPair();
                }
                else
                {
                    MessageBox.Show("Sorry, that's not correct.");
                }

                //Reset selections
                selectedWord = null;
                selectedDefinition = null;
                wordListView.SelectedItem = null;
                definitionListView.SelectedItem = null;

                //Check if the game is over (no more matching pairs)
                if (callNumbers.Count == 0)
                {
                    MessageBox.Show("Congratulations! You've completed the game.");
                    //You can perform any other game completion actions here.
                }
            }
        }

    }
}

