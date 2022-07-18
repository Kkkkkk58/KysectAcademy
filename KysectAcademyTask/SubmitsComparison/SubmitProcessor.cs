using KysectAcademyTask.FileComparison;
using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;
using KysectAcademyTask.Report.Reporters;
using KysectAcademyTask.Submit;
using KysectAcademyTask.Submit.SubmitFilters;

namespace KysectAcademyTask.SubmitsComparison;

internal class SubmitProcessor
{
    private readonly IReadOnlyList<SubmitInfo> _submits;
    private Filters _filters;
    private readonly string _rootDir;
    private readonly IReadOnlyList<ComparisonAlgorithm.Metrics> _metrics;

    public SubmitProcessor(string rootDir, Filters? filters, IReadOnlyList<ComparisonAlgorithm.Metrics> metrics)
    {
        _submits = new SubmitGetter(rootDir, filters).GetSubmits();
        _filters = filters ?? new Filters();
        _rootDir = rootDir;
        _metrics = metrics;
    }

    public ComparisonResultsTable GetComparisonResults()
    {
        ComparisonResultsTable results = new();
        for (int i = 0; i < _submits.Count - 1; ++i)
        {
            for (int j = i; j < _submits.Count; ++j)
            {
                AddComparisonToTableIfSuitable(results, _submits[i], _submits[j]);

            }
        }
        return results;
    }

    private void AddComparisonToTableIfSuitable(ComparisonResultsTable results, SubmitInfo submit1, SubmitInfo submit2)
    {
        if (AreSuitable(submit1, submit2))
        {

            string dirname1 =
                new SubmitInfoProcessor().SubmitInfoToDirectoryPath(submit1, _rootDir, "yyyyMMddHHmmss");
            string dirname2 =
                new SubmitInfoProcessor().SubmitInfoToDirectoryPath(submit2, _rootDir, "yyyyMMddHHmmss");
            FileProcessor fileProcessor = new(dirname1, dirname2, _filters.FileRequirements,
                _filters.DirectoryRequirements);
            ComparisonResultsTable curSubmitsTable = fileProcessor.GetComparisonResults(_metrics);
            ConsoleReporter r = new();
            r.MakeReport(curSubmitsTable);
            results.AddTable(curSubmitsTable);
        }
    }

    private bool AreSuitable(SubmitInfo submit1, SubmitInfo submit2)
    {
        return IsAuthorSatisfyingRequirements(submit1, submit2)
               && AreSubmitsFromDifferentAuthors(submit1, submit2)
               && IsSameHomework(submit1, submit2);
    }

    private bool IsAuthorSatisfyingRequirements(SubmitInfo submit1, SubmitInfo submit2)
    {
        return _filters.IsAuthorNameNullOrSatisfied(submit1.AuthorName)
               || _filters.IsAuthorNameNullOrSatisfied(submit2.AuthorName);
    }

    private bool AreSubmitsFromDifferentAuthors(SubmitInfo submit1, SubmitInfo submit2)
    {
        return submit1.AuthorName != submit2.AuthorName;
    }

    private bool IsSameHomework(SubmitInfo submit1, SubmitInfo submit2)
    {
        return string.Equals(submit1.HomeworkName,
            submit2.HomeworkName, StringComparison.CurrentCultureIgnoreCase);
    }
}