﻿using GB_Corporation.DTOs;
using GB_Corporation.Helpers;
using GB_Corporation.Interfaces.Repositories;
using GB_Corporation.Interfaces.Services;
using GB_Corporation.Models;

namespace GB_Corporation.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly IRepository<Template> _templateRepository;
        private readonly IWebHostEnvironment _appEnvironment;
        public TemplateService(IRepository<Template> templateRepository, IWebHostEnvironment appEnvironment)
        {
            _templateRepository = templateRepository;
            _appEnvironment = appEnvironment;
        }

        public bool IsExists(string name) => _templateRepository.GetResultSpec(x => x.Any(p => p.Name == name));

        public List<TemplateDTO> GetAll()
        {
            return AutoMapperExpression.AutoMapTemplateDTO(_templateRepository.GetListResultSpec(x => x).OrderBy(x => x.Name));
        }

        public void Create(TemplateCreateDTO model)
        {
            var template = AutoMapperExpression.AutoMapTemplate(model);

            _templateRepository.Create(template);
        }

        public void Delete(int id)
        {
            var template = _templateRepository.GetById(id);

            if(template.Link != null)
            {
                FileInfo file = new(template.Link);

                if(file.Exists)
                {
                    file.Delete();
                } 
            }
              
            _templateRepository.Delete(template);
        }

        public bool IsExists(int id)
        {
            if (_templateRepository.GetById(id) != null)
                return true;
            else
                return false;
        }

        public async void Upload(IFormFile file, int templateId)
        {
            var template = _templateRepository.GetById(templateId);

            string path = "Files/Templates/" + file.FileName;

            using (var fileStream = new FileStream(_appEnvironment.ContentRootPath + path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            template.LastUpdate = DateTime.Now.ToUniversalTime();
            template.Link = path;

            _templateRepository.Update(template);
        }

        public bool IsDocExists(int templateId)
        {
            var isIdExist = _templateRepository.GetResultSpec(x => x.Any(p => p.Id == templateId));
            
            if(!isIdExist)
                return false;

            if (_templateRepository.GetById(templateId).Link != null)
                return true;
            else
                return false;
        }

        public string GetFilePath(int templateId)
        {
            var path = _templateRepository.GetById(templateId).Link;
            return Path.Combine(_appEnvironment.ContentRootPath, path);
        }

        public string GetFileName(int templateId)
        {
            var path = _templateRepository.GetById(templateId).Link;
            string name = path.Split("/").ToList().Last();
            return name;
        }
    }
}
