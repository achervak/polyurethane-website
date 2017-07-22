using System.ComponentModel.DataAnnotations.Schema;

namespace Polyurethane.Data.Entities
{
    [Table("Image")]
    public class ImageEntity : BaseEntity
    {
        public string Description { get; set; }
        public string Location { get; set; }
    }
}
