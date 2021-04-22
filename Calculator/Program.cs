using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите выражение. Для выхода введите \"exit\". "); //Предлагаем ввести выражение
                string expression = Console.ReadLine();
                if (expression.Equals("exit"))
                    break;
                Console.WriteLine(Calculator.Calualte(expression)); //Считываем, и выводим результат
            }
        }
    }
}
