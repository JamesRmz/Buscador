using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.Data;
using System.Data.SqlClient;
using Common.Cache;

namespace Domain
{
    public class UserModel
    {
        UserDao userDao = new UserDao();

        private int idUser;
        private string loginName;
        private string password;
        private string nombre;
        private string apellido;
        private string cargo;
        private string email;

        public UserModel(int idUser,string loginName, string password, string nombre, string apellido, string cargo, string email)
        {
            this.idUser = idUser;
            this.loginName = loginName;
            this.password = password;
            this.nombre = nombre;
            this.apellido = apellido;
            this.cargo = cargo;
            this.email = email;
        }
        public UserModel()
        {

        }

        public DataTable MostrarUsuarios()
        {

            DataTable tabla = new DataTable();
            tabla = userDao.Mostrar();
            return tabla;
        }
        public DataTable MostrarPerfiles()
        {
            DataTable dt = new DataTable();
            dt = userDao.CargarCombo();
            return dt;
        }
        //public string editarPerfilUsuario()
        //{
        //    try
        //    {
        //        userDao.editProfile(idUser, loginName, password, nombre, apellido, email);
        //        LoginUser(loginName, password);
        //        return "El perfil ah sido Actuaizado";

        //    }
        //    catch (Exception ex)
        //    {
        //        return "Usuario existente, intente con otro "+ ex;
           
        //    }
            
        //}
        //------------- insertarUsuarios---------------------//


        public void InsertUser(string userName, string Password, string name, string lastName,string Position, string mail)
        {
            userDao.InsertUser(userName,Password,name,lastName,Position,mail);
        }

        //--------------EditarUsuarios-----------------------//


        public void EditarUsuarios(String LoginName, string Password, string FirstName, string LastName, string Position, string Email, string IdUser)
        {

            userDao.EditarUsuario(LoginName, Password, FirstName, LastName, Position, Email, Convert.ToInt32(IdUser));
        }

        public void EliminarUsuario(string Id)
        {
            userDao.EliminarUsuario(Convert.ToInt32(Id));
        }


        //lOGEO
        public bool LoginUser(string user, string pass)
        {
            return userDao.Login(user, pass);
        }


        //RECUPERAR CONTRASEÑA
        public string recoverPassword(string userRequesting)
        {
            return userDao.recoverPassword(userRequesting);
        }
        public void AnyMethod()
        {
            //Seguridad y permisos 
            if (UserLoginCache.Position == Positions.Administrator)
            {

            }
            if (UserLoginCache.Position == Positions.User)
            {

            }
        }


    }
}
