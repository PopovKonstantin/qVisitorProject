using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace qVisitor.Models
{
    [Table ("qvOrderType")]
    public class qvOrderType
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual ICollection<qvOrder> Orders { get; set; }

        public qvOrderType() { }
    }
}
