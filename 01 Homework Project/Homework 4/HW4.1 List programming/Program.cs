using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4._1_List_programming
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

        // 添加元素
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

        // ForEach方法，接受一个Action委托
        public void ForEach(Action<T> action)
        {
            Node<T> node = head;
            while (node != null)
            {
                action(node.Data);
                node = node.Next;
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // 创建整数链表
            GenericList<int> intList = new GenericList<int>();

            // 添加元素到链表
            for (int x = 0; x < 10; x++)
            {
                intList.Add(x);
            }

            // 打印链表中的所有元素
            Console.WriteLine("链表元素：");
            intList.ForEach(x => Console.WriteLine(x));

            // 求链表中的最大值
            int max = int.MinValue;
            intList.ForEach(x => {
                if (x > max) max = x;
            });
            Console.WriteLine("最大值: " + max);

            // 求链表中的最小值
            int min = int.MaxValue;
            intList.ForEach(x => {
                if (x < min) min = x;
            });
            Console.WriteLine("最小值: " + min);

            // 求链表中所有元素的和
            int sum = 0;
            intList.ForEach(x => sum += x);
            Console.WriteLine("总和: " + sum);
        }
    }

}
