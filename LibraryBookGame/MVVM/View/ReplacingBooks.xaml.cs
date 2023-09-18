using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryBookGame.MVVM.View
{
    
    public partial class ReplacingBooks : UserControl
    {

        private List<string> callNumbers = new List<string>();
        private int score = 0;
        private ListBoxItem draggedItem = null;
        private int nextDropIndex = 0; // Keeps track of the next position to drop a call number


        public ReplacingBooks()
        {
            InitializeComponent();
        }

        private void GenerateCallNumbers()
        {
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                // Generate a random call number (e.g., "005.73 ABC")
                string callNumber = $"{random.Next(1000, 10000) / 1000.0:F2} {GenerateRandomSurname()}";
                callNumbers.Add(callNumber);
            }
        }

        private string GenerateRandomSurname()
        {
            Random random = new Random();
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(characters, 3).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void DisplayCallNumbers()
        {
            CallNumbersListBox.Items.Clear();
            foreach (var callNumber in callNumbers)
            {
                CallNumbersListBox.Items.Add(callNumber);
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            

            // Generate 10 random call numbers
            GenerateCallNumbers();

            // Display the call numbers to the user
            DisplayCallNumbers();

            // Enable the Sort and Restart buttons
            SortButton.IsEnabled = true;
            RestartButton.IsEnabled = true;

            // Enable manual sorting in the ListBox
            CallNumbersListBox.AllowDrop = true;
            CallNumbersListBox.PreviewMouseLeftButtonDown += UserSortingListBox_PreviewMouseLeftButtonDown;
            CallNumbersListBox.Drop += CallNumbersListBox_Drop;

            // Reset the nextDropIndex to start from the beginning
            nextDropIndex = 0; // Reset to 0 for a new sorting operation

        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {

            // Clear call numbers
            callNumbers.Clear();

            // Clear the ListBox
            CallNumbersListBox.Items.Clear();

            // Disable the Sort and Restart buttons
            SortButton.IsEnabled = false;
            RestartButton.IsEnabled = false;

            // Clear and disable the score label
            ScoreLabel.Content = "";

        }

        private void CallNumbersListBox_DragOver(object sender, DragEventArgs e)
        {

        }

        private void CallNumbersListBox_Drop(object sender, DragEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                string selectedItem = (string)e.Data.GetData(typeof(string));

                // Ensure that listBox.SelectedItem is not null and is a valid index
                if (listBox.SelectedItem != null)
                {
                    int targetIndex = listBox.Items.IndexOf(listBox.SelectedItem);

                    if (targetIndex >= 0)
                    {
                        // Insert the item at the new position
                        listBox.Items.Insert(targetIndex, selectedItem);

                        // Remove the item from its original position
                        listBox.Items.Remove(selectedItem);

                        // Update the callNumbers list to reflect the new order
                        callNumbers.Insert(targetIndex, selectedItem);
                        callNumbers.Remove(selectedItem);
                    }
                }
            }
        }

        private void ListBoxItem_PreviewMouseMove(object sender, MouseEventArgs e)
        {

        }

        private void UserSortingListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void UserSortingListBox_Drop(object sender, DragEventArgs e)
        {

        }
    }
}
