using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Proyecto.Vector.Datos;
using Proyecto.Vector.RN;

namespace Proyecto.Vector.Presentacion
{
    public partial class AnalisisAvanzado : Page
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

        // Nuevas funciones para análisis avanzado

        protected void btnSubvectorCreciente_Click(object sender, EventArgs e)
        {
            try
            {
                VectorDatos vector = (VectorDatos)Session["VectorPrincipal"];
                if (vector == null) throw new InvalidOperationException("No hay vector principal guardado.");

                int longitud = negocio.SubvectorCrecienteMasLargo(vector);
                lblEstadisticas.Text = $"Longitud del subvector creciente más largo: {longitud}";
            }
            catch (Exception ex)
            {
                lblEstadisticas.Text = $"Error: {ex.Message}";
            }
        }

        protected void btnSegundoMayor_Click(object sender, EventArgs e)
        {
            try
            {
                VectorDatos vector = (VectorDatos)Session["VectorPrincipal"];
                if (vector == null) throw new InvalidOperationException("No hay vector principal guardado.");

                int segundoMayor = negocio.SegundoValorMayor(vector);
                lblEstadisticas.Text = $"El segundo valor mayor es: {segundoMayor}";
            }
            catch (Exception ex)
            {
                lblEstadisticas.Text = $"Error: {ex.Message}";
            }
        }

        protected void btnConteoSecuencias_Click(object sender, EventArgs e)
        {
            try
            {
                VectorDatos vector = (VectorDatos)Session["VectorPrincipal"];
                if (vector == null) throw new InvalidOperationException("No hay vector principal guardado.");

                int conteo = negocio.ConteoSecuenciasIguales(vector);
                lblEstadisticas.Text = $"Número de secuencias de elementos iguales: {conteo}";
            }
            catch (Exception ex)
            {
                lblEstadisticas.Text = $"Error: {ex.Message}";
            }
        }

        protected void btnReemplazoFrecuencias_Click(object sender, EventArgs e)
        {
            try
            {
                VectorDatos vector = (VectorDatos)Session["VectorPrincipal"];
                if (vector == null) throw new InvalidOperationException("No hay vector principal guardado.");

                VectorDatos vectorResultado = negocio.ReemplazoPorFrecuencias(vector);
                lblEstadisticas.Text = $"Vector con reemplazo por frecuencias: {negocio.Mostrar(vectorResultado)}";
            }
            catch (Exception ex)
            {
                lblEstadisticas.Text = $"Error: {ex.Message}";
            }
        }

        protected void btnBalancePosNeg_Click(object sender, EventArgs e)
        {
            try
            {
                VectorDatos vector = (VectorDatos)Session["VectorPrincipal"];
                if (vector == null) throw new InvalidOperationException("No hay vector principal guardado.");

                string resultado = negocio.BalancePositivoNegativo(vector);
                lblEstadisticas.Text = resultado;
            }
            catch (Exception ex)
            {
                lblEstadisticas.Text = $"Error: {ex.Message}";
            }
        }

        protected void btnMayorDiferencia_Click(object sender, EventArgs e)
        {
            try
            {
                VectorDatos vector = (VectorDatos)Session["VectorPrincipal"];
                if (vector == null) throw new InvalidOperationException("No hay vector principal guardado.");

                int diferencia = negocio.MayorDiferencia(vector);
                lblEstadisticas.Text = $"La mayor diferencia es: {diferencia}";
            }
            catch (Exception ex)
            {
                lblEstadisticas.Text = $"Error: {ex.Message}";
            }
        }
    }
}