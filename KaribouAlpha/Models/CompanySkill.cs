using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public class CompanySkill
    {
        [Key]
        public virtual Int64 Id { get; set; }

        [Required]
        public virtual string SkillName { get; set; }

        [Required]
        public virtual Int64 CompanyId { get; set; }

    }
}