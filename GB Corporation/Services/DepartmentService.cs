using GB_Corporation.DTOs;
using GB_Corporation.Enums;
using GB_Corporation.Helpers;
using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Models;

namespace GB_Corporation.Services
{
    public class DepartmentService
    {
        private readonly IRepository<SuperDictionary> _superDictionaryRepository;

        public DepartmentService(IRepository<SuperDictionary> superDictionaryRepository)
        {
            _superDictionaryRepository = superDictionaryRepository;
        }

        public List<ShortDTO> GetDepartments()
        {
            return AutoMapperExpression.AutoMapShortDTO(_superDictionaryRepository.GetListResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.Department)).ToList());
        }
    }
}
