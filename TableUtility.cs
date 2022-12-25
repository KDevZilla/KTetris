using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KBlock
{
    public class TableUtility
    {
        private static string FileName = @"D:\CODE\visual studio 2010\Projects\KBlock\KBlock\MyTable.txt";
        public static void SaveTableValue(TableCell T)
        {
            int i;
            int j;
            StringBuilder strB=new StringBuilder ();
            for(i=0;i<T.Rows.Count ;i++)
            {
                for(j=0;j< T.Rows[0].Cols.Count ;j++)
                {
                    Cell c =T.Rows  [i].Cols [j];
                    strB.Append (c.Value );
                    if(j<T.Rows[0].Cols.Count -1)
                    {
                        strB.Append (",");
                    }
                }
                strB.Append (Environment.NewLine );
            }
            Util.WriteFile(strB.ToString(), FileName );

             
        }
        public static  void LoadTable(TableCell T)
        {
            string str = Util.ReadFile(FileName);
            string[] ArrstrLine = str.Split(Environment.NewLine.ToCharArray ());
            int i;
            int j;
            int iRow = 0;
            for (i = 0; i < ArrstrLine.Length; i++)
            {
                if (ArrstrLine[i].Trim() == "")
                {
                    continue;
                }

                
                string[] ArrCellValue = ArrstrLine[i].Split(",".ToCharArray());
                for (j = 0; j < ArrCellValue.Length; j++)
                {
                    
                    T.Rows[iRow].Cols[j].Value = ArrCellValue[j];
                }
                iRow++;
            }
        }
    }
}
