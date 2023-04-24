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

        public SuperDictionaryService(IRepository<SuperDictionary> superDictionaryRepository)
        {
            _superDictionaryRepository = superDictionaryRepository;
        }

        public bool IsExists(int id)
        {
            return _superDictionaryRepository.GetResultSpec(x => x.Any(p => p.Id == id));
        }

        public List<ShortDTO> GetProgrammingLanguages()
        {
            return AutoMapperExpression.AutoMapShortDTO(_superDictionaryRepository.GetListResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.ProgrammingLanguage)).OrderBy(x => x.Name));
        }

        public bool IsProgrammingLanguageExists(string name)
        {
            return _superDictionaryRepository.GetResultSpec(x => x.Any(p => p.DictionaryId == (int)DictionaryEnum.ProgrammingLanguage && p.Name == name));
        }

        public void CreateProgrammingLanguage(ShortDTO model)
        {
            var programmingLanguage = new SuperDictionary
            {
                Name = model.Name,
                DictionaryId = (int)DictionaryEnum.ProgrammingLanguage,
            };

            _superDictionaryRepository.Create(programmingLanguage);
        }

        public void DeleteProgrammingLanguage(int id)
        {
            var programmingLanguage = _superDictionaryRepository.GetById(id);

            _superDictionaryRepository.Delete(programmingLanguage);
        }

        public void UpdateProgrammingLanguage(ShortDTO model)
        {
            var data = _superDictionaryRepository.GetById(model.Id);

            if (data != null)
            {
                data.Name = model.Name;

                _superDictionaryRepository.Update(data);
            }
        }

        public List<ShortDTO> GetDepartments()
        {
            return AutoMapperExpression.AutoMapShortDTO(_superDictionaryRepository.GetListResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.Department)).OrderBy(x => x.Name));
        }

        public bool IsDepartmentExists(string name)
        {
            return _superDictionaryRepository.GetResultSpec(x => x.Any(p => p.DictionaryId == (int)DictionaryEnum.Department && p.Name == name));
        }

        public void CreateDepartment(ShortDTO model)
        {
            var department = new SuperDictionary
            {
                Name = model.Name,
                DictionaryId = (int)DictionaryEnum.Department,
            };

            _superDictionaryRepository.Create(department);
        }

        public void DeleteDepartment(int id)
        {
            var department = _superDictionaryRepository.GetById(id);

            _superDictionaryRepository.Delete(department);
        }

        public void UpdateDepartment(ShortDTO model)
        {
            var data = _superDictionaryRepository.GetById(model.Id);

            if (data != null)
            {
                data.Name = model.Name;

                _superDictionaryRepository.Update(data);
            }
        }

        public List<ShortDTO> GetForeignLanguages()
        {
            return AutoMapperExpression.AutoMapShortDTO(_superDictionaryRepository.GetListResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.ForeignLanguage)).OrderBy(x => x.Name));
        }

        public bool IsForeignLanguageExists(string name)
        {
            return _superDictionaryRepository.GetResultSpec(x => x.Any(p => p.DictionaryId == (int)DictionaryEnum.ForeignLanguage && p.Name == name));
        }

        public void CreateForeignLanguage(ShortDTO model)
        {
            var foreignLanguage = new SuperDictionary
            {
                Name = model.Name,
                DictionaryId = (int)DictionaryEnum.ForeignLanguage,
            };

            _superDictionaryRepository.Create(foreignLanguage);
        }

        public void DeleteForeignLanguage(int id)
        {
            var foreignLanguage = _superDictionaryRepository.GetById(id);

            _superDictionaryRepository.Delete(foreignLanguage);
        }

        public void UpdateForeignLanguage(ShortDTO model)
        {
            var data = _superDictionaryRepository.GetById(model.Id);

            if (data != null)
            {
                data.Name = model.Name;

                _superDictionaryRepository.Update(data);
            }
        }

        public List<ShortDTO> GetPositions()
        {
            return AutoMapperExpression.AutoMapShortDTO(_superDictionaryRepository.GetListResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.Position)).OrderBy(x => x.Name));
        }

        public bool IsPositionExists(string name)
        {
            return _superDictionaryRepository.GetResultSpec(x => x.Any(p => p.DictionaryId == (int)DictionaryEnum.Position && p.Name == name));
        }

        public void CreatePosition(ShortDTO model)
        {
            var position = new SuperDictionary
            {
                Name = model.Name,
                DictionaryId = (int)DictionaryEnum.Position,
            };

            _superDictionaryRepository.Create(position);
        }

        public void DeletePosition(int id)
        {
            var position = _superDictionaryRepository.GetById(id);

            _superDictionaryRepository.Delete(position);
        }

        public void UpdatePosition(ShortDTO model)
        {
            var data = _superDictionaryRepository.GetById(model.Id);

            if (data != null)
            {
                data.Name = model.Name;

                _superDictionaryRepository.Update(data);
            }
        }
    }
}
