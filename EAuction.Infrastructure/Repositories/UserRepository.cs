using EAuction.Core.Entities;
using EAuction.Core.Repositories;
using EAuction.Infrastructure.Data;
using EAuction.Infrastructure.Repositories.Base;

namespace EAuction.Infrastructure.Repositories
{
    public class UserRepository:Repository<AppUser>,IUserRepository
    {
        private readonly WebAppContext _context;
        public UserRepository(WebAppContext context) : base(context)
        {
            _context = context;
        }
    }
}