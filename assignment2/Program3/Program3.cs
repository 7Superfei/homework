using System;
using System.Collections.Generic;
namespace assignment2
{
    class Program3
    {
        static int[] Es(int n, int[] array)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < n; i++)
            {
                if (array[i] == 0) continue;
                list.Add(array[i]);
                for (int j = i + 1; j < array.Length; j++)
                {
                    if ((array[j] != 0) && (array[j] % array[i] == 0))
                    {
                        array[j] = 0;
                    }
                }
            }
            return list.ToArray();
        }
        static void Main(string[] args)
        {
            int[] array = new int[99];
            Console.WriteLine("2~100的素数为：");
            for (int i = 0; i < 99; i++)
            {
                array[i] = i + 2;
            }
            foreach (var item in Es(99, array))
            {
                Console.WriteLine(item);
            }
        }
    }
}