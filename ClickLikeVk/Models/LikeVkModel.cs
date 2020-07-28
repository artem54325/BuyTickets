using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClickLikeVk.Models
{
    public class LikeVkModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]// Required
        public string Id { get; set; }
        [Required]
        public string IdPage { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
    }

    public class GroupLike
    {
        public string Value { get; set; }
        public int Count { get; set; }
    }
}
