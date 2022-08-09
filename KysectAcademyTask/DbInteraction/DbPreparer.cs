using KysectAcademyTask.DataAccess.Models.Entities;
using KysectAcademyTask.DataAccess.Models.Entities.Owned;
using KysectAcademyTask.DataAccess.Repos.Interfaces;
using KysectAcademyTask.Submit;

namespace KysectAcademyTask.DbInteraction;

public class DbPreparer
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
        PrepareSubmits(submits);
    }

    private void PrepareSubmits(IEnumerable<SubmitInfo> submits)
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
        IReadOnlyList<string> splitName = GetSplitName(authorName);

        if (_studentRepo.GetQueryWithProps(splitName[0], splitName[1], groupName).Any())
            return;

        IQueryable<Group> groupQuery = _groupRepo.GetQueryWithProps(groupName);
        int groupId = groupQuery.Single().Id;

        Student student = CreateStudent(authorName, groupId, splitName);
        _studentRepo.Add(student);
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

        IReadOnlyList<string> splitName = GetSplitName(submit.AuthorName);
        IQueryable<Student> studentQuery =
            _studentRepo.GetQueryWithProps(splitName[0], splitName[1], submit.GroupName);
        int studentId = studentQuery.Single().Id;

        DataAccess.Models.Entities.Submit submitData = CreateSubmit(submit.SubmitDate, homeWorkId, studentId);
        submitsToAdd.Add(submitData);
    }

    private Student CreateStudent(string authorName, int groupId, IReadOnlyList<string> splitName)
    {
        return new Student
        {
            GroupId = groupId,
            PersonalInformation = CreatePerson(authorName, splitName)
        };
    }

    private static Person CreatePerson(string authorName, IReadOnlyList<string> splitName)
    {
        return new Person
        {
            FirstName = splitName[0],
            LastName = splitName[1],
            FullName = authorName
        };
    }

    private static DataAccess.Models.Entities.Submit CreateSubmit(DateTime? submitDate, int homeWorkId, int studentId)
    {
        return new DataAccess.Models.Entities.Submit
        {
            Date = submitDate,
            HomeWorkId = homeWorkId,
            StudentId = studentId
        };
    }

    private static IReadOnlyList<string> GetSplitName(string authorName)
    {
        IReadOnlyList<string> splitName = authorName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (splitName.Count != 2)
        {
            throw new ArgumentException($"Unable to work with author's name: {authorName}");
        }

        return splitName;
    }
}