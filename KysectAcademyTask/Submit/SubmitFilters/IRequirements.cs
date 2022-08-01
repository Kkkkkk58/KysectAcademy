namespace KysectAcademyTask.Submit.SubmitFilters;

public interface IRequirements<in T>
{
    public bool AreSatisfiedBy(T item);
}