using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;
namespace SutIuliaLab7.Models
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Description { get; set; } = string.Empty;

        [OneToMany]
        public List<Product>? ListProducts { get; set; }

    }
}
