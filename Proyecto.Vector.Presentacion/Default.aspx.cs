using Proyecto.Vector.Datos;
using Proyecto.Vector.RN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto.Vector.Presentacion
{
    public partial class _Default : Page
    {
        private VectorNegocio negocio = new VectorNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack && ViewState["VectorSize"] != null)
            {
                GenerarCampos((int)ViewState["VectorSize"]);
            }
        }

        private void GenerarCampos(int n)
        {
            panelCampos.Controls.Clear();
            for (int i = 0; i < n; i++)
            {
                TextBox txt = new TextBox { ID = "txtNum" + i, CssClass = "form-control d-inline-block w-auto" };
                txt.Attributes.Add("placeholder", $"Número {i + 1}");
                panelCampos.Controls.Add(txt);
                panelCampos.Controls.Add(new LiteralControl("&nbsp;"));
            }
        }

        private VectorDatos ObtenerVectorDeUI()
        {
            if (ViewState["VectorSize"] == null)
            {
                throw new InvalidOperationException("No se ha definido el tamaño del vector.");
            }
            int n = (int)ViewState["VectorSize"];
            List<int> numeros = new List<int>();
            for (int i = 0; i < n; i++)
            {
                TextBox txt = (TextBox)panelCampos.FindControl("txtNum" + i);
                if (txt == null || !int.TryParse(txt.Text.Trim(), out int valor))
                {
                    throw new FormatException($"El valor en la posición {i + 1} no es válido.");
                }
                numeros.Add(valor);
            }
            VectorDatos vector = new VectorDatos(n);
            negocio.Llenar(vector, numeros.ToArray());
            return vector;
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = string.Empty;
            lblEstadisticas.Text = string.Empty;
            panelCampos.Controls.Clear();
            btnGuardar.Visible = false;

            if (!int.TryParse(txtN.Text.Trim(), out int n) || n <= 0)
            {
                lblMensaje.Text = "Error: Debes ingresar un número entero positivo.";
                return;
            }

            ViewState["VectorSize"] = n;
            GenerarCampos(n);
            btnGuardar.Visible = true;
            lblMensaje.Text = $"Ingrese {n} números para el vector.";
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                VectorDatos vector = ObtenerVectorDeUI();
                Session["VectorPrincipal"] = vector;
                lblResultado.Text = negocio.Mostrar(vector);
                lblMensaje.Text = $"Vector creado y guardado.";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = $"Error: {ex.Message}";
            }
        }

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                VectorDatos vector = (VectorDatos)Session["VectorPrincipal"];
                if (vector == null) throw new InvalidOperationException("No hay vector principal guardado.");

                double promedio = negocio.CalcularPromedio(vector);
                double desviacion = vector.Elementos.Length > 1 ? negocio.CalcularDesviacion(vector) : 0;
                lblEstadisticas.Text = $"Promedio: {promedio:F2} | Desviación estándar: {desviacion:F2}";
            }
            catch (Exception ex)
            {
                lblEstadisticas.Text = $"Error: {ex.Message}";
            }
        }

        protected void btnMaxMin_Click(object sender, EventArgs e)
        {
            try
            {
                VectorDatos vector = (VectorDatos)Session["VectorPrincipal"];
                if (vector == null) throw new InvalidOperationException("No hay vector principal guardado.");

                var (maxValor, maxPosiciones) = negocio.CalcularMaximo(vector);
                var (minValor, minPosiciones) = negocio.CalcularMinimo(vector);
                lblEstadisticas.Text = $"Máximo: {maxValor} | Mínimo: {minValor} ";
            }
            catch (Exception ex)
            {
                lblEstadisticas.Text = $"Error: {ex.Message}";
            }
        }

        protected void btnPalindromo_Click(object sender, EventArgs e)
        {
            try
            {
                VectorDatos vector = (VectorDatos)Session["VectorPrincipal"];
                if (vector == null) throw new InvalidOperationException("No hay vector principal guardado.");

                bool esPalindromo = negocio.EsPalindromo(vector);
                lblEstadisticas.Text = esPalindromo
                    ? "✅ El vector es palíndromo."
                    : "❌ El vector NO es palíndromo.";
            }
            catch (Exception ex)
            {
                lblEstadisticas.Text = $"Error: {ex.Message}";
            }
        }

        protected void btnSumaAcumulativa_Click(object sender, EventArgs e)
        {
            try
            {
                VectorDatos vector = (VectorDatos)Session["VectorPrincipal"];
                if (vector == null) throw new InvalidOperationException("No hay vector principal guardado.");

                VectorDatos resultado = negocio.SumaAcumulativa(vector);
                lblEstadisticas.Text = $"Suma Acumulativa: ({negocio.Mostrar(resultado)})";
            }
            catch (Exception ex)
            {
                lblEstadisticas.Text = $"Error: {ex.Message}";
            }
        }

        protected void btnFrecuencia_Click(object sender, EventArgs e)
        {
            try
            {
                VectorDatos vector = (VectorDatos)Session["VectorPrincipal"];
                if (vector == null) throw new InvalidOperationException("No hay vector principal guardado.");

                Dictionary<int, int> frecuencias = negocio.FrecuenciaElementos(vector);
                string resultado = string.Join(", ", frecuencias.Select(kv => $"{kv.Key}: {kv.Value}"));
                lblEstadisticas.Text = $"Frecuencia de Elementos: [{resultado}]";
            }
            catch (Exception ex)
            {
                lblEstadisticas.Text = $"Error: {ex.Message}";
            }
        }
    }
}