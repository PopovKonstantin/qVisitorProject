using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace qVisitor.Models
{
    [Table("refOrderVisitor")]
    public class refOrderVisitor
    {
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual qvOrder Order { get; set; }

        public int VisitorId { get; set; }
        [ForeignKey("VisitorId")]
        public virtual qvVisitor Visitor { get; set; }

        public refOrderVisitor() { }
    }
}