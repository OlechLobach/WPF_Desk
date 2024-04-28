using System;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace FileStatisticsApp
{
    public partial class MainWindow : Window
    {
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
                FilePathTextBox.Text = openFileDialog.FileName;
            }
        }

        private void OnGetFileStatisticsClick(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathTextBox.Text;

            if (string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("Будь ласка, введіть шлях до файлу.");
                return;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show($"Файл за шляхом {filePath} не знайдено.");
                return;
            }

            try
            {
                string fileContent = File.ReadAllText(filePath);

                int sentenceCount = fileContent.Split(new[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;

                int uppercaseCount = fileContent.Count(char.IsUpper);

                int lowercaseCount = fileContent.Count(char.IsLower);

                char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'y', 'A', 'E', 'I', 'O', 'U', 'Y' };
                int vowelCount = fileContent.Count(c => vowels.Contains(c));

                int consonantCount = fileContent.Count(c => char.IsLetter(c) && !vowels.Contains(c));

                int digitCount = fileContent.Count(char.IsDigit);

                StatisticsTextBlock.Text = $"Статистика файлу:\n" +
                                           $"Речення: {sentenceCount}\n" +
                                           $"Великі літери: {uppercaseCount}\n" +
                                           $"Маленькі літери: {lowercaseCount}\n" +
                                           $"Голосні: {vowelCount}\n" +
                                           $"Приголосні: {consonantCount}\n" +
                                           $"Цифри: {digitCount}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка під час обробки файлу: {ex.Message}");
            }
        }
    }
}