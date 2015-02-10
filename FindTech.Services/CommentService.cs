using FindTech.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTech.Services
{
    public interface ICommentService : IService<Comment>
    {
    }
    public class CommentService : Service<Comment>, ICommentService
    {
        public CommentService(IRepositoryAsync<Comment> commentRepository)
            : base(commentRepository)
        {
        }
    }
}
