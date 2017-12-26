using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Edi.LightBlog.Models;

namespace Edi.LightBlog.Core.Data
{
    public class PostTagAssociation
    {
        public int PostId { get; set; }

        public int TagId { get; set; }
    }

    public class PostTagAssociationRepository : DataRepositoryBase<PostTagAssociation>
    {
        public PostTagAssociationRepository() : base("PostTagAssociation")
        {
            SqlCreate = "INSERT INTO PostTagAssociation (PostId, TagId) " +
                        "VALUES (@PostId, @TagId)";
        }

        public IEnumerable<Tag> GetTagsByPost(int postId)
        {
            var sql = "SELECT * FROM Tags t " +
                      "INNER JOIN PostTagAssociation pta on t.Id = pta.TagId " +
                      "WHERE pta.PostId = @PostId";

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Tag>(sql, new { PostId = postId });
            }
        }

        public IEnumerable<Post> GetPostsByTag(int tagId)
        {
            var sql = "";

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Post>(sql, new { TagId = tagId });
            }
        }
    }
}
