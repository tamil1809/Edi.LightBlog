using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Edi.LightBlog.Models;

namespace Edi.LightBlog.Core.Data
{
    public class CommentRepository : DataRepositoryBase<Comment>
    {
        public CommentRepository() : base("Comments")
        {

        }

        public IEnumerable<Comment> GetCommentsByPostId(int postId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = $"SELECT * FROM {TableName} " +
                                $"WHERE PostId = @PostId";
                dbConnection.Open();
                return dbConnection.Query<Comment>(sQuery, new { PostId = postId });
            }
        }
    }
}
