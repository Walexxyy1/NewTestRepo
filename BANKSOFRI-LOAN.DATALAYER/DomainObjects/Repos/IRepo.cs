using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.DomainObjects.Repos
{
   public interface IRepo
    {
        T Get<T>(object Id) where T : class;
        IList GetList<T>() where T : class;
        IList GetList<T>(string conditions) where T : class;
        IList GetList<T>(string conditions, object parameters) where T : class;
        IList GetListPaged<T>(int pageNumber, int itemsPerPage, string conditions, string orderBy, object parameters) where T : class;
        long Insert<T>(T t) where T : class;
        long Update<T>(T t) where T : class;
        void Delete<T>(object id) where T : class;
        void Delete<T>(T t) where T : class;
        long DeleteList<T>(string conditions, object parameters) where T : class;
        long RecordCount<T>(string conditions, object parameters) where T : class;
        T Query<T>(string query) where T : class;
        T Query<T>(string query, object parameters) where T : class;
        IList QueryList<T>(string query) where T : class;
        IList QueryList<T>(string query, object parameters) where T : class;
        Task<T> GetAsync<T>(object id) where T : class;
        Task<IEnumerable<T>> GetListAsync<T>() where T : class;
        Task<IEnumerable<T>> GetListAsync<T>(string conditions) where T : class;
        Task<IEnumerable<T>> GetListAsync<T>(string conditions, object parameters) where T : class;
        Task<IEnumerable<T>> GetListPagedAsync<T>(int pageNumber, int itemsPerPage, string conditions, string orderBy, object parameters) where T : class;
        Task<long> InsertAsync<T>(T t) where T : class;
        Task<long> UpdateAsync<T>(T t) where T : class;
        Task DeleteAsync<T>(object id) where T : class;
        Task DeleteAsync<T>(T t) where T : class;
        Task<long> DeleteListAsync<T>(string conditions, object parameters) where T : class;
        Task<long> RecordCountAsync<T>(string conditions, object parameters) where T : class;
    }
}
