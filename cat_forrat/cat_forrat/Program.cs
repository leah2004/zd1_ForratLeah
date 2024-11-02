using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat_forrat
{
    internal class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Cat catt = new Cat();
                Console.WriteLine("введите имя коту");
                string name = Console.ReadLine();
                catt.Name = name;
                Console.WriteLine("введите вес коту");
                double ves = Convert.ToDouble(Console.ReadLine());
               
                    catt.Ves = ves;
                
               
            }
            catch(FormatException){ Console.WriteLine("неправильно введены данные"); }

            Console.ReadKey();

        }
    }
}
