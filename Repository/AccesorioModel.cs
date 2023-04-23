using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using systemMantenimiento.Models;
// so we want to connect mysql so nuget
// so the next video will upon linking with the web api ..
// now continue back to make web api
// now the api been handle ? what with web ? and mobile ? next video 
namespace systemMantenimiento
{
    // the next stage kinda hard is how does we get information from those app setting ?

    public class AccesorioRepository
    {

        private string _connectionString { get; set; }
        public AccesorioRepository()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            _connectionString = configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
        }
        public void Create(String accesorio, String descripcion, Double costo, int cantidad, String estado)
        {
            // okay next we create skeleton for the code


            MySqlTransaction mySqlTransaction = null;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {

                    connection.Open();

                    mySqlTransaction = connection.BeginTransaction();

                    string SQL = "INSERT INTO accesorios VALUES (null,@accesorio,@descripcion, @costo, @cantidad, @estado);";
                    MySqlCommand mySqlCommand = new MySqlCommand(SQL, connection);
                    // we add some parameter
                    mySqlCommand.Parameters.AddWithValue("@accesorio", accesorio);
                    mySqlCommand.Parameters.AddWithValue("@descripcion", descripcion);
                    mySqlCommand.Parameters.AddWithValue("@costo", costo);
                    mySqlCommand.Parameters.AddWithValue("@cantidad", cantidad);
                    mySqlCommand.Parameters.AddWithValue("@estado", estado);
                    mySqlCommand.ExecuteNonQuery();
                    // we have error here .. 
                    mySqlTransaction.Commit();
                    // c;ear some memory 
                    mySqlCommand.Dispose();

                }
                catch (MySqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }




        }
        public List<AccesorioModel> Read()
        {
            List<AccesorioModel> AccesorioModel = new();


            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {

                    connection.Open();
                    string SQL = "SELECT * FROM accesorios ";
                    MySqlCommand mySqlCommand = new MySqlCommand(SQL, connection);
                    using (var reader = mySqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            AccesorioModel.Add(new AccesorioModel()
                            {

                                Accesorio = reader["accesorio"].ToString(),
                                Descripcion = reader["descripcion"].ToString(),
                                Costo = Convert.ToDouble(reader["costo"]),
                                Cantidad = Convert.ToInt32(reader["cantidad"]),
                                Estado = reader["estado"].ToString(),

                                AccesorioId = Convert.ToInt32(reader["accesorioId"])
                            });
                        }
                    }


                    // c;ear some memory 
                    mySqlCommand.Dispose();

                }
                catch (MySqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    /// you may throw new exception here or use any log debugger to text file 
                }
            }


            return AccesorioModel;
        }
        public void Update(String accesorio, String descripcion, Double costo, int cantidad, String estado, int accesorioId)
        {

            MySqlTransaction mySqlTransaction = null;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {

                    connection.Open();
                    mySqlTransaction = connection.BeginTransaction();

                    string SQL = "UPDATE accesorios SET accesorio = @accesorio, descripcion = @descripcion, costo = @costo, cantidad = @cantidad,estado = @estado WHERE accesorioId = @accesorioId ";
                    MySqlCommand mySqlCommand = new MySqlCommand(SQL, connection);
                    // we add some parameter
                    mySqlCommand.Parameters.AddWithValue("@accesorio", accesorio);
                    mySqlCommand.Parameters.AddWithValue("@descripcion", descripcion);
                    mySqlCommand.Parameters.AddWithValue("@costo", costo);
                    mySqlCommand.Parameters.AddWithValue("@cantidad", cantidad);
                    mySqlCommand.Parameters.AddWithValue("@estado", estado);
                    mySqlCommand.Parameters.AddWithValue("@accesorioId", accesorioId);

                    mySqlCommand.ExecuteNonQuery();

                    mySqlTransaction.Commit();
                    // c;ear some memory 
                    mySqlCommand.Dispose();

                }
                catch (MySqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    /// you may throw new exception here or use any log debugger to text file 
                }
            }
        }
        public void Delete(int accesorioId)
        {


            MySqlTransaction mySqlTransaction = null;
            // the old using is using(xxx) { }  now it's weirder 
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {

                    connection.Open();
                    // sorry we tend to forget sometimes..  but with error like this you all can remember what's error if failure not all working  fine :P
                    mySqlTransaction = connection.BeginTransaction();

                    string SQL = "DELETE FROM accesorios WHERE accesorioId  = @accesorioId;";
                    MySqlCommand mySqlCommand = new MySqlCommand(SQL, connection);
                    // we add some parameter
                    mySqlCommand.Parameters.AddWithValue("@accesorioId", accesorioId);
                    mySqlCommand.ExecuteNonQuery();

                    mySqlTransaction.Commit();
                    // c;ear some memory 
                    mySqlCommand.Dispose();

                }
                catch (MySqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    /// you may throw new exception here or use any log debugger to text file 
                }
            }
        }

    }
}
