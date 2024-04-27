using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace ArrayOperationsApp
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
        private void OnLoadArrayFromFileClick(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathTextBox.Text;

            if (string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("Будь ласка, виберіть файл.");
                return;
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show($"Файл за шляхом {filePath} не знайдено.");
                return;
            }

            try
            {
                string[] arrayValues = File.ReadAllLines(filePath);

                ArrayInputTextBox.Text = string.Join(" ", arrayValues);

                ResultTextBlock.Text = "Масив успішно завантажено з файлу.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка під час завантаження з файлу: {ex.Message}");
            }
        }

        private void OnSaveArrayToFileClick(object sender, RoutedEventArgs e)
        {
            string input = ArrayInputTextBox.Text;

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Будь ласка, введіть значення елементів масиву.");
                return;
            }

            string[] arrayValues = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Text Files|*.txt|All Files|*.*",
                Title = "Зберегти масив у файл"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    File.WriteAllLines(filePath, arrayValues);

                    ResultTextBlock.Text = $"Масив збережено у файл: {filePath}.";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка під час збереження у файл: {ex.Message}");
                }
            }
        }
    }
}