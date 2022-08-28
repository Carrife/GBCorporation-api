using GB_Corporation.DTOs;
using GB_Corporation.Enums;
using GB_Corporation.Helpers;
using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Interfaces.Services;
using GB_Corporation.Models;

namespace GB_Corporation.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly IRepository<SuperDictionary> _superDictionaryRepository;

        public LanguageService(IRepository<SuperDictionary> superDictionaryRepository)
        {
            _superDictionaryRepository = superDictionaryRepository;
        }

        public List<ShortDTO> GetLanguages()
        {
            return AutoMapperExpression.AutoMapShortDTO(_superDictionaryRepository.GetListResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.Language)));
        }
    }
}
