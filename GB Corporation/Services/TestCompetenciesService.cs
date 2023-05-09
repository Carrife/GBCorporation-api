using GB_Corporation.DTOs;
using GB_Corporation.Enums;
using GB_Corporation.Helpers;
using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Interfaces.Services;
using GB_Corporation.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Xml;

namespace GB_Corporation.Services
{
    public class TestCompetenciesService : ITestCompetenciesService
    {
        private readonly IRepository<TestCompetencies> _testCompetenciesReporitory;
        private readonly IRepository<Employee> _employeeReporitory;
        private readonly IRepository<Template> _templateRepository;
        private readonly IRepository<SuperDictionary> _superDictionaryRepository;

        public TestCompetenciesService(IRepository<TestCompetencies> testCompetenciesReporitory, IRepository<Employee> employeeReporitory,
            IRepository<Template> templateRepository, IRepository<SuperDictionary> superDictionaryRepository)
        {
            _testCompetenciesReporitory = testCompetenciesReporitory;
            _employeeReporitory = employeeReporitory;
            _templateRepository = templateRepository;
            _superDictionaryRepository = superDictionaryRepository;
        }

        public List<ShortDTO> GetAll()
        {
            return AutoMapperExpression.AutoMapShortDTO(_templateRepository.GetListResultSpec(x => x.Where(p => p.Link != null)).OrderBy(x => x.Name));
        }

        public List<TestStatusDTO> GetUserTests(int userId, string role, TestProgressFilterDTO filters)
        {
            var predicate = CreatePredicate(filters, role, userId, out bool IsFilterActive);

            var totalElements = _testCompetenciesReporitory.GetResultSpec(x => x.Where(predicate)).Count();

            if (totalElements == 0)
                return new List<TestStatusDTO>();

            if (role != nameof(RoleEnum.Admin) && role != nameof(RoleEnum.HR) && role != nameof(RoleEnum.LineManager))
            {
                return AutoMapperExpression.AutoMapTestStatusDTO(_testCompetenciesReporitory.GetListResultSpec(x => x
                    .Where(predicate).Where(p => p.EmployeeId == userId))
                    .Include(x => x.Status)
                    .OrderBy(x => x.Title).ThenBy(x => x.Status.Name));
            }
            else
            {
                return AutoMapperExpression.AutoMapTestStatusDTO(_testCompetenciesReporitory.GetListResultSpec(x => x.Where(predicate))
                    .Include(x => x.Employee)
                    .Include(x => x.Status)
                    .OrderBy(x => x.Employee.Login).ThenBy(x => x.Title));
            }
                
        }

        private Expression<Func<TestCompetencies, bool>> CreatePredicate(TestProgressFilterDTO filters, string role, int userId, out bool IsFilterActive)
        {
            var predicate = PredicateBuilder.True<TestCompetencies>();
            IsFilterActive = false;

            if (!string.IsNullOrEmpty(filters.Login))
            {
                predicate = predicate.And(p => p.Employee.Login.ToLower().Contains(filters.Login.ToLower()));
                IsFilterActive = true;
            }

            if (filters.StatusIds != null && filters.StatusIds.Length > 0)
            {
                predicate = predicate.And(p => filters.StatusIds.Contains(p.StatusId));
                IsFilterActive = true;
            }

            if (!string.IsNullOrEmpty(filters.Test))
            {
                predicate = predicate.And(p => p.Title.ToLower().Contains(filters.Test.ToLower()));
                IsFilterActive = true;
            }

            if(role == nameof(RoleEnum.LineManager))
            {
                var userDepartmentId = _employeeReporitory.GetResultSpec(x => x.Where(p => p.Id == userId)).First().DepartmentId;

                var employeeIds = _employeeReporitory.GetListResultSpec(x => x
                    .Where(p => p.DepartmentId == userDepartmentId)).Select(x => x.Id).ToList();

                predicate = predicate.And(p => employeeIds.Contains(p.EmployeeId));

                IsFilterActive = true;
            }

            return predicate;
        }

        public List<TestResultDTO> GetUserResults(int? id, TestResultFilterDTO filters)
        {
            var predicate = CreatePredicate(filters, out bool IsFilterActive);

            var totalElements = _testCompetenciesReporitory.GetResultSpec(x => x.Where(predicate)).Count();

            if (totalElements == 0)
                return new List<TestResultDTO>();

            if (id != null)
            {
                return AutoMapperExpression.AutoMapTestResultDTO(_testCompetenciesReporitory.GetListResultSpec(x => x
                    .Where(predicate).Where(p => p.EmployeeId == id))
                    .Include(x => x.Status)
                    .OrderBy(x => x.Title).ThenBy(x => x.Status.Name));
            }
            else
            {
                return AutoMapperExpression.AutoMapTestResultDTO(_testCompetenciesReporitory.GetListResultSpec(x => x.Where(predicate))
                    .Include(x => x.Employee)
                    .Include(x => x.Status)
                    .OrderBy(x => x.Employee.Login).ThenBy(x => x.Title));
            }

        }

        private Expression<Func<TestCompetencies, bool>> CreatePredicate(TestResultFilterDTO filters, out bool IsFilterActive)
        {
            var predicate = PredicateBuilder.True<TestCompetencies>();
            IsFilterActive = false;

            if (!string.IsNullOrEmpty(filters.Login))
            {
                predicate = predicate.And(p => p.Employee.Login.ToLower().Contains(filters.Login.ToLower()));
                IsFilterActive = true;
            }

            if (!string.IsNullOrEmpty(filters.Test))
            {
                predicate = predicate.And(p => p.Title.ToLower().Contains(filters.Test.ToLower()));
                IsFilterActive = true;
            }

            predicate = predicate.And(p => p.TestResult != null);
            IsFilterActive = true;

            return predicate;
        }

        public bool IsDocExists(int competenceId)
        {
            var isCompetenceExist = _testCompetenciesReporitory.GetResultSpec(x => x.Any(p => p.Id == competenceId));

            if (!isCompetenceExist)
                return false;

            var docName = _testCompetenciesReporitory.GetResultSpec(x => x.Where(p => p.Id == competenceId).First()).Title;
            var isDocExist = _templateRepository.GetResultSpec(x => x.Any(p => p.Name == docName));

            if (!isDocExist)
                return false;

            var templateId = _templateRepository.GetResultSpec(x => x.Where(p => p.Name == docName).First()).Id;

            if (_templateRepository.GetById(templateId).Link != null)
                return true;
            else
                return false;
        }

        public int GetTemplateId(int competenceId)
        {
            var docName = _testCompetenciesReporitory.GetResultSpec(x => x.Where(p => p.Id == competenceId).First()).Title;
            
            return _templateRepository.GetResultSpec(x => x.Where(p => p.Name == docName).First()).Id;
        }

        public List<CompetenciesTestDTO> GetTestData(string docPath)
        {
            var questions = new List<CompetenciesTestDTO>();

            var xDoc = new XmlDocument();
            xDoc.Load(docPath);

            XmlElement? xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                foreach (XmlElement xnode in xRoot)
                {
                    var question = new CompetenciesTestDTO();

                    XmlNode? attr = xnode.Attributes.GetNamedItem("name");
                    question.Question = attr?.Value;
                    question.Answers = new List<TestAnswersDTO>();

                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        if (childnode.Name == "correctAnswer")
                        {
                            var answer = new TestAnswersDTO
                            {
                                Answer = childnode.InnerText,
                                IsCorrect = true
                            };

                            question.Answers.Add(answer);
                        }
                           
                        if (childnode.Name == "answer")
                        {
                            var answer = new TestAnswersDTO
                            {
                                Answer = childnode.InnerText,
                                IsCorrect = false
                            };

                            question.Answers.Add(answer);
                        }
                    }

                    questions.Add(question);
                }
            }

            return questions;
        }

        public void Create(TestCreateDTO model)
        {
            var statusId = _superDictionaryRepository.GetResultSpec(x => x.Where(p => p.DictionaryId == (int)DictionaryEnum.TestStatus &&
                p.Name == nameof(TestStatusEnum.Open))).First().Id;

            var data = new TestCompetencies
            {
                Title = model.Title,
                EmployeeId = model.EmployeeId,
                StatusId = statusId
            };

            _testCompetenciesReporitory.Create(data);
        }

        public void Complete(TestCompleteDTO model)
        {
            var data = _testCompetenciesReporitory.GetById(model.Id);

            data.TestResult = model.Result;
            data.Date = DateTime.Now.ToUniversalTime();
            data.StatusId = _superDictionaryRepository.GetResultSpec(x => x
                .Where(p => p.DictionaryId == (int)DictionaryEnum.TestStatus && p.Name == nameof(TestStatusEnum.Closed)).First()).Id;

            _testCompetenciesReporitory.Update(data);
        }
    }
}
