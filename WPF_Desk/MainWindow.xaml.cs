using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace FileViewerApp
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

        private void OnShowFileContentClick(object sender, RoutedEventArgs e)
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
                string fileContent = File.ReadAllText(filePath);
                FileContentTextBox.Text = fileContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка під час читання файлу: {ex.Message}");
            }
        }
    }
}