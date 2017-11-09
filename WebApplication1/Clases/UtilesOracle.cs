using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using RH_VacantesWeb.Clases;

namespace RHVacantes.Clases
{
    public class UtilesOracle
    {
        public Boolean actualizaRegistroVacante(Vacante vacanteActualizar)
        {
            Boolean elBuleano = true;
            try {
                String elQuery = "UPDATE RH_VACANTES " +
                        "SET PUESTO = :pPUESTO, DESCRIPCION = :pDESCRIPCION, STATUS = :pSTATUS, COMPETENCIAS = :pCOMPETENCIAS, " +
                            "UBICACION = :pUBICACION, TIPO_CONTRATO = :pTIPO_CONTRATO, HORARIO = :pHORARIO, ESCOLARIDAD = :pESCOLARIDAD, SEXO = :pSEXO, RANGO_EDAD = :pRANGO_EDAD " +
                "WHERE ID =:pID ";
                // ID, PUESTO, DESCRIPCION, UBICACION, TIPO_CONTRATO, RANGO_EDAD, COMPETENCIAS, SEXO, STATUS" 
                OracleCommand cmd = new OracleCommand(elQuery);
                /*******************************************************************/
                cmd.Parameters.Add("pPUESTO", OracleDbType.Varchar2);
                cmd.Parameters["pPuestO"].Value = vacanteActualizar.Puesto;
                cmd.Parameters.Add("pDESCRIPCION", OracleDbType.Clob);
                cmd.Parameters["pDESCRIPCION"].Value = vacanteActualizar.Descripcion;
                cmd.Parameters.Add("pSTATUS", OracleDbType.Varchar2);
                cmd.Parameters["pSTATUS"].Value = vacanteActualizar.Status;
                cmd.Parameters.Add("pCOMPETENCIAS",OracleDbType.Clob);
                cmd.Parameters["pCOMPETENCIAS"].Value = vacanteActualizar.Competencias;
                cmd.Parameters.Add("pUBICACION", OracleDbType.Varchar2);
                cmd.Parameters["pUBICACION"].Value = vacanteActualizar.Ubicacion;
                cmd.Parameters.Add("pTIPO_CONTRATO", OracleDbType.Varchar2);
                cmd.Parameters["pTIPO_CONTRATO"].Value = vacanteActualizar.Contrato;
                cmd.Parameters.Add("pHORARIO", OracleDbType.Varchar2);
                cmd.Parameters["pHORARIO"].Value = vacanteActualizar.Horario;
                cmd.Parameters.Add("pESCOLARIDAD", OracleDbType.Varchar2);
                cmd.Parameters["pESCOLARIDAD"].Value = vacanteActualizar.Escolaridad;
                cmd.Parameters.Add("pSEXO", OracleDbType.Varchar2);
                cmd.Parameters["pSEXO"].Value = vacanteActualizar.Sexo;
                cmd.Parameters.Add("pRANGO_EDAD", OracleDbType.Varchar2);
                cmd.Parameters["pRANGO_EDAD"].Value = vacanteActualizar.Rango;

                cmd.Parameters.Add("pID", OracleDbType.Int32);
                cmd.Parameters["pID"].Value = Int32.Parse(vacanteActualizar.Id);

                DataTable tablaDatos = conectaOracle( elQuery,true,cmd);             
            }
            catch (Exception ExCargaInfoInterno)
            {
                Console.WriteLine(ExCargaInfoInterno.Message);
                //Si algo truena, retachamos false
                elBuleano = false;
            }
            return elBuleano;
        }

        public DataTable obtieneVacantes(String elID){
            OracleCommand oracmd = new OracleCommand("SELECT RH_VACANTES.ID, RH_VACANTES.PUESTO, RH_VACANTES.DESCRIPCION,RH_ESCOLARIDAD.DESCRIPCION as ESCOLARIDAD,RH_VACANTES.COMPETENCIAS, RH_LOCACIONES.LOCACION as UBICACION, RH_TIPOCONTRATO.CONTRATO as TIPO_CONTRATO,RH_HORARIO.HORARIO as HORARIO, RH_VACANTES.RANGO_EDAD, RH_VACANTES.SEXO, RH_VACANTES.STATUS " +
                " FROM RH_VACANTES "+
                " LEFT JOIN RH_LOCACIONES"+
                " on RH_VACANTES.UBICACION = RH_LOCACIONES.ID"+
                " left join RH_TIPOCONTRATO"+
                " ON RH_VACANTES.TIPO_CONTRATO = RH_TIPOCONTRATO.ID"+
                " LEFT JOIN RH_HORARIO ON RH_VACANTES.HORARIO = RH_HORARIO.ID"+
                " LEFT JOIN RH_ESCOLARIDAD"+
                " ON RH_VACANTES.ESCOLARIDAD = RH_ESCOLARIDAD.ID"+
                " WHERE RH_VACANTES.ID =:pID");
            oracmd.Parameters.Add("pID", OracleDbType.Int32);
            oracmd.Parameters["pID"].Value = Convert.ToInt32(elID);
            return  conectaOracle( "", false,oracmd);
        }

        /**
         * Función que regresa una datatabla con los datos solicitados en el query enviado
         * 
         * @elQuery
         * parámetro para ser utilizado si se quiere hacer un update o un delete o si se quiere 
         * recibir los datos de un query sin calcular
         * 
         * @actualizaBorra
         * para hacer updates o deletes. Si se quiere hacer solo una consulta, se debe de enviar a false
         * 
         * @oracmd
         * comando con el que se calculan los querys dinamicamente. Si no se quiere utilizar, se debe de enviar 
         * NULL
         * 
         * */
        public DataTable conectaOracle( String elQuery, Boolean actualizaBorra,OracleCommand oracmd)
        {
            BDC objCnn = new BDC();
            var dataTable = new DataTable();
                using (OracleConnection cnn = objCnn.getConection())
                {
                    try
                    {
                        cnn.Open();
                        // Create the OracleCommand
                        if (null == oracmd)
                        {
                            OracleCommand cmd = new OracleCommand(elQuery);

                            cmd.Connection = cnn;
                            cmd.CommandType = CommandType.Text;
                            OracleDataReader rdr = cmd.ExecuteReader();

                            if (!actualizaBorra)
                            {
                                dataTable.Load(rdr);
                            }
                        }
                            //cuando se envía un OracleCommand, es decir, que se envían parámetros a buscar
                        else
                        {
                            oracmd.Connection = cnn;
                            oracmd.CommandType = CommandType.Text;
                            OracleDataReader rdr = oracmd.ExecuteReader();

                            if (!actualizaBorra)
                            {
                                dataTable.Load(rdr);
                            }

                        }
                    }
                    catch (Exception ExCargaInfoInterno)
                    {
                        Console.WriteLine(ExCargaInfoInterno.Message);
                       
                    }
                    finally
                    { 
                        if (cnn.State == ConnectionState.Open)
                            cnn.Close();
                         
                    }
                    return dataTable;
                }
        }

        public DataTable obtieneCV(String elID)
        {
            OracleCommand oracmd = new OracleCommand("SELECT ID_PADRE,NOMBRE_ADJUNTO,CONTENTTYPE,THEDATA" +
                " FROM RH_ADJUNTOS" +
                " WHERE ID_PADRE =:pID");
            oracmd.Parameters.Add("pID", OracleDbType.Int32);
            oracmd.Parameters["pID"].Value = Convert.ToInt32(elID);
            return conectaOracle("", false, oracmd);
        }
        public DataTable rolesUsuarioFirmado(String Usuario)
        {  
            BDC objCnn = new BDC();
            using (OracleConnection cnn = objCnn.getConection())
            {
                var dataTable = new DataTable();
                try
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("SELECT GROUP_ID FROM secdba.usergroup " +
                        " where USER_ID=:pUSER_ID and GROUP_ID ='bolsa_trab'");
                    cmd.Parameters.Add("pUSER_ID", OracleDbType.Char);
                    cmd.Parameters["pUSER_ID"].Value = Usuario;
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader rdr = cmd.ExecuteReader();
                    dataTable.Load(rdr);
                    
                }
                catch (Exception ExCargaInfoInterno)
                {
                    Console.WriteLine(ExCargaInfoInterno.Message);
                }
                finally
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                }

                return dataTable;
            }            
        }
        public DataTable obtieneNombreActiveDirectory(String elNombre)
        {
            BDC objCnn = new BDC();
            var dataTable = new DataTable();
            using (OracleConnection cnn = objCnn.getConection())
            {
                try
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("SELECT USER_TYPE, USER_ID,EMAIL,USER_NAME,EMPLEADO,USER_LEVEL FROM ALLUSERS" +
                         " WHERE USER_ID = :pUSER_ID");
                    cmd.Parameters.Add("pUSER_ID", OracleDbType.Char);
                    cmd.Parameters["pUSER_ID"].Value = elNombre;
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader rdr = cmd.ExecuteReader();
                    dataTable.Load(rdr);
                   
                }
                catch (Exception ExCargaInfoInterno)
                {
                    Console.WriteLine(ExCargaInfoInterno.Message);
                }
                finally
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                }
                return dataTable;
            }
        }
        public DataTable locaciones()
        {
            BDC objCnn = new BDC();
            var dataTable = new DataTable();
            using (OracleConnection cnn = objCnn.getConection())
            {
                try
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("SELECT ID,LOCACION FROM RH_LOCACIONES ORDER BY ID");
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader rdr = cmd.ExecuteReader();
                    dataTable.Load(rdr);

                }
                catch (Exception ExCargaInfoInterno)
                {
                    Console.WriteLine(ExCargaInfoInterno.Message);
                }
                finally
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                }
            }
                return dataTable;
        }

        public Boolean actualizaLocaciones(String elID, String locacion)
        {
            Boolean elBuleano = true;
            BDC objCnn = new BDC();
            using (OracleConnection cnn = objCnn.getConection())
            {
                try
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand(
                        "UPDATE RH_LOCACIONES RH_LOCACIONES SET RH_LOCACIONES.LOCACION = :pLOCACION WHERE RH_LOCACIONES.ID = :pID ");

                    cmd.Parameters.Add("pLOCACION", OracleDbType.Varchar2);
                    cmd.Parameters["pLOCACION"].Value = locacion;
                    cmd.Parameters.Add("pID", OracleDbType.Varchar2);
                    cmd.Parameters["pID"].Value = elID;
                    
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader rdr = cmd.ExecuteReader();

                }
                catch (Exception ExCargaInfoInterno)
                {
                    elBuleano = false;
                    Console.WriteLine(ExCargaInfoInterno.Message);
                }
                finally
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                }
            }
            return elBuleano;
        }
        public Boolean BorraLocaciones(String elID)
        {
            Boolean elBuleano = true;
            BDC objCnn = new BDC();
            using (OracleConnection cnn = objCnn.getConection())
            {
                try
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand(
                        "DELETE FROM RH_LOCACIONES WHERE RH_LOCACIONES.ID = :pID ");
                    cmd.Parameters.Add("pID", OracleDbType.Varchar2);
                    cmd.Parameters["pID"].Value = elID;
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader rdr = cmd.ExecuteReader();

                }
                catch (Exception ExCargaInfoInterno)
                {
                    elBuleano = false;
                    Console.WriteLine(ExCargaInfoInterno.Message);
                }
                finally
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                }
            }
            return elBuleano;
        }
        public Boolean CreaLocaciones(String Locacion)
        {
            Boolean elBuleano = true;
            BDC objCnn = new BDC();
            using (OracleConnection cnn = objCnn.getConection())
            {
                try
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("SELECT MAX(ID) as ID from RH_LOCACIONES");
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader rdr = cmd.ExecuteReader();
                    String elID="";
                    while (rdr.Read())
                    {
                         elID= rdr["ID"].ToString();
                    }
                    int j;
                    elBuleano = Int32.TryParse(elID, out j);
                    j++;
                    elID = j.ToString("000");
                    cmd = new OracleCommand(
                        "INSERT INTO RH_LOCACIONES(ID,LOCACION) "+
                        "VALUES (:pID, :pLOCACION) ");
                    cmd.Parameters.Add("pID", OracleDbType.Varchar2);
                    cmd.Parameters["pID"].Value = elID;
                    cmd.Parameters.Add("pLOCACION", OracleDbType.Varchar2);
                    cmd.Parameters["pLOCACION"].Value = Locacion;

                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    rdr = cmd.ExecuteReader();
                }
                catch (Exception ExCargaInfoInterno)
                {
                    elBuleano = false;
                    Console.WriteLine(ExCargaInfoInterno.Message);
                }
                finally
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                }
            }
            return elBuleano;
        }

        public DataTable TipoContratos()
        {
            BDC objCnn = new BDC();
            var dataTable = new DataTable();
            using (OracleConnection cnn = objCnn.getConection())
            {
                try
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("SELECT ID,CONTRATO FROM RH_TIPOCONTRATO ORDER BY ID");
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader rdr = cmd.ExecuteReader();
                    dataTable.Load(rdr);

                }
                catch (Exception ExCargaInfoInterno)
                {
                    Console.WriteLine(ExCargaInfoInterno.Message);
                }
                finally
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                }
            }
            return dataTable;
        }
        public Boolean ActualizaContratos(String elID, String elContrato)
        {
            Boolean elBooleano = true;
            BDC objCnn = new BDC();
            using (OracleConnection cnn = objCnn.getConection())
            {
                try
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("UPDATE RH_TIPOCONTRATO SET RH_TIPOCONTRATO.CONTRATO = :pCONTRATO WHERE RH_TIPOCONTRATO.ID = :pID ");

                    cmd.Parameters.Add("pCONTRATO", OracleDbType.Varchar2);
                    cmd.Parameters["pCONTRATO"].Value = elContrato;
                    cmd.Parameters.Add("pID", OracleDbType.Varchar2);
                    cmd.Parameters["pID"].Value = elID;
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader rdr = cmd.ExecuteReader();

                }
                catch (Exception ExCargaInfoInterno)
                {
                    elBooleano = false;
                    Console.WriteLine(ExCargaInfoInterno.Message);
                }
                finally
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                }
            }
            return elBooleano;
        }
        public Boolean BorraContratos(String elID)
        {
            Boolean elBooleano = true;
            BDC objCnn = new BDC();
            using (OracleConnection cnn = objCnn.getConection())
            {
                try
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("DELETE FROM RH_TIPOCONTRATO WHERE RH_TIPOCONTRATO.ID = :pID ");
                    cmd.Parameters.Add("pID", OracleDbType.Varchar2);
                    cmd.Parameters["pID"].Value = elID;
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader rdr = cmd.ExecuteReader();

                }
                catch (Exception ExCargaInfoInterno)
                {
                    elBooleano = false;
                    Console.WriteLine(ExCargaInfoInterno.Message);
                }
                finally
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                }
            }
            return elBooleano;
        }

        public Boolean CreaContratos(String elContrato)
        {
            Boolean elBooleano = true;
            BDC objCnn = new BDC();
            using (OracleConnection cnn = objCnn.getConection())
            {
                try
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("SELECT MAX(ID) as ID from RH_TIPOCONTRATO");
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader rdr = cmd.ExecuteReader();
                    String elID = "";
                    while (rdr.Read())
                    {
                        elID = rdr["ID"].ToString();
                    }
                    int j;
                    elBooleano = Int32.TryParse(elID, out j);
                    j++;
                    elID = j.ToString("000");
                    cmd = new OracleCommand(
                        "INSERT INTO RH_TIPOCONTRATO(ID,CONTRATO) " +
                        "VALUES (:pID, :pCONTRATO) ");
                    cmd.Parameters.Add("pID", OracleDbType.Varchar2);
                    cmd.Parameters["pID"].Value = elID;
                    cmd.Parameters.Add("pCONTRATO", OracleDbType.Varchar2);
                    cmd.Parameters["pCONTRATO"].Value = elContrato;
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    rdr = cmd.ExecuteReader();
                }
                catch (Exception ExCargaInfoInterno)
                {
                    elBooleano = false;
                    Console.WriteLine(ExCargaInfoInterno.Message);
                }
                finally
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                }
            }
            return elBooleano;
        }

        /*
         * Para los horarios
         * 
         *         
         */
        public DataTable ObtieneHorarios()
        {
            BDC objCnn = new BDC();
            var dataTable = new DataTable();
            using (OracleConnection cnn = objCnn.getConection())
            {
                try
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("SELECT ID, HORARIO FROM RH_HORARIO ORDER BY ID");
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader rdr = cmd.ExecuteReader();
                    dataTable.Load(rdr);
                }
                catch (Exception ExCargaInfoInterno)
                {
                    Console.WriteLine(ExCargaInfoInterno.Message);
                }
                finally
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                }
            }
            return dataTable;
        }
        /*
         * Para la escolaridad
         * 
         *         
         */
        public DataTable ObtieneEscolaridad()
        {
            BDC objCnn = new BDC();
            var dataTable = new DataTable();
            using (OracleConnection cnn = objCnn.getConection())
            {
                try
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("SELECT ID, DESCRIPCION FROM RH_ESCOLARIDAD ORDER BY ID");
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader rdr = cmd.ExecuteReader();
                    dataTable.Load(rdr);
                }
                catch (Exception ExCargaInfoInterno)
                {
                    Console.WriteLine(ExCargaInfoInterno.Message);
                }
                finally
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                }
            }
            return dataTable;
        }
        public Boolean ActualizaHorario(String elID, String elHorario)
        {
            Boolean elBooleano = true;
            BDC objCnn = new BDC();
            using (OracleConnection cnn = objCnn.getConection())
            {
                try
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("UPDATE RH_HORARIO SET RH_HORARIO.HORARIO = :pHORARIO WHERE RH_HORARIO.ID = :pID ");
                    cmd.Parameters.Add("pHORARIO", OracleDbType.Varchar2);
                    cmd.Parameters["pHORARIO"].Value = elHorario;
                    cmd.Parameters.Add("pID", OracleDbType.Varchar2);
                    cmd.Parameters["pID"].Value = elID;
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader rdr = cmd.ExecuteReader();

                }
                catch (Exception ExCargaInfoInterno)
                {
                    elBooleano = false;
                    Console.WriteLine(ExCargaInfoInterno.Message);
                }
                finally
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                }
            }
            return elBooleano;
        }
        public Boolean BorraHorario(String elID)
        {
            Boolean elBooleano = true;
            BDC objCnn = new BDC();
            using (OracleConnection cnn = objCnn.getConection())
            {
                try
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("DELETE FROM RH_HORARIO WHERE RH_HORARIO.ID = :pID ");
                    cmd.Parameters.Add("pID", OracleDbType.Varchar2);
                    cmd.Parameters["pID"].Value = elID;
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader rdr = cmd.ExecuteReader();

                }
                catch (Exception ExCargaInfoInterno)
                {
                    elBooleano = false;
                    Console.WriteLine(ExCargaInfoInterno.Message);
                }
                finally
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                }
            }
            return elBooleano;
        }

        public Boolean CreaHorario(String elContrato)
        {
            Boolean elBooleano = true;
            BDC objCnn = new BDC();
            using (OracleConnection cnn = objCnn.getConection())
            {
                try
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("SELECT MAX(ID) as ID from RH_HORARIO");
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    OracleDataReader rdr = cmd.ExecuteReader();
                    String elID = "";
                    while (rdr.Read())
                    {
                        elID = rdr["ID"].ToString();
                    }
                    int j;
                    elBooleano = Int32.TryParse(elID, out j);
                    j++;
                    elID = j.ToString("000");
                    cmd = new OracleCommand(
                        "INSERT INTO RH_HORARIO(ID,HORARIO) " +
                        "VALUES (:pID, :pHORARIO) ");
                    cmd.Parameters.Add("pID", OracleDbType.Varchar2);
                    cmd.Parameters["pID"].Value = elID;
                    cmd.Parameters.Add("pHORARIO", OracleDbType.Varchar2);
                    cmd.Parameters["pHORARIO"].Value = elContrato;
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    rdr = cmd.ExecuteReader();
                }
                catch (Exception ExCargaInfoInterno)
                {
                    elBooleano = false;
                    Console.WriteLine(ExCargaInfoInterno.Message);
                }
                finally
                {
                    if (cnn.State == ConnectionState.Open)
                        cnn.Close();
                }
            }
            return elBooleano;
        }

        //Fin de la clase
    }
}