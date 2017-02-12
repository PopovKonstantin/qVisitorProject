using System.ComponentModel.DataAnnotations.Schema;

namespace qVisitor.Models
{
    [Table ("qvHotEntranceDoc")]
    public class qvHotEntranceDoc
    {
        public int Id { get; set; }
        public byte [] Document { get; set; }

        public int HotEntranceId { get; set; }
        [ForeignKey ("HotEntranceId")]
        public qvHotEntrance HotEntrance { get; set; }
    }
}
