using System;
using System.Windows.Forms;
using System.Drawing;

namespace Otnoshenia
{
    public partial class Form1 : Form
    {
        static double[,] matrixA = new double[,]
        {
            {0.3, 0.6, 0.4, 0.9, 0.3, 0.1 },
            {0.1, 0.7, 0.0, 0.5, 0.1, 0.3 },
            {1.0, 0.2, 1.0, 0.8, 0.4, 0.5 },
            {0.5, 0.4, 0.7, 0.6, 1.0, 0.0 },
            {0.3, 0.6, 0.4, 0.9, 0.3, 0.1 },
            {1.0, 0.2, 1.0, 0.8, 0.4, 0.5 },
        };

        static double[,] matrixB = new double[,]
        {
            {0.7, 0.3, 0.2, 0.0, 0.3, 0.3 },
            {0.8, 0.6, 0.4, 0.9, 0.1, 1.0 },
            {0.6, 0.2, 0.1, 0.1, 0.5, 0.3 },
            {0.0, 0.3, 0.6, 0.5, 0.2, 0.7 },
            {0.7, 0.3, 0.2, 0.0, 0.3, 0.3 },
            {0.6, 0.2, 0.1, 0.1, 0.5, 0.3 },
        };

        static int N = 4;
        public Form1()
        {
            InitializeComponent();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            int operation = comboBox1.SelectedIndex;
            if(operation == -1) { label4.ForeColor = Color.Red; MessageBox.Show("Выберите операцию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            try 
            {
                double[,] A = new double[N, N];
                double[,] B = new double[N, N];

                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        if (Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value) < 0 || Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value) > 1)
                        { MessageBox.Show($"В ячейке {i}х{j} таблицы А стоит некорректное значение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                        A[i, j] = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value);
                        if (Convert.ToDouble(dataGridView2.Rows[i].Cells[j].Value) < 0 || Convert.ToDouble(dataGridView2.Rows[i].Cells[j].Value) > 1)
                        { MessageBox.Show($"В ячейке {i}х{j} таблицы В стоит некорректное значение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                        B[i, j] = Convert.ToDouble(dataGridView2.Rows[i].Cells[j].Value);
                    }
                }
                switch (operation)
                {
                    case 0:
                        double[,] C = Union(A, B);
                        for (int i = 0; i < N; i++)
                        {
                            for (int j = 0; j < N; j++)
                            {
                                dataGridView3.Rows[i].Cells[j].Value = C[i, j].ToString();
                            }
                        }
                        break;

                    case 1:
                        C = Intersec(A, B);
                        for (int i = 0; i < N; i++)
                        {
                            for (int j = 0; j < N; j++)
                            {
                                dataGridView3.Rows[i].Cells[j].Value = C[i, j].ToString();
                            }
                        }
                        break;

                    case 2:
                        C = Extension(A);
                        for (int i = 0; i < N; i++)
                        {
                            for (int j = 0; j < N; j++)
                            {
                                dataGridView3.Rows[i].Cells[j].Value = C[i, j].ToString();
                            }
                        }
                        break;

                    case 3:
                        C = Extension(B);
                        for (int i = 0; i < N; i++)
                        {
                            for (int j = 0; j < N; j++)
                            {
                                dataGridView3.Rows[i].Cells[j].Value = C[i, j].ToString();
                            }
                        }
                        break;

                    case 4:
                        C = Composition(A, B);
                        for (int i = 0; i < N; i++)
                        {
                            for (int j = 0; j < N; j++)
                            {
                                dataGridView3.Rows[i].Cells[j].Value = C[i, j].ToString();
                            }
                        }
                        break;
                }
            }
           catch(FormatException) { MessageBox.Show("Неверный тип введённых данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
        }

        static double[,] Union(double[,] A, double[,] B)
        {
            int N = A.GetLength(0);
            double[,] C = new double[N, N];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (A[i, j] >= B[i, j])
                        C[i, j] = A[i, j];
                    else
                        C[i, j] = B[i, j];
                }
            }
            return C;
        }

        static double[,] Intersec(double[,] A, double[,] B)
        {
            int N = A.GetLength(0);
            double[,] C = new double[N, N];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (A[i, j] <= B[i, j])
                        C[i, j] = A[i, j];
                    else
                        C[i, j] = B[i, j];
                }
            }
            return C;
        }

        static double[,] Extension(double[,] A)
        {
            int N = A.GetLength(0);
            double[,] C = new double[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    C[i, j] = 1 - A[i, j];
                }
            }
            return C;
        }

        static double[,] Composition(double[,] A, double[,] B)
        {
            double[] min = new double[N];
            double[,] C = new double[N, N];

            for (int i = 0; i < N; i++)
            {
                
                for (int j = 0; j < N; j++)
                {
                    for (int k = 0; k < N; k++) 
                    {
                        if (A[i, k] < B[k, j])
                            min[k] = A[i, k];
                        else
                            min[k] = B[k, j];

                        if (min[k] > C[i, j])
                            C[i, j] = min[k];
                    }
                }
            }
            return C;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.RowCount = N;
            dataGridView1.ColumnCount = N;
            dataGridView2.RowCount = N;
            dataGridView2.ColumnCount = N;
            dataGridView3.RowCount = N;
            dataGridView3.ColumnCount = N;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = matrixA[i, j].ToString();
                    dataGridView2.Rows[i].Cells[j].Value = matrixB[i, j].ToString();
                }
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label4.ForeColor = Color.White;
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            int dim = domainUpDown1.SelectedIndex;
            if (dim != -1)
            {
                N = dim + 2;
                dataGridView1.RowCount = N;
                dataGridView1.ColumnCount = N;
                dataGridView2.RowCount = N;
                dataGridView2.ColumnCount = N;
                dataGridView3.RowCount = N;
                dataGridView3.ColumnCount = N;
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = matrixA[i, j].ToString();
                        dataGridView2.Rows[i].Cells[j].Value = matrixB[i, j].ToString();
                    }
                }
            }
        }
    }
}
