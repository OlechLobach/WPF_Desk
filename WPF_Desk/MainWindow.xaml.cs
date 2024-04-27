using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.Win32; 

namespace WordReplacementApp
{
    public partial class MainWindow : Window
    {
        private string filePath; 

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnSelectFileClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files|*.txt|All Files|*.*",
                Title = "Виберіть файл"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                FilePathTextBox.Text = filePath; 
            }
        }

        private void OnReplaceButtonClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("Будь ласка, виберіть файл.");
                return;
            }

            string searchWord = SearchTextBox.Text;  
            string replaceWord = ReplaceTextBox.Text; 

            if (string.IsNullOrWhiteSpace(searchWord) || string.IsNullOrWhiteSpace(replaceWord))
            {
                MessageBox.Show("Будь ласка, введіть слова для пошуку та заміни.");
                return;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show($"Файл {filePath} не знайдено.");
                return;
            }

            string fileContent = File.ReadAllText(filePath);

            int replacementCount = 0;
            string updatedContent = Regex.Replace(
                fileContent,
                Regex.Escape(searchWord),
                match =>
                {
                    replacementCount++;
                    return replaceWord;
                });

            File.WriteAllText(filePath, updatedContent);

            ResultTextBlock.Text = $"Кількість замін: {replacementCount}";
        }
    }
}