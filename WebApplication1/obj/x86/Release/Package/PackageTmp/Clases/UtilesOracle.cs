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
                            "UBICACION = :pUBICACION, TIPO_CONTRATO = :pTIPO_CONTRATO, SEXO = :pSEXO " +
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
                cmd.Parameters.Add("pSEXO", OracleDbType.Varchar2);
                cmd.Parameters["pSEXO"].Value = vacanteActualizar.Sexo;

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
            OracleCommand oracmd = new OracleCommand( "SELECT RH_VACANTES.ID, RH_VACANTES.PUESTO, RH_VACANTES.DESCRIPCION,RH_VACANTES.COMPETENCIAS, RH_LOCACIONES.LOCACION as UBICACION, RH_TIPOCONTRATO.CONTRATO as TIPO_CONTRATO, RH_VACANTES.RANGO_EDAD, RH_VACANTES.SEXO, RH_VACANTES.STATUS "+
                " FROM RH_VACANTES "+
                " LEFT JOIN RH_LOCACIONES"+
                " on RH_VACANTES.UBICACION = RH_LOCACIONES.ID"+
                " left join RH_TIPOCONTRATO"+
                " ON RH_VACANTES.TIPO_CONTRATO = RH_TIPOCONTRATO.ID"+
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
                        " where USER_ID=:pUSER_ID and GROUP_ID ='rhapp'");
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
                    OracleCommand cmd = new OracleCommand("SELECT ID,LOCACION FROM RH_LOCACIONES");
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
        public DataTable TipoContratos()
        {
            BDC objCnn = new BDC();
            var dataTable = new DataTable();
            using (OracleConnection cnn = objCnn.getConection())
            {
                try
                {
                    cnn.Open();
                    OracleCommand cmd = new OracleCommand("SELECT ID,CONTRATO FROM RH_TIPOCONTRATO");
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
        //Fin de la clase
    }
}