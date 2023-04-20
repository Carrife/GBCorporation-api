using AutoMapper;
using GB_Corporation.DTOs;
using GB_Corporation.Models;

namespace GB_Corporation.Helpers
{
    public static class AutoMapperExpression
    {
        //Applicant
        public static List<ApplicantDTO> AutoMapApplicantDTO(IQueryable<Applicant> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Applicant, ApplicantDTO>()
                    .ForMember(dist => dist.NameRu, opt => opt.MapFrom(x => $"{x.SurnameRu} {x.NameRu} {x.PatronymicRu}"))
                    .ForMember(dist => dist.NameEn, opt => opt.MapFrom(x => $"{x.NameEn} {x.SurnameEn}"))
                    .ForMember(dist => dist.Status, opt => opt.MapFrom(x => x.Status.Name));
            });

            var mapper = new Mapper(config);
            return mapper.Map<List<ApplicantDTO>>(entities);
        }

        public static ApplicantUpdateDTO AutoMapApplicantUpdateDTO(Applicant entity)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Applicant, ApplicantUpdateDTO>();
            });

            var mapper = new Mapper(config);
            return mapper.Map<ApplicantUpdateDTO>(entity);
        }

        public static List<LogicTestDTO> AutoMapApplicantLogicTestDTO(IQueryable<ApplicantLogicTest> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApplicantLogicTest, LogicTestDTO>()
                    .ForMember(dist => dist.Date, opt => opt.MapFrom(x => x.Date.ToString("yyyy-MM-dd")));
            });

            var mapper = new Mapper(config);
            return mapper.Map<List<LogicTestDTO>>(entities);
        }

        public static List<ForeignLanguageTestDTO> AutoMapApplicantForeignLanguageTestDTO(IQueryable<ApplicantForeignLanguageTest> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApplicantForeignLanguageTest, ForeignLanguageTestDTO>()
                    .ForMember(dist => dist.ForeignLanguage, opt => opt.MapFrom(x => x.ForeignLanguage.Name))
                    .ForMember(dist => dist.Date, opt => opt.MapFrom(x => x.Date.ToString("yyyy-MM-dd")));
            });

            var mapper = new Mapper(config);
            return mapper.Map<List<ForeignLanguageTestDTO>>(entities);
        }

        public static List<ProgrammingTestDTO> AutoMapApplicantProgrammingTestDTO(IQueryable<ApplicantProgrammingTest> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApplicantProgrammingTest, ProgrammingTestDTO>()
                    .ForMember(dist => dist.ProgrammingLanguage, opt => opt.MapFrom(x => x.ProgrammingLanguage.Name))
                    .ForMember(dist => dist.Date, opt => opt.MapFrom(x => x.Date.ToString("yyyy-MM-dd")));
            });

            var mapper = new Mapper(config);
            return mapper.Map<List<ProgrammingTestDTO>>(entities);
        }

        //Employee
        public static List<EmployeeDTO> AutoMapEmployeeDTO(IQueryable<Employee> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Employee, EmployeeDTO>()
                    .ForMember(dist => dist.NameRu, opt => opt.MapFrom(x => $"{x.SurnameRu} {x.NameRu} {x.PatronymicRu}"))
                    .ForMember(dist => dist.NameEn, opt => opt.MapFrom(x => $"{x.NameEn} {x.SurnameEn}"))
                    .ForMember(dist => dist.Status, opt => opt.MapFrom(x => x.Status.Name))
                    .ForMember(dist => dist.Department, opt => opt.MapFrom(x => x.Department.Name))
                    .ForMember(dist => dist.Position, opt => opt.MapFrom(x => x.Position.Name));
            });

            var mapper = new Mapper(config);
            return mapper.Map<List<EmployeeDTO>>(entities);
        }

        public static EmployeeGetDTO AutoMapEmployeeGetDTO(Employee entity)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Employee, EmployeeGetDTO>();
                cfg.CreateMap<SuperDictionary, ShortDTO>();
                    
            });

            var mapper = new Mapper(config);
            return mapper.Map<EmployeeGetDTO>(entity);
        }

        //Template
        public static List<TemplateDTO> AutoMapTemplateDTO(IQueryable<Template> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Template, TemplateDTO>()
                    .ForMember(dist => dist.LastUpdate, opt => opt.MapFrom(x => x.LastUpdate.GetValueOrDefault().ToString("yyyy-MM-dd")));
            });

            var mapper = new Mapper(config);

            return mapper.Map<List<TemplateDTO>>(entities);
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
                cfg.CreateMap<TestCompetencies, TestCompetenciesDTO>()
                    .ForMember(dist => dist.Date, opt => opt.MapFrom(x => x.Date.ToString("yyyy-MM-dd")));
            });

            var mapper = new Mapper(config);

            return mapper.Map<List<TestCompetenciesDTO>>(entities);
        }

        //Hiring
        public static List<HiringDataDTO> AutoMapHiringDataDTO(IQueryable<HiringData> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<HiringData, HiringDataDTO>()
                    .ForMember(dist => dist.Date, opt => opt.MapFrom(x => x.Date.ToString("yyyy-MM-dd")))
                    .ForMember(dist => dist.Applicant, opt => opt.MapFrom(x => $"{x.Applicant.NameEn} {x.Applicant.SurnameEn} ({x.Applicant.Login})"))
                    .ForMember(dist => dist.Position, opt => opt.MapFrom(x => x.Position.Name))
                    .ForMember(dist => dist.Status, opt => opt.MapFrom(x => x.Status.Name));
            });

            var mapper = new Mapper(config);
            return mapper.Map<List<HiringDataDTO>>(entities);
        }

        public static HiringDTO AutoMapHiringDTO(HiringData entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<HiringData, HiringDTO>()
                    .ForMember(dist => dist.Date, opt => opt.MapFrom(x => x.Date.ToString("yyyy-MM-dd")))
                    .ForPath(dist => dist.Applicant.Name, opt => opt.MapFrom(x => $"{x.Applicant.NameEn} {x.Applicant.SurnameEn} ({x.Applicant.Login})"))
                    .ForPath(dist => dist.Applicant.Id, opt => opt.MapFrom(x => x.ApplicantId))
                    .ForMember(dist => dist.Position, opt => opt.MapFrom(x => x.Position.Name))
                    .ForMember(dist => dist.Status, opt => opt.MapFrom(x => x.Status.Name));
            });

            var mapper = new Mapper(config);
            return mapper.Map<HiringDTO>(entities);
        }

        public static List<HiringInterviewerDTO> AutoMapHiringInterviewerDTO(IQueryable<HiringInterviewer> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<HiringInterviewer, HiringInterviewerDTO>()
                    .ForMember(dist => dist.Interviewer, opt => opt.MapFrom(x => x.Interviewer))
                    .ForMember(dist => dist.Description, opt => opt.MapFrom(x => x.Description))
                    .ForMember(dist => dist.Position, opt => opt.MapFrom(x => x.Position));
                cfg.CreateMap<Role, ShortDTO>()
                    .ForMember(dist => dist.Name, opt => opt.MapFrom(x => x.Title)); ;
                cfg.CreateMap<Employee, ShortDTO>()
                    .ForMember(dist => dist.Name, opt => opt.MapFrom(x => $"{x.NameEn} {x.SurnameEn} ({x.Login})"));
            });

            var mapper = new Mapper(config);
            return mapper.Map<List<HiringInterviewerDTO>>(entities);
        }

        public static HiringAcceptDTO AutoMapHiringAcceptDTO(Applicant entity)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Applicant, HiringAcceptDTO>();
            });

            var mapper = new Mapper(config);
            return mapper.Map<HiringAcceptDTO>(entity);
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

        public static ShortDTO AutoMapShortDTO(Employee entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Employee, ShortDTO>()
                    .ForMember(dist => dist.Name, opt => opt.MapFrom(src => $"{src.NameEn} {src.SurnameEn} ({src.Login})"));
            });

            var mapper = new Mapper(config);

            return mapper.Map<ShortDTO>(entities);
        }

        public static List<ShortDTO> AutoMapShortDTO(IQueryable<ApplicantForeignLanguageTest> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApplicantForeignLanguageTest, ShortDTO>()
                    .ForMember(dist => dist.Name, opt => opt.MapFrom(src => $"{src.ForeignLanguage.Name} - {src.Result}% ({src.Date.ToString("yyyy-MM-dd")})"));
            });

            var mapper = new Mapper(config);

            return mapper.Map<List<ShortDTO>>(entities);
        }

        public static ShortDTO AutoMapShortDTO(ApplicantForeignLanguageTest entity)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApplicantForeignLanguageTest, ShortDTO>()
                    .ForMember(dist => dist.Name, opt => opt.MapFrom(src => $"{src.ForeignLanguage.Name} - {src.Result}% ({src.Date.ToString("yyyy-MM-dd")})"));
            });

            var mapper = new Mapper(config);

            return mapper.Map<ShortDTO>(entity);
        }

        public static List<ShortDTO> AutoMapShortDTO(IQueryable<ApplicantLogicTest> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApplicantLogicTest, ShortDTO>()
                    .ForMember(dist => dist.Name, opt => opt.MapFrom(src => $"{src.Result}% ({src.Date.ToString("yyyy-MM-dd")})"));
            });

            var mapper = new Mapper(config);

            return mapper.Map<List<ShortDTO>>(entities);
        }

        public static ShortDTO AutoMapShortDTO(ApplicantLogicTest entity)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApplicantLogicTest, ShortDTO>()
                    .ForMember(dist => dist.Name, opt => opt.MapFrom(src => $"{src.Result}% ({src.Date.ToString("yyyy-MM-dd")})"));
            });

            var mapper = new Mapper(config);

            return mapper.Map<ShortDTO>(entity);
        }

        public static List<ShortDTO> AutoMapShortDTO(IQueryable<ApplicantProgrammingTest> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApplicantProgrammingTest, ShortDTO>()
                    .ForMember(dist => dist.Name, opt => opt.MapFrom(src => $"{src.ProgrammingLanguage.Name} - {src.Result}% ({src.Date.ToString("yyyy-MM-dd")})"));
            });

            var mapper = new Mapper(config);

            return mapper.Map<List<ShortDTO>>(entities);
        }

        public static ShortDTO AutoMapShortDTO(ApplicantProgrammingTest entity)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApplicantProgrammingTest, ShortDTO>()
                    .ForMember(dist => dist.Name, opt => opt.MapFrom(src => $"{src.ProgrammingLanguage.Name} - {src.Result}% ({src.Date.ToString("yyyy-MM-dd")})"));
            });

            var mapper = new Mapper(config);

            return mapper.Map<ShortDTO>(entity);
        }
    }
}
