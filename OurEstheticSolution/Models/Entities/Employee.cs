using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OurEstheticSolution.Models.Entities;
public class Employee
{
    [Key] // Marks as Primary Key
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Address is required")]
    [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters")]
    public string? Address { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Designation is required")]
    [StringLength(50, ErrorMessage = "Designation cannot be longer than 50 characters")]
    public string? Designation { get; set; }


    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
