using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace WebCoreRest.Controllers
{
	[Route("api/GetJP")]
	[ApiController]
	public class OracleGetController : ControllerBase
	{
		[HttpGet]
		public string Get() 
        {

			//Create a connection to Oracle			
			string conString = "User Id=parus;Password=2wsxZAQ1q;" +

			//How to connect to an Oracle DB without SQL*Net configuration file
			//  also known as tnsnames.ora.
			//"Data Source=miacyp";
			"Data Source=192.168.0.242:1521/miac";
            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    var context = HttpContext;
                    try
                    {

                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "select CODE, NAME from JURPERSONS /*where CODE = :agnabbr*/";

                        // Assign id to the department number 50 
                        ///OracleParameter id = new OracleParameter("id", 50);
                        //cmd.Parameters.Add(id);
                        //OracleParameter agnabbr = new OracleParameter("agnabbr", "6283");
                        //cmd.Parameters.Add(agnabbr);

                        //Execute the command and use DataReader to display the data
                        
                        Task<string> content;
                        OracleDataReader reader = cmd.ExecuteReader();
                        context.Response.Headers["Content-Type"] = "text/plain; charset=utf-8";
                        while (reader.Read())
                        {
                            context.Response.WriteAsync("Юл: " + reader.GetString(0) + " " + reader.GetString(1) + "\n");
                        }
                        return reader.ToString();
                        //reader.Dispose();
                    }
                    catch (Exception ex)
                    {
                       return context.Response.WriteAsync(ex.Message).ToString();
                    }
                    con.Close();
                }
            }
        }

        [HttpGet("{code}")]
        public string Get(string code)
        {

            //Create a connection to Oracle			
            string conString = "User Id=parus;Password=2wsxZAQ1q;" +

            //How to connect to an Oracle DB without SQL*Net configuration file
            //  also known as tnsnames.ora.
            //"Data Source=miacyp";
            "Data Source=192.168.0.242:1521/miac";
            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    var context = HttpContext;
                    try
                    {

                        con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "select CODE, NAME from JURPERSONS where CODE = :code";
                        OracleParameter jp_code = new OracleParameter("code", code);
                        cmd.Parameters.Add(jp_code);

                        //Execute the command and use DataReader to display the data

                        Task<string> content;
                        OracleDataReader reader = cmd.ExecuteReader();
                        context.Response.Headers["Content-Type"] = "text/plain; charset=utf-8";
                        while (reader.Read())
                        {
                            context.Response.WriteAsync("Юл: " + reader.GetString(0) + " " + reader.GetString(1) + "\n");
                        }
                        return reader.ToString();
                        //reader.Dispose();
                    }
                    catch (Exception ex)
                    {
                        return context.Response.WriteAsync(ex.Message).ToString();
                    }
                    con.Close();
                }
            }
        }

    }
}
