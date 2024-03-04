using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buoi01
{
    class AdjList
    {
        LinkedList<int>[] v;
        int n;  // Số đỉnh
        // Để đơn giản : thêm các thành phần chỉ tham gia vào giải thuật
        bool[] visited;     // Dùng đánh dấu đỉnh đã đi qua
        int[] index;        // Dùng đánh dấu các TPLT
        int inconnect;      // Dùng đếm số TPLT
        //Propeties
        public int N { get => n; set => n = value; }
        public LinkedList<int>[] V { get => v; set => v = value; }
        public int Inconnect { get => inconnect; set => inconnect = value; }
        // Contructor
        public AdjList() { }
        public AdjList(int k)   // Khởi tạo v có k đỉnh
        {
            v = new LinkedList<int>[k];
        }
        // copy g --> đồ thị hiện tại v
        public AdjList(LinkedList<int>[] g)
        {
            v = g;
        }
        // Đọc file AdjList.txt --> danh sách kề v
        public void FileToAdjList(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            n = int.Parse(sr.ReadLine());
            v = new LinkedList<int>[n];
            for (int i = 0; i < n; i++)
            {
                v[i] = new LinkedList<int>();
                string st = sr.ReadLine();
                // Đặt điều kiện không phải đỉnh cô lập
                if (st != "")
                {
                    string[] s = st.Split();
                    for (int j = 0; j < s.Length; j++)
                    {
                        int x = int.Parse(s[j]);
                        v[i].AddLast(x);
                    }
                }
            }
            sr.Close();
        }
        // Xuất đồ thị
        public void Output()
        {
            Console.WriteLine("Đồ thị danh sách kề - số đỉnh : " + n);
            for (int i = 0; i < v.Length; i++)
            {
                Console.Write("   Đỉnh {0} ->", i);
                foreach (int x in v[i])
                    Console.Write("{0, 3}", x);
                Console.WriteLine();
            }
        }
        // Duyệt đồ thị theo BFS với đỉnh xuất phát s
        public void BFS(int s)
        {
            // Khởi tạo các giá trị visited[i]
            visited = new bool[v.Length];
            for (int i = 0; i < visited.Length; i++)
                visited[i] = false;
            // Sử dụng Queue
            Queue<int> q = new Queue<int>();
            visited[s] = true;  // Đánh dấu duyệt s
            q.Enqueue(s);       // s -> q
            while (q.Count != 0)
            {
                s = q.Dequeue();    // s <- q
                Console.Write(s + " - ");   // Duyệt s
                foreach (int u in v[s])     // Xét các đỉnh kề của s
                {
                    if (visited[u]) continue;   // Đã duyệt -> bỏ qua
                    visited[u] = true;      // Đánh dấu duyệt u
                    q.Enqueue(u);           // u -> q
                }
            }
        }
        // Tìm đường đi từ đỉnh x đến y theo BFS
        public void BFS_XtoY(int x, int y)
        {
            // pre[] : lưu đỉnh nằm trước trên đường đi
            int[] pre = new int[v.Length];
            for (int i = 0; i < v.Length; i++)
                pre[i] = -1;
            // khởi tạo các giá trị cho visited
            visited = new bool[v.Length];
            for (int i = 0; i < visited.Length; i++)
                visited[i] = false;

            Queue<int> q = new Queue<int>();
            visited[x] = true;
            q.Enqueue(x);
            while (q.Count != 0)
            {
                int s = q.Dequeue();
                foreach (int u in v[s])
                {
                    if (visited[u]) continue;
                    visited[u] = true;
                    q.Enqueue(u);
                    pre[u] = s;
                }
            }
            // Xuất đường đi từ x đến y
            Console.WriteLine();
            int k = y;
            Stack<int> stk = new Stack<int>();
            while (pre[k] != -1)
            {
                stk.Push(k);
                k = pre[k];
            }
            Console.WriteLine();
            Console.Write(" Đường đi từ " + x + " -> " + y + " :   " + x);
            while (stk.Count > 0)
            {
                k = stk.Pop();
                Console.Write(" -> " + k);
            }
            Console.WriteLine();
        }
        // Xét tính liên thông và xác định giá trị cho visite[], index[]
        // Xác định inconnect : số thành phần liên thông (TPLT)
        public void Connected()
        {
            // inconnect : số TPLT
            inconnect = 0;
            // index : lưu các đỉnh cùng một TPLT
            index = new int[n];
            for (int i = 0; i < n; i++)
                index[i] = -1;

            // Khởi tạo giá trị cho visited[i]
            visited = new bool[n];
            for (int i = 0; i < visited.Length; i++)
                visited[i] = false;

            for (int i = 0; i < visited.Length; i++)
                if (visited[i] == false)        // Nếu chưa duyệt đỉnh i
                {
                    // Khởi đầu cho một TPLT mới -> tăng inconnect++
                    inconnect++;
                    // Tìm và đánh dấu các đỉnh cùng TPLT
                    BFS_Connected(i);
                }
            Console.WriteLine();
        }
        // Lượt duyệt mới vớt đỉnh bắt đầu: s
        public void BFS_Connected(int s)
        {
            // Sử dụng một queue cho giải thuật
            Queue<int> q = new Queue<int>();
            visited[s] = true;      // Duyệt đỉnh s
            q.Enqueue(s);           // Đưa s vào q
            while (q.Count != 0)    // Lặp khi queue còn phần tử
            {
                s = q.Dequeue();            // Lấy từ queue ra một phần tử -> s
                index[s] = inconnect;       // gán giá trị TPLT
                foreach (int u in v[s])
                {
                    if (visited[u] == false)
                    {
                        visited[u] = true;
                        q.Enqueue(u);
                    }
                }
            }
        }
        // Xuất các thành phần liên thông
        public void OutConnected()
        {
            for (int i = 1; i <= inconnect; i++)
            {
                Console.Write("  TPLT {0} : ", i);
                for (int j = 0; j < index.Length; j++)
                    if (index[j] == i)
                        Console.Write(j + " ");
                Console.WriteLine();
            }

        }
        // Bỏ các cạnh kề của đỉnh x (để xét Đỉnh Khớp)
        public void RemoveEdgeX(int x)
        {
            for (int i = 0; i < v.Length; i++)
            {
                if (i == x)
                    v[i].Clear();
                else
                    v[i].Remove(x);
            }
        }
        // Bỏ cạnh (x,y) (để xét cạnh cầu)
        public void RemoveEdgeXY(int x, int y)
        {
            v[x].Remove(y);
            v[y].Remove(x);
        }
        // Đọc file Grid.txt -> tạo đồ thị tương ứng và 2 đỉng x, y
        public void GridToAdjList(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            // Xác định dòng, cột và số đỉnh n
            string[] s = sr.ReadLine().Split();
            int row = int.Parse(s[0]);
            int col = int.Parse(s[1]);
            n = row * col;
            // Khởi tạo v[]
            v = new LinkedList<int>[n];
            for (int i = 0; i < n; i++)
                v[i] = new LinkedList<int>();
            // Xác định đỉnh đầu x và đỉnh đến y
            s = sr.ReadLine().Split();
            int x = int.Parse(s[1]) + int.Parse(s[0]) * col;
            int y = int.Parse(s[3]) + int.Parse(s[2]) * col;

            // đọc Grid.txt ra ma trận a
            int[,] a = new int[row, col];
            for (int i = 0; i < row; i++)
            {
                s = sr.ReadLine().Split();
                for (int j = 0; j < col; j++)
                    a[i, j] = int.Parse(s[j]);
            }
            // Xuất ma trận a
            Console.WriteLine("  Ma trận lưới a[] : ");
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                    if (a[i, j] == 1)
                    {
                        //Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write("  " + a[i, j]);
                        //Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                        Console.Write("  " + a[i, j]);
                Console.WriteLine();
            }

            // Đọc a[] ra AdjList
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                {
                    if (a[i, j] == 1)
                    {
                        int k = j + i * col;
                        if (i > 0 && a[i - 1, j] == 1)
                        {
                            if (v[k].Contains(k - col) == false) v[k].AddLast(k - col);
                            if (v[k - col].Contains(k) == false) v[k - col].AddLast(k);
                        }
                        if (j > 0 && a[i, j - 1] == 1)
                        {
                            if (v[k].Contains(k - 1) == false) v[k].AddLast(k - 1);
                            if (v[k - 1].Contains(k) == false) v[k - 1].AddLast(k);
                        }
                        if (j < col - 1 && a[i, j + 1] == 1)
                        {
                            if (v[k].Contains(k + 1) == false) v[k].AddLast(k + 1);
                            if (v[k + 1].Contains(k) == false) v[k + 1].AddLast(k);
                        }
                        if (i < row - 1 && a[i + 1, j] == 1)
                        {
                            if (v[k].Contains(k + col) == false) v[k].AddLast(k + col);
                            if (v[k + col].Contains(k) == false) v[k + col].AddLast(k);
                        }
                    }
                }
            // Tìm và xuất đường đi
            PathOnGrid(x, y, row, col, a);
        }
        // Tìm đường đi trên đồ thị và Xuất lộ trình trên lưới
        public void PathOnGrid(int x, int y, int row, int col, int[,] a)
        {
            // pre[] : lưu đỉnh nằm trước trên đường đi
            int[] pre = new int[v.Length];
            for (int i = 0; i < v.Length; i++) pre[i] = -1;

            // khởi tạo các giá trị cho visited
            visited = new bool[v.Length];
            for (int i = 0; i < visited.Length; i++) visited[i] = false;

            Queue<int> q = new Queue<int>();
            visited[x] = true;
            q.Enqueue(x);
            while (q.Count != 0)
            {
                int s = q.Dequeue();
                foreach (int u in v[s])
                {
                    if (visited[u]) continue;
                    visited[u] = true;
                    q.Enqueue(u);
                    pre[u] = s;
                }
            }
            // Xuất đường đi từ x đến y theo tọa độ
            Console.WriteLine("  Tìm đường đi");
            Console.ReadLine(); Console.WriteLine();
            int k = y;
            Stack<int> stk = new Stack<int>(); // dùng xuất tọa độ
            Stack<int> st = new Stack<int>(); // dùng xuất tô màu

            // bỏ k vào stack
            while (k != -1)
            {
                stk.Push(k); st.Push(k);
                k = pre[k];
            }
            Console.WriteLine();
            // Xuất theo tọa độ

            Console.WriteLine("  Đường đi từ ({0}, {1}) -> ({2}, {3}) : ", (x / col), x % col, (y / col), y % col, (x / col), x % col);
            Console.Write("    ({0}, {1}) ", (x / col), x % col);
            while (stk.Count > 0)
            {
                k = stk.Pop();
                Tuple<int, int> e = new Tuple<int, int>(k / col, k % col);
                Console.Write(" -> ({0}, {1})", e.Item1, e.Item2);
            }
            // Xuất theo tô màu đường đi
            Console.WriteLine();
            st.Push(x);
            for (int i = 0; i < row; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < col; j++)
                {
                    k = j + i * col;
                    if (st.Contains(k))
                    {
                        //Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.BackgroundColor = ConsoleColor.Magenta;
                        Console.Write("  " + a[i, j]);
                        //Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (!st.Contains(k) && a[i, j] == 1)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write("  " + a[i, j]);
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("  " + a[i, j]);
                    }
                }
            }

            Console.WriteLine();
        }
        // Đồ thị 2 phía dùng BFS. Gọi hàm : IsBipartite(0)
        public bool IsBipartite(int x)
        {
            int[] color = new int[n];
            for (int i = 0; i < n; ++i)
                color[i] = -1;
            color[x] = 1;
            Queue<int> q = new Queue<int>();
            q.Enqueue(x);
            while (q.Count > 0)
            {
                int s = q.Dequeue();
                if (v[s].Contains(s)) return false;
                foreach (int u in v[s])
                {
                    if (color[u] == -1)
                    {
                        color[u] = 1 - color[s];
                        q.Enqueue(u);
                    }
                    else if (color[u] == color[s])
                        return false;
                }
            }
            return true;
        }
    }
}