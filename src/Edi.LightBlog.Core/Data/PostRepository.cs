using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Edi.LightBlog.Models;

namespace Edi.LightBlog.Core.Data
{
    public class PostRepository : DataRepositoryBase<Post>
    {
        public PostRepository() : base("Posts")
        {

        }

        public Post GetPostBySlug(string slug)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = $"SELECT * FROM {TableName} " +
                                $"WHERE Slug = @Slug";
                dbConnection.Open();
                return dbConnection.Query<Post>(sQuery, new { Slug = slug }).FirstOrDefault();
            }
        }
    }
}
