using System;
using System.Collections.Generic;

namespace assignment2
{
    class Program1
    {
        static int[] PrimeFactor(int n)
        {
            int i = 2;
            List<int> list = new List<int>();
            while (i * i <= n)
            {
                if (n % i == 0)
                {
                    n = n / i;
                    list.Add(i);
                    continue;
                }
                if (i == 2) i++;
                else i += 2;
            }
            if (n > 2) list.Add(n);
            return list.ToArray();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("请输入一个正整数");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("该正整数的素因子为");
            foreach (int i in PrimeFactor(n)) { Console.WriteLine(i); }
        }
    }
}