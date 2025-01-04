using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Globalization;
using Modelo;
using System.Net;
using System.Security.Policy;
using System.Data.SqlTypes;



namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, M.Descripcion AS Marca, C.Descripcion AS Categoria, \r\nMIN(I.ImagenUrl) AS Imagen\r\nFROM ARTICULOS A \r\nLEFT JOIN MARCAS M ON A.IdMarca = M.Id\r\nLEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id\r\nLEFT JOIN IMAGENES I ON A.Id = I.IdArticulo\r\nGROUP BY A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, M.Descripcion, C.Descripcion;");
                datos.getearConsulta();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Marca = new Marca();
                    if (!(datos.Lector["Marca"] is DBNull))
                        aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    if (!(datos.Lector["Categoria"] is DBNull))
                        aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    aux.Imagenes = new List<Imagen>();
                    Imagen imagen = new Imagen();
                    if (!(datos.Lector["Imagen"] is DBNull))
                        imagen.Url = (string)datos.Lector["Imagen"];
                    aux.Imagenes.Add(imagen);

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {

                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, Precio, IdMarca, IdCategoria) VALUES ('" + nuevo.Codigo + "','" + nuevo.Nombre + "','" + nuevo.Descripcion + "'," + nuevo.Precio + ", @idMarca, @idCategoria)");// sql "insert"
                datos.setearParametroDesp("@idMarca", nuevo.Marca.Id);
                datos.setearParametroDesp("@idCategoria", nuevo.Categoria.Id);


                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar(Articulo arti)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update ARTICULOS set  Codigo = '@codigo', Nombre = '@nombre', Descripcion = '@descripcion', IdMarca = @idmarca, IdCategoria =@idcategoria, Precio = @precio Where Id = @id");
                datos.setearParametro("@codigo", arti.Codigo);
                datos.setearParametro("@nombre", arti.Nombre);
                datos.setearParametro("@descripcion", arti.Descripcion);
                datos.setearParametro("@idmarca", arti.Marca.Id.ToString());
                datos.setearParametro("@idcategoria", arti.Categoria.Id.ToString());
                datos.setearParametro("@precio", arti.Precio.ToString());//arreglar xq me da error el decimal
                datos.setearParametro("@id", arti.Id.ToString());

                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }


        }

        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from ARTICULOS where id = @id");
                datos.setearParametro("@id",id.ToString());
                datos.ejecutarAccion();
            }
            catch (Exception ex) { throw ex; }
        }

        public void eliminar()
        {
            throw new NotImplementedException();
        }
    }
}