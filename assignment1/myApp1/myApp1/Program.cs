using System;
namespace myApp1
{
    class Program
    {
        static void Main(string[] args) 
        {
            double a,b;
            string c;
            Console.WriteLine("请输入第一个数字");
            a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("请输入第二个数字");
            b = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("请输入运算符");
            c = Console.ReadLine();
            switch (c) {
                case "+":
                    Console.WriteLine($"{a}{c}{b} = " + (a + b));
                    break;
                case "-":
                    Console.WriteLine($"{a}{c}{b} = " + (a - b));
                    break;
                case "*":
                    Console.WriteLine($"{a}{c}{b} = " + (a * b));
                    break;
                case "/":
                    if (b == 0)
                        Console.WriteLine("错误");
                    else
                    {
                        Console.WriteLine($"{a}{c}{b} = " + (a / b));
                    }
                    break;
                default:
                    Console.WriteLine("没有该运算符");
                    break;
            }
        }
    }
}

