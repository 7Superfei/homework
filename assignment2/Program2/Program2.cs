using System;
namespace assignment2
{
    class Program2
    {
        static int Maxval(string[] stringArray)
        {
            int max = -10000;
            foreach (var item in stringArray)
            {
                if (int.Parse(item) > max)
                {
                    max = int.Parse(item);
                }
            }
            return max;
        }
        static int Minval(string[] stringArray)
        {
            int min = 10000;
            foreach (var item in stringArray)
            {
                if (int.Parse(item) < min)
                {
                    min = int.Parse(item);
                }
            }
            return min;
        }
        static int Sum(string[] stringArray)
        {
            int sum = 0;
            foreach (var item in stringArray)
            {
                sum += int.Parse(item);
            }
            return sum;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("请输入数组元素：");
            string Input = Console.ReadLine();
            string[] stringArray = Input.Split(' ');
            int n = stringArray.Length;
            Console.WriteLine($"该数组的最大值为{Maxval(stringArray)}" +
                $",最小值为{Minval(stringArray)}" +
                $",平均值为{Convert.ToDouble(Sum(stringArray)) / n}" +
                $",所有数组元素的和为{Sum(stringArray)}");
        }
    }
}