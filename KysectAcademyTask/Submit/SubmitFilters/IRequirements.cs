namespace KysectAcademyTask.Submit.SubmitFilters;

internal interface IRequirements<in T>
{
    public bool AreSatisfiedBy(T item);
}