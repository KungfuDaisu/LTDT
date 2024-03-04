using Buoi01;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Buoi03
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
            // Tiêu đề menu
            string title = "TÌM KIẾM TRÊN ĐỒ THỊ BẰNG THUẬT TOÁN BFS (Breadth First Search)";
            // Danh sách các mục chọn
            string[] ms = { "1. Bài 1 : Liệt kê các đỉnh liên thông với đỉnh x bằng thuật toán BFS",
                "2. Bài 2 : Tìm đường đi từ đỉnh x -> y",
                "3. Bài 3 : Xét tính liên thông. Số TPLT, xuất các TPLT",
                "4. Bài 4 : Đỉnh khớp",
                "5. Bài 5 : Cạnh cầu",
                "6. Bài 6 : Kiểm tra đồ thị 2 phía",
                "7. Bài 7 : Đường đi trên lưới",
                "8. Bài 8 : Mê cung",
                "0. Thoát" };
            int chon;
            do
            {
                Console.Clear();
                // Xuất menu
                menu.ShowMenu(title, ms);
                Console.Write("     Chọn : ");
                chon = int.Parse(Console.ReadLine());
                switch (chon)
                {
                    case 1:
                        {
                            // Bài 1 : duyệt đồ thị từ đỉnh x theo BFS
                            string filePath = "../../../TextFile/AdjList.txt";
                            AdjList g = new AdjList();
                            g.FileToAdjList(filePath); g.Output();
                            Console.Write("  Nhập đỉnh xuất phát x : ");
                            int x = int.Parse(Console.ReadLine());
                            Console.Write("  Các đỉnh liên thông với {0} : ", x); g.BFS(x);
                            break;
                        }
                    case 2:
                        {
                            // Bài 2 : Tìm đường đi từ đỉnh x -> y
                            string filePath = "../../../TextFile/AdjList2.txt";
                            AdjList g = new AdjList();
                            g.FileToAdjList(filePath); g.Output();
                            Console.Write("  Nhập đỉnh xuất phát x : ");
                            int x = int.Parse(Console.ReadLine());
                            Console.Write("        Nhập đỉnh đến y : ");
                            int y = int.Parse(Console.ReadLine());
                            g.BFS_XtoY(x, y);
                            break;
                        }
                    case 3:
                        {
                            // Bài 3 : Xét tính liên thông. Số TPLT, xuất các TPLT
                            string filePath = "../../../TextFile/AdjList2.txt";
                            AdjList g = new AdjList();
                            g.FileToAdjList(filePath); g.Output(); g.Connected();
                            if (g.Inconnect == 1)
                                Console.WriteLine("  Đồ thị liên thông");
                            else
                            {
                                Console.WriteLine("  Đồ thị có {0} thành phần liên thông", g.Inconnect);
                                g.OutConnected();    // Xuất các TPLT
                            }
                            break;
                        }
                    case 4:
                        {   // Bài 4 : Đỉnh khớp
                            string filePath = "../../../TextFile/CutBridge.txt";
                            AdjList g = new AdjList();
                            g.FileToAdjList(filePath); g.Output();
                            g.Connected(); int gInconnect1 = g.Inconnect;

                            Console.Write("  Nhập đỉnh cần xét x : ");
                            int x = int.Parse(Console.ReadLine());
                            g.RemoveEdgeX(x); g.Connected();
                            int gInconnect2 = g.Inconnect;
                            if (gInconnect2 > gInconnect1 + 1)
                                Console.WriteLine("    Đỉnh {0} là đỉnh khớp", x);
                            else
                                Console.WriteLine("    Đỉnh {0} không phải là đỉnh khớp", x);
                            break;
                        }
                    case 5:
                        {   // Bài 5 : Cạnh cầu
                            string filePath = "../../../TextFile/CutBridge.txt";
                            AdjList g = new AdjList();
                            g.FileToAdjList(filePath); g.Output();
                            g.Connected(); int gInconnect1 = g.Inconnect;

                            Console.Write(" Nhập đỉnh đầu x = ");
                            int x = int.Parse(Console.ReadLine());
                            Console.Write(" Nhập đỉnh cuối y = ");
                            int y = int.Parse(Console.ReadLine());

                            g.RemoveEdgeXY(x, y);
                            g.Connected(); int gInconnect2 = g.Inconnect;
                            if (gInconnect2 > gInconnect1)
                                Console.WriteLine("    Cạnh ({0}, {1}) là cạnh cầu", x, y);
                            else
                                Console.WriteLine("    Cạnh ({0}, {1}) không phải cạnh cầu", x, y);
                            break;
                        }
                    case 6:
                        {   // Kiểm tra đồ thị 2 phía
                            string filePath = "../../../TextFile/Bipartite.txt";
                            AdjList g = new AdjList();
                            g.FileToAdjList(filePath); g.Output();
                            if (g.IsBipartite(0))
                                Console.WriteLine("    Đồ thị 2 phía");
                            else
                                Console.WriteLine("    Không phải đồ thị 2 phía");
                            break;
                        }
                    case 7:
                        {
                            // Bài 3 : đường đi trên lưới
                            string filePath = "../../../TextFile/Grid.txt";
                            AdjList g = new AdjList();
                            g.GridToAdjList(filePath);
                            break;
                        }
                    case 8:
                        {
                            // Bài 3 : đường đi trên lưới (mê cung)
                            string filePath = "../../../TextFile/maze.txt";
                            AdjList g = new AdjList();
                            g.GridToAdjList(filePath);
                            break;
                        }
                }
                Console.ReadKey();
            } while (chon != 0);
        }
    }
}