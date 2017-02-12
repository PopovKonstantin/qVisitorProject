using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace qVisitor.Models
{ 
    [Table ("qvOrderStatusHistory")]
    public class qvOrderStatusHistory
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public qvOrder Orders { get; set; }

        public int OldStatusId { get; set; }
        public int NewStatusId { get; set; }

        [ForeignKey("OldStatusId")]
        public virtual qvOrderStatus OldStatus {get;set;}
        
        [ForeignKey("NewStatusId")]
        public virtual qvOrderStatus NewStatus {get;set;}
        public DateTime ActionDate { get; set; }
    }
}
