using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace NumberGenerator
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            GenerateNumbersCommand = new RelayCommand(GenerateNumbers_Click);
        }

        public ICommand GenerateNumbersCommand { get; }

        private string _statistics;
        public string Statistics
        {
            get { return _statistics; }
            set
            {
                _statistics = value;
                OnPropertyChanged(nameof(Statistics));
            }
        }

        private string _result;
        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        private void GenerateNumbers_Click()
        {
            // Генеруємо 100 цілих чисел
            List<int> numbers = GenerateNumbers(100);

            // Зберігаємо прості числа у файл
            List<int> primeNumbers = GetPrimeNumbers(numbers);
            SaveNumbersToFile("prime_numbers.txt", primeNumbers);

            // Зберігаємо числа Фібоначчі у файл
            List<int> fibonacciNumbers = GetFibonacciNumbers(numbers);
            SaveNumbersToFile("fibonacci_numbers.txt", fibonacciNumbers);

            // Формуємо статистику
            Statistics = $"Згенеровано чисел: {numbers.Count}\n" +
                         $"Кількість простих чисел: {primeNumbers.Count}\n" +
                         $"Кількість чисел Фібоначчі: {fibonacciNumbers.Count}";

            // Формуємо рядок результату
            Result = string.Join(Environment.NewLine, numbers.Select(n => n.ToString()));
        }

        private List<int> GenerateNumbers(int count)
        {
            Random random = new Random();
            List<int> numbers = new List<int>();
            for (int i = 0; i < count; i++)
            {
                numbers.Add(random.Next(1, 1000)); // Згенеруємо числа від 1 до 1000
            }
            return numbers;
        }

        private List<int> GetPrimeNumbers(List<int> numbers)
        {
            List<int> primes = new List<int>();
            foreach (int number in numbers)
            {
                if (IsPrime(number))
                {
                    primes.Add(number);
                }
            }
            return primes;
        }

        private bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;
            int sqrt = (int)Math.Sqrt(number);
            for (int i = 3; i <= sqrt; i += 2)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private List<int> GetFibonacciNumbers(List<int> numbers)
        {
            List<int> fibonacci = new List<int>();
            foreach (int number in numbers)
            {
                if (IsFibonacci(number))
                {
                    fibonacci.Add(number);
                }
            }
            return fibonacci;
        }

        private bool IsFibonacci(int number)
        {
            double sqrt5 = Math.Sqrt(5);
            double phi = (1 + sqrt5) / 2;
            int n = (int)Math.Floor(Math.Log(number * sqrt5) / Math.Log(phi) + 0.5);
            int fib = (int)(Math.Pow(phi, n) / sqrt5 + 0.5);
            return fib == number;
        }

        private void SaveNumbersToFile(string fileName, List<int> numbers)
        {
            File.WriteAllLines(fileName, numbers.Select(n => n.ToString()));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _execute();
    }
}