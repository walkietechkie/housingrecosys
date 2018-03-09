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
            bucketId = bucketId.Replace("_", "%");
            try 
            { 
                System.Configuration.ConnectionStringSettings con = System.Configuration.ConfigurationManager.ConnectionStrings["walkietechkieEntities"];

                using (SqlConnection connection = new SqlConnection(con.ConnectionString))
                {
                    connection.Open();
                    var sql = string.Empty;
                    var lastChar = bucketId[bucketId.Length - 1];
                    bucketId = bucketId.Remove(bucketId.Length - 1);
                    if (lastChar.Equals('1'))
                    {
                        sql = @"SELECT TOP 1000 PropertyName, longitude_units, latitude_units,PropertyAddress,
                                PropertyType,new_price FROM TBL_Property WHERE Search like '{0}'
                                 and PropertyType = 'Condominiums'";
                        sql = String.Format(sql, bucketId); //bucketId
                    }
                    else
                    {
                        sql = @"SELECT TOP 1000 PropertyName, longitude_units, latitude_units,PropertyAddress,
                                PropertyType,new_price FROM TBL_Property WHERE Search like '{0}'";
                        sql = String.Format(sql, bucketId); //bucketId
                    }

                    //var sql = "SELECT PropertyName, longitude_units, latitude_units FROM TBL_Propery WHERE Amenities_Compile = '{0}'";
                        
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new PropertyModel
                                {
                                    PropertyName = reader.GetString(0) + " - "+ reader.GetString(3) + " - " + reader.GetString(4) + " - $" + reader.GetDouble(5).ToString(),
                                    Longitude = reader.GetDouble(2),
                                    Latitude = reader.GetDouble(1),
                                });
                            }
                        }
                    }
                    //do check if the result is null then retrun 5 for the Location and Budget
                    if(result.Count == 0)
                    {
                        sql = @"SELECT TOP 5 PropertyName, longitude_units, latitude_units,PropertyAddress,
                                PropertyType,new_price FROM TBL_Property WHERE Search like '{0}%'";
                        sql = String.Format(sql, bucketId.Substring(0,4)); //bucketId
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    result.Add(new PropertyModel
                                    {
                                        PropertyName = reader.GetString(0) + " - " + reader.GetString(3) + " - " + reader.GetString(4) + " - $" + reader.GetDouble(5).ToString(),
                                        Longitude = reader.GetDouble(2),
                                        Latitude = reader.GetDouble(1),
                                    });
                                }
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