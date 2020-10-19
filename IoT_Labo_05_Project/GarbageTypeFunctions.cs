using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using IoT_Labo_05_Project.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace IoT_Labo_05_Project
{
    public static class GarbageTypeFunctions
    {
        [FunctionName("GetGarbageTypes")]
        public static async Task<IActionResult> GetGarbageTypes(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "categorieen")] HttpRequest req,
        ILogger log)
        {
            try
            {
                List<GarbageType> garbageTypeList = new List<GarbageType>();
                string connectionString = Environment.GetEnvironmentVariable("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM GarbageType";

                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            GarbageType garbageType = new GarbageType();
                            garbageType.GarbageTypeId = Guid.Parse(reader["GarbageTypeId"].ToString());
                            garbageType.Name = reader["Name"].ToString();

                            garbageTypeList.Add(garbageType);
                        }
                    }
                }

                return new OkObjectResult(garbageTypeList); //stuur object terug
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                throw;
            }
        }
    }
}
