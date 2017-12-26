using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Dapper;

namespace Edi.LightBlog.Core.Data
{
    /// <summary>
    /// CRUD interface
    /// </summary>
    public interface IDatabaseOperators<T>
    {
        int Create(T entity);

        IEnumerable<T> Read();

        T Read(object key);

        int Update(T entity);

        int Delete(object key);

        int Count();
    }

    public class DataRepositoryBase<T> : IDatabaseOperators<T> where T : class
    {
        protected string ConnectionString;

        public IDbConnection Connection => new SqliteConnection(ConnectionString);

        public string TableName { get; set; }

        public string PrimaryKeyName { get; set; }

        public string SqlCreate { get; set; }

        public string SqlUpdate { get; set; }

        public DataRepositoryBase(string tableName, string primaryKeyName = "Id")
        {
            ConnectionString = AppDomain.CurrentDomain.GetData(Constants.DbConnectionName) as string;

            TableName = tableName;
            if (!string.IsNullOrEmpty(primaryKeyName))
            {
                PrimaryKeyName = primaryKeyName;
            }
        }

        public int Create(T entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = SqlCreate;
                dbConnection.Open();
                return dbConnection.Execute(sQuery, entity);
            }
        }

        public IEnumerable<T> Read()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<T>($"SELECT * FROM {TableName}");
            }
        }

        public T Read(object key)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = $"SELECT * FROM {TableName} " +
                                $"WHERE {PrimaryKeyName} = @Key";
                dbConnection.Open();
                return dbConnection.Query<T>(sQuery, new { Key = key }).FirstOrDefault();
            }
        }

        public int Update(T entity)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = SqlUpdate;
                dbConnection.Open();
                return dbConnection.Execute(sQuery, entity);
            }
        }

        public int Delete(object key)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = $"DELETE FROM {TableName} WHERE {PrimaryKeyName} = @Key";
                dbConnection.Open();
                return dbConnection.Execute(sQuery, new { Key = key });
            }
        }

        public int Count()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = $"SELECT COUNT({PrimaryKeyName}) FROM {TableName}";
                dbConnection.Open();
                return dbConnection.QuerySingle(sQuery);
            }
        }
    }
}
