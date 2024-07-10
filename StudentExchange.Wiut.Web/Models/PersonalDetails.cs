﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace StudentExchange.Wiut.Web.Models;

public class PersonalDetails
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string ForeName { get; set; }
    public string? SecondForeName { get; set; }
    public string? ThirdForeName { get; set; }
    [Required]
    public string FamilyName { get; set; }
    public string? PrefferedName { get; set; }
    public string? PreviousFamilyName { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    [Required]
    public string Gender { get; set; }
    [Required]
    public string UniversityStudentId { get; set; }
    [Required]
    public string CountryOfBirth { get; set; }
    [Required]
    public string Nationality {  get; set; }
    [Required]
    public string PassportNumber { get; set; }
    [Required]
    public DateTime DateOfIssue {  get; set; }
    [Required]
    public DateTime DateOfExpiry { get; set; }
    [Required]
    public byte[] PassportFile { get; set; }
    [Required]
    public string PassportFileName { get; set; }
    [Required]
    public string PassportFileType { get; set; }
    //Relationship
    public string StudentId { get; set; }
    [ForeignKey("StudentId")]
    public Student Student { get; set; }

    /*if (model.PassportFile != null && model.PassportFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await model.PassportFile.CopyToAsync(memoryStream);
                    model.PassportFileData = memoryStream.ToArray();
                }
                
                model.PassportFileName = model.PassportFile.FileName;
                model.PassportFileType = model.PassportFile.ContentType;
            }*/
}