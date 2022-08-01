namespace KysectAcademyTask.Submit.SubmitFilters;

public readonly struct Filters
{
    public SubmitInfoRequirements? SubmitInfoRequirements { get; init; }
    public FileRequirements? FileRequirements { get; init; }
    public DirectoryRequirements? DirectoryRequirements { get; init; }

    public Filters(SubmitInfoRequirements? submitInfoRequirements, FileRequirements? fileRequirements,
        DirectoryRequirements? directoryRequirements)
    {
        SubmitInfoRequirements = submitInfoRequirements;
        FileRequirements = fileRequirements;
        DirectoryRequirements = directoryRequirements;
    }

    public bool AreDirectoryRequirementsNullOrSatisfied(string path)
    {
        return DirectoryRequirements is null || ((DirectoryRequirements)DirectoryRequirements).AreSatisfiedBy(path);
    }

    public bool AreFileRequirementsNullOrSatisfied(string path)
    {
        return FileRequirements is null || ((FileRequirements)FileRequirements).AreSatisfiedBy(path);
    }

    public bool AreSubmitRequirementsNullOrSatisfied(SubmitInfo submitInfo)
    {
        return SubmitInfoRequirements is null ||
               ((SubmitInfoRequirements)SubmitInfoRequirements).AreSatisfiedBy(submitInfo);
    }

    public bool IsAuthorNameNullOrIsContainedInWhiteList(string authorName)
    {
        return SubmitInfoRequirements?.AuthorFilter is null
               || ((SubmitInfoRequirements)SubmitInfoRequirements).AuthorFilter.WhiteList.Count == 0
               || ((SubmitInfoRequirements)SubmitInfoRequirements).AuthorFilter.WhiteList.Contains(authorName);
    }
}