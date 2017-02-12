using System.ComponentModel.DataAnnotations.Schema;

namespace qVisitor.Models
{
    [Table ("qvVisitorLuggage")]
    public class qvVisitorLuggage
    {
        public int Id { get; set; }
        public string Luggage { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual qvOrder Order { get; set; }

        public int VisitorId { get; set; }
        [ForeignKey ("VisitorId")]
        public virtual qvVisitor Visitor { get; set; }
    }
}
