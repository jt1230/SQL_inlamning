using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

namespace SQLövning
{
    internal class Databas
    {
        public string ConnectionString { get; set; } = @"Server=(localdb)\mssqllocaldb;Database={0};Trusted_Connection=True;";
        public string DatabaseName { get; set; } = "master";

        public void ExecuteSQL(string sql, ParamData[] parameters) // (string,string)[] => topple är ngt som fungerar som en variabel, men kan ta emot två eller fler variabler
        {
            // Sätt ihop connectionstring
            var connString = string.Format(ConnectionString, DatabaseName); //måsvinge 0, kmr att ersättas mot det andra parametern i parentesen
            // Förbered SQLConnection
            using (var connection = new SqlConnection(connString)) //måste köra/installera System.Data.SqlClient nuget för att denna ska fungera
            {
                // Öppna koppling till databasen
                connection.Open();
                // Förbered query
                using (var command = new SqlCommand(sql, connection))
                {
                    //använd parametrar
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            if (param != null && param.Name != null && param.Data != null) //ej nödvändigt, men för att vara extra säker på att inga parametrar är null 
                                command.Parameters.AddWithValue(param.Name, param.Data); //om ej null, försöka loopa igenom alla parametrarna.
                                                                                         //Om parametern i sig inte är null så ska den försöka lägga in det i parameterlistan
                        }
                    }
                    // Kör query
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetDataTable(string sql, ParamData[] parameters) // (string,string)() => topple är ngt som fungerar som en variabel, men kan ta emot två eller fler variabler
        {
            var dt = new DataTable(); //"nytt", skiljer sig från ExecuteSQL
            // Sätt ihop connectionstring
            var connString = string.Format(ConnectionString, DatabaseName); //måsvinge 0, kmr att ersättas mot det andra parametern i parentesen
            // Förbered SQLConnection
            using (var connection = new SqlConnection(connString)) //måste köra/installera System.Data.SqlClient nuget för att denna ska fungera
            {
                // Öppna koppling till databasen
                connection.Open();
                // Förbered query
                using (var command = new SqlCommand(sql, connection))
                {
                    //använd parametrar
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            if (param != null && param.Name != null && param.Data != null) //ej nödvändigt, men för att vara extra säker på att inga parametrar är null 
                                command.Parameters.AddWithValue(param.Name, param.Data); //om ej null, försöka loopa igenom alla parametrarna.
                                                                                         //Om parametern i sig inte är null så ska den försöka lägga in det i parameterlistan
                        }
                    }
                    // Kör query
                    using (var adapter = new SqlDataAdapter(command)) //"nytt", skiljer sig från ExecuteSQL
                    {
                        adapter.Fill(dt);
                    }

                    return dt;
                }
            }
        }

    }

    //Ifall vi inte skulle använda oss av en topple!
    public class ParamData
    {
        public string Name { get; set; }
        public string Data { get; set; }
    }
}