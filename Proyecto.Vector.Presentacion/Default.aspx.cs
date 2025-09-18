using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Proyecto.Vector.Datos;
using Proyecto.Vector.RN;

namespace Proyecto.Vector.Presentacion
{
    public partial class _Default : Page
    {
        private VectorNegocio negocio = new VectorNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Reconstruir los TextBox dinámicos en cada postback
            if (IsPostBack && ViewState["VectorSize"] != null)
            {
                int n = (int)ViewState["VectorSize"];
                GenerarCampos(n);
            }
        }

        // CA1 + CA2 + CA5: Solicitar tamaño y validar
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

            // Generar TextBox dinámicos
            GenerarCampos(n);

            btnGuardar.Visible = true;
            lblMensaje.Text = $"Ingrese {n} números para el vector.";
        }

        // Genera los campos dinámicos
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

        // CA3 + CA4: Guardar elementos en VectorDatos
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

            // Almacenar en VectorDatos
            VectorDatos vector = new VectorDatos(numeros.Count);
            negocio.Llenar(vector, numeros.ToArray());

            // Mostrar resultado
            lblResultado.Text = $"({negocio.Mostrar(vector)})";
            lblMensaje.Text = $"Vector creado correctamente con {n} elementos.";
        }
    }
}
