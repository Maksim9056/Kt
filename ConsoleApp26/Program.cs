using System.Collections.Generic;
using System.Drawing;

namespace ConsoleApp26
{
    class TreeNode
    {
        public int Value { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }

        public TreeNode(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    class BinarySearchTree
    {
        private TreeNode root;

        public BinarySearchTree()
        {
            root = null;
        }

        public void Insert(int value)
        {
            root = InsertNode(root, value);
        }

        private TreeNode InsertNode(TreeNode node, int value)
        {
            if (node == null)
                return new TreeNode(value);

            if (value < node.Value)
                node.Left = InsertNode(node.Left, value);
            else if (value > node.Value)
                node.Right = InsertNode(node.Right, value);

            return node;
        }

        public void PrintTree()
        {
            Console.WriteLine("Бинарное дерево поиска:");
            PrintNode(root);
            Console.WriteLine();
        }

        private void PrintNode(TreeNode node)
        {
            if (node != null)
            {
                PrintNode(node.Left);
                Console.Write(node.Value + " ");
                PrintNode(node.Right);
            }
        }

        public void BalanceTree()
        {
            var sortedList = new List<int>();
            InOrderTraversal(root, sortedList);
            root = BuildBalancedTree(sortedList, 0, sortedList.Count - 1);
        }

        private void InOrderTraversal(TreeNode node, List<int> sortedList)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left, sortedList);
                sortedList.Add(node.Value);
                InOrderTraversal(node.Right, sortedList);
            }
        }

        private TreeNode BuildBalancedTree(List<int> sortedList, int start, int end)
        {
            if (start > end)
                return null;

            int mid = (start + end) / 2;
            var node = new TreeNode(sortedList[mid]);

            node.Left = BuildBalancedTree(sortedList, start, mid - 1);
            node.Right = BuildBalancedTree(sortedList, mid + 1, end);

            return node;
        }
    }

    class BinaryHeap
    {
        private List<int> heap;

        public BinaryHeap()
        {
            heap = new List<int>();
        }

        public void Insert(int value)
        {
            heap.Add(value);
            HeapifyUp(heap.Count - 1);
        }

        private void HeapifyUp(int index)
        {
            int parent = (index - 1) / 2;
            while (index > 0 && heap[index] > heap[parent])
            {
                Swap(index, parent);
                index = parent;
                parent = (index - 1) / 2;
            }
        }

        private void Swap(int a, int b)
        {
            int temp = heap[a];
            heap[a] = heap[b];
            heap[b] = temp;
        }

        public void PrintHeap()
        {
            Console.WriteLine("Дерево бинарной кучи:");
            foreach (int value in heap)
            {
                Console.Write(value + " ");
            }
            Console.WriteLine();
        }

        public List<int> HeapSort()
        {
            List<int> sortedList = new List<int>();
            while (heap.Count > 0)
            {
                sortedList.Add(ExtractMax());
            }
            return sortedList;
        }

        private int ExtractMax()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException("Куча пуста");

            int max = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(0);

            return max;
        }

        private void HeapifyDown(int index)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;
            int largest = index;

            if (left < heap.Count && heap[left] > heap[largest])
            {
                largest = left;
            }

            if (right < heap.Count && heap[right] > heap[largest])
            {
                largest = right;
            }

            if (largest != index)
            {
                Swap(index, largest);
                HeapifyDown(largest);
            }
        }
    }
    class AVLNode
    {
        public int key;
        public AVLNode left;
        public AVLNode right;
        public int height;

        public AVLNode(int key)
        {
            this.key = key;
            this.left = null;
            this.right = null;
            this.height = 1;
        }
    }

    class AVLTree
    {
        AVLNode root;

        public AVLTree()
        {
            this.root = null;
        }

        public void insert(int key)
        {
            root = insertHelper(root, key);
        }


        private AVLNode insertHelper(AVLNode root, int key)
        {

            if (root == null)
            {
                return new AVLNode(key);
            }

            if (key < root.key)
            {
                root.left = insertHelper(root.left, key);
            }
            else if (key > root.key)
            {
                root.right = insertHelper(root.right, key);
            }

            root.height = 1 + Math.Max(getHeight(root.left), getHeight(root.right));

            int balanceFactor = getBalance(root);

            if (balanceFactor > 1)
            {
                if (key < root.left.key)
                {
                    return rightRotate(root);
                }
                else
                {
                    root.left = leftRotate(root.left);
                    return rightRotate(root);
                }
            }

            if (balanceFactor < -1)
            {
                if (key > root.right.key)
                {
                    return leftRotate(root);
                }
                else
                {
                    root.right = rightRotate(root.right);
                    return leftRotate(root);
                }
            }

            return root;
        }

        public AVLNode delete(AVLNode root, int key)
        {
            if (root == null)
            {
                return root;
            }
            else if (key < root.key)
            {
                root.left = delete(root.left, key);
            }
            else if (key > root.key)
            {
                root.right = delete(root.right, key);
            }
            else
            {
                if (root.left == null)
                {
                    AVLNode temp = root.right;
                    root = null;
                    return temp;
                }
                else if (root.right == null)
                {
                    AVLNode temp = root.left;
                    root = null;
                    return temp;
                }
                AVLNode tempS = getMinValueNode(root.right);
                root.key = tempS.key;
                root.right = delete(root.right, tempS.key);
            }

            if (root == null)
            {
                return root;
            }

            root.height = 1 + Math.Max(getHeight(root.left), getHeight(root.right));

            int balanceFactor = getBalance(root);

            if (balanceFactor > 1)
            {
                if (getBalance(root.left) >= 0)
                {
                    return rightRotate(root);
                }
                else
                {
                    root.left = leftRotate(root.left);
                    return rightRotate(root);
                }
            }

            if (balanceFactor < -1)
            {
                if (getBalance(root.right) <= 0)
                {
                    return leftRotate(root);
                }
                else
                {
                    root.right = rightRotate(root.right);
                    return leftRotate(root);
                }
            }

            return root;
        }

        public int getHeight(AVLNode root)
        {
            if (root == null)
            {
                return 0;
            }
            return root.height;
        }

        public void Balanse()
        {
            getBalance(root);
            Print();
        }

        public int getBalance(AVLNode root)
        {
            if (root == null)
            {
                return 0;
            }
            return getHeight(root.left) - getHeight(root.right);
        }

        public AVLNode leftRotate(AVLNode z)
        {
            AVLNode y = z.right;
            AVLNode T2 = y.left;

            y.left = z;
            z.right = T2;

            z.height = 1 + Math.Max(getHeight(z.left), getHeight(z.right));
            y.height = 1 + Math.Max(getHeight(y.left), getHeight(y.right));

            return y;
        }

        public AVLNode rightRotate(AVLNode z)
        {
            AVLNode y = z.left;
            AVLNode T3 = y.right;

            y.right = z;
            z.left = T3;

            z.height = 1 + Math.Max(getHeight(z.left), getHeight(z.right));
            y.height = 1 + Math.Max(getHeight(y.left), getHeight(y.right));

            return y;
        }

        public AVLNode getMinValueNode(AVLNode root)
        {
            if (root == null || root.left == null)
            {
                return root;
            }
            return getMinValueNode(root.left);
        }

        public void visualize()
        {
            _visualize_helper(this.root, "", true);
        }

        private void _visualize_helper(AVLNode node, String prefix, bool isLeft)
        {
            if (node == null)
            {
                return;
            }

            String nodeStr = node.key.ToString();
            String line = prefix + (isLeft ? "├── " : "└── ");
            Console.WriteLine(line + nodeStr);

            String childPrefix = prefix + (isLeft ? "│   " : "    ");
            _visualize_helper(node.left, childPrefix, true);
            _visualize_helper(node.right, childPrefix, false);
        }

        public void inOrderTraversal()
        {
            inOrderTraversalHelper(root);
            //  System.out.println();
            Console.WriteLine();
        }

        private void inOrderTraversalHelper(AVLNode node)
        {
            if (node != null)
            {
                inOrderTraversalHelper(node.left);
                Console.WriteLine(node.key + " ");
                inOrderTraversalHelper(node.right);
            }
        }

        public void Print()
        {
            if (root == null)
            {
                Console.WriteLine("AVLTree is empty.");
                return;
            }

            PrintNode(root, "", true);
        }


        private void PrintNode(AVLNode node, string indent, bool isLast)
        {
            if (node != null)
            {
                Console.Write(indent);

                if (isLast)
                {
                    Console.Write("└── ");
                    indent += "    ";
                }
                else
                {
                    Console.Write("├── ");
                    indent += "│   ";
                }

                Console.WriteLine(node.key);

                PrintNode(node.left, indent, node.right == null);
                PrintNode(node.right, indent, true);
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();

            List<int> list = new List<int>();
            HashSet<int> uniqueValues = new HashSet<int>();

            while (list.Count < 30)
            {
                int value = random.Next(0, 100);

                if (uniqueValues.Add(value))
                {
                    list.Add(value);
                }
            }


            //for (int I = 0; I < 30; I++)
            //{
            //    bool isDuplicate = true;
            //    do
            //    {


            //        var value = random.Next(0, 100);

            //        if (list.Any(I => I == value))
            //        {

            //        }
            //        else
            //        {
            //            isDuplicate = false;

            //            list.Add(value);

            //        }
            //    } while (isDuplicate);

            //}
            int[] anArrayNodes = new int[30];

            for (int i = 0; i < anArrayNodes.Length; i++)
            {

                anArrayNodes[i] = list[i];
            }

            AVLTree avlTree = new AVLTree();


            // Insert nodes into the BST



            foreach (int node in anArrayNodes)
            {
                avlTree.insert(node);
            }

            // Visualize the BST
            avlTree.Print();
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine();
            AVLNode index = binaryHeap.heap[0];
            for (int i = 0; i < binaryHeap.heap.Length;)
            {
                list.Add(index);

                if (binaryHeap.size == 0)
                {
                    break;
                }
                else
                {
                    binaryHeap.Delete();
                }
            }

            List<int> sorted_arr = new List<int>();

            foreach (int i in d)
            {
                binaryHeap.Insert(i);
            }

            for (int i = 0; i < d.Length; i++)
            {
                sorted_arr.Add(binaryHeap.getElement(0));
                if (binaryHeap.size == 0)
                {
                    break;
                }
                else
                {
                    binaryHeap.Delete();
                }
            }

            AVLNode index = binaryHeap.heap[0];
            for (int i = 0; i < binaryHeap.heap.Length;)
            {
                list.Add(index);

                if (binaryHeap.size == 0)
                {
                    break;
                }
                else
                {
                    binaryHeap.Delete();
                }
            }

            List<int> sorted_arr = new List<int>();

            foreach (int i in d)
            {
                binaryHeap.Insert(i);
            }

            for (int i = 0; i < d.Length; i++)
            {
                sorted_arr.Add(binaryHeap.getElement(0));
                if (binaryHeap.size == 0)
                {
                    break;
                }
                else
                {
                    binaryHeap.Delete();
                }
            }

            AVLNode index = binaryHeap.heap[0];
            for (int i = 0; i < binaryHeap.heap.Length;)
            {
                list.Add(index);

                if (binaryHeap.size == 0)
                {
                    break;
                }
                else
                {
                    binaryHeap.Delete();
                }
            }

            List<int> sorted_arr = new List<int>();

            foreach (int i in d)
            {
                binaryHeap.Insert(i);
            }

            for (int i = 0; i < d.Length; i++)
            {
                sorted_arr.Add(binaryHeap.get
                    Element(0));
                if (binaryHeap.size == 0)
                {
                    break;
                }
                else
                {
                    binaryHeap.Delete();
                }
            }

            avlTree.Balanse();
            // In-order traversal of BST
            Console.Write("In-order Traversal: ");
            avlTree.inOrderTraversal();

            Console.WriteLine("Сгенерированный список:");
            foreach (int value in list)
            {
                Console.Write(value + " ");
            }
            Console.WriteLine();
            Console.WriteLine();

            // Создание и вывод бинарного дерева поиска
            var bst = new BinarySearchTree();
            foreach (int value in list)
            {
                bst.Insert(value);
            }
            bst.PrintTree();

            // Балансировка дерева и вывод
            bst.BalanceTree();
            Console.WriteLine("АВЛ сбалансированное дерево:");
            bst.PrintTree();
            Console.WriteLine();

            // Создание и вывод бинарной кучи
            var heap = new BinaryHeap();
            foreach (int value in list)
            {
                heap.Insert(value);
            }
            heap.PrintHeap();
            Console.WriteLine();

            // Сортировка списка при использовании кучи (Heap Sort) и вывод
            var sortedList = heap.HeapSort();
            Console.WriteLine("Отсортированный список методом бинарной кучи:");
            foreach (int value in sortedList)
            {
                Console.Write(value + " ");
            }
            Console.WriteLine();
            Console.ReadLine();
        }
        public void Method()
        {

            //var index = root
                
                
                
            //    .heap[0];
            //for (int i = 0; i < binaryHeap.heap.Length;)
            //{

            //    list.Add(index);

            //    if (binaryHeap.size == 0)
            //    {
            //        break;
            //    }
            //    else
            //    {
            //        binaryHeap.Delete();
            //    }

            //}
            //List<int> sorted_arr = new List<int>();

            //foreach (int i in d)
            //{
            //    binaryHeap.Insert(i);
            //}


            //for (int i = 0; i < d.Length; i++)
            //{
            //    sorted_arr.Add(binaryHeap.getElement(0));
            //    if (binaryHeap.size == 0)
            //    {
            //        break;
            //    }
            //    else
            //    {
            //        binaryHeap.Delete();
            //    }
            //}

            //foreach (int i in sorted_arr)
            //{
            //    Console.WriteLine(sorted_arr[i]);

            //}

        }
    }
}