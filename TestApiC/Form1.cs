using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Threading;

namespace TestApiC
{
    public partial class Form1 : Form
    {
        static string nombre = "";
        static string precio = "";

        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            GetApiDataAsync(textBox1.Text);
            
            

        }
        private static readonly HttpClient client = new HttpClient();

    

        private static async Task GetApiDataAsync(String codigo)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost/apis/productos.php?codigo=" + codigo);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);

                Producto producto = JsonConvert.DeserializeObject<Producto>(responseBody);

                //MessageBox.Show(responseBody + "\n" + producto.nombre_producto + "\n" + producto.precio);

                label2.Text = "Resultado: " + producto.nombre_producto + " " + producto.precio;

                richTextBox1.Clear();
                richTextBox1.AppendText("Nombre: " + producto.nombre_producto + "\n" + "Precio: " + producto.precio);

                dataGridView1.Rows.Clear();
                dataGridView1.Rows.Add(producto.nombre_producto, producto.precio);


            }
            catch (HttpRequestException e)
            {
                //Interpolacion
                Console.WriteLine($"Request error: {e.Message}");
            }
        }

    }
    public class Producto
    {
        public string status { get; set; }
        public string nombre_producto { get; set; }
        public decimal precio { get; set; }
    }
}
