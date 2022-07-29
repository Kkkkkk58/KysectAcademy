﻿using KysectAcademyTask.DataAccess.Models.Entities.Base;

namespace KysectAcademyTask.DataAccess.Models.Entities;

public class Submit : BaseEntity
{
    public int StudentId { get; set; }
    public Student StudentNavigation { get; set; }
    public DateTime? Date { get; set; } = null!;
    public int HomeWorkId { get; set; }
    public HomeWork HomeWorkNavigation { get; set; }
    public ICollection<FileEntity> Files { get; set; } = null!;
}