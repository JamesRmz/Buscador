using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Common.Cache;

namespace DataAccess
{
    public class UserDao:ConnectionToSql
    {
       
        DataTable tabla = new DataTable();
        SqlDataReader leer;
       


        //Mostrar Usuarios//
        public DataTable Mostrar()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "MostrarUsuarios";
                    command.CommandType = CommandType.StoredProcedure;
                    leer = command.ExecuteReader();
                    tabla.Load(leer);

                    connection.Close();
                    return tabla;  
                }
            }
        }

        //Editar Perfil////
        //public void editProfile(int id, string userName, string Password, string name, string lastName, string mail)
        //{
        //    using(var connection = GetConnection())
        //    {
        //        connection.Open();
        //        using(var command = new SqlCommand())
        //        {
        //            command.Connection = connection;
        //            command.CommandText = "update Usuarios set " + 
        //                "LoginName=@userName, password=@Password, Nombre=@name, Apellidos=@lastName, Email=@mail where UsuarioID=@id";
        //            command.Parameters.AddWithValue("@UserName",userName);
        //            command.Parameters.AddWithValue("@Password",Password);
        //            command.Parameters.AddWithValue("@name",name);
        //            command.Parameters.AddWithValue("@lastName",lastName);
        //            command.Parameters.AddWithValue("@mail",mail);
        //            command.Parameters.AddWithValue("@id",id);
        //            command.CommandType = CommandType.Text;
        //            command.ExecuteNonQuery();

        //        }
        //    }
        //}

        //Editar Usuarios//
        public void EditarUsuario(String LoginName, string Password,string FirstName,string LastName,string Position,string Email, int IdUser)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "EditarUsuarios";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LoginName",LoginName);
                    command.Parameters.AddWithValue("@Password",Password);
                    command.Parameters.AddWithValue("@FirstName",FirstName);
                    command.Parameters.AddWithValue("@LastName",LastName);
                    command.Parameters.AddWithValue("@Position",Position);
                    command.Parameters.AddWithValue("@Email",Email);
                    command.Parameters.AddWithValue("@IdUser",IdUser);

                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    connection.Close();


                }
            }

        }

        //insertar Usuarios//
            public void InsertUser(string userName, string Password, string name, string lastName,string Position, string mail)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using(var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "InsertUser";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LoginName",userName);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@FirstName", name);
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.Parameters.AddWithValue("@Position",Position);
                    command.Parameters.AddWithValue("@Email", mail);

                    command.ExecuteNonQuery();


                    command.Parameters.Clear();
                    connection.Close();
                
                }
            }
        }
        //Eliminar Usuarios//
        public void EliminarUsuario(int Id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using(var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "EliminarUsuario";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdUser",Id);

                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    connection.Close();
                }
            }
        }

        //Cargar Combobox de Perfiles//
        public DataTable CargarCombo()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "Llenar_Combobox";
                    command.CommandType = CommandType.StoredProcedure;


                    leer = command.ExecuteReader();
                    tabla.Load(leer);

                    connection.Close();
                    return tabla;
                }
            }

        }
        //Iniciar sesion//
        public bool Login(string user, string pass)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "Select * from Usuarios where LoginName=@user and password=@pass";
                    command.Parameters.AddWithValue("@user", user);
                    command.Parameters.AddWithValue("@pass", pass);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UserLoginCache.IdUser = reader.GetInt32(0);
                            UserLoginCache.LoginName = reader.GetString(1);
                            UserLoginCache.Password = reader.GetString(2); 
                            UserLoginCache.FirstName = reader.GetString(3);
                            UserLoginCache.LastName = reader.GetString(4);
                            UserLoginCache.Position = reader.GetString(5);
                            UserLoginCache.Email = reader.GetString(6);
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        //Recuoerar contraseña por correo electronico//
        public string recoverPassword(string userRequesting)
        {
            using(var connection = GetConnection())
            {
                connection.Open();
                using(var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "Select * from Usuarios where LoginName=@user or Email=@email";
                    command.Parameters.AddWithValue("@user", userRequesting);
                    command.Parameters.AddWithValue("@email", userRequesting);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read()== true)
                    {
                        string userName = reader.GetString(3) + " " + reader.GetString(4);
                        string userMail = reader.GetString(6);
                        string accountPassword = reader.GetString(2);

                        var mailService = new MailServices.SystemSupportMail();
                        mailService.sendMail(
                            subject: "SYSTEM: Solicitud de recuperacion de contraseña.n",
                            body:"Hola  "+ userName +"\n\n\nSu contraseña es:  "
                            +accountPassword+ "\n\n\nPor favor realice el cambio de su contraseña inmediatamente al ingresar al Sistema",
                            recipientMail: new List<string> { userMail });
                        return "Hola, " + userName + "\nSolicitaste la recuperacaion de tu contraseña.\n " +
                            "Por favor revisa tu correo electronico" + userMail +
                            "\nPorvaor realice el cambio de su contraseña\n" + "\ninmediatamente al ingresar al Sistema\n";
                        
                    }
                    else
                    {
                        return "Lo sientimos no tenemos ninguna cuanta creada con ese nombre de usuario o email.";
                    }
                }
            }

        }

        //Perfiles//
        public void Anymethod()
        {
            if (UserLoginCache.Position == Positions.Administrator)
            {

            }
            if (UserLoginCache.Position == Positions.User)
            {

            }
        }

    }
}
