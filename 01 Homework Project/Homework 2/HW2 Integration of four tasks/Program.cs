using System;
using System.Linq;

namespace HW2_Integration_of_four_tasks
{
    class Program
    {
        static int MAX = -100000;
        static int MIN = 100000;

        static void Main(string[] args)
        {
            Program program = new Program();

            // Task 1 - Factorization
            Console.WriteLine("Beginning of task 1: Input a number!");
            int numberForTask1 = int.Parse(Console.ReadLine());
            program.PerformPrimeFactorization(numberForTask1);
            program.PrintTask1Result(); // Task 1 output function

            // Task 2 - Array Operations
            Console.WriteLine("Beginning of task 2: Input the number of array elements.");
            int arrayLengthForTask2 = int.Parse(Console.ReadLine());
            double[] arrayForTask2 = new double[arrayLengthForTask2];
            for (int i = 0; i < arrayLengthForTask2; i++)
            {
                Console.Write($"Please input the {i + 1}th element: ");
                arrayForTask2[i] = double.Parse(Console.ReadLine());
            }
            program.PerformArrayOperations(arrayForTask2);
            program.PrintTask2Result(); // Task 2 output function

            // Task 3 - Prime Numbers
            Console.WriteLine("Beginning of task 3: I will output prime numbers from 2 to 100.");
            program.PrintPrimeNumbersUpTo100();
            program.PrintTask3Result(); // Task 3 output function

            // Task 4 - Matrix Check
            Console.WriteLine("Beginning of task 4: Input m, n that indicates m*n matrix (use space to separate!).");
            string matrixDimensionsInput = Console.ReadLine();
            string[] matrixDimensions = matrixDimensionsInput.Split(' ');
            int m = int.Parse(matrixDimensions[0]);
            int n = int.Parse(matrixDimensions[1]);

            Console.WriteLine("Type in the matrix! m rows, n columns, use space to separate columns, use enter to separate rows!");
            int[,] matrixForTask4 = new int[m, n];
            for (int i = 0; i < m; i++)
            {
                string rowInput = Console.ReadLine();
                string[] rowValues = rowInput.Split(' ');
                for (int j = 0; j < n; j++)
                {
                    matrixForTask4[i, j] = int.Parse(rowValues[j]);
                }
            }
            program.CheckMatrixForMagicSquare(matrixForTask4, m, n);
            program.PrintTask4Result(); // Task 4 output function
        }

        // Task 1: Prime Factorization
        void PerformPrimeFactorization(int number)
        {
            int count = 0;
            while (number % 2 == 0)
            {
                count++;
                number /= 2;
            }
            if (count > 0)
            {
                task1Result = "2";
            }

            for (int i = 3; i <= Math.Sqrt(number); i += 2)
            {
                int count2 = 0;
                while (number % i == 0)
                {
                    count2++;
                    number /= i;
                }
                if (count2 > 0)
                {
                    task1Result += $", {i}";
                }
            }

            if (number > 2)
            {
                task1Result += $", {number}";
            }
        }

        // Task 2: Array Operations (max, min, avg, sum)
        void PerformArrayOperations(double[] arr)
        {
            double avg = 0, max = MAX, min = MIN, sum = 0;
            foreach (var item in arr)
            {
                if (item > max) max = item;
                if (item < min) min = item;
                sum += item;
            }
            avg = sum / arr.Length;
            task2Result = $"Max = {max}, Min = {min}, Sum = {sum}, Avg = {avg}";
        }

        // Task 3: Print Prime Numbers from 2 to 100
        void PrintPrimeNumbersUpTo100()
        {
            int n = 100;
            bool[] isPrime = new bool[n + 1];
            for (int i = 2; i <= n; i++)
            {
                isPrime[i] = true;
            }

            for (int i = 2; i * i <= n; i++)
            {
                if (isPrime[i])
                {
                    for (int j = i * i; j <= n; j += i)
                    {
                        isPrime[j] = false;
                    }
                }
            }

            task3Result = "Prime numbers between 2 and 100: ";
            for (int i = 2; i <= n; i++)
            {
                if (isPrime[i])
                {
                    task3Result += i + " ";
                }
            }
        }

        // Task 4: Check if matrix is a magic square
        void CheckMatrixForMagicSquare(int[,] matrix, int m, int n)
        {
            if (m != n)
            {
                task4Result = "false!";
                return;
            }
            int diagonalValue = matrix[0, 0];
            for (int i = 1; i < m; i++)
            {
                if (matrix[i, i] != diagonalValue)
                {
                    task4Result = "false!";
                    return;
                }
            }
            task4Result = "true!";
        }

        // Task 1 Result Output
        string task1Result = "";
        void PrintTask1Result()
        {
            Console.WriteLine("Task 1 Output: " + task1Result);
        }

        // Task 2 Result Output
        string task2Result = "";
        void PrintTask2Result()
        {
            Console.WriteLine("Task 2 Output: " + task2Result);
        }

        // Task 3 Result Output
        string task3Result = "";
        void PrintTask3Result()
        {
            Console.WriteLine("Task 3 Output: " + task3Result);
        }

        // Task 4 Result Output
        string task4Result = "";
        void PrintTask4Result()
        {
            Console.WriteLine("Task 4 Output: " + task4Result);
        }
    }
}
