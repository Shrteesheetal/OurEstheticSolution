﻿using Azure;
using Microsoft.EntityFrameworkCore;
using OurEstheticSolution.Data;
using OurEstheticSolution.Interface;
using OurEstheticSolution.Models.Entities;

using OurEstheticSolution.ViewModel;

namespace OurEstheticSolution.Repository
{
    public class IServiceRepo : IService
    {
        private readonly AppDBContext _context; // Add this field to hold the DbContext instance

        // Constructor to inject the DbContext
        public IServiceRepo(AppDBContext context)
        {
            _context = context;
        }

        public void DeleteService(Guid id)
        {
            var service = _context.Services.Find(id); // Find the service by ID

            if (service != null)
            {
                _context.Services.Remove(service); // Remove the service entity
                _context.SaveChanges();            // Save changes to the database
            }
            else
            {
                throw new Exception("Service not found.");
            }
        }


        public IEnumerable<ServiceViewModel> GetAllServices()
        {
            return _context.Services// Use DbContext's Set<T>() to access the Services DbSet
                .Select(service => new ServiceViewModel
                {
                    Id = service.Id,
                    Name = service.Name,
                    Description = service.Description,
                    TimePeriod = service.TimePeriod,
                    TotalCost = service.TotalCost,
                    Tools = service.Tools,
                    CreatedDate = service.CreatedDate,
                    CreatedBy = service.CreatedBy,
                    Imagepath=service.Imagepath
                    


                })
                .ToList();
        }

        public ServiceViewModel GetById(Guid id)
        {
            var service = _context.Services
                .Where(s => s.Id == id)
                .Select(s => new ServiceViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    TimePeriod = s.TimePeriod,
                    TotalCost = s.TotalCost,
                    Tools = s.Tools,
                    CreatedDate = s.CreatedDate,
                    CreatedBy = s.CreatedBy,
                    Imagepath=s.Imagepath

                })
                .FirstOrDefault();

            if (service == null)
            {
                throw new Exception($"Service with ID {id} not found.");
            }

            return service;
        }


        public void InsertService(ServiceViewModel model)
        {
            // Map ServiceViewModel to Service entity
            var serviceEntity = new Service
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                TimePeriod = model.TimePeriod,
                TotalCost = model.TotalCost,
                Tools = model.Tools,
                CreatedBy = model.CreatedBy,
                CreatedDate = DateTime.Now,
                Imagepath=model.Imagepath
            };

            _context.Services.Add(serviceEntity); // Use Set<T>() to access the DbSet
            _context.SaveChanges(); // Save changes to the database
        }

        public void UpdateService(ServiceViewModel model)
        {
            var service = _context.Services.FirstOrDefault(s => s.Id == model.Id);

            if (service != null)
            {
                // Map fields from ViewModel to Entity
                service.Id = model.Id;
                service.Name = model.Name;
                service.Description = model.Description;
                service.TimePeriod = model.TimePeriod;
                service.TotalCost = model.TotalCost;
                service.Tools = model.Tools;
                service.CreatedDate = model.CreatedDate;
                service.Imagepath = model.Imagepath;
             

                _context.SaveChanges(); // Commit changes
            }
            else
            {
                throw new Exception("Service not found.");
            }
        }
      
    }
};


    



