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

    public class ProductoRepository
    {

        private string _connectionString { get; set; }
        public ProductoRepository()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            _connectionString = configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
        }
        public void Create(String producto, Double costo, int serie, int stock, string estado)
        {
            // okay next we create skeleton for the code


            MySqlTransaction mySqlTransaction = null;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {

                    connection.Open();

                    mySqlTransaction = connection.BeginTransaction();

                    string SQL = "INSERT INTO productos VALUES (null,@producto,@costo,@serie,@stock,@estado);";
                    MySqlCommand mySqlCommand = new MySqlCommand(SQL, connection);
                    // we add some parameter
                    mySqlCommand.Parameters.AddWithValue("@producto", producto);
                    mySqlCommand.Parameters.AddWithValue("@costo", costo);
                    mySqlCommand.Parameters.AddWithValue("@serie", serie);
                    mySqlCommand.Parameters.AddWithValue("@stock", stock);
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
        public List<ProductoModel> Read()
        {
            List<ProductoModel> ProductoModel = new();


            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {

                    connection.Open();
                    string SQL = "SELECT * FROM productos ";
                    MySqlCommand mySqlCommand = new MySqlCommand(SQL, connection);
                    using (var reader = mySqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            ProductoModel.Add(new ProductoModel()
                            {

                                Producto = reader["producto"].ToString(),
                                Costo = Convert.ToDouble(reader["costo"]),
                                Serie = Convert.ToInt32(reader["serie"]),
                                Stock = Convert.ToInt32(reader["stock"]),
                                Estado = reader["estado"].ToString(),
                                ProductoId = Convert.ToInt32(reader["productoId"])
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


            return ProductoModel;
        }
        public void Update(String producto, Double costo, int serie, int stock, string estado,  int productoId)
        {

            MySqlTransaction mySqlTransaction = null;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {

                    connection.Open();
                    mySqlTransaction = connection.BeginTransaction();

                    string SQL = "UPDATE productos SET producto = @producto, costo = @costo, serie = @serie,stock = @stock , estado = @estado WHERE productoId = @productoId ";
                    MySqlCommand mySqlCommand = new MySqlCommand(SQL, connection);
                    // we add some parameter
                    mySqlCommand.Parameters.AddWithValue("@producto", producto);
                    mySqlCommand.Parameters.AddWithValue("@costo", costo);
                    mySqlCommand.Parameters.AddWithValue("@serie", serie);
                    mySqlCommand.Parameters.AddWithValue("@stock", stock);
                    mySqlCommand.Parameters.AddWithValue("@estado", estado);
                    mySqlCommand.Parameters.AddWithValue("@productoId", productoId);

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
        public void Delete(int productoId)
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

                    string SQL = "DELETE FROM productos WHERE productoId  = @productoId;";
                    MySqlCommand mySqlCommand = new MySqlCommand(SQL, connection);
                    // we add some parameter
                    mySqlCommand.Parameters.AddWithValue("@productoId", productoId);
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
