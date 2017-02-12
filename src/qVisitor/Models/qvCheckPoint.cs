using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace qVisitor.Models
{
    [Table ("qvCheckPoint")]
    public class qvCheckPoint
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ObjectId { get; set; }
        [ForeignKey("ObjectId")]
        public virtual qvObject Object { get; set; }

        public virtual ICollection<qvEntrance> Entrances { get; set; }
        public virtual ICollection<qvNotRecognizedDoc> NotRecognizedDocs { get; set; }
    }
}
