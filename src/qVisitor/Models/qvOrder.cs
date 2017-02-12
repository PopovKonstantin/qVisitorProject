
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace qVisitor.Models
{
    [Table ("qvOrder")]
    public class qvOrder
    {
        public int Id { get; set; }

        public int OrderTypeid { get; set; }
        [ForeignKey("OrderTypeid")]
        public virtual qvOrderType OrderType { get; set; }

        public int OrderStausid { get; set; }
        [ForeignKey("OrderStausid")]
        public virtual qvOrderStatus OrderStatus { get; set;}

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }

        public virtual ICollection <qvVisitorLuggage> VisitorLuggages { get; set; }
        public virtual ICollection <qvEntrance> Entrances { get; set; }
        public virtual ICollection <qvOrderStatusHistory> OrderStatusHistories { get; set; }
        public virtual ICollection <refOrderVisitor> RefOrderVisitors { get; set; }

        public qvOrder() { }

    }
}
