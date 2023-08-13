using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebServer.Entity
{
    [Table("links")]
    public class Links
    {
        [Key, Required]
        public int id { get; set; }
        public int product_id { get; set; }
        public int parent_id { get; set; }
        public int quantity { get; set; }
    }
}
