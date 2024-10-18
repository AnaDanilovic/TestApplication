using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using TestApplication.Processors;

namespace TestApplication
{
    public partial class TextManagerWindow : Window
    {
        private TextBox textBox;
        private Button loadButton;
        private Button saveButton;
        private IProcessFile _processFile;
        private bool resultsAvailable = false;

        public TextManagerWindow(IProcessFile processFile)
        {
            _processFile = processFile;
            Title = "Test Application";
            Width = 600;
            Height = 300;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            StackPanel stackPanel = new StackPanel();
            stackPanel.VerticalAlignment = VerticalAlignment.Center;
            Content = stackPanel;

            textBox = new TextBox
            {
                Margin = new Thickness(10),
                Height = 150,
                AcceptsReturn = true,
                TextWrapping = TextWrapping.Wrap
            };
            stackPanel.Children.Add(textBox);

            StackPanel buttonPanel = new StackPanel();
            buttonPanel.Orientation = Orientation.Horizontal;
            buttonPanel.HorizontalAlignment = HorizontalAlignment.Right;

            loadButton = new Button
            {
                Content = "Load",
                Margin = new Thickness(10),
                Width = 80
            };
            loadButton.Click += LoadButton_Click;
            buttonPanel.Children.Add(loadButton);

            saveButton = new Button
            {
                Content = "Save",
                Margin = new Thickness(10),
                Width = 80,
                IsEnabled = false
            };
            saveButton.Click += SaveButton_Click;
            buttonPanel.Children.Add(saveButton);

            stackPanel.Children.Add(buttonPanel);
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Json files (*.json)|*.json",
                Title = "Open Json File"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    string fileContent = File.ReadAllText(filePath);
                    string proccessedFileContent =_processFile.ParseFile(fileContent);
                    textBox.Text = proccessedFileContent;
                    resultsAvailable = true;
                    saveButton.IsEnabled = true;
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("The selected file was not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"An error occurred while reading the file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!resultsAvailable)
                return; 

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text files (*.csv)|*.csv",
                Title = "Save Results"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    string csvContent = textBox.Text;

                    File.WriteAllText(saveFileDialog.FileName, csvContent);
                    MessageBox.Show("Results saved successfully.", "Save Complete", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
