using GB_Corporation.DTOs;
using GB_Corporation.Helpers;
using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Interfaces.Services;
using GB_Corporation.Models;
using System.Xml;

namespace GB_Corporation.Services
{
    public class TestCompetenciesService : ITestCompetenciesService
    {
        private readonly IRepository<TestCompetencies> _testCompetenciesReporitory;
        private readonly IRepository<Employee> _employeeReporitory;
        private readonly IRepository<Template> _templateRepository;

        public TestCompetenciesService(IRepository<TestCompetencies> testCompetenciesReporitory, IRepository<Employee> employeeReporitory,
            IRepository<Template> templateRepository)
        {
            _testCompetenciesReporitory = testCompetenciesReporitory;
            _employeeReporitory = employeeReporitory;
            _templateRepository = templateRepository;
        }

        public List<TemplateDTO> GetAll()
        {
            return AutoMapperExpression.AutoMapTemplateDTO(_templateRepository.GetListResultSpec(x => x.Where(p => p.Link != null)).OrderBy(x => x.Name));
        }

        public List<TestCompetenciesDTO> GetUserTests(int id)
        {
            return AutoMapperExpression.AutoMapTestCompetenciesDTO(_testCompetenciesReporitory.GetListResultSpec(x => 
                x.Where(p => p.EmployeeId == id))
                .OrderBy(x => x.Title).ThenBy(x => x.Date));
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

        public void Complete(TestCompleteDTO model)
        {
            var competence = new TestCompetencies
            {
                Title = model.Title,
                TestResult = model.Result,
                EmployeeId = model.UserId,
                Date = DateTime.Now.ToUniversalTime()
        };

            _testCompetenciesReporitory.Create(competence);
        }
    }
}
