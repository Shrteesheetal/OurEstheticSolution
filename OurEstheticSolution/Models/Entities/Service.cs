﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurEstheticSolution.Models.Entities
{
    public class Service
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Service name is required")]
        [StringLength(100, ErrorMessage = "Service name cannot be longer than 100 characters")]
        public string? Name { get; set; }

        [StringLength(900, ErrorMessage = "Description cannot be longer than 300 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Time period is required")]
        [StringLength(50, ErrorMessage = "Time period cannot be longer than 50 characters")]
        public string? TimePeriod { get; set; }


        [Range(0, double.MaxValue, ErrorMessage = "Total cost must be a positive number")]
        public decimal TotalCost { get; set; }

        [StringLength(200, ErrorMessage = "Tools info cannot be longer than 200 characters")]
        public string? Tools { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Created by is required")]
        [StringLength(100, ErrorMessage = "Created by name cannot be longer than 100 characters")]
        public string? CreatedBy { get; set; }

        [Display(Name = "Image Path")]
        [StringLength(255, ErrorMessage = "Image path cannot exceed 255 characters.")]
        [DataType(DataType.ImageUrl)]
        public string? Imagepath { get; set; }

    }
}
