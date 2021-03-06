﻿using System;
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
            SqlCreate = $"INSERT INTO {TableName} (PostId, CategoryId) " +
                         "VALUES (@PostId, @CategoryId)";
        }

        public IEnumerable<Category> GetCatsByPost(int postId)
        {
            var sql = "SELECT * FROM Categories c " +
                     $"INNER JOIN {TableName} psa on c.Id = psa.CategoryId " +
                      "WHERE psa.PostId = @PostId";

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Category>(sql, new { PostId = postId });
            }
        }

        public int DeleteByCategoryId(int catId)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = $"DELETE FROM {TableName} WHERE CategoryId = @Key";
                dbConnection.Open();
                return dbConnection.Execute(sQuery, new { Key = catId });
            }
        }
    }
}
