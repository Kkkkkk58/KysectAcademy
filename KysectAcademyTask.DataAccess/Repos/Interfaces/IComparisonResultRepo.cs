﻿using KysectAcademyTask.DataAccess.Models.Entities;
using KysectAcademyTask.DataAccess.Repos.Base;

namespace KysectAcademyTask.DataAccess.Repos.Interfaces;

public interface IComparisonResultRepo : IRepo<ComparisonResult>
{
    public IQueryable<ComparisonResult> GetQueryWithProps(Submit submit1, Submit submit2);
}