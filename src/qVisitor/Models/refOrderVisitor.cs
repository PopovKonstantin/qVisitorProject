using System.ComponentModel.DataAnnotations.Schema;

namespace qVisitor.Models
{
    [Table ("refOrderVisitor")]
    public class refOrderVisitor
    {

        public int OrderId { get; set; }
        
        public int VisitorId {get;set; }
        [ForeignKey("OrderId")]
        public qvOrder Order { get; set; }
        [ForeignKey("VisitorId")]
        public qvVisitor Visitor { get; set; }
    }
}