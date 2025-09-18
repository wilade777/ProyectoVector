using Proyecto.Vector.Datos;
using Proyecto.Vector.RN;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq; // Es necesario para los métodos de extensión como OrderBy y ToArray

namespace Proyecto.Vector.Presentacion
{
    public partial class DosVectores : Page
    {
        private VectorNegocio negocio = new VectorNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["VectorPrincipal"] == null)
                {
                    lblVectorA.Text = "No hay un vector principal cargado. Por favor, defina y guarde un vector en la página principal para continuar.";
                }
                else
                {
                    VectorDatos vectorA = (VectorDatos)Session["VectorPrincipal"];
                    lblVectorA.Text = $"({negocio.Mostrar(vectorA)})";
                }
            }

            // Reconstruir los TextBox dinámicos en postback
            if (IsPostBack)
            {
                if (ViewState["SubvectorSize"] != null)
                {
                    GenerarCampos((int)ViewState["SubvectorSize"], panelSubvector, "txtSubNum");
                }
                if (ViewState["Vector2Size"] != null)
                {
                    GenerarCampos((int)ViewState["Vector2Size"], panelVector2, "txtVec2Num");
                }
                if (ViewState["Vector3Size"] != null)
                {
                    GenerarCampos((int)ViewState["Vector3Size"], panelVector3, "txtVec3Num");
                }
                if (ViewState["Vector4Size"] != null)
                {
                    GenerarCampos((int)ViewState["Vector4Size"], panelVector4, "txtVec4Num");
                }
            }
        }

        private void GenerarCampos(int n, Panel panel, string prefix)
        {
            panel.Controls.Clear();
            for (int i = 0; i < n; i++)
            {
                TextBox txt = new TextBox { ID = prefix + i, CssClass = "form-control" };
                txt.Attributes.Add("placeholder", $"Número {i + 1}");
                panel.Controls.Add(txt);
                panel.Controls.Add(new LiteralControl("<br/>"));
            }
        }

        private VectorDatos ObtenerVectorDesdePanel(Panel panel, string prefix, string sizeKey)
        {
            if (ViewState[sizeKey] == null)
            {
                throw new InvalidOperationException($"No se ha definido el tamaño del vector.");
            }
            int n = (int)ViewState[sizeKey];
            List<int> numeros = new List<int>();
            for (int i = 0; i < n; i++)
            {
                TextBox txt = (TextBox)panel.FindControl(prefix + i);
                if (txt == null || !int.TryParse(txt.Text.Trim(), out int valor))
                {
                    throw new FormatException($"El valor en la posición {i + 1} no es válido.");
                }
                numeros.Add(valor);
            }
            return new VectorDatos(numeros.ToArray());
        }

        // --- Eventos para Búsqueda de Subvector ---

        protected void btnGenerarSubvector_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtSubvectorSize.Text.Trim(), out int n) || n <= 0)
            {
                lblResultados.Text = "Error: Ingrese un tamaño válido para el subvector.";
                return;
            }
            ViewState["SubvectorSize"] = n;
            GenerarCampos(n, panelSubvector, "txtSubNum");
            lblResultados.Text = $"Ingrese {n} números para el subvector B.";
        }
        // Agrega este nuevo método para guardar el subvector en la sesión
        protected void btnGuardarSubvector_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtiene los datos del panel y los guarda en una variable de sesión
                VectorDatos subvectorB = ObtenerVectorDesdePanel(panelSubvector, "txtSubNum", "SubvectorSize");
                Session["SubvectorB"] = subvectorB;
                lblResultados.Text = "✅ Subvector B guardado. Ahora puedes buscarlo.";
            }
            catch (Exception ex)
            {
                lblResultados.Text = $"Error al guardar el subvector: {ex.Message}";
            }
        }

        protected void btnBuscarSubvector_Click(object sender, EventArgs e)
        {
            try
            {
                VectorDatos vectorA = (VectorDatos)Session["VectorPrincipal"];
                if (vectorA == null) throw new InvalidOperationException("Vector A no definido. Vaya a la página principal.");

                VectorDatos vectorB = ObtenerVectorDesdePanel(panelSubvector, "txtSubNum", "SubvectorSize");

                var resultado = negocio.BuscarSubvector(vectorA, vectorB);
                lblResultados.Text = resultado.encontrado
                    ? $"✅ Subvector encontrado en la posición: {resultado.posicion}."
                    : "❌ Subvector no encontrado.";
            }
            catch (Exception ex)
            {
                lblResultados.Text = $"Error: {ex.Message}";
            }
        }

        // --- Eventos para Producto Escalar ---
        protected void btnGenerarVector2_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtVector2Size.Text.Trim(), out int n) || n <= 0)
            {
                lblResultados.Text = "Error: Ingrese un tamaño válido para el vector 2.";
                return;
            }
            ViewState["Vector2Size"] = n;
            GenerarCampos(n, panelVector2, "txtVec2Num");
            lblResultados.Text = $"Ingrese {n} números para el vector C.";
        }

        protected void btnProductoEscalar_Click(object sender, EventArgs e)
        {
            try
            {
                VectorDatos vectorA = (VectorDatos)Session["VectorPrincipal"];
                if (vectorA == null) throw new InvalidOperationException("Vector A no definido. Vaya a la página principal.");

                VectorDatos vectorB = ObtenerVectorDesdePanel(panelVector2, "txtVec2Num", "Vector2Size");

                int resultado = negocio.CalcularProductoEscalar(vectorA, vectorB);
                lblResultados.Text = $"El producto escalar es: {resultado}";
            }
            catch (Exception ex)
            {
                lblResultados.Text = $"Error: {ex.Message}";
            }
        }

        // --- Eventos para Intercalar Vectores ---
        protected void btnGenerarVector3_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtVector3Size.Text.Trim(), out int n) || n <= 0)
            {
                lblResultados.Text = "Error: Ingrese un tamaño válido para el vector 3.";
                return;
            }
            ViewState["Vector3Size"] = n;
            GenerarCampos(n, panelVector3, "txtVec3Num");
            lblResultados.Text = $"Ingrese {n} números para el vector D.";
        }

        protected void btnIntercalar_Click(object sender, EventArgs e)
        {
            try
            {
                VectorDatos vectorA = (VectorDatos)Session["VectorPrincipal"];
                if (vectorA == null) throw new InvalidOperationException("Vector A no definido. Vaya a la página principal.");

                VectorDatos vectorB = ObtenerVectorDesdePanel(panelVector3, "txtVec3Num", "Vector3Size");

                VectorDatos resultado = negocio.IntercalarVectores(vectorA, vectorB);
                lblResultados.Text = $"Vector intercalado: ({negocio.Mostrar(resultado)})";
            }
            catch (Exception ex)
            {
                lblResultados.Text = $"Error: {ex.Message}";
            }
        }

        // --- Eventos para Fusión Ordenada ---
        protected void btnGenerarVector4_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtVector4Size.Text.Trim(), out int n) || n <= 0)
            {
                lblResultados.Text = "Error: Ingrese un tamaño válido para el vector 4.";
                return;
            }
            ViewState["Vector4Size"] = n;
            GenerarCampos(n, panelVector4, "txtVec4Num");
            lblResultados.Text = $"Ingrese {n} números para el vector E.";
        }

        protected void btnFusionOrdenada_Click(object sender, EventArgs e)
        {
            try
            {
                VectorDatos vectorA = (VectorDatos)Session["VectorPrincipal"];
                if (vectorA == null) throw new InvalidOperationException("Vector A no definido. Vaya a la página principal.");

                VectorDatos vectorB = ObtenerVectorDesdePanel(panelVector4, "txtVec4Num", "Vector4Size");

                VectorDatos resultado = negocio.FusionOrdenada(vectorA, vectorB);
                lblResultados.Text = $"Vector fusionado y ordenado: ({negocio.Mostrar(resultado)})";
            }
            catch (Exception ex)
            {
                lblResultados.Text = $"Error: {ex.Message}";
            }
        }
    }
}