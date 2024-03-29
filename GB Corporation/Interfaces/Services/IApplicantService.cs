﻿using GB_Corporation.DTOs;

namespace GB_Corporation.Interfaces.Services
{
    public interface IApplicantService
    {
        List<ApplicantDTO> ListAll(ApplicantFilterDTO filters);
        ApplicantUpdateDTO GetById(int id);
        List<ShortDTO> ListActiveShort();
        void Create(ApplicantCreateDTO register);
        bool IsExists(string login);
        bool IsExists(int id);
        void Update(ApplicantUpdateDTO model);
        ApplicantTestsDTO GetTestDatas(int id);
        void CreateTestData(LogicTestDTO model);
        void CreateTestData(ForeignLanguageTestDTO model);
        void CreateTestData(ProgrammingTestDTO model);
    }
}
