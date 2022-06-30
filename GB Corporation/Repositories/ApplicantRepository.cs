using GB_Corporation.Data;
using GB_Corporation.Interfaces.Repositories;

namespace GB_Corporation.Repositories
{
    public class ApplicantRepository : IApplicantRepository
    {
        private readonly AppDbContext _context;

        public ApplicantRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
