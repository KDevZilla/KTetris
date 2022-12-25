using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTetris
{
    public class Row<T>
    {
        public List<T> Cols = new List<T>();
    }
    public class GenericTable<T>
    {
        public List<Row<T>> Rows = new List<Row<T>>();
      
    }
}
