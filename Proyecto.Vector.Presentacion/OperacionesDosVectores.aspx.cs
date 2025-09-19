using Proyecto.Vector.Datos;
using Proyecto.Vector.RN;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Proyecto.Vector.Presentacion
{
    public partial class DosVectores : Page
    {
        private VectorNegocio negocio = new VectorNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar Vector A desde sesión
                CargarVectorA();

                // Cargar Vector B desde sesión si existe
                CargarVectorB();
            }
            else
            {
                // Reconstruir campos dinámicos en postback
                ReconstruirCamposDinamicos();
            }
        }

        private void CargarVectorA()
        {
            if (Session["VectorPrincipal"] == null)
            {
                lblVectorA.Text = "No hay un vector principal cargado. Por favor, cargue uno en la página principal.";
            }
            else
            {
                VectorDatos vectorA = (VectorDatos)Session["VectorPrincipal"];
                lblVectorA.Text = $"Vector A: ({negocio.Mostrar(vectorA)})";
            }
        }

        private void CargarVectorB()
        {
            if (Session["VectorB"] != null)
            {
                VectorDatos vectorB = (VectorDatos)Session["VectorB"];
                lblVectorBGuardado.Text = $"({negocio.Mostrar(vectorB)})";
            }
            else
            {
                lblVectorBGuardado.Text = "No hay vector B guardado";
            }
        }

        private void ReconstruirCamposDinamicos()
        {
            if (ViewState["VectorBSize"] != null)
            {
                GenerarCampos((int)ViewState["VectorBSize"], panelVectorB, "txtVecB");
            }
        }

        private void GenerarCampos(int n, Panel panel, string prefix)
        {
            panel.Controls.Clear();
            for (int i = 0; i < n; i++)
            {
                // Crear contenedor para cada campo
                Panel fieldPanel = new Panel { CssClass = "input-group mb-2" };

                // Label
                Label lbl = new Label
                {
                    Text = $"Elemento {i + 1}:",
                    CssClass = "input-group-text"
                };

                // TextBox
                TextBox txt = new TextBox
                {
                    ID = prefix + i,
                    CssClass = "form-control",
                    TextMode = TextBoxMode.Number
                };
                txt.Attributes.Add("placeholder", $"Número {i + 1}");

                fieldPanel.Controls.Add(lbl);
                fieldPanel.Controls.Add(txt);
                panel.Controls.Add(fieldPanel);
            }
        }

        private VectorDatos ObtenerVectorDesdePanel(Panel panel, string prefix, string sizeKey)
        {
            if (ViewState[sizeKey] == null)
            {
                throw new InvalidOperationException("No se ha definido el tamaño del vector.");
            }

            int n = (int)ViewState[sizeKey];
            List<int> numeros = new List<int>();

            for (int i = 0; i < n; i++)
            {
                TextBox txt = (TextBox)panel.FindControl(prefix + i);
                if (txt == null)
                {
                    throw new FormatException($"No se encontró el campo en la posición {i + 1}.");
                }

                if (string.IsNullOrEmpty(txt.Text.Trim()))
                {
                    throw new FormatException($"El valor en la posición {i + 1} está vacío.");
                }

                if (!int.TryParse(txt.Text.Trim(), out int valor))
                {
                    throw new FormatException($"El valor '{txt.Text}' en la posición {i + 1} no es un número válido.");
                }

                numeros.Add(valor);
            }

            return new VectorDatos(numeros.ToArray());
        }

        // --- EVENTOS PARA VECTOR B ---
        protected void btnGenerarVectorB_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtVectorBSize.Text.Trim(), out int n) || n <= 0)
                {
                    lblResultados.Text = "❌ Error: Ingrese un tamaño válido para el Vector B (mayor que 0).";
                    return;
                }

                ViewState["VectorBSize"] = n;
                GenerarCampos(n, panelVectorB, "txtVecB");

                // Mostrar botón de guardar (como en Default.aspx)
                btnGuardarVectorB.Visible = true;

                lblResultados.Text = $"✅ Ingrese los {n} valores para el Vector B y luego guárdelo.";
            }
            catch (Exception ex)
            {
                lblResultados.Text = $"❌ Error: {ex.Message}";
            }
        }

        protected void btnGuardarVectorB_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["VectorBSize"] == null)
                {
                    lblResultados.Text = "❌ Error: Primero debe generar los campos del Vector B.";
                    return;
                }

                VectorDatos vectorB = ObtenerVectorDesdePanel(panelVectorB, "txtVecB", "VectorBSize");

                if (vectorB.Elementos == null || vectorB.Elementos.Length == 0)
                {
                    lblResultados.Text = "❌ Error: El Vector B no puede estar vacío.";
                    return;
                }

                Session["VectorB"] = vectorB;
                lblVectorBGuardado.Text = $"({negocio.Mostrar(vectorB)})";
                lblResultados.Text = $"✅ Vector B guardado correctamente: ({negocio.Mostrar(vectorB)})";
            }
            catch (Exception ex)
            {
                lblResultados.Text = $"❌ Error al guardar Vector B: {ex.Message}";
            }
        }

        // --- OPERACIONES ---
        protected void btnBuscarSubvector_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que ambos vectores existan
                if (Session["VectorPrincipal"] == null)
                {
                    lblResultados.Text = "❌ Error: Vector A no definido. Vaya a la página principal.";
                    return;
                }

                if (Session["VectorB"] == null)
                {
                    lblResultados.Text = "❌ Error: Vector B no definido. Genere y guarde el Vector B primero.";
                    return;
                }

                VectorDatos vectorA = (VectorDatos)Session["VectorPrincipal"];
                VectorDatos vectorB = (VectorDatos)Session["VectorB"];

                // Validar que los vectores no estén vacíos
                if (vectorA.Elementos == null || vectorA.Elementos.Length == 0)
                {
                    lblResultados.Text = "❌ Error: Vector A está vacío.";
                    return;
                }

                if (vectorB.Elementos == null || vectorB.Elementos.Length == 0)
                {
                    lblResultados.Text = "❌ Error: Vector B está vacío.";
                    return;
                }

                // Ejecutar la búsqueda
                var resultado = negocio.BuscarSubvector(vectorA, vectorB);

                if (resultado.encontrado)
                {
                    lblResultados.Text = $"✅ Subvector B ENCONTRADO en A en la posición: {resultado.posicion + 0}";
                    lblResultados.Text += $"<br/>📊 Vector A: [{string.Join(", ", vectorA.Elementos)}]";
                    lblResultados.Text += $"<br/>🔍 Vector B: [{string.Join(", ", vectorB.Elementos)}]";
                }
                else
                {
                    lblResultados.Text = "❌ Subvector B NO ENCONTRADO en A";
                    lblResultados.Text += $"<br/>📊 Vector A: [{string.Join(", ", vectorA.Elementos)}]";
                    lblResultados.Text += $"<br/>🔍 Vector B: [{string.Join(", ", vectorB.Elementos)}]";
                }
            }
            catch (Exception ex)
            {
                lblResultados.Text = $"❌ Error en búsqueda de subvector: {ex.Message}";
            }
        }

        protected void btnProductoEscalar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidarVectores();
                VectorDatos vectorA = (VectorDatos)Session["VectorPrincipal"];
                VectorDatos vectorB = (VectorDatos)Session["VectorB"];

                int resultado = negocio.CalcularProductoEscalar(vectorA, vectorB);
                lblResultados.Text = $"✅ Producto Escalar A·B = {resultado}";
                lblResultados.Text += $"<br/>📊 Vector A: [{string.Join(", ", vectorA.Elementos)}]";
                lblResultados.Text += $"<br/>📊 Vector B: [{string.Join(", ", vectorB.Elementos)}]";
            }
            catch (Exception ex)
            {
                lblResultados.Text = $"❌ Error en producto escalar: {ex.Message}";
            }
        }

        protected void btnIntercalar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidarVectores();
                VectorDatos vectorA = (VectorDatos)Session["VectorPrincipal"];
                VectorDatos vectorB = (VectorDatos)Session["VectorB"];

                VectorDatos resultado = negocio.IntercalarVectores(vectorA, vectorB);
                lblResultados.Text = $"✅ Vector intercalado: ({negocio.Mostrar(resultado)})";
                lblResultados.Text += $"<br/>📊 Vector A: [{string.Join(", ", vectorA.Elementos)}]";
                lblResultados.Text += $"<br/>📊 Vector B: [{string.Join(", ", vectorB.Elementos)}]";
            }
            catch (Exception ex)
            {
                lblResultados.Text = $"❌ Error al intercalar: {ex.Message}";
            }
        }

        protected void btnFusionOrdenada_Click(object sender, EventArgs e)
        {
            try
            {
                ValidarVectores();
                VectorDatos vectorA = (VectorDatos)Session["VectorPrincipal"];
                VectorDatos vectorB = (VectorDatos)Session["VectorB"];

                VectorDatos resultado = negocio.FusionOrdenada(vectorA, vectorB);
                lblResultados.Text = $"✅ Vector fusionado y ordenado: ({negocio.Mostrar(resultado)})";
                lblResultados.Text += $"<br/>📊 Vector A: [{string.Join(", ", vectorA.Elementos)}]";
                lblResultados.Text += $"<br/>📊 Vector B: [{string.Join(", ", vectorB.Elementos)}]";
            }
            catch (Exception ex)
            {
                lblResultados.Text = $"❌ Error en fusión: {ex.Message}";
            }
        }

        private void ValidarVectores()
        {
            if (Session["VectorPrincipal"] == null)
            {
                throw new InvalidOperationException("Vector A no definido. Vaya a la página principal.");
            }

            if (Session["VectorB"] == null)
            {
                throw new InvalidOperationException("Vector B no definido. Genere y guarde el Vector B primero.");
            }

            VectorDatos vectorA = (VectorDatos)Session["VectorPrincipal"];
            VectorDatos vectorB = (VectorDatos)Session["VectorB"];

            if (vectorA.Elementos == null || vectorA.Elementos.Length == 0)
            {
                throw new InvalidOperationException("Vector A está vacío.");
            }

            if (vectorB.Elementos == null || vectorB.Elementos.Length == 0)
            {
                throw new InvalidOperationException("Vector B está vacío.");
            }
        }
    }
}