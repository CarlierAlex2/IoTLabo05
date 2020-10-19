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
    public static class CityFunctions
    {
        [FunctionName("GetCities")]
        public static async Task<IActionResult> GetCities(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "steden")] HttpRequest req,
        ILogger log)
        {
            try
            {
                List<City> cityList = new List<City>();
                string connectionString = Environment.GetEnvironmentVariable("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM City";

                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            City city = new City();
                            city.CityId = Guid.Parse(reader["CityId"].ToString());
                            city.Name = reader["Name"].ToString();

                            cityList.Add(city);
                        }
                    }
                }

                return new OkObjectResult(cityList); //stuur object terug
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                throw;
            }
        }
    }
}
