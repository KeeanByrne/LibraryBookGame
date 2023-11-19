using System;
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
                    // Entry starts with "(3)" - add to wordListView
                    wordListView.Items.Add(entry);
                }
                else
                {
                    // Entry doesn't start with "(3)" - add to definitionListView
                    definitionListView.Items.Add(entry);
                }
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Call the method to display filtered entries in ListViews
            DisplayFilteredEntries();
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HowToPlayButton_Click(object sender, RoutedEventArgs e)
        {

        }

        


    }
}


