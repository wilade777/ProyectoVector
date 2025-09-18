using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto.Vector.Datos;


namespace Proyecto.Vector.RN
{
    public class VectorNegocio
    {
        public void Llenar(VectorDatos v, int[] datos)
        {
            if (datos.Length != v.N)
                throw new Exception($"El vector requiere {v.N} elementos, pero se recibieron {datos.Length}.");

            v.Elementos = datos;
        }

        public string Mostrar(VectorDatos v)
        {
            return string.Join(", ", v.Elementos);
        }

    }
}
