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
            ArticuloNegocio Negocio = new ArticuloNegocio();
            try
            {
                if (articulo == null) 
                    articulo = new Articulo();

                articulo.Codigo = codigoTxt.Text;
                articulo.Nombre = nombreTxt.Text;
                articulo.Descripcion = descripcionTxt.Text;
                articulo.Precio = decimal.Parse(precioTxt.Text);
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;
                articulo.Imagenes[0].Url = urlImagenTxt.Text;
                cargarImagen(urlImagenTxt.Text);


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
                urlImagenTxt.Text = articulo.Imagenes[0].Url;
                cargarImagen(articulo.Nombre);
            }



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
