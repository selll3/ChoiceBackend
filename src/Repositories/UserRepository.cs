using System.Linq.Expressions;
using Choice_Ym.Abstractions;
using Choice_Ym.Data;
using Choice_Ym.Models;
using Choice_Ym.Repository;

namespace Choice_Ym.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ChoiceymDbContext context) : base(context)
        {
        }
    }
}