using System;

namespace KaribouAlpha.Models
{
    public class ProductFileViewDTO
    {
        public long ID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public ProductFileType Type { get; set; }
        public ProductFileCategory Category { get; set; }
        public byte[] Bytes { get; set; }
        public byte[] Image { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
 