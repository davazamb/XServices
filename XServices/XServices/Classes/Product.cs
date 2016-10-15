using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XServices.Classes
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int ProductId { get; set; }

        [Unique]
        public string Description { get; set; }

        public decimal Price { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Service> Services { get; set; }

        public override int GetHashCode()
        {
            return ProductId;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2:C2}", ProductId, Description, Price);
        }
    }

}
