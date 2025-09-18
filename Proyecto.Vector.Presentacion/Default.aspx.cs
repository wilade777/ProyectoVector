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
            // Código de inicialización si es necesario
        }

        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Leer la entrada del usuario
                string input = txtVector.Text;

                // 2. Parsear la cadena de texto a un arreglo de enteros
                int[] elementos = input.Split(',')
                                     .Select(s => int.Parse(s.Trim()))
                                     .ToArray();

                // 3. Crear una instancia del objeto de datos (VectorDatos)
                VectorDatos vector = new VectorDatos(elementos.Length);

                // 4. Usar la capa de negocio para llenar el vector
                negocio.Llenar(vector, elementos);

                // 5. Usar la capa de negocio para obtener la representación en texto
                string resultado = negocio.Mostrar(vector);

                // 6. Mostrar el resultado en la etiqueta
                lblResultado.Text = $"({resultado})";
            }
            catch (FormatException)
            {
                lblResultado.Text = "Error: El formato de entrada no es válido. Asegúrate de usar números separados por comas.";
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción (ej. si la capa de negocio lanza una excepción)
                lblResultado.Text = $"Error: {ex.Message}";
            }
        }


    }
}