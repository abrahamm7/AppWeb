using AppWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWeb.Services
{
    public interface IReceipModule
    {
        List<Receip> ItemsPurchased(); //Get all items purchased//
    }
}
