using System;
using System.Collections.Generic;

namespace IART_A3
{
    public class TreeNode<T>
    {
        public T Data { get; set; }
        public readonly List<TreeNode<T>> Children;

        public TreeNode(T data)
        {
            Data = data;
            Children = new List<TreeNode<T>>();
        }

        public TreeNode<T> AddChild(T data)
        {
            var n = new TreeNode<T>(data);
            Children.Add(n);
            return n;
        }

        public TreeNode<T> GetChild(int i)
        {
            return Children.Count < i ? Children[i] : null;
        }

        public double Cost(Func<T, double> f)
        {
            return f(Data);
        }

        public static void Traverse(TreeNode<T> node, Action<T> visitor)
        {
            visitor(node.Data);
            foreach (var treeNode in node.Children)
                Traverse(treeNode, visitor);
        }
    }
}
