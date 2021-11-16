using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CartMvc.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Category_ID { get; set; }
        [Required]
        [Display(Name = "Name")]
        [Index("Category_Index", IsUnique = true)]
        [MinLength(3)]
        [MaxLength(80)]
        public string Category_Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        [MinLength(3)]
        [MaxLength(255)]
        public string Description { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}