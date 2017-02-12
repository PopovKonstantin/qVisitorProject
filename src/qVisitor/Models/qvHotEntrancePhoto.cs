using System.ComponentModel.DataAnnotations.Schema;

namespace qVisitor.Models
{
    [Table ("HotEntrancePhoto")]
    public class qvHotEntrancePhoto
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }

        public int HotEntranceId { get; set; }
        [ForeignKey("HotEntranceId")]
        public qvHotEntrance HotEntrance { get; set; }
    }
}
