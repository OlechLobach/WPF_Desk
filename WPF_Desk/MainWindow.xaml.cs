using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace RandomNumberGenerator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnGenerateAndSaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Random random = new Random();
                int[] numbers = new int[10000];

                for (int i = 0; i < numbers.Length; i++)
                {
                    numbers[i] = random.Next(1, 10001); 
                }

                var evenNumbers = numbers.Where(n => n % 2 == 0).Select(n => n.ToString()).ToArray();
                var oddNumbers = numbers.Where(n => n % 2 != 0).Select(n => n.ToString()).ToArray();

                // Запис у файли
                string evenFilePath = "even_numbers.txt";
                string oddFilePath = "odd_numbers.txt";

                File.WriteAllLines(evenFilePath, evenNumbers);
                File.WriteAllLines(oddFilePath, oddNumbers);

                EvenCountTextBlock.Text = $"Парних чисел: {evenNumbers.Length}";
                OddCountTextBlock.Text = $"Непарних чисел: {oddNumbers.Length}";

                MessageBox.Show("Генерація та збереження завершено.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}");
            }
        }
    }
}