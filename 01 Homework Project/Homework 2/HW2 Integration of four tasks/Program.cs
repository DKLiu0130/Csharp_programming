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

            // Task 3 - Prime Numbers
            Console.WriteLine("Beginning of task 3: I will output prime numbers from 2 to 100.");
            program.PrintPrimeNumbersUpTo100();

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
                Console.WriteLine("2");
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
                    Console.WriteLine(i);
                }
            }

            if (number > 2)
            {
                Console.WriteLine(number);
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
            Console.WriteLine($"Max = {max}, Min = {min}, Sum = {sum}, Avg = {avg}");
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

            Console.WriteLine("Prime numbers between 2 and 100:");
            for (int i = 2; i <= n; i++)
            {
                if (isPrime[i])
                {
                    Console.Write(i + " ");
                }
            }
            Console.WriteLine(); // To move to the next line after printing the primes
        }

        // Task 4: Check if matrix is a magic square
        void CheckMatrixForMagicSquare(int[,] matrix, int m, int n)
        {
            if (m != n)
            {
                Console.WriteLine("false!");
                return;
            }
            int diagonalValue = matrix[0, 0];
            for (int i = 1; i < m; i++)
            {
                if (matrix[i, i] != diagonalValue)
                {
                    Console.WriteLine("false!");
                    return;
                }
            }
            Console.WriteLine("true!");
        }
    }
}
