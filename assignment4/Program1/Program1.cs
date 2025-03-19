using System;
using System.Collections.Generic;

namespace assignment4
{
    public class Node<T>
    {
        public Node<T> Next { get; set; }
        public T Data { get; set; }

        public Node(T t)
        {
            Next = null;
            Data = t;
        }
    }

    // 泛型链表类
    public class GenericList<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public GenericList()
        {
            tail = head = null;
        }

        public Node<T> Head
        {
            get => head;
        }

        public void Add(T t)
        {
            Node<T> n = new Node<T>(t);

            if (tail == null)
            {
                head = tail = n;
            }
            else
            {
                tail.Next = n;
                tail = n;
            }
        }

        // ForEach 方法
        public void ForEach(Action<T> action)
        {
            for (Node<T> node = head; node != null; node = node.Next)
            {
                action(node.Data);
            }
        }
    }

    class Program1
    {
        static void Main(string[] args)
        {
            // 整型List
            GenericList<int> intlist = new GenericList<int>();
            for (int x = 0; x < 10; x++)
            {
                intlist.Add(x);
            }

            Console.WriteLine("整形数据元素");
            intlist.ForEach(i => Console.WriteLine(i));

            int sum = 0, max = int.MinValue, min = int.MaxValue;
            intlist.ForEach(i =>
            {
                sum += i;
                if (i > max) max = i;
                if (i < min) min = i;
            });

            Console.WriteLine($"Sum: {sum}, Max: {max}, Min: {min}");

            // 字符串型List
            GenericList<string> strList = new GenericList<string>();
            for (int x = 0; x < 10; x++)
            {
                strList.Add("str" + x);
            }

            Console.WriteLine("\n字符串数据元素");
            strList.ForEach(s => Console.WriteLine(s));
        }
    }
}