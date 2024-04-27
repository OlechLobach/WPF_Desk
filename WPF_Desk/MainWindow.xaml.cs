using System;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace FileReversalApp
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

        private void OnReverseFileClick(object sender, RoutedEventArgs e)
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

            string fileContent = File.ReadAllText(filePath);

            string reversedContent = new string(fileContent.Reverse().ToArray());

            string newFilePath = Path.Combine(Path.GetDirectoryName(filePath), "reversed_" + Path.GetFileName(filePath));
            File.WriteAllText(newFilePath, reversedContent);

            ResultTextBlock.Text = $"Файл перевернуто та збережено як {newFilePath}.";
        }
    }
}