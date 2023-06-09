﻿using KysectAcademyTask.DataAccess.EfStructures;
using KysectAcademyTask.DataAccess.Models.Entities;
using KysectAcademyTask.DataAccess.Repos.Base;
using KysectAcademyTask.DataAccess.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KysectAcademyTask.DataAccess.Repos;

public class HomeWorkRepo : BaseRepo<HomeWork>, IHomeWorkRepo
{
    public HomeWorkRepo(SubmitComparisonDbContext context) : base(context)
    {
    }

    public HomeWorkRepo(DbContextOptions<SubmitComparisonDbContext> options) : base(options)
    {
    }

    public IQueryable<HomeWork> GetQueryWithProps(string name)
    {
        return Table
            .Where(homeWork => homeWork.Name == name);
    }
}