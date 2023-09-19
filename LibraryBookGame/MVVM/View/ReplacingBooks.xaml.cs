using System;
using System.Collections.Generic;
using System.Globalization;
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
        private List<string> draggedItems = null;
        private int score = 0;
        private ListBoxItem draggedItem = null;
        private int nextDropIndex = 0; // Keeps track of the next position to drop a call number
        private Random random = new Random();

        public ReplacingBooks()
        {
            InitializeComponent();
        }

        private void GenerateCallNumbers()
        {
            CultureInfo cultureInfo = new CultureInfo("en-US"); //So that the call numbers are separated by a period and not by a comma (Chat-GPT)

            for (int i = 0; i < 10; i++)
            {
                // Generate a random number between 0.00 and 99.99
                double randomNumber = random.Next(1000) / 100.0;

                // Format the number with two leading zeros, two digits behind the period
                string formattedNumber = randomNumber.ToString("0.00", cultureInfo);

                // Combine the formatted number and the generated surname
                string callNumber = $"00{formattedNumber} {GenerateRandomSurname()}";
                callNumbers.Add(callNumber);
            }
        }

        private string GenerateRandomSurname()
        {
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

        //Method to check if the call numbers sorted by the user are correctly placed in ascending order

        private bool IsSortedInAscendingOrder(ListBox listBox)
        {
            List<string> sortedList = new List<string>();
            foreach (var item in listBox.Items)
            {
                sortedList.Add(item.ToString());
            }

            // Logic to check if sorting is in ascending order
            for (int i = 0; i < sortedList.Count - 1; i++)
            {
                if (string.Compare(sortedList[i], sortedList[i + 1], StringComparison.Ordinal) > 0)
                {
                    return false;
                }
            }
            return true;
        }


        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            
            // Method call to generate 10 random call numbers
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
            // Check if the call numbers are sorted in ascending order
            bool isSorted = IsSorted(callNumbers);

            // Update the score based on sorting correctness
            score = isSorted ? 100 : 0;
            ScoreLabel.Content = $"Score: {score}%";
        }

        private bool IsSorted(List<string> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (string.Compare(list[i], list[i + 1], StringComparison.Ordinal) > 0)
                {
                    return false;
                }
            }
            return true;
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

        private void ListBoxItem_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ListBoxItem listBoxItem = sender as ListBoxItem;

                if (listBoxItem != null && listBoxItem.DataContext != null)
                {
                    draggedItem = listBoxItem;
                    DragDrop.DoDragDrop(listBoxItem, listBoxItem.DataContext, DragDropEffects.Move);
                }
            }
        }

        private void CallNumbersListBox_DragOver(object sender, DragEventArgs e)
        {
            if (draggedItem != null)
            {
                e.Effects = DragDropEffects.Move;
            }
            e.Handled = true;
        }

        private void CallNumbersListBox_Drop(object sender, DragEventArgs e)
        {
            if (draggedItem != null)
            {
                int targetIndex = CallNumbersListBox.Items.IndexOf(CallNumbersListBox.SelectedItem);

                if (targetIndex >= 0)
                {
                    int selectedIndex = CallNumbersListBox.Items.IndexOf(draggedItem.DataContext);

                    if (selectedIndex >= 0)
                    {
                        callNumbers.RemoveAt(selectedIndex);
                        callNumbers.Insert(targetIndex, (string)e.Data.GetData(typeof(string)));
                        DisplayCallNumbers();
                    }
                }

                draggedItem = null;
            }
        }

        //Here new text Box

        private void UserSortingListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem listBoxItem)
            {
                DragDrop.DoDragDrop(listBoxItem, listBoxItem.DataContext, DragDropEffects.Move);
            }
        }

        private void UserSortingListBox_Drop(object sender, DragEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                string selectedItem = (string)e.Data.GetData(typeof(string));

                // Ensure that listBox.SelectedIndex is within a valid range
                if (listBox.SelectedIndex < 0 || listBox.SelectedIndex >= listBox.Items.Count)
                {
                    // Insert the item at the end of the list
                    listBox.Items.Add(selectedItem);
                }
                else
                {
                    // Insert the item at the selected index
                    listBox.Items.Insert(listBox.SelectedIndex, selectedItem);
                }
            }
        }
        
    }
}
