using AutoMapper;
using GB_Corporation.DTOs;
using GB_Corporation.DTOs.EmployeeDTOs;
using GB_Corporation.DTOs.TemplateDTOs;
using GB_Corporation.DTOs.TestCompetenciesDTOs;
using GB_Corporation.Models;

namespace GB_Corporation.Helpers
{
    public static class AutoMapperExpression
    {
        //Applicant
        public static List<ApplicantDTO> AutoMapApplicantDTO(IQueryable<Applicant> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Applicant, ApplicantDTO>();
                cfg.CreateMap<SuperDictionary, ShortDTO>();
            });

            var mapper = new Mapper(config);
            return mapper.Map<List<ApplicantDTO>>(entities);
        }

        public static Applicant AutoMapApplicant(ApplicantDTO entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApplicantDTO, Applicant>();
                cfg.CreateMap<ShortDTO, SuperDictionary>();
            });

            var mapper = new Mapper(config);
            return mapper.Map<Applicant>(entities);
        }

        public static List<LogicTestDTO> AutoMapApplicantLogicTestDTO(IQueryable<ApplicantLogicTest> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApplicantLogicTest, LogicTestDTO>()
                    .ForMember(dist => dist.Date, opt => opt.MapFrom(x => x.Date.AddDays(1)));
            });

            var mapper = new Mapper(config);
            return mapper.Map<List<LogicTestDTO>>(entities);
        }

        public static List<ForeignLanguageTestDTO> AutoMapApplicantForeignLanguageTestDTO(IQueryable<ApplicantForeignLanguageTest> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApplicantForeignLanguageTest, ForeignLanguageTestDTO>()
                    .ForMember(dist => dist.ForeignLanguage, opt => opt.MapFrom(x => x.ForeignLanguage.Name))
                    .ForMember(dist => dist.Date, opt => opt.MapFrom(x => x.Date.AddDays(1)));
            });

            var mapper = new Mapper(config);
            return mapper.Map<List<ForeignLanguageTestDTO>>(entities);
        }

        public static List<ProgrammingTestDTO> AutoMapApplicantProgrammingTestDTO(IQueryable<ApplicantProgrammingTest> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApplicantProgrammingTest, ProgrammingTestDTO>()
                    .ForMember(dist => dist.ProgrammingLanguage, opt => opt.MapFrom(x => x.ProgrammingLanguage.Name))
                    .ForMember(dist => dist.Date, opt => opt.MapFrom(x => x.Date.AddDays(1)));
            });

            var mapper = new Mapper(config);
            return mapper.Map<List<ProgrammingTestDTO>>(entities);
        }

        //Employee
        public static List<EmployeeDTO> AutoMapEmployee(IQueryable<Employee> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Employee, EmployeeDTO>()
                    .ForMember(dist => dist.Department, opt => opt.MapFrom(x => x.Department))
                    .ForMember(dist => dist.Language, opt => opt.MapFrom(x => x.Language))
                    .ForMember(dist => dist.Role, opt => opt.MapFrom(x => x.Role));
                cfg.CreateMap<Role, ShortDTO>()
                    .ForMember(dist => dist.Name, opt => opt.MapFrom(x => x.Title));
                cfg.CreateMap<SuperDictionary, ShortDTO>();
            });

            var mapper = new Mapper(config);
            return mapper.Map<List<EmployeeDTO>>(entities);
        }

        public static EmployeeDTO AutoMapEmployee(Employee entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Employee, EmployeeDTO>()
                    .ForMember(dist => dist.Department, opt => opt.MapFrom(x => x.Department))
                    .ForMember(dist => dist.Language, opt => opt.MapFrom(x => x.Language))
                    .ForMember(dist => dist.Role, opt => opt.MapFrom(x => x.Role));
                cfg.CreateMap<Role, ShortDTO>()
                    .ForMember(dist => dist.Name, opt => opt.MapFrom(x => x.Title));
                cfg.CreateMap<SuperDictionary, ShortDTO>();
            });

            var mapper = new Mapper(config);

            return mapper.Map<EmployeeDTO>(entities);
        }

        public static Employee AutoMapEmployee(EmployeeUpdateDTO entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EmployeeUpdateDTO, Employee>()
                    .ForMember(dist => dist.Department, opt => opt.MapFrom(x => x.Department))
                    .ForMember(dist => dist.Language, opt => opt.MapFrom(x => x.Language));
                cfg.CreateMap<ShortDTO, SuperDictionary>();
            });

            var mapper = new Mapper(config);

            return mapper.Map<Employee>(entities);
        }

        //Template
        public static List<TemplateDTO> AutoMapTemplateDTO(IQueryable<Template> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Template, TemplateDTO>();
            });

            var mapper = new Mapper(config);

            return mapper.Map<List<TemplateDTO>>(entities);
        }

        public static Template AutoMapTemplate(TemplateDTO entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<TemplateDTO, Template>();
            });

            var mapper = new Mapper(config);

            return mapper.Map<Template>(entities);
        }

        public static Template AutoMapTemplate(TemplateCreateDTO entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<TemplateCreateDTO, Template>();
            });

            var mapper = new Mapper(config);

            return mapper.Map<Template>(entities);
        }

        //TestCompetencies
        public static List<TestCompetenciesDTO> AutoMapTestCompetenciesDTO(IQueryable<TestCompetencies> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<TestCompetencies, TestCompetenciesDTO>();
            });

            var mapper = new Mapper(config);

            return mapper.Map<List<TestCompetenciesDTO>>(entities);
        }

        //Hiring
        public static List<ApplicantHiringDataDTO> AutoMapApplicantHiringDataDTO(IQueryable<ApplicantHiringData> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApplicantHiringData, ApplicantHiringDataDTO>();
                cfg.CreateMap<SuperDictionary, ShortDTO>();
                cfg.CreateMap<Applicant, ShortDTO>()
                    .ForMember(dist => dist.Name, opt => opt.MapFrom(x => $"{x.NameEn} {x.SurnameEn} ({x.Login})"));
                cfg.CreateMap<Employee, ShortDTO>()
                    .ForMember(dist => dist.Name, opt => opt.MapFrom(x => $"{x.NameEn} {x.SurnameEn} ({x.Login})"));
                cfg.CreateMap<ApplicantLogicTest, HiringTestDTO>()
                    .ForMember(dist => dist.Title, opt => opt.MapFrom(x => ""));
                cfg.CreateMap<ApplicantForeignLanguageTest, HiringTestDTO>()
                    .ForMember(dist => dist.Title, opt => opt.MapFrom(x => x.ForeignLanguage.Name));
                cfg.CreateMap<ApplicantProgrammingTest, HiringTestDTO>()
                    .ForMember(dist => dist.Title, opt => opt.MapFrom(x => x.ProgrammingLanguage.Name));
            });

            var mapper = new Mapper(config);
            return mapper.Map<List<ApplicantHiringDataDTO>>(entities);
        }

        //ShortDTO
        public static List<ShortDTO> AutoMapShortDTO(IQueryable<SuperDictionary> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<SuperDictionary, ShortDTO>();
            });

            var mapper = new Mapper(config);

            return mapper.Map<List<ShortDTO>>(entities);
        }

        public static List<ShortDTO> AutoMapShortDTO(IQueryable<Role> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Role, ShortDTO>()
                    .ForMember(dist => dist.Name, opt => opt.MapFrom(src => src.Title));
            });

            var mapper = new Mapper(config);

            return mapper.Map<List<ShortDTO>>(entities);
        }

        public static List<ShortDTO> AutoMapShortDTO(IQueryable<Applicant> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Applicant, ShortDTO>()
                    .ForMember(dist => dist.Name, opt => opt.MapFrom(src => $"{src.NameEn} {src.SurnameEn} ({src.Login})"));
            });

            var mapper = new Mapper(config);

            return mapper.Map<List<ShortDTO>>(entities);
        }

        public static List<ShortDTO> AutoMapShortDTO(IQueryable<Employee> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Employee, ShortDTO>()
                    .ForMember(dist => dist.Name, opt => opt.MapFrom(src => $"{src.NameEn} {src.SurnameEn} ({src.Login})"));
            });

            var mapper = new Mapper(config);

            return mapper.Map<List<ShortDTO>>(entities);
        }

        public static List<ShortDTO> AutoMapShortDTO(IQueryable<ApplicantForeignLanguageTest> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApplicantForeignLanguageTest, ShortDTO>()
                    .ForMember(dist => dist.Name, opt => opt.MapFrom(src => $"{src.ForeignLanguage.Name} - {src.Result}% ({src.Date.ToShortDateString()})"));
            });

            var mapper = new Mapper(config);

            return mapper.Map<List<ShortDTO>>(entities);
        }

        public static List<ShortDTO> AutoMapShortDTO(IQueryable<ApplicantLogicTest> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApplicantLogicTest, ShortDTO>()
                    .ForMember(dist => dist.Name, opt => opt.MapFrom(src => $"{src.Result} ({src.Date.ToShortDateString()})"));
            });

            var mapper = new Mapper(config);

            return mapper.Map<List<ShortDTO>>(entities);
        }

        public static List<ShortDTO> AutoMapShortDTO(IQueryable<ApplicantProgrammingTest> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApplicantProgrammingTest, ShortDTO>()
                    .ForMember(dist => dist.Name, opt => opt.MapFrom(src => $"{src.ProgrammingLanguage.Name} - {src.Result}% ({src.Date.ToShortDateString()})"));
            });

            var mapper = new Mapper(config);

            return mapper.Map<List<ShortDTO>>(entities);
        }
    }
}
