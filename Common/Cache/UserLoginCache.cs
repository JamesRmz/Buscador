using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Aqui se guardan los datos de Usuario para iniciar sesion 
namespace Common.Cache
{
    public static class UserLoginCache
    {
        public static int IdUser { get; set;}
        public static string LoginName { get; set;}
        public static string Password { get; set;}
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string Position { get; set; }
        public static string Email { get; set; }

    }
}
