using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class Form1 : Form
    {
        CN_Productos objetoCN = new CN_Productos();
        private string idProducto=null;
        private bool Editar = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MostrarProdctos();
        }

        private void MostrarProdctos() {

            CN_Productos objeto = new CN_Productos();
            DAT.DataSource = objeto.MostrarProd();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //INSERTAR
            if (Editar == false)
            {
                try
                {
                    objetoCN.InsertarPRod(txtNombre.Text, txtDesc.Text, txtMarca.Text, txtPrecio.Text, txtStock.Text);
                    MessageBox.Show("se inserto correctamente");
                    MostrarProdctos();
                    limpiarForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("no se pudo insertar los datos por: " + ex);
                }
            }
            //EDITAR
            if (Editar == true) {

                try
                {
                    objetoCN.EditarProd(txtNombre.Text, txtDesc.Text, txtMarca.Text, txtPrecio.Text, txtStock.Text,idProducto);
                    MessageBox.Show("se edito correctamente");
                    MostrarProdctos();
                    limpiarForm();
                    Editar = false;
                }
                catch (Exception ex) {
                    MessageBox.Show("no se pudo editar los datos por: " + ex);
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (DAT.SelectedRows.Count > 0)
                {
                    Editar = true;
                    txtNombre.Text = DAT.CurrentRow.Cells["Nombre"].Value.ToString();
                    txtMarca.Text = DAT.CurrentRow.Cells["Marca"].Value.ToString();
                    txtDesc.Text = DAT.CurrentRow.Cells["Descripcion"].Value.ToString();
                    txtPrecio.Text = DAT.CurrentRow.Cells["Precio"].Value.ToString();
                    txtStock.Text = DAT.CurrentRow.Cells["Stock"].Value.ToString();
                    idProducto = DAT.CurrentRow.Cells["Id"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("seleccione una fila por favor");
                }
            }catch(Exception err)
            {
                MessageBox.Show(Convert.ToString(err));
            }
        }

        private void limpiarForm() {
            txtDesc.Clear();
            txtMarca.Text = "";
            txtPrecio.Clear();
            txtStock.Clear();
            txtNombre.Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (DAT.SelectedRows.Count > 0)
                {
                    idProducto = DAT.CurrentRow.Cells["Id"].Value.ToString();
                    objetoCN.EliminarPRod(idProducto);
                    MessageBox.Show("Eliminado correctamente");
                    MostrarProdctos();
                }
                else
                    MessageBox.Show("seleccione una fila por favor");

            }catch(Exception er)
            {
                MessageBox.Show("No se encontraron elementos en la tabla");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
