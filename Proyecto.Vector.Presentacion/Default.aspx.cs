using Proyecto.Vector.Datos;
using Proyecto.Vector.RN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto.Vector.Presentacion
{
    public partial class _Default : Page
    {
        private VectorNegocio negocio = new VectorNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Oculta el panel de elementos en la primera carga de la página
            if (!IsPostBack)
            {
                pnlIngresarElementos.Visible = false;
            }
        }

        protected void btnDefinirTamano_Click(object sender, EventArgs e)
        {
            try
            {
                int n = int.Parse(txtTamano.Text);
                if (n <= 0)
                {
                    lblResultado.Text = "Error: El tamaño del vector debe ser un número positivo.";
                    return;
                }

                // Guarda el tamaño en la sesión para usarlo después
                Session["VectorSize"] = n;

                // Oculta el primer panel y muestra el segundo
                pnlDefinirTamano.Visible = false;
                pnlIngresarElementos.Visible = true;
                lblResultado.Text = string.Empty; // Limpia el mensaje de error
            }
            catch (FormatException)
            {
                lblResultado.Text = "Error: Por favor, ingrese un número válido.";
            }
        }

        protected void btnMostrarVector_Click(object sender, EventArgs e)
        {
            try
            {
                // Recupera el tamaño del vector de la sesión
                int n = (int)Session["VectorSize"];

                // 1. Leer la entrada del usuario
                string input = txtElementos.Text;

                // 2. Parsear la cadena de texto a un arreglo de enteros
                int[] elementos = input.Split(',')
                                       .Select(s => int.Parse(s.Trim()))
                                       .ToArray();

                // Validar que la cantidad de elementos coincida con N
                if (elementos.Length != n)
                {
                    lblResultado.Text = $"Error: El vector debe tener {n} elementos, pero se ingresaron {elementos.Length}.";
                    return;
                }

                // 3. Crear una instancia del objeto de datos (VectorDatos)
                VectorDatos vector = new VectorDatos(n);

                // 4. Usar la capa de negocio para llenar el vector
                negocio.Llenar(vector, elementos);

                // 5. Usar la capa de negocio para obtener la representación en texto
                string resultado = negocio.Mostrar(vector);

                // 6. Mostrar el resultado en la etiqueta
                lblResultado.Text = $"Vector: ({resultado})";
            }
            catch (FormatException)
            {
                lblResultado.Text = "Error: El formato de entrada no es válido. Asegúrate de usar números separados por comas.";
            }
            catch (Exception ex)
            {
                lblResultado.Text = $"Error: {ex.Message}";
            }
        }
    }
}