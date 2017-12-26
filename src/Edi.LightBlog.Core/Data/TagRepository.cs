using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Edi.LightBlog.Models;
using Microsoft.Data.Sqlite;

namespace Edi.LightBlog.Core.Data
{
    public class TagRepository : DataRepositoryBase<Tag>
    {
        public TagRepository() : base("Tags")
        {
            SqlCreate = "INSERT INTO Tags (TagName, NormalizedName, CreateOnUtc) " +
                        "VALUES (@TagName, @NormalizedName, @CreateOnUtc)";

            SqlUpdate = "UPDATE Tags SET TagName = @TagName, NormalizedName = @NormalizedName WHERE Id = @Id";
        }
    }
}
