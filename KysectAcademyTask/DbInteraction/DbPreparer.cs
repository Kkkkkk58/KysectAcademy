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

            if (!_groupRepo.GetQueryWithProps(submit.GroupName).Any())
            {
                _groupRepo.Add(new Group
                {
                    Name = submit.GroupName
                });
            }

            if (!_homeWorkRepo.GetQueryWithProps(submit.HomeworkName).Any())
            {
                _homeWorkRepo.Add(new HomeWork
                {
                    Name = submit.HomeworkName
                });
            }

            string[] splitName = submit.AuthorName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (!_studentRepo.GetQueryWithProps(splitName[0], splitName[1], submit.GroupName).Any())
            {
                IQueryable<Group> groupQuery = _groupRepo.GetQueryWithProps(submit.GroupName);
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

            if (!_submitRepo.GetQueryWithProps(submit.AuthorName, submit.GroupName, submit.HomeworkName, submit.SubmitDate).Any())
            {
                IQueryable<HomeWork> homeWorkQuery = _homeWorkRepo.GetQueryWithProps(submit.HomeworkName);
                int homeWorkId = homeWorkQuery.Single().Id;

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

        _submitRepo.AddRange(submitsToAdd);
    }
}