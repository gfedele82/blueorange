using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly EmailContext _context;
        public EmailRepository(EmailContext context) => _context = context;

        public async Task<Schema.DBEmail> GetAsync(Guid Id)
        {
            return await _context.Email.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Schema.DBEmail> SaveOrUpdateAsync(Schema.DBEmail email)
        {
            _context.ChangeTracker.Clear();
            var entity = await _context.Email.FindAsync(email.Id);
            if(entity == null)
            {
                await _context.Email.AddAsync(email);
            }
            else
            {
                _context.Email.Update(email);
            }
            _context.SaveChanges();
            return email;
        }
    }
}
