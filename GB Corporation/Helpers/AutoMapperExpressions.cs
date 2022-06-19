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
        //Employee
        public static List<EmployeeDTO> AutoMapEmployee(List<Employee> entities)
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
        public static List<TemplateDTO> AutoMapTemplateDTO(List<Template> entities)
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
        public static List<TestCompetenciesDTO> AutoMapTestCompetenciesDTO(List<TestCompetencies> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<TestCompetencies, TestCompetenciesDTO>();
            });

            var mapper = new Mapper(config);

            return mapper.Map<List<TestCompetenciesDTO>>(entities);
        }

        //ShortDTO
        public static List<ShortDTO> AutoMapShortDTO(List<SuperDictionary> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<SuperDictionary, ShortDTO>();
            });

            var mapper = new Mapper(config);

            return mapper.Map<List<ShortDTO>>(entities);
        }

        public static List<ShortDTO> AutoMapShortDTO(List<Role> entities)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Role, ShortDTO>()
                    .ForMember(dist => dist.Name, opt => opt.MapFrom(src => src.Title));
            });

            var mapper = new Mapper(config);

            return mapper.Map<List<ShortDTO>>(entities);
        }
    }
}
