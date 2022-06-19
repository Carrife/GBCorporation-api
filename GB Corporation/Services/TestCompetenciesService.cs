using GB_Corporation.DTOs.TestCompetenciesDTOs;
using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Interfaces.Services;
using GB_Corporation.Models;
using System.Xml;

namespace GB_Corporation.Services
{
    public class TestCompetenciesService : ITestCompetenciesService
    {
        private readonly ITestCompetenciesReporitory _testCompetenciesReporitory;
        private readonly IEmployeeRepository _employeeReporitory;
        public TestCompetenciesService(ITestCompetenciesReporitory testCompetenciesReporitory, IEmployeeRepository employeeReporitory)
        {
            _testCompetenciesReporitory = testCompetenciesReporitory;
            _employeeReporitory = employeeReporitory;
        }

        public List<TestDTO> GetTestData(string docPath)
        {
            var questions = new List<TestDTO>();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(docPath);

            XmlElement? xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                foreach (XmlElement xnode in xRoot)
                {
                    TestDTO question = new TestDTO();

                    XmlNode? attr = xnode.Attributes.GetNamedItem("name");
                    question.Question = attr?.Value;
                    question.Answers = new List<TestAnswersDTO>();

                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        if (childnode.Name == "correctAnswer")
                        {
                            TestAnswersDTO answer = new TestAnswersDTO
                            {
                                Answer = childnode.InnerText,
                                IsCorrect = true
                            };

                            question.Answers.Add(answer);
                        }
                           
                        if (childnode.Name == "answer")
                        {
                            TestAnswersDTO answer = new TestAnswersDTO
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
            int employeeId = _employeeReporitory.GetAll().Where(x => x.NameEn+" "+x.SurnameEn == model.User).First().Id;

            TestCompetencies competence = new TestCompetencies
            {
                Title = model.Title,
                TestResult = model.Result,
                EmployeeId = employeeId,
                Date = DateTime.Now
            };

            _testCompetenciesReporitory.Create(competence);
        }
    }
}
