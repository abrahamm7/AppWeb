using AppWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWeb.Services
{
    public interface IUserModule
    {
        List<User> GetAllUsers(); //Get all users//
        void DeleteUser(int id); //Delete user//
        void EditUser(User user); //Edit user//
        void InsertUser(User user); //Insert user//             
    }
}
