using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAppLayout.Models
{
    // Comment Entitysi veri tabanında Post Comment bilgilerini tutmak için kullanacağımız entity simule etsin

    /// <summary>
    /// Makale bilgilerinin tutulduğu entity
    /// </summary>
    public class Post
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Navigation property Makalenin Yorumları bilgisi join için
        /// </summary>
        public List<Comment> Comments { get; set; }

    }

    public class Comment
    {
        public string Id { get; set; }
        public string Body { get; set; }
        public string UserId { get; set; } // Comment Kim tarafından atıldı
        public string PostId { get; set; } // Yorumun hangi makaleye atıldığını tuttuk.
        public DateTime Date { get; set; } // Yorumun atıldığı tarih

        // navigation property join işlemleri için kullanırız.
        public User CommentUser { get; set; }
        public Post CommentPost { get; set; }

    }

    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

    }

    public class CommentManager
    {
       public Post GetPostComments()
        {
          

            var post = new Post
            {
                Id = "37bb8e1b-b978-445b-b6c7-0fd158cca689",
                Comments = new List<Comment>(),
                Description = "Makale-1",
                Title = "Makale"
            };

            var user1 = new User
            {
                Email = "test@test.com",
                Id = Guid.NewGuid().ToString(),
                UserName = "test-user"
            };

            var user2 = new User
            {
                Email = "test1@test.com",
                Id = Guid.NewGuid().ToString(),
                UserName = "test-user1"
            };

            post.Comments.Add(new Comment
            {
                Body = "Comment-1",
                CommentPost = post,
                CommentUser = user1,
                Date = DateTime.Now.AddDays(-2),
                Id = Guid.NewGuid().ToString(),
                PostId = post.Id,
                UserId = user1.Id
            });

            post.Comments.Add(new Comment
            {
                Body = "Comment-2",
                CommentPost = post,
                CommentUser = user2,
                Date = DateTime.Now.AddDays(-3),
                Id = Guid.NewGuid().ToString(),
                PostId = post.Id,
                UserId = user2.Id
            });

            return post;


        }
    }


    /// <summary>
    /// Sayfaya çekilecek olan veriler viewmodel olarak tanımlanırlar
    /// </summary>
    public class PostDetailViewModel
    {
        public string PostId { get; set; }
        public string PostTitle { get; set; } // Makale başlığı
        public string PostBody { get; set; } // makale içeriği
        public List<CommentViewModel> PostComments { get; set; } // makaleye ait yorumlar

    }

    public class CommentViewModel
    {
        public string CommentBy { get; set; }
        public string Message { get; set; }
        public DateTime CommentDate { get; set; }

    }
}
