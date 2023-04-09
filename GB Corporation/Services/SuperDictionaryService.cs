using GB_Corporation.DTOs;
using GB_Corporation.Enums;
using GB_Corporation.Helpers;
using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Interfaces.Services;
using GB_Corporation.Models;

namespace GB_Corporation.Services
{
    public class SuperDictionaryService : ISuperDictionaryService
    {
        private readonly IRepository<SuperDictionary> _superDictionaryRepository;
        private readonly IRepository<ApplicantLogicTest> _applicantLogicTestsRepository;
        private readonly IRepository<ApplicantForeignLanguageTest> _applicantForeignLanguageTestRepository;
        private readonly IRepository<ApplicantProgrammingTest> _applicantProgrammingTestRepository;

        public SuperDictionaryService(IRepository<SuperDictionary> superDictionaryRepository, 
            IRepository<ApplicantLogicTest> applicantLogicTestsRepository,
            IRepository<ApplicantForeignLanguageTest> applicantForeignLanguageTestRepository,
            IRepository<ApplicantProgrammingTest> applicantProgrammingTestRepository)
        {
            _superDictionaryRepository = superDictionaryRepository;
            _applicantLogicTestsRepository = applicantLogicTestsRepository;
            _applicantForeignLanguageTestRepository = applicantForeignLanguageTestRepository;
            _applicantProgrammingTestRepository = applicantProgrammingTestRepository;
        }

        public List<ShortDTO> GetProgrammingLanguages()
        {
            return AutoMapperExpression.AutoMapShortDTO(_superDictionaryRepository.GetListResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.ProgrammingLanguage)));
        }

        public List<ShortDTO> GetDepartments()
        {
            return AutoMapperExpression.AutoMapShortDTO(_superDictionaryRepository.GetListResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.Department)));
        }

        public List<ShortDTO> GetForeignLanguages()
        {
            return AutoMapperExpression.AutoMapShortDTO(_superDictionaryRepository.GetListResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.ForeignLanguage)));
        }

        public List<ShortDTO> GetPositions()
        {
            return AutoMapperExpression.AutoMapShortDTO(_superDictionaryRepository.GetListResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.Position)));
        }
    }
}
