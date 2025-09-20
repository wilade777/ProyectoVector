using Microsoft.VisualStudio.TestTools.UnitTesting;
using Proyecto.Vector.RN;
using Proyecto.Vector.Datos;
using System;
using System.Linq;

namespace Proyecto.Vector.Tests
{
    [TestClass]
    public class VectorNegocioTests
    {
        private VectorNegocio _negocio;

        [TestInitialize]
        public void Setup()
        {
            _negocio = new VectorNegocio();
        }

        // --- SPRINT 1: FUNDAMENTOS Y OPERACIONES BÁSICAS ---

        [TestMethod]
        public void CalcularPromedio_VectorValido_RetornaPromedioCorrecto()
        {
            var vector = new VectorDatos(new int[] { 1, 2, 3, 4, 5 });
            double resultado = _negocio.CalcularPromedio(vector);
            Assert.AreEqual(3.0, resultado);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CalcularPromedio_VectorVacio_LanzaExcepcion()
        {
            var vector = new VectorDatos(0);
            _negocio.CalcularPromedio(vector);
        }

        [TestMethod]
        public void CalcularDesviacion_VectorValido_RetornaDesviacionCorrecta()
        {
            var vector = new VectorDatos(new int[] { 2, 4, 4, 4, 5, 5, 7, 9 });
            double resultado = _negocio.CalcularDesviacion(vector);
            Assert.AreEqual(2.138, resultado, 0.001);
        }

        [TestMethod]
        public void CalcularMaximo_VectorValido_RetornaValorYPosicionesCorrectas()
        {
            var vector = new VectorDatos(new int[] { 10, 5, 20, 15, 20 });
            var resultado = _negocio.CalcularMaximo(vector);
            Assert.AreEqual(20, resultado.valorMax);
            CollectionAssert.AreEqual(new int[] { 2, 4 }, resultado.posiciones);
        }

        [TestMethod]
        public void CalcularMinimo_VectorValido_RetornaValorYPosicionesCorrectas()
        {
            var vector = new VectorDatos(new int[] { 10, 5, 20, 5, 20 });
            var resultado = _negocio.CalcularMinimo(vector);
            Assert.AreEqual(5, resultado.valorMin);
            CollectionAssert.AreEqual(new int[] { 1, 3 }, resultado.posiciones);
        }

        [TestMethod]
        public void EsPalindromo_VectorPalindromo_RetornaTrue()
        {
            var vector = new VectorDatos(new int[] { 1, 2, 3, 2, 1 });
            bool esPalindromo = _negocio.EsPalindromo(vector);
            Assert.IsTrue(esPalindromo);
        }

        [TestMethod]
        public void EsPalindromo_VectorNoPalindromo_RetornaFalse()
        {
            var vector = new VectorDatos(new int[] { 1, 2, 3, 4, 5 });
            bool esPalindromo = _negocio.EsPalindromo(vector);
            Assert.IsFalse(esPalindromo);
        }

        [TestMethod]
        public void SumaAcumulativa_VectorValido_RetornaVectorCorrecto()
        {
            var vector = new VectorDatos(new int[] { 1, 2, 3, 4 });
            var resultado = _negocio.SumaAcumulativa(vector);
            CollectionAssert.AreEqual(new int[] { 1, 3, 6, 10 }, resultado.Elementos);
        }

        [TestMethod]
        public void FrecuenciaElementos_VectorValido_RetornaDiccionarioCorrecto()
        {
            var vector = new VectorDatos(new int[] { 1, 2, 2, 3, 1, 4, 4, 4 });
            var resultado = _negocio.FrecuenciaElementos(vector);
            Assert.AreEqual(2, resultado[1]);
            Assert.AreEqual(2, resultado[2]);
            Assert.AreEqual(1, resultado[3]);
            Assert.AreEqual(3, resultado[4]);
        }

        // --- SPRINT 2: OPERACIONES CON DOS VECTORES ---

        [TestMethod]
        public void BuscarSubvector_SubvectorEncontrado_RetornaTrueYPosicion()
        {
            var vectorA = new VectorDatos(new int[] { 1, 5, 8, 3, 4, 5 });
            var vectorB = new VectorDatos(new int[] { 8, 3, 4 });
            var resultado = _negocio.BuscarSubvector(vectorA, vectorB);
            Assert.IsTrue(resultado.encontrado);
            Assert.AreEqual(2, resultado.posicion);
        }

        [TestMethod]
        public void BuscarSubvector_SubvectorNoEncontrado_RetornaFalse()
        {
            var vectorA = new VectorDatos(new int[] { 1, 5, 8, 3, 4, 5 });
            var vectorB = new VectorDatos(new int[] { 8, 9, 4 });
            var resultado = _negocio.BuscarSubvector(vectorA, vectorB);
            Assert.IsFalse(resultado.encontrado);
        }

        [TestMethod]
        public void CalcularProductoEscalar_VectoresValidos_RetornaProductoCorrecto()
        {
            var vector1 = new VectorDatos(new int[] { 1, 2, 3 });
            var vector2 = new VectorDatos(new int[] { 4, 5, 6 });
            int resultado = _negocio.CalcularProductoEscalar(vector1, vector2);
            Assert.AreEqual(32, resultado);
        }

        [TestMethod]
        public void IntercalarVectores_VectoresValidos_RetornaVectorIntercalado()
        {
            var vec1 = new VectorDatos(new int[] { 1, 3, 5 });
            var vec2 = new VectorDatos(new int[] { 2, 4, 6 });
            var resultado = _negocio.IntercalarVectores(vec1, vec2);
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6 }, resultado.Elementos);
        }

        [TestMethod]
        public void FusionOrdenada_VectoresValidos_RetornaVectorFusionadoYOrdenado()
        {
            var vec1 = new VectorDatos(new int[] { 1, 5, 8 });
            var vec2 = new VectorDatos(new int[] { 2, 4, 6 });
            var resultado = _negocio.FusionOrdenada(vec1, vec2);
            CollectionAssert.AreEqual(new int[] { 1, 2, 4, 5, 6, 8 }, resultado.Elementos);
        }

        [TestMethod]
        public void CompactacionPorCondicion_SoloPares_RetornaVectorCompactado()
        {
            var vector = new VectorDatos(new int[] { 1, 2, 3, 4, 5, 6 });
            var resultado = _negocio.CompactacionPorCondicion(vector, e => e % 2 == 0);
            CollectionAssert.AreEqual(new int[] { 2, 4, 6 }, resultado.Elementos);
        }

        // --- SPRINT 3: TRANSFORMACIÓN Y MANIPULACIÓN ---

        [TestMethod]
        public void RotacionCircular_RotacionPositiva_RetornaVectorRotado()
        {
            var vector = new VectorDatos(new int[] { 1, 2, 3, 4, 5 });
            int k = 2;
            var resultado = _negocio.RotacionCircular(vector, k);
            CollectionAssert.AreEqual(new int[] { 4, 5, 1, 2, 3 }, resultado.Elementos);
        }

        [TestMethod]
        public void EliminarDuplicados_VectorConDuplicados_RetornaVectorSinDuplicados()
        {
            var vector = new VectorDatos(new int[] { 1, 2, 2, 3, 1, 4 });
            var resultado = _negocio.EliminarDuplicados(vector);
            CollectionAssert.AreEquivalent(new int[] { 1, 2, 3, 4 }, resultado.Elementos);
        }

        [TestMethod]
        public void OrdenarParesImpares_VectorValido_RetornaVectorOrdenado()
        {
            var vector = new VectorDatos(new int[] { 5, 8, 3, 6, 2, 1 });
            var resultado = _negocio.OrdenarParesImpares(vector);
            CollectionAssert.AreEqual(new int[] { 2, 6, 8, 1, 3, 5 }, resultado.Elementos);
        }

        [TestMethod]
        public void InversionParcial_IndicesValidos_RetornaVectorInvertidoParcialmente()
        {
            var vector = new VectorDatos(new int[] { 1, 2, 3, 4, 5, 6, 7 });
            var resultado = _negocio.InversionParcial(vector, 2, 5); // Invierte de 3 a 6
            CollectionAssert.AreEqual(new int[] { 1, 2, 6, 5, 4, 3, 7 }, resultado.Elementos);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InversionParcial_IndicesInvalidos_LanzaExcepcion()
        {
            var vector = new VectorDatos(new int[] { 1, 2, 3 });
            _negocio.InversionParcial(vector, 2, 1);
        }
    }
}