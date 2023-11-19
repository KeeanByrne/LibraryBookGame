/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LibraryBookGame.MVVM.View
{
    public partial class FindingCallNumbers : UserControl
    {
        // Create a class to represent a node in the tree

        private List<string> wordList = new List<string>();
        public class TreeNode
        {
            public string Name { get; set; }
            public List<TreeNode> Children { get; set; } = new List<TreeNode>();
        }

        private List<TreeNode> treeNodes = new List<TreeNode>();
        private List<string> flattenedList = new List<string>();

        public FindingCallNumbers()
        {
            InitializeComponent();

            // Specify the path to your text file
            string filePath = @"D:\Repository\LibraryBookGame\LibraryBookGame\Text\DataCallNumbers.txt";

            // Call the method to read and populate the TreeView
            ReadDataAndPopulateTree(filePath);

            // Flatten the tree structure for display in the ListView
            FlattenTreeForListView(treeNodes);

            
        }

        private void ReadDataAndPopulateTree(string filePath)
        {
            try
            {
                // Read all lines from the file
                var lines = File.ReadAllLines(filePath);

                // Assuming each line in the file represents a node in the tree
                foreach (var line in lines)
                {
                    // Split the line based on some delimiter (e.g., comma) if needed
                    string[] parts = line.Split(',');

                    // Create a tree node
                    TreeNode node = new TreeNode
                    {
                        Name = parts[0], // Assuming the first part is the node name
                        // Add additional properties if needed
                    };

                    // Add the node to the list
                    treeNodes.Add(node);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"Error reading file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FlattenTreeForListView(List<TreeNode> nodes)
        {
            foreach (var node in nodes)
            {
                // Add the current node to the flattened list
                flattenedList.Add(node.Name);

                // Recursively add child nodes
                FlattenTreeForListView(node.Children);
            }
        }

        *//*private void DisplayFilteredEntries()
        {
            // Clear existing items in ListViews
            wordListView.Items.Clear();
            definitionListView.Items.Clear();

            // Filter and populate the ListViews
            foreach (var entry in flattenedList)
            {
                if (entry.StartsWith("(3)"))
                {
                    // Entry starts with "(3)" - add to wordListView
                    wordListView.Items.Add(entry);
                }
                else
                {
                    // Entry doesn't start with "(3)" - add to definitionListView
                    definitionListView.Items.Add(entry);
                }
            }
        }*//*

        private void DisplayFilteredEntries()
        {
            // Clear existing items in ListViews
            wordListView.Items.Clear();
            definitionListView.Items.Clear();

            // Filter and populate the ListViews
            foreach (var entry in flattenedList)
            {
                if (entry.StartsWith("(3)"))
                {
                    // Entry starts with "(3)" - remove numeric prefix and add to wordListView
                    wordListView.Items.Add(RemoveNumericPrefix(entry));
                }
                else
                {
                    // Entry doesn't start with "(3)" - add to definitionListView
                    definitionListView.Items.Add(entry);
                }
            }
        }

        private string RemoveNumericPrefix(string entry)
        {
            // Find the index of the first non-numeric character after the opening parenthesis
            int startIndex = entry.IndexOf('(') + 1;
            while (startIndex < entry.Length && char.IsDigit(entry[startIndex]))
            {
                startIndex++;
            }

            // Remove the numeric prefix and trim any leading whitespace
            return entry.Substring(startIndex).Trim();
        }



        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Call the method to display filtered entries in ListViews
            DisplayFilteredEntries();
            DisplayRandomWordEntry();
            
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HowToPlayButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DisplayRandomWordEntry()
        {
            // Clear existing items in wordList
            wordList.Clear();

            // Filter and populate wordList
            foreach (var entry in flattenedList)
            {
                if (entry.StartsWith("(3)"))
                {
                    // Entry starts with "(3)" - add to wordList
                    wordList.Add(entry);
                }
            }

            // Clear existing items in wordListView
            wordListView.Items.Clear();

            // Display a random entry from wordList if there are any
            if (wordList.Count > 0)
            {
                Random random = new Random();
                int randomIndex = random.Next(wordList.Count);

                wordListView.Items.Add(wordList[randomIndex]);
            }
        }

    }
}


*//*

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace LibraryBookGame.MVVM.View
{
    public partial class FindingCallNumbers : UserControl
    {
        // Create a class to represent a node in the tree

        private List<string> wordList = new List<string>();
        public class TreeNode
        {
            public string Name { get; set; }
            public List<TreeNode> Children { get; set; } = new List<TreeNode>();
        }

        private List<TreeNode> treeNodes = new List<TreeNode>();
        private List<string> flattenedList = new List<string>();

        public FindingCallNumbers()
        {
            InitializeComponent();

            // Specify the path to your text file
            string filePath = @"D:\Repository\LibraryBookGame\LibraryBookGame\Text\DataCallNumbers.txt";

            // Call the method to read and populate the TreeView
            ReadDataAndPopulateTree(filePath);

            // Flatten the tree structure for display in the ListView
            FlattenTreeForListView(treeNodes);
        }

        private void ReadDataAndPopulateTree(string filePath)
        {
            try
            {
                // Read all lines from the file
                var lines = File.ReadAllLines(filePath);

                // Assuming each line in the file represents a node in the tree
                foreach (var line in lines)
                {
                    // Split the line based on some delimiter (e.g., comma) if needed
                    string[] parts = line.Split(',');

                    // Create a tree node
                    TreeNode node = new TreeNode
                    {
                        Name = parts[0], // Assuming the first part is the node name
                        // Add additional properties if needed
                    };

                    // Add the node to the list
                    treeNodes.Add(node);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"Error reading file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FlattenTreeForListView(List<TreeNode> nodes)
        {
            foreach (var node in nodes)
            {
                // Add the current node to the flattened list
                flattenedList.Add(node.Name);

                // Recursively add child nodes
                FlattenTreeForListView(node.Children);
            }
        }

        private void DisplayFilteredEntries()
        {
            // Clear existing items in ListViews
            wordListView.Items.Clear();
            definitionListView.Items.Clear();

            // Filter and populate the ListViews
            foreach (var entry in flattenedList)
            {
                if (entry.StartsWith("(3)"))
                {
                    // Entry starts with "(3)" - remove numeric prefix and add to wordListView
                    wordListView.Items.Add(RemoveNumericPrefix(entry));
                }
                else
                {
                    // Entry doesn't start with "(3)" - add to definitionListView
                    definitionListView.Items.Add(entry);
                }
            }
        }

        private string RemoveNumericPrefix(string entry)
        {
            // Find the index of the first non-numeric character after the opening parenthesis
            int startIndex = entry.IndexOf('(') + 1;
            while (startIndex < entry.Length && char.IsDigit(entry[startIndex]))
            {
                startIndex++;
            }

            // Remove the numeric prefix and trim any leading whitespace
            return entry.Substring(startIndex).Trim();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Call the method to display filtered entries in ListViews
            DisplayFilteredEntries();
            DisplayRandomWordEntry();
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            // Your code for restarting the game
        }

        private void HowToPlayButton_Click(object sender, RoutedEventArgs e)
        {
            // Your code for displaying game instructions
        }

        private void DisplayRandomWordEntry()
        {
            // Clear existing items in wordList
            wordList.Clear();

            // Filter and populate wordList
            foreach (var entry in flattenedList)
            {
                if (entry.StartsWith("(3)"))
                {
                    // Entry starts with "(3)" - add to wordList
                    wordList.Add(entry);
                }
            }

            // Clear existing items in wordListView
            wordListView.Items.Clear();

            // Display a random entry from wordList if there are any
            if (wordList.Count > 0)
            {
                Random random = new Random();
                int randomIndex = random.Next(wordList.Count);

                wordListView.Items.Add(wordList[randomIndex]);
            }
        }

        // Your other methods and event handlers go here
    }
}
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace LibraryBookGame.MVVM.View
{
    public partial class FindingCallNumbers : UserControl
    {
        // Create a class to represent a node in the tree

        private List<string> wordList = new List<string>();
        public class TreeNode
        {
            public string Name { get; set; }
            public List<TreeNode> Children { get; set; } = new List<TreeNode>();
        }

        private List<TreeNode> treeNodes = new List<TreeNode>();
        private List<string> flattenedList = new List<string>();

        public FindingCallNumbers()
        {
            InitializeComponent();

            // Specify the path to your text file
            string filePath = @"D:\Repository\LibraryBookGame\LibraryBookGame\Text\DataCallNumbers.txt";

            // Call the method to read and populate the TreeView
            ReadDataAndPopulateTree(filePath);

            // Flatten the tree structure for display in the ListView
            FlattenTreeForListView(treeNodes);
        }

        private void ReadDataAndPopulateTree(string filePath)
        {
            try
            {
                // Read all lines from the file
                var lines = File.ReadAllLines(filePath);

                // Assuming each line in the file represents a node in the tree
                foreach (var line in lines)
                {
                    // Split the line based on some delimiter (e.g., comma) if needed
                    string[] parts = line.Split(',');

                    // Create a tree node
                    TreeNode node = new TreeNode
                    {
                        Name = parts[0], // Assuming the first part is the node name
                        // Add additional properties if needed
                    };

                    // Add the node to the list
                    treeNodes.Add(node);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"Error reading file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FlattenTreeForListView(List<TreeNode> nodes)
        {
            foreach (var node in nodes)
            {
                // Add the current node to the flattened list
                flattenedList.Add(node.Name);

                // Recursively add child nodes
                FlattenTreeForListView(node.Children);
            }
        }

        /*private void DisplayFilteredEntries()
        {
            // Clear existing items in ListViews
            wordListView.Items.Clear();
            definitionListView.Items.Clear();

            // Filter and populate the ListViews
            foreach (var entry in flattenedList)
            {
                if (entry.StartsWith("(3)"))
                {
                    // Entry starts with "(3)" - remove numeric prefix and add to wordListView
                    wordListView.Items.Add(RemoveNumericPrefix(entry));
                }
                else
                {
                    // Entry doesn't start with "(3)" - add to definitionListView
                    definitionListView.Items.Add(entry);
                }
            }
        }*/

        private void DisplayFilteredEntries()
        {
            // Clear existing items in ListViews
            wordListView.Items.Clear();
            definitionListView.Items.Clear();

            // Filter and populate the ListViews
            foreach (var entry in flattenedList)
            {
                if (entry.StartsWith("(3)"))
                {
                    // Entry starts with "(3)" - remove numeric prefix and add to wordListView
                    wordListView.Items.Add(RemoveNumericPrefix(entry));
                }
                else
                {
                    // Entry doesn't start with "(3)" - remove numeric prefix and add to definitionListView
                    definitionListView.Items.Add(RemoveNumericPrefix(entry));
                }
            }
        }




        private string RemoveNumericPrefix(string entry)
        {
            // Use regular expression to extract numeric prefix and description
            var match = Regex.Match(entry, @"\((\d+)\)\s*(.+)");

            if (match.Success)
            {
                // Extract description and format it
                string description = match.Groups[2].Value.Trim();

                return $"{description}";
            }

            // Return the entry as is if no match is found
            return entry.Trim();
        }




        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Call the method to display filtered entries in ListViews
            DisplayFilteredEntries();
            DisplayRandomWordEntry();
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            // Your code for restarting the game
        }

        private void HowToPlayButton_Click(object sender, RoutedEventArgs e)
        {
            // Your code for displaying game instructions
        }

        private void DisplayRandomWordEntry()
        {
            // Clear existing items in wordList
            wordList.Clear();

            // Filter and populate wordList
            foreach (var entry in flattenedList)
            {
                if (entry.StartsWith("(3)"))
                {
                    // Entry starts with "(3)" - add to wordList
                    wordList.Add(RemoveNumericPrefix(entry));
                }
            }

            // Clear existing items in wordListView
            wordListView.Items.Clear();

            // Display a random entry from wordList if there are any
            if (wordList.Count > 0)
            {
                Random random = new Random();
                int randomIndex = random.Next(wordList.Count);

                wordListView.Items.Add(wordList[randomIndex]);
            }
        }

        // Your other methods and event handlers go here
    }
}
