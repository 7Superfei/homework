using System;
namespace myApp4
{
    class Program
    {
        static bool IsTopri(int m,int n, int[,] array)
        {
            int s = m > n ? n : m;
            int standard = array[0, 0];
            for (int i = 0; i < s; i++)
            {
                if (standard != array[i, i])
                {
                    return false;
                }
            }
            return true;
        }
        static void Main(string[] args)
        {
            int m, n;
            Console.WriteLine("请输入行数");
            m = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("请输入列数");
            n = Convert.ToInt32(Console.ReadLine());
            int[,] array = new int[m, n];
            Console.WriteLine("请输入矩阵的数据，" +
                "每输入一行按回车，每个数字用空格隔开");
            for(int i = 0; i < m; i++)
            {
                string Line;
                Line = Console.ReadLine();
                string[] stringArray = Line.Split(' ');
                for(int j = 0; j < n; j++)
                {
                    array[i,j] = Convert.ToInt32(stringArray[j]);
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