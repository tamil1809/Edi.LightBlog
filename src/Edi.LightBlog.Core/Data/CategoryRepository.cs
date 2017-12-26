using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Edi.LightBlog.Models;

namespace Edi.LightBlog.Core.Data
{
    public class CategoryRepository: DataRepositoryBase<Category>
    {
        public CategoryRepository() : base("Categories")
        {
            SqlCreate = "INSERT INTO Categories (Title, Description, RouteName) " +
                        "VALUES (@Title, @Description, @RouteName)";

            SqlUpdate = "UPDATE Categories SET Title=@Title, Description = @Description, RouteName = @RouteName WHERE Id = @Id";
        }
    }
}
