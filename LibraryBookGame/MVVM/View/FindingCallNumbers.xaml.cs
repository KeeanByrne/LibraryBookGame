using System;
using System.Collections.Generic;
using System.IO; // Add this for File class
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LibraryBookGame.MVVM.View
{
    /// <summary>
    /// Interaction logic for FindingCallNumbers.xaml
    /// </summary>
    public partial class FindingCallNumbers : UserControl
    {
        // Assuming you have a class-level List<string> to store the definitions
        private List<string> definitions = new List<string>();

        public FindingCallNumbers()
        {
            InitializeComponent();

            // Specify the path to your text file
            string filePath = "path/to/your/textfile.txt";

            // Call the method to read and populate the ListView
            ReadDataFromFile(filePath);
        }

        private void ReadDataFromFile(string filePath)
        {
            try
            {
                // Read all lines from the file
                definitions = File.ReadAllLines(filePath).ToList();

                // Bind the definitions list to the ListView
                definitionListView.ItemsSource = definitions;
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., file not found, etc.)
                MessageBox.Show($"Error reading file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Add your StartButton_Click logic here
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            // Add your RestartButton_Click logic here
        }

        private void HowToPlayButton_Click(object sender, RoutedEventArgs e)
        {
            // Add your HowToPlayButton_Click logic here
        }
    }
}
