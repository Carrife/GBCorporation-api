using GB_Corporation.Data;
using GB_Corporation.Interfaces.Repositories;

namespace GB_Corporation.Repositories
{
    public class ApplicantHiringDataRepository : IApplicantHiringDataRepository
    {
        private readonly AppDbContext _context;

        public ApplicantHiringDataRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
