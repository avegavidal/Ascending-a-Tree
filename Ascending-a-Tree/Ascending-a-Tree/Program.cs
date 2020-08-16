using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace Ascending_a_Tree
{
    public class Logger
    {
        public void Log(string message) { Console.WriteLine(message); }
    }
    public class Node
    {
        public Node LeftChild { get; set; }
        public Node RightChild { get; set; }
        public int Weight { get; set; }
    }

    class Tree
    {
        List<int> orderedWeights = new List<int>();
        public Node Root { get; set; }
        public bool Add(int value)
        {
            Node before = null, after = this.Root;
            Node newNode = new Node();
            while (after != null)
            {
                before = after;
                if (value < after.Weight)  
                    after = after.LeftChild;
                else if (value > after.Weight)
                    after = after.RightChild;
                else
                {
                    //Does nothing because the value is already in the tree.
                    return false;
                }
            }
            newNode.Weight = value;
            if (this.Root == null)
                this.Root = newNode;
            else
            {
                if (value < before.Weight)
                    before.LeftChild = newNode;
                else
                    before.RightChild = newNode;
            }
            return true;
        }

        public List<int> GetOrderedWeights(Node tree)
        {
            //recursive function
            if (tree != null)
            {
                GetOrderedWeights(tree.LeftChild);
                orderedWeights.Add(tree.Weight);
                GetOrderedWeights(tree.RightChild);
            }
            return orderedWeights;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Tree _tree = new Tree();
            Logger _logger = new Logger();
            int number=0;
            string input;
            bool continueAdding = true;
            bool passValueToTree = false;

            _logger.Log("Enter the weight of the node, hit enter to add a new one node, write 'READY' when you are ready to order the tree");
            while (continueAdding)
            {
                input = Console.ReadLine();
                try
                {
                    if(input.ToUpper() == "READY")
                    {
                        continueAdding = false;
                        break;
                    }
                    number = int.Parse(input);
                    passValueToTree = true;
                }
                catch
                {
                    _logger.Log("Please use integer for the weight...");
                }
                finally
                {
                    if (passValueToTree)
                    {
                        _tree.Add(number);   
                    }
                }
            }
            _logger.Log("Your Binary tree in ascending order is:");
            List<int> result = _tree.GetOrderedWeights(_tree.Root);
            result.ForEach(i => Console.Write("{0}\t", i));
        }
    }
}
