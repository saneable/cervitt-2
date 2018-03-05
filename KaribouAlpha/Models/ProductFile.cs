using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public enum ProductFilePrivacy
    {
        Public = 0,
        Private = 1,
        Invisible = 2,
        VisibleToSelectedGroups = 3
    }

    public enum ProductFileType
    {
        Image = 0,
        WordDocument = 1,
        Other = 3
    }

    public enum ProductFileCategory
    {
        Roadmap = 0,
        Whitepaper = 1,
        FlashPost = 2,
        Teaser = 3,
        ValueProposition = 4,
        CaseStudy = 5,
        Demo = 6,
        Webinar = 7,
        ProjectPlan = 8,
        StretegyPaper = 9,
        CompetitorComparison = 10,
        TrainingMaterial = 11,
        PricingSheet = 12,
        LegalGuideline = 13,
        ContactUs = 14
    }

    public class ProductFile
    {
        public long ID { get; set; }
        [Required]
        public long ProductID { get; set; }
        [Required]
        public String Name { get; set; }
        public String Description { get; set; }
        [Required]
        public ProductFileType Type { get; set; }
        [Required]
        public ProductFileCategory Category { get; set; }
        public ProductFilePrivacy Privacy { get; set; }
        [Required]
        public byte[] Bytes { get; set; }
        public byte[] Image { get; set; }
        [Required]
        public DateTime UploadedAt { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
        public ICollection<ProductFileFeedback> Feedback { get; set; }
        public ICollection<ProductFileDownload> Downloads { get; set; }
        public ICollection<CompanyFollowerGroup> GroupsVisibleTo { get; set; }
    }
}
 