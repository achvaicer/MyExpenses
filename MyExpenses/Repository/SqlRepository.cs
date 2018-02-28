using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MyExpenses.Models;
using PetaPoco;

namespace MyExpenses.Repository
{
    public class SqlRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        protected readonly Database _db;
        private readonly string _serverConnectionString;
        protected readonly string _keyName;
        private readonly bool _autoIncrement;

        public SqlRepository(string keyName, string connectionString, bool autoIncrement = true)
        {
            _keyName = keyName ?? "Id";
            _serverConnectionString = connectionString;
            _autoIncrement = autoIncrement;

            _db = new Database(GetDatabaseConnectionString(), "System.Data.SqlClient");
        }

        public SqlRepository(string keyName, Database database, bool autoIncrement = true)
        {
            _keyName = keyName ?? "Id";
            _serverConnectionString = database.Connection.ConnectionString;
            _autoIncrement = autoIncrement;

            _db = database;
        }

        public virtual TEntity Single(object key)
        {
            var entity = _db.Single<TEntity>(string.Format("WHERE [{0}] = @0", _keyName), key);
            return entity;
        }

        public virtual void Save(TEntity item)
        {
            var id = GetKeyValue(item);
            if (Exists(id))
            {
                _db.Update(item);
            }
            else
            {
                _db.Insert(GetTableName(), _keyName, _autoIncrement, item);
            }
        }


        public virtual IEnumerable<TEntity> All()
        {
            return _db.Query<TEntity>("SELECT * FROM " + GetTableName("[", "]"));
        }

        public virtual IEnumerable<TEntity> Paged(string orderBy, int page, int rows, string direction = "ASC")
        {
            var sql = @"SELECT  *
                        FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY [{1}] {2}) AS RowNum, *
                                  FROM {0}
                                ) AS RowConstrainedResult
                        WHERE   RowNum >= @Begin
                            AND RowNum < @End
                        ORDER BY RowNum";

            sql = string.Format(sql, GetTableName("[", "]"), orderBy, direction);

            var begin = (page - 1) * rows;
            return _db.Query<TEntity>(sql, new { Begin = begin + 1, End = begin + rows + 1 });
        }

        public virtual int Count()
        {
            return _db.ExecuteScalar<int>(string.Format("SELECT COUNT(1) FROM {0}", GetTableName("[", "]")));
        }

        public virtual bool Exists(object key)
        {
            return _db.ExecuteScalar<int>(string.Format("SELECT COUNT(1) FROM {0} WHERE [{1}] = @0", GetTableName("[", "]"), _keyName), key) == 1;

        }

        public virtual void Delete(object key)
        {
            var tableName = GetTableName("[", "]");
            _db.Execute(string.Format("DELETE FROM {0} WHERE [{1}] = @0", tableName, _keyName), key);
        }



        protected object GetKeyValue(TEntity item)
        {
            var type = item.GetType();
            var pinfo = type.GetProperty(_keyName);
            var finfo = type.GetField(_keyName);
            if (pinfo != null)
                return pinfo.GetValue(item, null);
            if (finfo != null)
                return finfo.GetValue(item);

            throw new Exception("Primary key not found on object");
        }


        private static string GetTableName(string initialWrapper = null, string finalWrapper = null)
        {
            return string.Format("{0}{1}{2}", initialWrapper, typeof(TEntity).Name, finalWrapper);
        }




        private string GetDatabaseConnectionString()
        {
            var builder = new SqlConnectionStringBuilder(_serverConnectionString);

            return builder.ToString();
        }

    }
}
