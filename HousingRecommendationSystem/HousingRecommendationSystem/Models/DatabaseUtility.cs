using System;
using System.Data.SqlClient;
using System.Text;

namespace HousingRecommendationSystem.Models
{
    public class DatabaseUtility : IDatabaseUtility
    {
        public System.Collections.Generic.IEnumerable<PropertyModel> GetPropertyModelByBucketId(string bucketId)
        {
            var result = new System.Collections.Generic.List<PropertyModel>();
            try 
            { 
                System.Configuration.ConnectionStringSettings con = System.Configuration.ConfigurationManager.ConnectionStrings["walkietechkieEntities"];

                using (SqlConnection connection = new SqlConnection(con.ConnectionString))
                {
                    connection.Open();
                    var sql = @"SELECT TOP 100 PropertyName, longitude_units, latitude_units,PropertyAddress,
                                PropertyType,new_price FROM TBL_Propery WHERE Amenities_Compile = '{0}'";
                        
                    using (SqlCommand command = new SqlCommand(String.Format(sql, bucketId), connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new PropertyModel
                                {
                                    PropertyName = reader.GetString(0) + " -" + reader.GetString(3) + " -" + reader.GetString(4) + " - $" + String.Format("{0:c}", reader.GetDouble(5).ToString()),
                                    Longitude = reader.GetDouble(1),
                                    Latitude = reader.GetDouble(2),
                                });
                            }
                        }
                    }

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            return result;
        }
    }
}