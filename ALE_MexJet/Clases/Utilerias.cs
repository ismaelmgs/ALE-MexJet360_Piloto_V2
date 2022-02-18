using ALE_MexJet.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

namespace ALE_MexJet.Clases
{
    public static class Utilerias
    {
        public static DataTable ConvertListToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }

            return table;
        }

        public static string ObtieneFechaServidor()
        {
            try
            {
                return "fecha";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SaveErrorEnBitacora(string sError, string sPagina, string sClase, string sMetodo, string sCaption)
        {
            try
            {
                object[] obj = new object[]
                                        {
                                            "@Descripcion", sError, 
                                            "@Pagina", sPagina, 
                                            "@Clase", sClase, 
                                            "@Metodo", sMetodo
                                        };

                DataTable dtErrores = new DBUtils().DBSaveBitacoraError(obj);

                if (dtErrores.Rows.Count > 0)
                {
                    //oModal.ShowMessage(dtErrores.Rows[0][0].S(), sCaption);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}