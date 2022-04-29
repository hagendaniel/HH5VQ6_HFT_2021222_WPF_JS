using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HH5VQ6_HFT_2021221.Models
{
    [Table("Places")]
    public class Place
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlaceId { get; set; }

        [MaxLength(30)]
        public string PlaceName { get; set; }
        public string Country { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Season> Seasons { get; set; }
    }
}
