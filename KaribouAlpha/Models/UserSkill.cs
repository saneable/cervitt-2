using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public class UserSkill
    {
        [Key]
        public virtual Int64 Id { get; set; }

        [Required]
        public virtual Int64 UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        public virtual string SkillName { get; set; }
        
        public virtual bool Active { get; set; }

        public virtual DateTime CreateDate { get; set; }

        public virtual DateTime? ModifyDate { get; set; }
    }
}