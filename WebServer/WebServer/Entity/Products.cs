using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebServer.Entity
{
    [Table("product")]
    public class Product
    {
        [Key, Required]
        public int id { get; set; }
        public string name { get; set; } = String.Empty;
        public int price { get; set; }
    }
}
