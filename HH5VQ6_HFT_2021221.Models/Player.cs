using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HH5VQ6_HFT_2021221.Models
{
    [Table("Players")]
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }

        [Required]
        [MaxLength(50)]
        public string PlayerName { get; set; }

        public DateTime Born { get; set; }
        public int Debt { get; set; }

        public int Age { get; set; }
        [DefaultValue("true")]
        public bool AliveOrDead { get; set; }

        [ForeignKey(nameof(Map))]
        public int? EliminatedOnMap_MapId { get; set; } //It can also have null value in case the player is not eliminated

        [ForeignKey(nameof(Season))]
        public int SeasonId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Season Season { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Map Map { get; set; }

        public Player()
        {
            AliveOrDead = true;
        }
    }
}
