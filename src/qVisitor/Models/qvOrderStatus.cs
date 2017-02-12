using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace qVisitor.Models
{
    [Table ("qvOrderStatus")]
    public class qvOrderStatus
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual ICollection<qvOrder> Orders { get; set; }

        public qvOrderStatus() { }
    }
}
