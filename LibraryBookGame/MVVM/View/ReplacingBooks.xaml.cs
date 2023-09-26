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
using System.Windows.Threading;

namespace LibraryBookGame.MVVM.View

    //Add feature to tell User which callNumber is in the incorrect position.

    //Also make how to play button. 

    //Reset pop up asking if the user is sure they want to restart the game

    //Add timer feature for gamification


    //Call Numbers are disappearing in the callNumbers list Box. 

{
   
    public partial class ReplacingBooks : UserControl
    {
        //Variables for callnumber storage
        private List<string> callNumbers = new List<string>();
        /*private List<string> draggedItems = null;*/
        private ListBoxItem draggedItem = null;
        private HashSet<string> droppedCallNumbers = new HashSet<string>();

        //Variables for Game functionality
        private int score = 0;
        private Random random = new Random();

        //Variables for the timer
        private int remainingSeconds = 30;
        private DispatcherTimer timer;


        public ReplacingBooks()
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
                MessageBox.Show("Oh no! Your time has run out. Click the 'Done' Button to calculate your score!");

            }
        }

        private void RestartTimer()
        {
            //If Statement fixed issue where timer would automatically start after restart button is clicked // (Chat-GPT)

            if (timer.IsEnabled)
            {
                timer.Stop();
            }
            remainingSeconds = 30;
            timerLabel.Content = remainingSeconds.ToString();
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
                // Check if the call number has not been dropped
                if (!droppedCallNumbers.Contains(callNumber))
                {
                    CallNumbersListBox.Items.Add(callNumber);
                }
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
            //Starts Timer
            timer.Start();

            //Method call to generate 10 random call numbers
            GenerateCallNumbers();

            //Display the call numbers to the user in the CallNumbersListBox
            DisplayCallNumbers();

            //Enables the Sort and Restart buttons
            SortButton.IsEnabled = true;
            RestartButton.IsEnabled = true;

            //Enables manual sorting in the ListBox
            CallNumbersListBox.AllowDrop = true;
            CallNumbersListBox.PreviewMouseLeftButtonDown += UserSortingListBox_PreviewMouseLeftButtonDown;
            CallNumbersListBox.Drop += CallNumbersListBox_Drop;

        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            //Checks if the call numbers are sorted in ascending order
            bool isSorted = IsSorted(callNumbers);

            //Calculates the score based on sorting correctness
            int correctPlacements = CalculateCorrectPlacements(UserSortingListBox);
            score = isSorted ? (correctPlacements * 10) : 0;

            //Updates the score label
            ScoreLabel.Content = $"Score: {score}%";
        }

        private int CalculateCorrectPlacements(ListBox listBox)
        {
            int correctPlacements = 0;
            List<string> sortedList = new List<string>();
            foreach (var item in listBox.Items)
            {
                sortedList.Add(item.ToString());
            }

            // Loop to check if sorting is in ascending order and count correct placements
            for (int i = 0; i < sortedList.Count - 1; i++)
            {
                if (string.Compare(sortedList[i], sortedList[i + 1], StringComparison.Ordinal) <= 0)
                {
                    correctPlacements++;
                }
            }

            // Add 1 to count the first placement as correct
            correctPlacements++;

            return correctPlacements; // Multiply by 10 to get a score out of 100
        }


        private bool IsSorted(List<string> list)
        {
            List<string> sortedList = new List<string>();
            foreach (var item in UserSortingListBox.Items)
            {
                sortedList.Add(item.ToString());
            }

            //Logic to check if the call numbers have been placed in ascending order
            for (int i = 0; i < sortedList.Count - 1; i++)
            {
                if (string.Compare(sortedList[i], sortedList[i + 1], StringComparison.Ordinal) > 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to restart?", "Restart Game", MessageBoxButton.YesNo);

            //If Yes selected then both ListBoxs are cleared. If no is selected, nothing is cleared from either list box

            if(result == MessageBoxResult.Yes)
            {
                //Restarts Countdown from 30 seconds
                RestartTimer();

                //Clears call numbers
                callNumbers.Clear();

                //Clears the ListBox
                CallNumbersListBox.Items.Clear();

                //Clears the UserSortingListBox
                UserSortingListBox.Items.Clear();

                //Disables the Sort and Restart buttons
                SortButton.IsEnabled = false;
                RestartButton.IsEnabled = false;

                //Clears and disables the score label
                ScoreLabel.Content = "";

            }
           
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
                //Checks if the dragged item is in the UserSortingListBox
                if (UserSortingListBox.Items.Contains(draggedItem.DataContext))
                {
                    
                    draggedItem = null;
                    return;
                }

                int targetIndex = CallNumbersListBox.Items.IndexOf(CallNumbersListBox.SelectedItem);

                if (targetIndex >= 0)
                {
                    int selectedIndex = CallNumbersListBox.Items.IndexOf(draggedItem.DataContext);

                    if (selectedIndex >= 0)
                    {
                        string droppedCallNumber = (string)e.Data.GetData(typeof(string));

                        // Check if the call number has already been dropped
                        if (!droppedCallNumbers.Contains(droppedCallNumber))
                        {
                            callNumbers.RemoveAt(selectedIndex);
                            callNumbers.Insert(targetIndex, droppedCallNumber);
                            droppedCallNumbers.Add(droppedCallNumber);

                            // Updates the ListBox display
                            DisplayCallNumbers();

                            
                            CallNumbersListBox.SelectedItem = droppedCallNumber;
                        }
                    }
                }

                draggedItem = null;
            }
        }



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

        private void HowToPlayButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("10 Random Call Numbers will be generated for you to sort in ASCENDING ORDER. Drag and drop them from the box on the left to the box on the right", "Start Game",
            MessageBoxButton.OK);

        }
    }
}
