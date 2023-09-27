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
        private readonly SqlConnectionStringBuilder _connectionString;

        public AuthController(ILogger<AuthController> logger, SqlConnectionStringBuilder connectionString)
        {
            _logger = logger;
            _connectionString = connectionString;
        }

        [HttpPost]
        [AllowAnonymous]
        public bool Auth(string username, string password)
        {
            try {
                using (var connection = new SqlConnection(_connectionString.ConnectionString)) {

                    connection.Open();

                    String sql = "SELECT Username, Password FROM Users";

                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        using (SqlDataReader reader = command.ExecuteReader()) {
                            while (reader.Read()) {
                                if (username == reader.GetString(0) && password == reader.GetString(1)) { return true; }
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
