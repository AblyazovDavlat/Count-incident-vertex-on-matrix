using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RoadCountApp
{
    public partial class SnowflakeForm : Form
    {
        public SnowflakeForm()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            string filePath;
            string[] readFile;
            ByteMatrix byteMatrix;
            int resultCount;

            if (inputTextBox.Text.Length != 0)
            {
                filePath = inputTextBox.Text;
            }
            else
            {
                Console.WriteLine("Please. enter path ro input file");
                return;
            }

            if (File.Exists(filePath))
            {
                readFile = File.ReadAllLines(filePath);
            }
            else
            {
                Console.WriteLine("File is not exist");
                return;
            }

            byteMatrix = new ByteMatrix(Int32.Parse(readFile[0]));

            //Parse array of strings into matrix
            for (int i = 1; i < readFile.Length; i++)
            {
                byteMatrix.matrix[i-1] = readFile[i].Split(' ').ToArray().Select(j => Byte.Parse(j.ToString())).ToArray();
            }

            resultCount = CountNumberRoads(byteMatrix);
            File.WriteAllText(Path.GetDirectoryName(filePath) + "\\output.txt" ,resultCount.ToString());
            startButton.Text = "Done";
        }

        //ByteMatrix byteMatrix - NxN, nodes values: 0 or 1
        //int count - number roads in matrix
        //Input matrix is symmetrical, because count number of ones in the right triangle of the matrix.
        private int CountNumberRoads(ByteMatrix byteMatrix)
        {
            int count = 0;

            for (int i = 0; i < byteMatrix.N; i++)
            {
                for (int j = i; j < byteMatrix.N; j++)
                {
                    count += byteMatrix.matrix[i][j];
                }
            }
            return count;
        }
    }

    public class ByteMatrix
    {
        public byte[][] matrix;
        public int N;

        public ByteMatrix()
        {
            matrix = new byte[0][];
            this.N = 0;
        }

        public ByteMatrix(int N)
        {
            matrix = new byte[N][];
            this.N = N;
        }
    }
}
