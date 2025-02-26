using System;
using System.Collections.Generic;
namespace myApp1
{
    class Program
    {
        static int[] PrimeFactor(int input)
        {
            List<int> ints = new List<int>();
            int i = 2;
            while (i * i <= input)
            {
                if (input % i != 0)
                {
                    i++;
                }
                else
                {
                    ints.Add(i);
                    input /= i;
                }
            }
            if (input > 1)
            {
                ints.Add(input);
            }
            return ints.ToArray();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("请输入一个数字：");
            int input = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("它的所有素因子为：");
            foreach(var item in PrimeFactor(input))
            {
                Console.WriteLine(item);
            }
        }
    }
}