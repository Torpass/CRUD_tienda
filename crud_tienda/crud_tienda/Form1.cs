using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace crud_tienda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string codigo = txtCodigo.Text;
                string nombre = txtNombre.Text;
                string descripcion = txtDescripcion.Text;
                double price = double.Parse(txtPrice.Text);
                int existencias = int.Parse(txtExistencias.Text);

                if (codigo != "" && nombre != "" && descripcion != "" && price != 0 && existencias < 0) 
                {


                    string sql = "INSERT INTO productos (codigo, nombre, precio_publico, descripcion, existencias) VALUES ('" + codigo + "', '" + nombre + "', '" + price + "', '" + descripcion + "', '" + existencias + "')";
                    MySqlConnection ConexionBD = Conexion.conexion();
                    ConexionBD.Open();

                    try
                    {
                        MySqlCommand comando = new MySqlCommand(sql, ConexionBD);
                        comando.ExecuteNonQuery();
                        MessageBox.Show("Registro guardado");
                        limpiar();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Error al guardar {ex.Message}");
                    }
                    finally
                    {
                        ConexionBD.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Completa todos los campos de manera adecuada");
                }

            }
            catch (FormatException fex)
            {
                MessageBox.Show($"Error, datos invalidos: {fex.Message}");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigo.Text;
            MySqlDataReader reader = null;

            string sql = "SELECT id, codigo, nombre, precio_publico, descripcion, existencias FROM productos WHERE codigo LIKE '" + codigo + "' LIMIT 1";
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtId.Text = reader.GetString(0);
                        txtNombre.Text = reader.GetString(2);
                        txtPrice.Text = reader.GetString(3);
                        txtExistencias.Text = reader.GetString(5);
                        txtDescripcion.Text = reader.GetString(4 );
                    }
                }
                else
                {
                    MessageBox.Show("No hay nada compai");
                }
            }
            catch (MySqlException ex )
            {
                MessageBox.Show($"Error {ex.Message}");
            }
            finally { conexionBD.Close(); }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            string codigo = txtCodigo.Text;
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            double price = double.Parse(txtPrice.Text);
            int existencias = int.Parse(txtExistencias.Text);

            string sql = "UPDATE productos SET codigo = '" + codigo + "', nombre = '" + nombre + "', precio_publico = '" + price + "', descripcion = '" + descripcion + "', existencias = '" + existencias + "' WHERE id = '"+ id +"'";
            MySqlConnection ConexionBD = Conexion.conexion();
            ConexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, ConexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro actualizado");
                limpiar();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error al actualizar {ex.Message}");
            }
            finally
            {
                ConexionBD.Close();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;


            string sql = "DELETE FROM productos WHERE id = '" + id + "'";
            MySqlConnection ConexionBD = Conexion.conexion();
            ConexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, ConexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro Eliminado");
                limpiar();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error al Eliminar {ex.Message}");
            }
            finally
            {
                ConexionBD.Close();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        private void limpiar()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrice.Text = "";
            txtCodigo.Text = "";
            txtExistencias.Text = "";
        }
    }
}
