using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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


    }
}
