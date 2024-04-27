using System;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace NumberAnalysisApp
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
                Title = "Виберіть файл з числами"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                FilePathTextBox.Text = openFileDialog.FileName;
            }
        }

        private void OnAnalyzeFileClick(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathTextBox.Text;

            if (string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("Будь ласка, введіть або виберіть шлях до файлу.");
                return;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show($"Файл за шляхом {filePath} не знайдено.");
                return;
            }

            try
            {
                string[] numberStrings = File.ReadAllLines(filePath);
                int[] numbers = numberStrings.Select(int.Parse).ToArray();

                int positiveCount = numbers.Count(n => n > 0);
                int negativeCount = numbers.Count(n => n < 0);
                int twoDigitCount = numbers.Count(n => Math.Abs(n) >= 10 && Math.Abs(n) < 100);
                int fiveDigitCount = numbers.Count(n => Math.Abs(n) >= 10000 && Math.Abs(n) < 100000);

                PositiveCountTextBlock.Text = $"Додатні числа: {positiveCount}";
                NegativeCountTextBlock.Text = $"Від'ємні числа: {negativeCount}";
                TwoDigitCountTextBlock.Text = $"Двозначні числа: {twoDigitCount}";
                FiveDigitCountTextBlock.Text = $"П'ятизначні числа: {fiveDigitCount}";

                string directory = Path.GetDirectoryName(filePath);
                File.WriteAllLines(Path.Combine(directory, "positive_numbers.txt"), numbers.Where(n => n > 0).Select(n => n.ToString()));
                File.WriteAllLines(Path.Combine(directory, "negative_numbers.txt"), numbers.Where(n => n < 0).Select(n => n.ToString()));
                File.WriteAllLines(Path.Combine(directory, "two_digit_numbers.txt"), numbers.Where(n => Math.Abs(n) >= 10 && Math.Abs(n) < 100).Select(n => n.ToString()));
                File.WriteAllLines(Path.Combine(directory, "five_digit_numbers.txt"), numbers.Where(n => Math.Abs(n) >= 10000 && Math.Abs(n) < 100000).Select(n => n.ToString()));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка під час обробки файлу: {ex.Message}");
            }
        }
    }
}