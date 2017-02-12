using System.ComponentModel.DataAnnotations.Schema;

namespace qVisitor.Models
{
    [Table ("qvVisitorScan")]
    public class qvVisitorScan
    {
        public int Id { get; set; }
        public byte[] Scan  { get; set; }

        public int VisitorId { get; set; }
        [ForeignKey("VisitorId")]
        public virtual qvVisitor Visitor { get; set; }

    }
}
