using Proyecto.Vector.Datos;
using Proyecto.Vector.RN;
using System;
using System.Web.UI;

namespace Proyecto.Vector.Presentacion
{
    public partial class TransformacionAvanzada : Page
    {
        private VectorNegocio negocio = new VectorNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarVectorPrincipal();
            }
        }

        private void CargarVectorPrincipal()
        {
            if (Session["VectorPrincipal"] == null)
            {
                lblVectorA.Text = "No hay un vector principal cargado. Por favor, defina y guarde un vector en la página principal para continuar.";
                DeshabilitarOperaciones();
            }
            else
            {
                VectorDatos vectorA = (VectorDatos)Session["VectorPrincipal"];
                lblVectorA.Text = $"({negocio.Mostrar(vectorA)})";
                lblVectorResultante.Text = $"({negocio.Mostrar(vectorA)})";
            }
        }

        private void DeshabilitarOperaciones()
        {
            btnRotarDerecha.Enabled = false;
            btnRotarIzquierda.Enabled = false;
            btnEliminarDuplicados.Enabled = false;
            btnOrdenarParesImpares.Enabled = false;
            btnInvertirParcial.Enabled = false;
            btnCompactar.Enabled = false;
        }

        private void ActualizarResultado(VectorDatos resultado, string operacion)
        {
            lblResultados.Text = $"✅ {operacion} realizado correctamente.";
            lblVectorResultante.Text = $"({negocio.Mostrar(resultado)})";

            // Opcional: guardar el resultado como nuevo vector principal
            Session["VectorPrincipal"] = resultado;
            lblVectorA.Text = $"({negocio.Mostrar(resultado)})";
        }

        // Rotación Circular
        protected void btnRotarDerecha_Click(object sender, EventArgs e)
        {
            try
            {
                VectorDatos vectorA = (VectorDatos)Session["VectorPrincipal"];
                if (vectorA == null) throw new InvalidOperationException("Vector A no definido.");

                int k = int.Parse(txtRotaciones.Text.Trim());
                VectorDatos resultado = negocio.RotacionCircular(vectorA, k);
                ActualizarResultado(resultado, $"Rotación a la derecha ({k} posiciones)");
            }
            catch (Exception ex)
            {
                lblResultados.Text = $"Error: {ex.Message}";
            }
        }

        protected void btnRotarIzquierda_Click(object sender, EventArgs e)
        {
            try
            {
                VectorDatos vectorA = (VectorDatos)Session["VectorPrincipal"];
                if (vectorA == null) throw new InvalidOperationException("Vector A no definido.");

                int k = -int.Parse(txtRotaciones.Text.Trim()); // Negativo para rotación izquierda
                VectorDatos resultado = negocio.RotacionCircular(vectorA, k);
                ActualizarResultado(resultado, $"Rotación a la izquierda ({Math.Abs(k)} posiciones)");
            }
            catch (Exception ex)
            {
                lblResultados.Text = $"Error: {ex.Message}";
            }
        }

        // Eliminar Duplicados
        protected void btnEliminarDuplicados_Click(object sender, EventArgs e)
        {
            try
            {
                VectorDatos vectorA = (VectorDatos)Session["VectorPrincipal"];
                if (vectorA == null) throw new InvalidOperationException("Vector A no definido.");

                VectorDatos resultado = negocio.EliminarDuplicados(vectorA);
                ActualizarResultado(resultado, "Eliminación de duplicados");
            }
            catch (Exception ex)
            {
                lblResultados.Text = $"Error: {ex.Message}";
            }
        }

        // Ordenar Pares e Impares
        protected void btnOrdenarParesImpares_Click(object sender, EventArgs e)
        {
            try
            {
                VectorDatos vectorA = (VectorDatos)Session["VectorPrincipal"];
                if (vectorA == null) throw new InvalidOperationException("Vector A no definido.");

                VectorDatos resultado = negocio.OrdenarParesImpares(vectorA);
                ActualizarResultado(resultado, "Ordenamiento de pares e impares");
            }
            catch (Exception ex)
            {
                lblResultados.Text = $"Error: {ex.Message}";
            }
        }

        // Inversión Parcial
        protected void btnInvertirParcial_Click(object sender, EventArgs e)
        {
            try
            {
                VectorDatos vectorA = (VectorDatos)Session["VectorPrincipal"];
                if (vectorA == null) throw new InvalidOperationException("Vector A no definido.");

                int inicio = int.Parse(txtInicioInversion.Text.Trim());
                int fin = int.Parse(txtFinInversion.Text.Trim());

                VectorDatos resultado = negocio.InversionParcial(vectorA, inicio, fin);
                ActualizarResultado(resultado, $"Inversión parcial (índices {inicio} a {fin})");
            }
            catch (Exception ex)
            {
                lblResultados.Text = $"Error: {ex.Message}";
            }
        }

        // Compactación por Condición
        protected void btnCompactar_Click(object sender, EventArgs e)
        {
            try
            {
                VectorDatos vectorA = (VectorDatos)Session["VectorPrincipal"];
                if (vectorA == null) throw new InvalidOperationException("Vector A no definido.");

                string condicionSeleccionada = ddlCondicion.SelectedValue;
                Func<int, bool> condicion = ObtenerCondicion(condicionSeleccionada);

                VectorDatos resultado = negocio.CompactacionPorCondicion(vectorA, condicion);
                ActualizarResultado(resultado, $"Compactación por condición ({ddlCondicion.SelectedItem.Text})");
            }
            catch (Exception ex)
            {
                lblResultados.Text = $"Error: {ex.Message}";
            }
        }

        private Func<int, bool> ObtenerCondicion(string tipoCondicion)
        {
            switch (tipoCondicion)
            {
                case "pares":
                    return x => x % 2 == 0;
                case "impares":
                    return x => x % 2 != 0;
                case "positivos":
                    return x => x > 0;
                case "negativos":
                    return x => x < 0;
                case "primos":
                    return EsPrimo;
                default:
                    return x => true;
            }
        }

        private bool EsPrimo(int numero)
        {
            if (numero <= 1) return false;
            if (numero == 2) return true;
            if (numero % 2 == 0) return false;

            for (int i = 3; i <= Math.Sqrt(numero); i += 2)
            {
                if (numero % i == 0) return false;
            }
            return true;
        }
    }
}