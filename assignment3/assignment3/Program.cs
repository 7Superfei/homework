using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
namespace assignment3
{
    //抽象形状，接口类
    interface Shape
    {
        string Name { get; set; }
        double Area();
        bool IsLegal();
        void Print();
    }
    //具体形状，实现类
    class Rectangle : Shape//长方形
    {
        public string Name { get; set; }
        public double a { get; set; }//长
        public double b { get; set; }//宽
        public double Area()//计算面积
        {
            return a * b;
        }
        public bool IsLegal()//判断是否合法
        {
            return (a>0&&b>0);
        }
        public void Print() //输出信息
        {
            Console.WriteLine($"该图形为{Name}，边长为{a}，{b}，面积为{Area()}");
        }
    }
    class Square : Shape//正方形
    {
        public string Name { get; set; }
        public double a { get; set; }//边长
        public double Area()//计算面积
        {
            return a * a;
        }
        public bool IsLegal()//判断是否合法
        {
            return (a > 0);
        }
        public void Print()
        {
            Console.WriteLine($"该图形为{Name}，边长为{a}，面积为{Area()}");
        }
    }
    class Triangle : Shape//三角形
    {
        public string Name { get; set; }
        public double a { get; set; }//边1
        public double b { get; set; }//边2
        public double c { get; set; }//边3
        public double Area()//计算面积
        {
            double p = (a + b + c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }
        public bool IsLegal()//判断是否合法
        {
            return (a>0&&b>0&&c>0&&(a + b > c) && (a + c > b) && (b + c > a));
        }
        public void Print()
        {
            Console.WriteLine($"该图形为{Name}，边长为{a}，{b}，{c}，面积为{Area()}");
        }
    }
    //工厂类，创建形状
    class Factory
    {
        public static Shape CreateShape(double a,double b)
        {
            Rectangle  ShapeA = new Rectangle();
            ShapeA.a = a;
            ShapeA.b = b;
            ShapeA.Name = "长方形";
            return ShapeA;
        }
        public static Shape CreateShape(double a)
        {
            Square ShapeB = new Square();
            ShapeB.a = a;
            ShapeB.Name = "正方形";
            return ShapeB;
        }
        public static Shape CreateShape(double a,double b,double c)
        {
            Triangle ShapeC = new Triangle();
            ShapeC.a = a;
            ShapeC.b = b;
            ShapeC.c = c;
            ShapeC.Name = "三角形";
            return ShapeC;
        }
    }
    class Program
    {
        static Shape CreatShapes()//随机创建一个形状
        {
            Random random = new Random();
            int type = random.Next(1, 4);
            double a, b, c;
            Shape shape = null;
            switch (type)
            {
                case 1://长方形
                    a = Convert.ToDouble(random.Next(1, 10));
                    b = Convert.ToDouble(random.Next(1, 10));
                    shape = Factory.CreateShape(a, b);
                    break;
                case 2://正方形
                    a = Convert.ToDouble(random.Next(1, 10));
                    shape = Factory.CreateShape(a);
                    break;
                case 3://三角形
                    a = Convert.ToDouble(random.Next(1, 10));
                    b = Convert.ToDouble(random.Next(1, 10));
                    c= Convert.ToDouble(random.Next(1, 10));
                    shape = Factory.CreateShape(a, b, c);
                    break;
                default:
                    Console.WriteLine("非法输入！");
                    break;
            }
            if (!shape.IsLegal())
            {
                return null;
            }
            return shape;
        }
        static void Main(string[] args)
        {
            List<Shape> shapes = new List<Shape>();
            Console.WriteLine("请输入图形个数");
            int n = Convert.ToInt32 (Console.ReadLine());
            while (shapes.Count < n)
            {
                Shape shape = CreatShapes();
                if (shape != null)
                {
                    shape.Print();
                    shapes.Add(shape);
                }
            }
            double  sum = 0;
            foreach (Shape s in shapes) 
            {
                sum += s.Area();
            }
            Console.WriteLine("这" + n + "个图形的面积总和为" + sum);
        }
    }
}