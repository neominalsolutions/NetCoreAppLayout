using Microsoft.AspNetCore.Mvc;
using NetCoreAppLayout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAppLayout.Controllers
{
    public class PostController : Controller
    {
        [HttpGet("post-detail/{postId}")]
        public IActionResult Detail(string postId)
        {
            // db nesnesi
            var post = new CommentManager().GetPostComments();

            if(post.Id != postId)
            {
                // kayıt yok sayfası döndürsün.
                return NotFound();
            }

            // post datasını alıp view modelle mappledik. yani post entity bilgisi ile view'e gönderilecek olan modeli doldurduk.

            var model = new PostDetailViewModel
            {
                PostBody = post.Description,
                PostId = post.Id,
                PostTitle = post.Title,
                PostComments = post.Comments.Select(a => new CommentViewModel
                {
                    CommentBy = a.CommentUser.UserName,
                    CommentDate = a.Date,
                    Message = a.Body

                }).ToList()
            };


            return View(model);
        }

        [HttpPost("send-comment")] // Json olarak veri gönderdiğimizde post bodyden veriyi yakalamamızı sağlar. sadece application/json tipinde veri gönderebiliriz. [FromForm] ise dün postman üzerinden x-www-urlencoded olarak veri gönderdiğimiz yöntem.
        public JsonResult SendComment([FromBody] CommentInputModel model)
        {
            // database kayıt.
            return Json(new{message = "Kayıt başarılı" });
        }
    }
}
