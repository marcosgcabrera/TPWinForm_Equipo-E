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
using System.Security.Policy;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private List<Articulo> ListaArticulos;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();
        }

       

        private void ocultarColumnas()
        {
            if(dataGridView1.Columns.Contains("Imagen")) //Se agrega este if para asegurarnos de que la columna exista andes de querer ocultar
            dataGridView1.Columns["Imagen"].Visible = false;

            dataGridView1.Columns["Id"].Visible =false;
        }
 

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)//muestra la imagen del articulo seleccionado
        {
            if (dataGridView1.CurrentRow != null) //agregamos esta verificación para asegurarnos de haya un elemento disponible para seleccionar en dicha posicion
            {
                Articulo Seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;
                cargarImagen(Seleccionado.Imagenes[0].Url);
            }
          
        }

        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                ListaArticulos = negocio.listar();
                dataGridView1.DataSource = ListaArticulos;//guardamos los datos en el atrributo lista privada
                cargarImagen(ListaArticulos[0].Imagenes[0].Url);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            FormAgregar alta = new FormAgregar();
            alta.ShowDialog();
            cargar();

        }


        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;
            FormAgregar modificar = new FormAgregar(seleccionado);
            modificar.ShowDialog();
            cargar();
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada; //creo variable vacia

            string filtro = filtroTbo.Text;

            if (filtro != "")
            {
                listaFiltrada = ListaArticulos.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Categoria.Descripcion.ToUpper().Contains(filtro.ToUpper()));  //devuelve lista y filtra por nombre o categoria, Contains verifica si lo que viene en filtro esta contenido en el nombre que esta analizando.

            }
            else
            {
                listaFiltrada = ListaArticulos;
            }


            dataGridView1.DataSource = null;//limpio/datagridview es la lista que permite mostrar la informacion de la base de datos
            dataGridView1.DataSource = listaFiltrada;
            ocultarColumnas();
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio articulo = new ArticuloNegocio();
            Articulo seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("Queres eliminarlo?", "Eliminado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {

                    seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;
                    articulo.eliminar(seleccionado.Id);
                    cargar();
                }

            }
            catch (Exception ex)
            {

               MessageBox.Show(ex.ToString());
            }


        }
    }
}
