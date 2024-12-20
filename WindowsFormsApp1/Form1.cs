using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Modelo;
<<<<<<< HEAD
using System.Security.Policy;
=======
>>>>>>> 6fe681de58c22850f3b81e59fff08edf446af0c0

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
<<<<<<< HEAD
        private List<Articulo> ListaArticulos;
=======
>>>>>>> 6fe681de58c22850f3b81e59fff08edf446af0c0
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
<<<<<<< HEAD
            ListaArticulos = negocio.listar();
            dataGridView1.DataSource = ListaArticulos;//guardamos los datos en el atrributo lista privada
            cargarImagen(ListaArticulos[0].Imagenes[0].Url);

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)//muestra la imagen del articulo seleccionado
        {
           Articulo Seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;
            cargarImagen(Seleccionado.Imagenes[0].Url);
        }

        private void cargarImagen(string imagen) //
        {
            try
            {
                pbArticulo.Load(imagen);
            }
            catch (Exception ex)
            {
                pbArticulo.Load("https://doc24.com.ar/wp-content/uploads/2023/10/placeholder-2-1.png");
            }
=======
            dataGridView1.DataSource = negocio.listar();
>>>>>>> 6fe681de58c22850f3b81e59fff08edf446af0c0
        }
    }
}
