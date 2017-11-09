using System;
using Oracle.DataAccess.Client;

namespace RH_VacantesWeb.Clases
{
    public class BDC
    {
       //string strCon = "Data Source=XE; Persist Security Info=True; User ID=administrador; Password=adminadmin";
        string strCon = "Data Source=opera.sanchez.com.mx; Persist Security Info=True; User ID=EDUARDO; Password=ed0121";
       //string strCon = "Data Source=opera; Persist Security Info=True; User ID=EDUARDO; Password=ed0121";
        public OracleConnection getConection()
        {
            try
            {
                OracleConnection cnn = new OracleConnection(strCon);
                return cnn;
            }
            catch (Exception ExGetConection)
            {
                throw new Exception("ERROR: Error al realizar la conexión a la Base de Datos [" + ExGetConection.Message + "]");
            }
        }
    }
}