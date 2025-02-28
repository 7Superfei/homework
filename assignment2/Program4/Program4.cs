using System;
namespace assignment2
{
    class Program4
    {
        static bool IsTopri(int m, int n, int[,] array)
        {
            int low = m > n ? n : m;
            for (int i = 0; i < m; i++)
            {
                int e = 0;
                int standard = array[i, e];
                for (int j = i; j < low; j++)
                {
                    if (array[j, e] != standard)
                    {
                        return false;
                    }
                    e++;
                }
            }
            for (int i = 0; i < n; i++)
            {
                int e = 0;
                int standard = array[e, i];
                for (int j = i; j < low; j++)
                {
                    if (array[e, j] != standard)
                    {
                        return false;
                    }
                    e++;
                }
            }
            return true;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("请输入行数");
            int m = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("请输入列数");
            int n = Convert.ToInt32(Console.ReadLine());
            int[,] array = new int[m, n];
            Console.WriteLine("请输入矩阵的数据，" +
                "每输入一行按回车，每个数字用空格隔开");
            for (int i = 0; i < m; i++)
            {
                string Line;
                Line = Console.ReadLine();
                string[] stringArray = Line.Split(' ');
                for (int j = 0; j < n; j++)
                {
                    array[i, j] = Convert.ToInt32(stringArray[j]);
                }
            }
            if (IsTopri(m, n, array))
            {
                Console.WriteLine("true");
            }
            else
            {
                Console.WriteLine("false");
            }
        }
    }
}