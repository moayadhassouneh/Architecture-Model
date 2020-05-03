using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DataAccess.Models
{
    public partial class Channel
    {
        [Key]
        [Column("ChannelID")]
        public int ChannelId { get; set; }
        [Required]
        [StringLength(150)]
        public string NameEn { get; set; }
        [StringLength(150)]
        public string NameAr { get; set; }
        [StringLength(50)]
        public string Frequency { get; set; }
        [Column("ProgramID")]
        public int? ProgramId { get; set; }
    }
}
