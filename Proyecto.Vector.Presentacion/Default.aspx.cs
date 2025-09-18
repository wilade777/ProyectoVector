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
            if (IsPostBack)
            {
                if (ViewState["VectorSize"] != null)
                {
                    int n = (int)ViewState["VectorSize"];
                    GenerarCampos(n, panelCampos, "txtNum");
                }

                if (ViewState["SubvectorSize"] != null)
                {
                    int n = (int)ViewState["SubvectorSize"];
                    GenerarCampos(n, panelSubvector, "txtSubNum");
                }

                if (ViewState["Vector2Size"] != null)
                {
                    int n = (int)ViewState["Vector2Size"];
                    GenerarCampos(n, panelVector2, "txtVec2Num");
                }
            }
        }
        // MÉTODO GENERALIZADO PARA GENERAR CAMPOS
        private void GenerarCampos(int n, Panel panel, string prefix)
        {
            panel.Controls.Clear();
            for (int i = 0; i < n; i++)
            {
                TextBox txt = new TextBox { ID = prefix + i };
                txt.Attributes.Add("placeholder", $"Número {i + 1}");
                panel.Controls.Add(txt);
                panel.Controls.Add(new LiteralControl("<br/>"));
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

            VectorDatos vector = new VectorDatos(numeros.Count);
            negocio.Llenar(vector, numeros.ToArray());

            // Obtener máximo y mínimo
            var (maxValor, maxPosiciones) = negocio.CalcularMaximo(vector);
            var (minValor, minPosiciones) = negocio.CalcularMinimo(vector);

            lblEstadisticas.Text = $"Máximo: {maxValor} | " + $"Mínimo: {minValor} ";

        }
        // Verificar Palíndromo
        protected void btnPalindromo_Click(object sender, EventArgs e)
        {
            VectorDatos vector = RecuperarVector();
            if (vector == null) return;

            bool esPalindromo = negocio.EsPalindromo(vector);
            lblEstadisticas.Text = esPalindromo
                ? "✅ El vector es palíndromo."
                : "❌ El vector NO es palíndromo.";
        }
        // Recuo+perar Vector
        private VectorDatos RecuperarVector()
        {
            if (ViewState["VectorSize"] == null) return null;

            int n = (int)ViewState["VectorSize"];
            List<int> numeros = new List<int>();

            for (int i = 0; i < n; i++)
            {
                TextBox txt = (TextBox)panelCampos.FindControl("txtNum" + i);
                if (txt == null || !int.TryParse(txt.Text.Trim(), out int valor))
                {
                    lblEstadisticas.Text = $"Error: el valor en la posición {i + 1} no es válido.";
                    return null;
                }
                numeros.Add(valor);
            }

            VectorDatos vector = new VectorDatos(numeros.Count);
            negocio.Llenar(vector, numeros.ToArray());
            return vector;
        }
        protected void btnGenerarSubvector_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtSubvectorSize.Text.Trim(), out int n) || n <= 0)
            {
                lblEstadisticas.Text = "Error: Ingrese un tamaño válido para el subvector.";
                return;
            }

            ViewState["SubvectorSize"] = n;
            GenerarCampos(n, panelSubvector, "txtSubNum");

            panelSubvector.Visible = true;
            btnBuscarSubvector.Visible = true;
            lblEstadisticas.Text = $"Ingrese {n} números para el subvector B.";
        }

        protected void btnBuscarSubvector_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener vector principal A
                VectorDatos vectorA = RecuperarVector();
                if (vectorA == null) return;

                // Obtener subvector B
                VectorDatos vectorB = RecuperarSubvector();
                if (vectorB == null) return;

                // Buscar subvector
                var resultado = negocio.BuscarSubvector(vectorA, vectorB);

                if (resultado.encontrado)
                    lblEstadisticas.Text = $"✅ Subvector encontrado en la posición: {resultado.posicion}";
                else
                    lblEstadisticas.Text = "❌ Subvector no encontrado";
            }
            catch (Exception ex)
            {
                lblEstadisticas.Text = $"Error: {ex.Message}";
            }
        }

        private VectorDatos RecuperarSubvector()
        {
            if (ViewState["SubvectorSize"] == null) return null;

            int n = (int)ViewState["SubvectorSize"];
            List<int> numeros = new List<int>();

            for (int i = 0; i < n; i++)
            {
                TextBox txt = (TextBox)panelSubvector.FindControl("txtSubNum" + i);
                if (txt == null || !int.TryParse(txt.Text.Trim(), out int valor))
                {
                    lblEstadisticas.Text = $"Error: valor inválido en subvector posición {i + 1}";
                    return null;
                }
                numeros.Add(valor);
            }

            return new VectorDatos(numeros.ToArray());
        }

        // =========================================================================
        // NUEVOS MÉTODOS PARA PRODUCTO ESCALAR
        // =========================================================================
        protected void btnGenerarVector2_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtVector2Size.Text.Trim(), out int n) || n <= 0)
            {
                lblEstadisticas.Text = "Error: Ingrese un tamaño válido para el vector 2.";
                return;
            }

            ViewState["Vector2Size"] = n;
            GenerarCampos(n, panelVector2, "txtVec2Num");

            panelVector2.Visible = true;
            btnProductoEscalar.Visible = true;
            lblEstadisticas.Text = $"Ingrese {n} números para el vector 2.";
        }

        protected void btnProductoEscalar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener vector 1
                VectorDatos vector1 = RecuperarVector();
                if (vector1 == null) return;

                // Obtener vector 2
                VectorDatos vector2 = RecuperarVector2();
                if (vector2 == null) return;

                // Calcular producto escalar
                int resultado = negocio.CalcularProductoEscalar(vector1, vector2);
                lblEstadisticas.Text = $"Producto escalar: {resultado}";
            }
            catch (Exception ex)
            {
                lblEstadisticas.Text = $"Error: {ex.Message}";
            }
        }

        private VectorDatos RecuperarVector2()
        {
            if (ViewState["Vector2Size"] == null) return null;

            int n = (int)ViewState["Vector2Size"];
            List<int> numeros = new List<int>();

            for (int i = 0; i < n; i++)
            {
                TextBox txt = (TextBox)panelVector2.FindControl("txtVec2Num" + i);
                if (txt == null || !int.TryParse(txt.Text.Trim(), out int valor))
                {
                    lblEstadisticas.Text = $"Error: valor inválido en vector 2 posición {i + 1}";
                    return null;
                }
                numeros.Add(valor);
            }

            return new VectorDatos(numeros.ToArray());
        }

        // =========================================================================
        // MÉTODO AUXILIAR PARA CREAR VectoresDatos DESDE ARRAY
        // =========================================================================
        private VectorDatos CrearVectorDesdeArray(int[] elementos)
        {
            VectorDatos vector = new VectorDatos(elementos.Length);
            for (int i = 0; i < elementos.Length; i++)
            {
                vector.Elementos[i] = elementos[i];
            }
            return vector;
        }
    }
}