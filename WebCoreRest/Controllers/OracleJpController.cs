using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace WebCoreRest.Controllers
{
    public class Jurpers
    {
        public Int64 rn { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }

    [Route("api/GetJurpers")]
	[ApiController]
	public class OracleJpController : ControllerBase
	{
        [HttpGet]
        [Produces("application/json")]
        public IEnumerable<Jurpers> Get()
        {
            //Create a connection to Oracle			
            string conString = "User Id=parus;Password=2wsxZAQ1q;" +
            "Data Source=192.168.0.242:1521/miac";

            OracleConnection con = new OracleConnection(conString);
            con.Open();

            string cmdQuery = "select RN, CODE, NAME from JURPERSONS";

            // Create the OracleCommand
            OracleCommand cmd = new OracleCommand(cmdQuery);

            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;

                // Execute command, create OracleDataReader object
                OracleDataReader reader = cmd.ExecuteReader();

                var JurpersList = new List<Jurpers>();

                while (reader.Read())
                {
                    JurpersList.Add(new Jurpers
                    {
                        rn = reader.GetInt64(0),
                        code = reader.GetString(1),
                        name = reader.GetString(2)
                    });
                }
                return JurpersList;
            }
            catch (Exception ex)
            {
                throw ex;
                //return ex..Message.ToString();
            }
            con.Close();

        }
       

        [HttpGet("{code}")]
        [Produces("application/json")]
        public IEnumerable<Jurpers> Get(string code)
        {
            string conString = "User Id=parus;Password=2wsxZAQ1q;" +
            "Data Source=192.168.0.242:1521/miac";

            OracleConnection con = new OracleConnection(conString);
            OracleParameter jp_code = new OracleParameter("code", code);
            con.Open();

            string cmdQuery = "select RN, CODE, NAME from JURPERSONS where CODE = :code";

            // Create the OracleCommand
            OracleCommand cmd = new OracleCommand(cmdQuery);

            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(jp_code);

                // Execute command, create OracleDataReader object
                OracleDataReader reader = cmd.ExecuteReader();

                var JurpersList = new List<Jurpers>();

                while (reader.Read())
                {
                    JurpersList.Add(new Jurpers
                    {
                        rn = reader.GetInt64(0),
                        code = reader.GetString(1),
                        name = reader.GetString(2)
                    });
                }
                return JurpersList;
            }
            catch (Exception ex)
            {
                throw ex;
                //return ex..Message.ToString();
            }
            con.Close();
        }

    }
}
