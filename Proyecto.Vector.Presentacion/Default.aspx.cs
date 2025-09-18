using Proyecto.Vector.Datos;
using Proyecto.Vector.RN;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto.Vector.Presentacion
{
    public partial class _Default : Page
    {
        private VectorNegocio negocio = new VectorNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Reconstruir TextBox dinámicos en postback
            if (IsPostBack && ViewState["VectorSize"] != null)
            {
                int n = (int)ViewState["VectorSize"];
                GenerarCampos(n);
            }
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            panelCampos.Controls.Clear();

            if (!int.TryParse(txtN.Text.Trim(), out int n) || n <= 0)
            {
                lblMensaje.Text = "Error: Debes ingresar un número entero positivo para el tamaño del vector.";
                btnGuardar.Visible = false;
                return;
            }

            // Guardar tamaño en ViewState
            ViewState["VectorSize"] = n;

            // Crear TextBox dinámicos
            GenerarCampos(n);

            btnGuardar.Visible = true;
            lblMensaje.Text = $"Ingrese {n} números para el vector.";
        }

        private void GenerarCampos(int n)
        {
            panelCampos.Controls.Clear();
            for (int i = 0; i < n; i++)
            {
                TextBox txt = new TextBox { ID = "txtNum" + i };
                txt.Attributes.Add("placeholder", $"Número {i + 1}");
                panelCampos.Controls.Add(txt);
                panelCampos.Controls.Add(new LiteralControl("<br/>"));
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ViewState["VectorSize"] == null) return;

            int n = (int)ViewState["VectorSize"];
            List<int> numeros = new List<int>();

            for (int i = 0; i < n; i++)
            {
                TextBox txt = (TextBox)panelCampos.FindControl("txtNum" + i);
                if (txt == null || !int.TryParse(txt.Text.Trim(), out int valor))
                {
                    lblMensaje.Text = $"Error: el valor en la posición {i + 1} no es válido.";
                    return;
                }
                numeros.Add(valor);
            }

            VectorDatos vector = new VectorDatos(numeros.Count);
            negocio.Llenar(vector, numeros.ToArray());

            lblResultado.Text = $"({negocio.Mostrar(vector)})";
            lblMensaje.Text = $"Vector creado correctamente con {n} elementos.";
        }

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            if (ViewState["VectorSize"] == null)
            {
                lblEstadisticas.Text = "Error: No hay vector definido.";
                return;
            }

            int n = (int)ViewState["VectorSize"];
            List<int> numeros = new List<int>();

            for (int i = 0; i < n; i++)
            {
                TextBox txt = (TextBox)panelCampos.FindControl("txtNum" + i);
                if (txt == null || !int.TryParse(txt.Text.Trim(), out int valor))
                {
                    lblEstadisticas.Text = $"Error: el valor en la posición {i + 1} no es válido.";
                    return;
                }
                numeros.Add(valor);
            }

            if (numeros.Count == 0)
            {
                lblEstadisticas.Text = "Error: El vector está vacío.";
                return;
            }

            VectorDatos vector = new VectorDatos(numeros.Count);
            negocio.Llenar(vector, numeros.ToArray());

            double promedio = negocio.CalcularPromedio(vector);
            double desviacion = numeros.Count > 1 ? negocio.CalcularDesviacion(vector) : 0;

            lblEstadisticas.Text = $"Promedio: {promedio:F2} | Desviación estándar: {desviacion:F2}";

        }


        // Boton de Maximo MInimo
        protected void btnMaxMin_Click(object sender, EventArgs e)
        {
            if (ViewState["VectorSize"] == null)
            {
                lblMaxMin.Text = "Error: No hay vector definido.";
                return;
            }

            int n = (int)ViewState["VectorSize"];
            List<int> numeros = new List<int>();

            for (int i = 0; i < n; i++)
            {
                TextBox txt = (TextBox)panelCampos.FindControl("txtNum" + i);
                if (txt == null || !int.TryParse(txt.Text.Trim(), out int valor))
                {
                    lblMaxMin.Text = $"Error: el valor en la posición {i + 1} no es válido.";
                    return;
                }
                numeros.Add(valor);
            }

            VectorDatos vector = new VectorDatos(numeros.Count);
            negocio.Llenar(vector, numeros.ToArray());

            // Obtener máximo y mínimo
            var (maxValor, maxPosiciones) = negocio.CalcularMaximo(vector);
            var (minValor, minPosiciones) = negocio.CalcularMinimo(vector);

            lblMaxMin.Text = $"Máximo: {maxValor} | " + $"Mínimo: {minValor} ";

        }

    }
}
