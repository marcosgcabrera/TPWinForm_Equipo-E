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
        private Articulo articulo = null;
        public FormAgregar()
        {
            InitializeComponent();
        }
        public FormAgregar(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }
        private void botonAceptar_Click(object sender, EventArgs e)
        {
            //Articulo nuevoArticulo = new Articulo();
            ArticuloNegocio Negocio = new ArticuloNegocio();
            try
            {
                if (articulo == null) 
                    articulo = new Articulo();

                articulo.Codigo = codigoTxt.Text;
                articulo.Nombre = nombreTxt.Text;
                articulo.Descripcion = descripcionTxt.Text;
                articulo.Precio = decimal.Parse(precioTxt.Text);


                if (articulo.Id == 0)

                {

                    Negocio.agregar(articulo);//agrego
                    MessageBox.Show("Articulo Agregado!");
                    
                }
                else
                {
                    Negocio.modificar(articulo);//modifico
                    MessageBox.Show(" Articulo Modificado!");
                    
                }

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
            cboMarca.ValueMember = "Id";
            cboMarca.DisplayMember = "Descripcion";
            cboCategoria.DataSource = categoriaNegocio.listar();
            cboCategoria.ValueMember = "Id";
            cboCategoria.DisplayMember = "Descripcion";

            if (articulo != null)
            {
                codigoTxt.Text = articulo.Codigo;
                nombreTxt.Text = articulo.Nombre;
                descripcionTxt.Text = articulo.Descripcion;
                precioTxt.Text = articulo.Precio.ToString();
                cboMarca.SelectedValue = articulo.Marca.Id;
                cboCategoria.SelectedValue = articulo.Categoria.Id;
                //imagentxt.Text = articulo.UrlImagen;
                //cargarImagen(articulo.Nombre);
            }


        }
    }
}
