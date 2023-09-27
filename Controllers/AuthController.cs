using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace EveryMind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        //private readonly ConnectionStringSettings _connectionString;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
            //_connectionString = ConfigurationManager.ConnectionStrings["everymindAPI"];
        }

        [HttpPost]
        [AllowAnonymous]
        public bool Auth(string username, string password)
        {
            try {

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "34.121.179.44";
                builder.UserID = "sqlserver";
                builder.Password = "Jesusbebendopinga321@";
                builder.InitialCatalog = "everymindDB";


                using (var connection = new SqlConnection(builder.ConnectionString)) {

                    connection.Open();

                    String sql = "SELECT Username, Password FROM Users";

                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                var sqlUsername = reader.GetString(0);
                                var sqlPassword = reader.GetString(1);
                                if (username == sqlUsername && password == sqlPassword) { return true; }
                            }
                        }
                    }
                }
            }
            catch (SqlException e) {
                Console.WriteLine(e.ToString());
            }
            return false;
        }

            
        
    }
}
