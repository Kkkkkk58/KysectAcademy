using KysectAcademyTask.DataAccess.Models.Entities;
using KysectAcademyTask.DataAccess.Models.Entities.Owned;
using KysectAcademyTask.DataAccess.Repos.Interfaces;
using KysectAcademyTask.Submit;

namespace KysectAcademyTask.DbInteraction;

internal class DbPreparer
{
    private readonly IGroupRepo _groupRepo;
    private readonly IHomeWorkRepo _homeWorkRepo;
    private readonly IStudentRepo _studentRepo;
    private readonly ISubmitRepo _submitRepo;

    public DbPreparer(IGroupRepo groupRepo, IHomeWorkRepo homeWorkRepo,
        IStudentRepo studentRepo, ISubmitRepo submitRepo)
    {
        _groupRepo = groupRepo;
        _homeWorkRepo = homeWorkRepo;
        _studentRepo = studentRepo;
        _submitRepo = submitRepo;
    }

    public void Prepare(IReadOnlyCollection<SubmitInfo> submits)
    {
        var submitsToAdd = new List<DataAccess.Models.Entities.Submit>();

        foreach (SubmitInfo submit in submits)
        {

            PrepareGroup(submit.GroupName);
            PrepareHomeWork(submit.HomeworkName);
            PrepareAuthor(submit.AuthorName, submit.GroupName);
            PrepareSubmitAddToList(submit, submitsToAdd);
        }

        _submitRepo.AddRange(submitsToAdd);
    }
    
    private void PrepareGroup(string groupName)
    {
        if (!_groupRepo.GetQueryWithProps(groupName).Any())
        {
            _groupRepo.Add(new Group
            {
                Name = groupName
            });
        }
    }

    private void PrepareHomeWork(string homeWorkName)
    {
        if (!_homeWorkRepo.GetQueryWithProps(homeWorkName).Any())
        {
            _homeWorkRepo.Add(new HomeWork
            {
                Name = homeWorkName
            });
        }
    }

    private void PrepareAuthor(string authorName, string groupName)
    {
        string[] splitName = authorName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (splitName.Length != 2)
        {
            throw new ArgumentException($"Unable to work with author's name: {authorName}");
        }

        if (_studentRepo.GetQueryWithProps(splitName[0], splitName[1], groupName).Any())
        {
            return;
        }

        IQueryable<Group> groupQuery = _groupRepo.GetQueryWithProps(groupName);
        int groupId = groupQuery.Single().Id;

        _studentRepo.Add(new Student
        {
            GroupId = groupId,
            PersonalInformation = new Person
            {
                FirstName = splitName[0],
                LastName = splitName[1]
            }
        });
    }

    private void PrepareSubmitAddToList(SubmitInfo submit, ICollection<DataAccess.Models.Entities.Submit> submitsToAdd)
    {
        if (_submitRepo
            .GetQueryWithProps(submit.AuthorName, submit.GroupName, submit.HomeworkName, submit.SubmitDate)
            .Any())
        {
            return;
        }

        IQueryable<HomeWork> homeWorkQuery = _homeWorkRepo.GetQueryWithProps(submit.HomeworkName);
        int homeWorkId = homeWorkQuery.Single().Id;

        string[] splitName = submit.AuthorName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        IQueryable<Student> studentQuery =
            _studentRepo.GetQueryWithProps(splitName[0], splitName[1], submit.GroupName);
        int studentId = studentQuery.Single().Id;

        submitsToAdd.Add(new DataAccess.Models.Entities.Submit
        {
            Date = submit.SubmitDate,
            HomeWorkId = homeWorkId,
            StudentId = studentId
        });
    }
}