using MyBlog.Data;
using MyBlog.Data.UnitOfWork;
using MyBlog.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service
{
    public class CommentService : BaseService
    {
       public List<CommentDTO> GetComments(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                try
                {
                    var result = uow.Comments.Search(x => x.BlogId == id).Where(x => x.IsConfirmed == false).ToList();

                    List<CommentDTO> list = new List<CommentDTO>();

                    foreach (var item in result)
                    {
                        CommentDTO dTO = new CommentDTO
                        {
                            CommentId = item.CommentId,
                            CommentContent = item.CommentContent,
                            BlogId = item.BlogId,
                            UserId = item.UserId,
                            IsConfirmed = item.IsConfirmed,
                            CreatedDate = item.CreatedDate
                        };

                        list.Add(dTO);
                    }

                    return list;
                }
                catch (Exception ex)
                {

                    return null;
                }
            }
        } 
        
        public CommentDTO GetConfirmed(int id)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                try
                {
                    var result = uow.Comments.Get(id);

                    result.IsConfirmed = true;

                    uow.Commit();
                    return Mapper.Map<Comment, CommentDTO>(result);
                }
                catch (Exception ex)
                {
                    uow.RollBack();
                    return null;
                }
            }
        }
    }
}
