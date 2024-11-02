using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat_forrat
{
    internal class Cat
    {
        private string name; //скрытое поле
        private double ves;

        public string Name // свойство
        {
            // получение значения - просто возврат name
            get
            {
                return name;
            }
            // установка значения - используем проверку
            set
            {
                bool OnlyLetters = true;
                // ключ. слово value - это то, что хотят свойству присвоить
                foreach (var ch in value)
                {
                    if (!char.IsLetter(ch))
                    {
                        OnlyLetters = false;
                    }
                }

                if (OnlyLetters)
                {
                    name = value;
                }
                else
                {
                    Console.WriteLine($"{value} - неправильное имя!!!");
                }
            }
        }

        public double Ves {
            get {
                return ves;          
            }
            set {

                if (value > 0) //проверяем утсановленное значение
                {
                    ves = value; //устанвливаем вес если проходит проверку
                    Meow();
                }
                else {
                    Console.WriteLine($" неправильный вес!!!"); // сообщение об ошибке

                }              
           }
        
        }
            
        public void Meow()// вывод на экарн информации о животном
        {
            Console.WriteLine($"{name}: МЯЯЯЯУ!!!! ------ вес данного животного {ves} кг");
        }
    }
}
