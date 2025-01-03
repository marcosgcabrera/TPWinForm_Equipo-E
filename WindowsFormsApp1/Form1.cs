﻿using System;
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
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            FormAgregar alta = new FormAgregar();
            alta.ShowDialog();

        }


        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;
            FormAgregar modificar = new FormAgregar(seleccionado);
            modificar.ShowDialog();
        }
    }
}
