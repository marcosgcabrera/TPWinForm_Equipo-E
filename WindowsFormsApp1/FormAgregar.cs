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

        private void precioTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar)&& e.KeyChar != (char)8) //esto hace que rechace characteres que no sean numeros
            {
                e.Handled = true;
            }
        }

        private void urlImagenTxt_Leave(object sender, EventArgs e)
        {
            cargarImagen(urlImagenTxt.Text);
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
    }
}
