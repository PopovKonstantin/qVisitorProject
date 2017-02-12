using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace qVisitor.Models
{
    [Table ("qvVisitorPhoto")]
    public class qvVisitiorPhoto
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public DateTime PhotoDate { get; set; }

        public int VisitorId { get; set; }
        [ForeignKey ("VisitorId")]
        public qvVisitor Visitor { get; set; }

        public qvVisitiorPhoto() { }
    }
}
