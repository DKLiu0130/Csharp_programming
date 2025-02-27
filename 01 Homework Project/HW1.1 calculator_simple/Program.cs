using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1._1_calculator_simple
{
    class Program
    {
        static void Main(string[] args)
        {
            // 提示用户输入第一个数字
            Console.Write("请输入第一个数字: ");
            double num1 = Convert.ToDouble(Console.ReadLine());

            // 提示用户输入运算符
            Console.Write("请输入运算符 (+, -, *, /): ");
            string operation = Console.ReadLine();

            // 提示用户输入第二个数字
            Console.Write("请输入第二个数字: ");
            double num2 = Convert.ToDouble(Console.ReadLine());

            double result = 0;

            // 根据用户选择的运算符执行不同的操作
            switch (operation)
            {
                case "+":
                    result = num1 + num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;
                case "*":
                    result = num1 * num2;
                    break;
                case "/":
                    // 防止除数为零
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    else
                    {
                        Console.WriteLine("除数不能为零！");
                        return;
                    }
                    break;
                default:
                    Console.WriteLine("无效的运算符！");
                    return;
            }

            // 打印计算结果
            Console.WriteLine("结果: " + result);
        }
    }
}
