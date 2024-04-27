using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace ArraySaveApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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