using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Vector.Datos
{
    public class VectorDatos
    {
        public int[] Elementos { get; set; }

        public VectorDatos(int tamaño)
        {
            Elementos = new int[tamaño];
        }
    }
}


