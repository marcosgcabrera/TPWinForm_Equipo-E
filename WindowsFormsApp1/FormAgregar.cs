using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelo;
using Negocio;

namespace WindowsFormsApp1
{
    public partial class FormAgregar : Form
    {
        public FormAgregar()
        {
            InitializeComponent();
        }

        private void botonAceptar_Click(object sender, EventArgs e)
        {
            Articulo nuevoArticulo = new Articulo();
            ArticuloNegocio Negocio = new ArticuloNegocio();
            try
            {
                nuevoArticulo.Codigo = codigoTxt.Text;
                nuevoArticulo.Nombre = nombreTxt.Text;
                nuevoArticulo.Descripcion = descripcionTxt.Text;
                nuevoArticulo.Precio = decimal.Parse(precioTxt.Text);
           

                Negocio.agregar(nuevoArticulo);
                MessageBox.Show("Articulo Agregado!");
                Close();

            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FormAgregar_Load(object sender, EventArgs e)//hacemos llamado a las funciones para listar las opciones desplegables/ estas mismas se encuentran cada una en sus clases
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            cboMarca.DataSource = marcaNegocio.listar();
            cboCategoria.DataSource = categoriaNegocio.listar();

        }
    }
}
