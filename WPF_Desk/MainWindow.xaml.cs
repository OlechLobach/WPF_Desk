using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace ModeratorApp
{
    public partial class MainWindow : Window
    {
        private string textFilePath; 
        private string moderationFilePath; 

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnSelectTextFileClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files|*.txt|All Files|*.*",
                Title = "Виберіть файл з текстом"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                textFilePath = openFileDialog.FileName;
                TextFilePathTextBox.Text = textFilePath;
            }
        }

        private void OnSelectModerationFileClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files|*.txt|All Files|*.*",
                Title = "Виберіть файл зі словами для модерації"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                moderationFilePath = openFileDialog.FileName;
                ModerationFilePathTextBox.Text = moderationFilePath;
            }
        }

        private void OnModerateButtonClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textFilePath) || string.IsNullOrWhiteSpace(moderationFilePath))
            {
                MessageBox.Show("Будь ласка, виберіть обидва файли.");
                return;
            }

            if (!File.Exists(textFilePath) || !File.Exists(moderationFilePath))
            {
                MessageBox.Show("Один або обидва файли не знайдені.");
                return;
            }

            string textFileContent = File.ReadAllText(textFilePath);

            string moderationWordsContent = File.ReadAllText(moderationFilePath);
            string[] moderationWords = moderationWordsContent.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in moderationWords)
            {
                string regexPattern = $"\\b{Regex.Escape(word)}\\b";
                textFileContent = Regex.Replace(textFileContent, regexPattern, new string('*', word.Length));
            }

            File.WriteAllText(textFilePath, textFileContent);

            ResultTextBlock.Text = "Модерація завершена.";
        }
    }
}