using KysectAcademyTask.DataAccess.Repos.Interfaces;

namespace KysectAcademyTask.DataAccess.Repos;

public class AllRepos
{
    public IComparisonResultRepo ComparisonResultRepo { get; } 
    public IGroupRepo GroupRepo { get; }
    public IHomeWorkRepo HomeWorkRepo { get; }
    public IStudentRepo StudentRepo { get; }
    public ISubmitRepo SubmitRepo { get; }

    public AllRepos(IComparisonResultRepo comparisonResultRepo, IGroupRepo groupRepo,
        IHomeWorkRepo homeWorkRepo, IStudentRepo studentRepo, ISubmitRepo submitRepo)
    {
        ComparisonResultRepo = comparisonResultRepo;
        GroupRepo = groupRepo;
        HomeWorkRepo = homeWorkRepo;
        StudentRepo = studentRepo;
        SubmitRepo = submitRepo;
    }
}