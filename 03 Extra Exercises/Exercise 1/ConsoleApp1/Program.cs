using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            call_function.Function1 x = new call_function.Function1();
            x.display();

            Program p = new Program();
            p.Func1();

            ConsoleApp2.Program.Func_out();
        }

        void Func1()
        {
            Console.WriteLine("Func1");
        }
    }
}
