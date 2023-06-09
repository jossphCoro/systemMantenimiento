﻿using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
// so we want to connect mysql so nuget
// so the next video will upon linking with the web api ..
// now continue back to make web api
// now the api been handle ? what with web ? and mobile ? next video 
namespace systemMantenimiento
{
    // the next stage kinda hard is how does we get information from those app setting ?

    public class CategoriaRepository
    {

        private string _connectionString { get; set; }
        public CategoriaRepository()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            _connectionString = configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
        }
        public void Create(String nombre, String tipo, String descripcion)
        {
            // okay next we create skeleton for the code


            MySqlTransaction mySqlTransaction = null;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {

                    connection.Open();

                    mySqlTransaction = connection.BeginTransaction();

                    string SQL = "INSERT INTO categorias VALUES (null,@nombre,@tipo,@descripcion);";
                    MySqlCommand mySqlCommand = new MySqlCommand(SQL, connection);
                    // we add some parameter
                    mySqlCommand.Parameters.AddWithValue("@nombre", nombre);
                    mySqlCommand.Parameters.AddWithValue("@tipo", tipo);
                    mySqlCommand.Parameters.AddWithValue("@descripcion", descripcion);
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
        public List<CategoriaModel> Read()
        {
            List<CategoriaModel> CategoriaModel = new();

           
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {

                    connection.Open();
                    string SQL = "SELECT * FROM categorias ";
                    MySqlCommand mySqlCommand = new MySqlCommand(SQL, connection);
                    using (var reader = mySqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                           
                            CategoriaModel.Add(new CategoriaModel()
                            {

                                Nombre = reader["nombre"].ToString(),
                                Tipo = reader["tipo"].ToString(),
                                Descripcion = reader["descripcion"].ToString(),
                                CategoriaId = Convert.ToInt32(reader["categoriaId"])
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


            return CategoriaModel;
        }
        public void Update(String nombre, String tipo, String descripcion, int categoriaId)
        {

            MySqlTransaction mySqlTransaction = null;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {

                    connection.Open();
                    mySqlTransaction = connection.BeginTransaction();

                    string SQL = "UPDATE categorias SET nombre = @nombre, tipo = @tipo, descripcion = @descripcion WHERE categoriaId = @categoriaId ";
                    MySqlCommand mySqlCommand = new MySqlCommand(SQL, connection);
                    // we add some parameter
                    mySqlCommand.Parameters.AddWithValue("@nombre", nombre);
                    mySqlCommand.Parameters.AddWithValue("@tipo", tipo);
                    mySqlCommand.Parameters.AddWithValue("@descripcion", descripcion);
                    mySqlCommand.Parameters.AddWithValue("@categoriaId", categoriaId);

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
        public void Delete(int categoriaId)
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

                    string SQL = "DELETE FROM categorias WHERE categoriaId  = @categoriaId;";
                    MySqlCommand mySqlCommand = new MySqlCommand(SQL, connection);
                    // we add some parameter
                    mySqlCommand.Parameters.AddWithValue("@categoriaId", categoriaId);
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
