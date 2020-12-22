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

namespace clasificador_flores
{
    public partial class Form1 : Form
    {

        string path;
        string linea;
        string[] secciones;
        string[] flores = { "setosa-versicolor", "virginica-versicolor", "setosa-virginica" };
        int contador = 0;
        int filas = 0;
        StreamReader txt;
        List<string> opciones = new List<string> { "Iris-setosa", "Iris-versicolor", "Iris-virginica" };


        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(flores);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            listBox1.Items.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.path = openFileDialog.FileName;
                }
                catch
                {
                    throw;
                }
            }
            txt = File.OpenText(path);
            Console.WriteLine(txt.GetType());

            while((linea=txt.ReadLine())!=null)
            {
                secciones = linea.Split(',');
                string list="";
                for(int i=0;i<secciones.Length;i++)
                {
                    list += secciones[i] + " ";
                }
                listBox1.Items.Add(list);
                


            }

            txt.Close();
                
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            columnas();
            salidas();


            if (comboBox1.SelectedItem.ToString().Equals(flores[0]))
            {
                data(opciones[0], 0);
                data(opciones[1], 1);
            
            
            }


            if (comboBox1.SelectedItem.ToString().Equals(flores[1]))
            {
                data(opciones[1], 0);
                data(opciones[2], 1);


            }


            if (comboBox1.SelectedItem.ToString().Equals(flores[2]))
            {
                data(opciones[0], 0);
                data(opciones[2], 1);


            }
            contador++;








        }

        public void columnas()
        {
            for (int i = 1; i <=4; i++)
            {
                DataGridViewTextBoxColumn entradas = new DataGridViewTextBoxColumn();
                entradas.HeaderText = "X" + i.ToString();
                entradas.Width = 50;
                dataGridView1.Columns.Add(entradas);



            }
        }

        public void salidas()
        {
            DataGridViewTextBoxColumn salidaEsperada = new DataGridViewTextBoxColumn();
            salidaEsperada.HeaderText = "Yesp";
            salidaEsperada.Width = 50;
            dataGridView1.Columns.Add(salidaEsperada);

            DataGridViewTextBoxColumn salidaCalculada = new DataGridViewTextBoxColumn();
            salidaCalculada.HeaderText = "Ycalc";
            salidaCalculada.Width = 50;
            dataGridView1.Columns.Add(salidaCalculada);

        }

        public void data(string flor,int i)
        {

            Console.WriteLine("entrando a data");
            txt = File.OpenText(this.path);
            Console.WriteLine("Agregada primera opcion");
            int k = 0;
            
            Console.WriteLine(flor[i]);
            while ((linea = this.txt.ReadLine()) != null)
            {
                secciones = linea.Split(',');
                Console.WriteLine("Leyendo linea");

                if (secciones.Contains(flor))
                {
                    Console.WriteLine("Escribiendo");
                    dataGridView1.Rows.Add();
                    if(i==1)
                    {
                        k += 50;
                    }
                    for (int j = 0; j < secciones.Length; j++)
                    {
                        this.dataGridView1.Rows[k].Cells[j].Value = (secciones[j]);
                        
                        

                    }

                    if(i==0)
                    {
                        this.dataGridView1.Rows[k].Cells[4].Value = "1.0";
                    }
                    else
                    {
                        this.dataGridView1.Rows[k].Cells[4].Value = "-1.0";
                    }
                    if (i == 1)
                    {
                        k -= 50;
                    }
                    k++;
                    filas++;
                }

                Console.WriteLine(filas);
                    


            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(contador==0)
            {
                label1.Text = "Opcion no seleccionada";
            }
            else
            {
                var pesos = primeros_pesos(4);
                listBox2.Items.Clear();
                System.Console.WriteLine("\n");
                var entradas = x(4);
                string valor;
                perceptron(4, entradas, pesos);
            }
        }

        public static float[] primeros_pesos(int no_entradas)
        {
            
            Random random = new Random();
            float[] array = new float[no_entradas + 1];

            for (int i = 0; i <= no_entradas; i++)
            {
                array[i] = (float)(random.Next(0, 10) / 10.0);

            }

            return array;
        }


        public float[,] x(int no_entradas)
        {
            float[,] entradas = new float[100, no_entradas + 1];
            string valor;

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j <= no_entradas; j++)
                {
                    valor = this.dataGridView1.Rows[i].Cells[j].Value.ToString();
                    entradas[i, j] = float.Parse(valor);

                }

            }


            return entradas;

        }

        public void perceptron(int no_entradas, float[,] entradas, float[] pesos)
        {
            float acumulador = 0;
            int contador = 0;
            float resultado = 0;
            bool flag = true;
            while (contador < 500)
            {
                listBox2.Items.Add("Epoca: " + contador.ToString());
                listBox2.Items.Add("\n\n");
                listBox2.Items.Add("W1= " + pesos[0].ToString());
                listBox2.Items.Add("W2= " + pesos[1].ToString());
                listBox2.Items.Add("W3= " + pesos[2].ToString());
                listBox2.Items.Add("W4= " + pesos[3].ToString());
                
                listBox2.Items.Add("Umbral= " + pesos[4].ToString());
                listBox2.Items.Add("\n");
                for (int i = 0; i < 100; i++)
                {
                    acumulador = 0;
                    
                    
                    

                    for (int j = 0; j <= no_entradas; j++)
                    {

                        acumulador += pesos[j] * (float)(entradas[i, j]);
                    }

                    if (acumulador > 0)
                    {
                        resultado = 1;
                        dataGridView1.Rows[i].Cells[no_entradas + 1].Value = resultado.ToString();
                    }
                    else
                    {
                        resultado = -1;
                        dataGridView1.Rows[i].Cells[no_entradas + 1].Value = resultado.ToString();
                    }

                    if (resultado != entradas[i, no_entradas])
                    {
                        pesos = aprendizaje(no_entradas, i, pesos, entradas);
                        Console.WriteLine("Mal clasificado");
                        flag = false;
                    }



                }
                contador++;
                if (flag)
                {
                    
                    break;
                }
                flag = true;
            }

        }

        public float[] aprendizaje(int no_entradas, int index, float[] pesos, float[,] entradas)
        {
            float delta = 0;
            for (int i = 0; i < no_entradas; i++)
            {
                delta = 0;
                delta = entradas[index, i] * entradas[index, no_entradas];
                pesos[i] = delta + pesos[i];
            }

            pesos[no_entradas] = entradas[index, no_entradas] + pesos[no_entradas];
            return pesos;
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

        }
    }
}

