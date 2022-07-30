using KysectAcademyTask.DataAccess.Repos.Interfaces;

namespace KysectAcademyTask.DataAccess.Repos;

public class AllRepos
{
    public IComparisonResultRepo ComparisonResultRepo { get; }
    public IFileEntityRepo FileEntityRepo { get; }
    public IGroupRepo GroupRepo { get; }
    public IHomeWorkRepo HomeWorkRepo { get; }
    public IStudentRepo StudentRepo { get; }
    public ISubmitRepo SubmitRepo { get; }

    public AllRepos(IComparisonResultRepo comparisonResultRepo, IFileEntityRepo fileEntityRepo, IGroupRepo groupRepo,
        IHomeWorkRepo homeWorkRepo, IStudentRepo studentRepo, ISubmitRepo submitRepo)
    {
        ComparisonResultRepo = comparisonResultRepo;
        FileEntityRepo = fileEntityRepo;
        GroupRepo = groupRepo;
        HomeWorkRepo = homeWorkRepo;
        StudentRepo = studentRepo;
        SubmitRepo = submitRepo;
    }
}