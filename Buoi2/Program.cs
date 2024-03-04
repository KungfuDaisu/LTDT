using System;
using System.IO;
using System.Text;
using System.Collections.Generic; // Thư viện cho đối tượng LinkedList
using Buoi01;

namespace Buoi02
{
    class Program
    {
        static void Main(string[] args)
        {
            // Xuất text theo Unicode (có dấu tiếng Việt)
            Console.OutputEncoding = Encoding.Unicode;
            // Nhập text theo Unicode (có dấu tiếng Việt)
            Console.InputEncoding = Encoding.Unicode;

            /* Tạo menu */
            Menu menu = new Menu();
            string title = "VẬN DỤNG CÁC THAO TÁC CƠ BẢN TRÊN ĐỒ THỊ";   // Tiêu đề menu
            // Danh sách các mục chọn
            string[] ms = { "1. Bài 1: Chuyển danh sách cạnh sang danh sách kề",
                "2. Bài 2: Chuyển danh sách kề sang danh sách cạnh",
                "3. Bài 3: Đỉnh Bồn chứa",
                "4. Bài 4: Đồ thị chuyển vị",
                "5. Bài 5: Độ dài trung bình của cạnh",
                "0. Thoát" };
            int chon;
            do
            {
                // Xuất menu
                menu.ShowMenu(title, ms);
                Console.Write("     Chọn : ");
                chon = int.Parse(Console.ReadLine());
                switch (chon)
                {
                    case 1:
                        {   // Bài 1: Chuyển danh sách cạnh sang danh sách kề
                            // Tạo đường dẫn fileInput -> đồ thị EdgeList
                            string fileInput = "../../../TextFile/EdgeList.txt";
                            EdgeList ge = new EdgeList();
                            ge.FileToEdgeList(fileInput); ge.Output();
                            // Tạo đồ thị AdjList ga từ EdgeList ge
                            AdjList ga = new AdjList();
                            ga = EdgeListToAdjList(ge); ga.Output();
                            // Tạo đường dẫn fileOutput
                            string fileOutput = "../../../TextFile/AdjList.txt";
                            ga.AdjListToFile(fileOutput);
                            break;
                        }
                    case 2:
                    case 3:
                        {
                            // Bài 3 : Bồn chứa
                            // Khởi tạo g là đồ thị ma trận kề : AdjMatrix g
                            AdjMatrix g = new AdjMatrix();

                            // Tạo đường dẫn fileInput : DirectedMatrix.txt
                            string fileInput = "../../../TextFile/DirectedMatrix.txt";
                            g.FileToAdjMatrix(fileInput);

                            // Xuất đồ thị g lên màn hình
                            g.Output();

                            // Tạo đường dẫn fileOutput : Storage.txt
                            string fileOutput = "../../../TextFile/Storage.txt";

                            // Gọi hàm : Storage(g, fileOutput);
                            Storage(g, fileOutput);
                            break;
                        }
                    case 4:
                        {
                            // Bài 4 : Đồ thị chuyển vị
                            // Tạo đường dẫn file Input : "DirectedList.txt";
                            string fileInput = "../../../TextFile/DirectedList.txt";

                            // Khai báo đồ thị g : AdjList g = new AdjList();
                            AdjList g = new AdjList();

                            // Tạo đồ thị từ fileInput và xuất đồ thị
                            g.FileToAdjList(fileInput);
                            g.Output();

                            // Khai báo G là đồ thị chuyển vị : AdjList G = new AdjList();
                            AdjList G = new AdjList();

                            // Gọi hàm : G = TransposeG(g); G.Output();
                            G = TransposeG(g);
                            Console.WriteLine("Đồ thị chuyển vị:");
                            G.Output();

                            // Xuất đồ thi chuyển vị G lên màn hình và ghi kết quả vào file Transpose.txt
                            G.AdjListToFile("chuyenvi.txt");
                            break;
                        }
                    case 5:
                        {
                            // Tạo tham số fileInput
                            string fileInput = "../../../TextFile/WeightEdgeList.txt";
                            WeightEdgeList g = new WeightEdgeList();
                            g.FileToWeightEdgeList(fileInput);
                            g.Output();
                            Console.WriteLine("Cạnh dài nhất :");
                            MaxEdge(g);
                            Console.WriteLine("Chiều dài TB : {0:0.00}", AverageEdge(g));
                            break;
                        }
                }
                Console.WriteLine(" Nhấn một phím bất kỳ");
                Console.ReadKey();
                Console.Clear();
            } while (chon != 0);
        }
        // Chuyển file EdgeList sang đồ thị danh sách kề AdjList
        static AdjList EdgeListToAdjList(EdgeList ge)
        {
            // Khởi tạo đồ thị AdjList
            AdjList ga = new AdjList();
            // Xác định số đỉnh n của đồ thị : ga.N = ge.N;
            ga.N = ge.N;
            // Khởi tạo array v[] của đồ thị AdjList
            // Khởi tạo các danh sách liên kết ga.V[i]  
            // Duyệt từng đỉnh i trong ga , i = 0..ga.N-1
            // Khởi tạo các dslk : ga.V[i] = new LinkedList<int>();
            // Xây dựng các phấn tử cho các dslk
            ga.V = new LinkedList<int>[ga.N];

            for (int i = 0; i < ga.N; i++)
            {
                ga.V[i] = new LinkedList<int>();
            }
            // Duyệt từng cạnh e trong đồ thị EdgeList
            foreach (Tuple<int, int> e in ge.G)
            {
                // AddLast e.Item2 vào ga.V[e.Item1]
                ga.V[e.Item1].AddLast(e.Item2);
                // AddLast e.Item1 vào ga.V[e.Item2]
                ga.V[e.Item2].AddLast(e.Item1);
            }
            return ga;	// Đồ thị trả về
        }
        static void Storage(AdjMatrix g, string fileOut)
        {
            // Khởi tạo : StreamWriter sw
            StreamWriter sw = new StreamWriter(fileOut);

            // Khai báo biến đếm : count=0
            int count = 0;

            // Duyệt các đỉnh i của g
            for (int i = 0; i < g.N; i++)
            {
                // Nếu (g.IsStorage(i) == true)
                if (g.IsStorage(i))
                {
                    // Đếm count lên 1
                    count++;

                    // Xuất lên màn hình : ("Đỉnh " + i + " là đỉnh bồn chứa");
                    Console.WriteLine("Đỉnh " + i + " là đỉnh bồn chứa");

                    // Ghi file sw : ("Đỉnh " + i + " là đỉnh bồn chứa");
                    sw.WriteLine("Đỉnh " + i + " là đỉnh bồn chứa");
                }
            }

            // Ghi file sw : ("Số đỉnh là bồn chứa : " + count);
            sw.WriteLine("Số đỉnh là bồn chứa : " + count);

            // Xuất lên màn hình : ("Số đỉnh là bồn chứa : " + count);
            Console.WriteLine("Số đỉnh là bồn chứa : " + count);

            // Đóng file sw
            sw.Close();
        }
        static AdjList TransposeG(AdjList g)
        {
            // Khai báo đồ thị G : AdjList G
            AdjList G = new AdjList();

            // Xác định số đỉnh G.N là số đỉnh g.N
            G.N = g.N;

            // Cấp phát vùng nhớ cho G.V : new LinkedList<int>[G.N];
            G.V = new LinkedList<int>[G.N];

            // Khởi tạo các dslk G.V[i] = new LinkedList<int>() , i = 0..G.N-1
            for (int i = 0; i < G.N; i++)
            {
                G.V[i] = new LinkedList<int>();
            }

            // Duyệt từng đỉnh i của G
            for (int i = 0; i < G.N; i++)
            {
                // Duyệt từng đỉnh x trong G.V[i]
                foreach (int x in g.V[i])
                {
                    // AddLast i vào G.V[x] : G.V[x].AddLast(i);
                    G.V[x].AddLast(i);
                }
            }

            // Trả về G
            return G;
        }
        static void MaxEdge(WeightEdgeList g)
        {
            int max = -int.MaxValue;
            // Dùng một dslk lst dùng chứa các cạnh dài nhất
            LinkedList<Tuple<int, int, int>> lst = new LinkedList<Tuple<int, int, int>>();

            // Tìm độ dài nhất max, trong g.G
            foreach (var edge in g.G)
            {
                if (edge.Item3 > max)
                {
                    max = edge.Item3;
                }
            }

            // Tìm các cạnh dài nhất (item3 = max) và Add vào lst
            foreach (var edge in g.G)
            {
                if (edge.Item3 == max)
                {
                    lst.AddLast(edge);
                }
            }

            // Xuất các cạnh dài nhất & số lượng
            Console.WriteLine("Số lượng cạnh dài nhất : " + lst.Count);
            foreach (var edge in lst)
            {
                Console.WriteLine(" ({0}, {1}) = {2}", edge.Item1, edge.Item2, edge.Item3);
            }
        }
        static double AverageEdge(WeightEdgeList g)
        {
            double sum = 0;

            // Tính tổng độ dài các cạnh
            foreach (var edge in g.G)
            {
                sum += edge.Item3;
            }

            // Tính độ dài trung bình
            double avg = sum / g.M;

            return avg;
        }
    }
}
