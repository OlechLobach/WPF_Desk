using System;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace FileSearchApp
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

        private void OnFindWordClick(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathTextBox.Text;
            string searchWord = SearchWordTextBox.Text;

            if (string.IsNullOrWhiteSpace(filePath) || string.IsNullOrWhiteSpace(searchWord))
            {
                MessageBox.Show("Будь ласка, введіть шлях до файлу та слово для пошуку.");
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

                if (fileContent.Contains(searchWord))
                {
                    int position = fileContent.IndexOf(searchWord);
                    ResultTextBlock.Text = $"Слово '{searchWord}' знайдено в позиції {position}.";
                }
                else
                {
                    ResultTextBlock.Text = $"Слово '{searchWord}' не знайдено у файлі.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка під час пошуку: {ex.Message}");
            }
        }

        private void OnCountOccurrencesClick(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathTextBox.Text;
            string searchWord = SearchWordTextBox.Text;

            if (string.IsNullOrWhiteSpace(filePath) || string.IsNullOrWhiteSpace(searchWord))
            {
                MessageBox.Show("Будь ласка, введіть шлях до файлу та слово для підрахунку.");
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

                int occurrences = fileContent.Split(new[] { searchWord }, StringSplitOptions.None).Length - 1;

                ResultTextBlock.Text = $"Слово '{searchWord}' знайдено {occurrences} разів.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка під час підрахунку: {ex.Message}");
            }
        }

        private void OnReverseSearchClick(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathTextBox.Text;
            string searchWord = SearchWordTextBox.Text;

            if (string.IsNullOrWhiteSpace(filePath) || string.IsNullOrWhiteSpace(searchWord))
            {
                MessageBox.Show("Будь ласка, введіть шлях до файлу та слово для пошуку.");
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
                string reverseWord = new string(searchWord.Reverse().ToArray());

                int occurrences = fileContent.Split(new[] { reverseWord }, StringSplitOptions.None).Length - 1;

                ResultTextBlock.Text = $"Зворотне слово '{reverseWord}' знайдено {occurrences} разів.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка під час зворотного пошуку: {ex.Message}");
            }
        }
    }
}