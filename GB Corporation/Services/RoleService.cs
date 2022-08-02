using GB_Corporation.DTOs;
using GB_Corporation.Helpers;
using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Models;

namespace GB_Corporation.Services
{
    public class RoleService
    {
        private readonly IRepository<Role> _roleRepository;

        public RoleService(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public List<ShortDTO> GetRoles()
        {
            return AutoMapperExpression.AutoMapShortDTO(_roleRepository.GetListResultSpec(x => x).ToList());
        }
    }
}
