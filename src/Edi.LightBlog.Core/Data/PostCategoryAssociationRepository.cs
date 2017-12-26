using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Edi.LightBlog.Models;

namespace Edi.LightBlog.Core.Data
{
    public class PostCategoryAssociation
    {
        public int PostId { get; set; }

        public int CategoryId { get; set; }
    }

    public class PostCategoryAssociationRepository : DataRepositoryBase<PostCategoryAssociation>
    {
        public PostCategoryAssociationRepository() : base("PostCategoryAssociation")
        {
            SqlCreate = "INSERT INTO PostCategoryAssociation (PostId, CategoryId) " +
                        "VALUES (@PostId, @CategoryId)";
        }

        public IEnumerable<Category> GetCatsByPost(int postId)
        {
            var sql = "SELECT * FROM Categories c " +
                      "INNER JOIN PostCategoryAssociation psa on c.Id = psa.CategoryId " +
                      "WHERE psa.PostId = @PostId";

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Category>(sql, new { PostId = postId });
            }
        }
    }
}
