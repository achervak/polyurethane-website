using System.ComponentModel.DataAnnotations.Schema;

namespace Polyurethane.Data.Entities
{
    [Table("Image")]
    public class ImageEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
