using System;
using System.IO;
using System.Text;
namespace Buoi01
{
    class AdjMatrix
    {
        public int n;   // số đỉmh
        public int[,] a;    // Ma trận kề
        // propeties
        public int N { get => n; set => n = value; }
        public int[,] A { get => a; set => a = value; }
        // constructor không đối số
        public AdjMatrix() { }
        // constructor có đối số k là số đỉnh của đồ thị
        public AdjMatrix(int k)
        {
            n = k;
            a = new int[n, n];
        }
        // Đọc file AdjMatrix --> ma trận a
        public void FileToAdjMatrix(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            n = int.Parse(sr.ReadLine());
            a = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                string[] s = sr.ReadLine().Split();
                for (int j = 0; j < n; j++)
                    a[i, j] = int.Parse(s[j]);
            }
            sr.Close();
        }
        // Xuất ma trận a lên màn hình
        public void Output()
        {
            Console.WriteLine("Đồ thị ma trận kề - số đỉnh : " + n);
            Console.WriteLine();
            Console.Write(" Đỉnh |");
            for (int i = 0; i < n; i++) Console.Write("    {0}", i);
            Console.WriteLine(); Console.WriteLine("  " + new string('-', 6 * n));
            for (int i = 0; i < n; i++)
            {
                Console.Write("    {0} |", i);
                for (int j = 0; j < n; j++)
                    Console.Write("  {0, 3}", a[i, j]);
                Console.WriteLine();
            }
        }
        #region Bài 1
        // Tính bậc của đỉnh i
        public int DegVi(int i)
        {
            // Duyệt từng cột j trên dòng i
            // Đếm số lượng ô(i, j) = 1
            int deg = 0;
            for (int j = 0; j < n; j++)
            {
                if (a[i, j] == 1)
                {
                    deg++;
                }
            }
            // Trả về kết quả
            return deg;
        }
        // Bậc của các đỉnh, tham số là tên file để ghi kết quả
        public void DegVs(string filePath)
        {
            // Sử dụng đối tượng : StreamWriter sw = new StreamWriter(filePath);
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                // Duyệt từng đỉnh của đồ thị
                for (int i = 0; i < n; i++)
                {
                    //      Tính bậc của đỉnh i : DegVi(i);
                    int degree = DegVi(i);
                    sw.WriteLine($"Bậc của đỉnh {i}: {degree}");
                    Console.WriteLine($"Bậc của đỉnh {i}: {degree}");
                }

                //      Ghi vào file filePath và xuất lên màn hình theo yêu cầu
                // Đóng file
                sw.Close();
            }

        }
        #endregion

        #region Bài 2
        // 1. Tính bậc ra của đỉnh i
        public int DegOut(int i)
        {
            int degOut = 0;
            for (int j = 0; j < n; j++)
            {
                degOut += a[i, j];
            }
            return degOut;
        }
        // 2. Tính bậc vào của đỉnh j
        public int DegIn(int j)
        {
            int degIn = 0;
            for (int i = 0; i < n; i++)
            {
                degIn += a[i, j];
            }
            return degIn;
        }
        // 3. Xuất bậc vào bậc ra của các đỉnh theo yêu cầu
        public void DegInOut(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                for (int i = 0; i < n; i++)
                {
                    int degreeIn = DegIn(i);
                    int degreeOut = DegOut(i);

                    sw.WriteLine($"Đỉnh {i}: Bậc ra = {degreeOut}, Bậc vào = {degreeIn}");
                    Console.WriteLine($"Đỉnh {i}: Bậc ra = {degreeOut}, Bậc vào = {degreeIn}");
                }
            }
        }
        #endregion

    }
}
