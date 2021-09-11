using System.ComponentModel.DataAnnotations;

namespace ForumWebApi.Models
{
    public class PostFrontend
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Topic { get; set; }

        [Required]
        [MinLength(10)]
        public string Content { get; set; }
        public string CreateAt { get; set; }
        public string UpdateAt { get; set; }
    }
}
