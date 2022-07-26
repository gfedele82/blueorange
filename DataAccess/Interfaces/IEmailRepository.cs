using System;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IEmailRepository
    {
        Task<Schema.DBEmail> GetAsync(Guid Id);

        Task<Schema.DBEmail> SaveOrUpdateAsync(Schema.DBEmail email);
    }
}
