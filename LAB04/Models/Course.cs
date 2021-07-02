namespace LAB04.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string LecturerId { get; set; }

      
        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(255,ErrorMessage ="Ko được quá 255 ký tự")]
        
        public string Place { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public List<Category> ListCategory = new List<Category>();

        public string Name;
    }
}
