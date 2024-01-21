using BANKSOFRI_LOAN.DATALAYER.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Collections;

namespace BANKSOFRI_LOAN.DATALAYER.DomainObjects.Repos
{
    public class Repo : IRepo
    {
        private readonly IDbConnection _connection;
        ConfigHelper _config = new ConfigHelper();
        public Repo()
        {
            _connection = new SqlConnection(_config.GetCardConnectionString());
        }

        public T Get<T>(object Id) where T : class
        {
            return _connection.Get<T>(Id);
        }

        public IList GetList<T>() where T : class
        {
            return _connection.GetList<T>().ToList();
        }

        public IList GetList<T>(string conditions) where T : class
        {
            return _connection.GetList<T>(conditions).ToList();
        }

        public IList GetList<T>(string conditions, object parameters) where T : class
        {
            return _connection.GetList<T>(conditions, parameters).ToList();
        }

        public IList GetListPaged<T>(int pageNumber, int itemsPerPage, string conditions, string orderBy,
            object parameters) where T : class
        {
            return _connection.GetListPaged<T>(pageNumber, itemsPerPage, conditions, orderBy, parameters).ToList();
        }

        public long Insert<T>(T t) where T : class
        {
            return Convert.ToInt64(_connection.Insert(t));
        }

        public long Update<T>(T t) where T : class
        {
            return Convert.ToInt64(_connection.Update(t));
        }

        public void Delete<T>(object id) where T : class
        {
            _connection.Delete(id);
        }

        public void Delete<T>(T t) where T : class
        {
            _connection.Delete(t);
        }

        public long DeleteList<T>(string conditions, object parameters) where T : class
        {
            return Convert.ToInt64(_connection.DeleteList<T>(conditions, parameters));
        }

        public long RecordCount<T>(string conditions, object parameters) where T : class
        {
            return Convert.ToInt64(_connection.RecordCount<T>(conditions, parameters));
        }
        public T Query<T>(string query) where T : class
        {
            return _connection.Query<T>(query).FirstOrDefault();
        }
        public T Query<T>(string query, object parameters) where T : class
        {
            return _connection.Query<T>(query, parameters).FirstOrDefault();
        }
        public IList QueryList<T>(string query) where T : class
        {
            return _connection.Query<T>(query).ToList();
        }
        public IList QueryList<T>(string query, object parameters) where T : class
        {
            return _connection.Query<T>(query, parameters).ToList();
        }

        public async Task<T> GetAsync<T>(object id) where T : class
        {
            return await _connection.GetAsync<T>(id);
        }

        public async Task<IEnumerable<T>> GetListAsync<T>() where T : class
        {
            return await _connection.GetListAsync<T>();
        }

        public async Task<IEnumerable<T>> GetListAsync<T>(string conditions) where T : class
        {
            return await _connection.GetListAsync<T>(conditions);
        }

        public async Task<IEnumerable<T>> GetListAsync<T>(string conditions, object parameters) where T : class
        {
            return await _connection.GetListAsync<T>(conditions, parameters);
        }

        public async Task<IEnumerable<T>> GetListPagedAsync<T>(int pageNumber, int itemsPerPage, string conditions,
            string orderBy, object parameters) where T : class
        {
            return await _connection.GetListPagedAsync<T>(pageNumber, itemsPerPage, conditions, orderBy, parameters);
        }

        public async Task<long> InsertAsync<T>(T t) where T : class
        {
            return Convert.ToInt64(await _connection.InsertAsync(t));
        }

        public async Task<long> UpdateAsync<T>(T t) where T : class
        {
            return Convert.ToInt64(await _connection.UpdateAsync(t));
        }

        public async Task DeleteAsync<T>(object id) where T : class
        {
            await _connection.DeleteAsync(id);
        }

        public async Task DeleteAsync<T>(T t) where T : class
        {
            await _connection.DeleteAsync(t);
        }

        public async Task<long> DeleteListAsync<T>(string conditions, object parameters) where T : class
        {
            return Convert.ToInt64(await _connection.DeleteListAsync<T>(conditions, parameters));
        }

        public async Task<long> RecordCountAsync<T>(string conditions, object parameters) where T : class
        {
            return Convert.ToInt64(await _connection.RecordCountAsync<T>(conditions, parameters));
        }


    }
}
