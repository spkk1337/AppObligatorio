using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class CasosOrdenAsc : IComparer<Caso>
    {
        public int Compare(Caso x, Caso y)
        {
            return string.Compare(x.Nombre, y.Nombre);
        }
    }
}