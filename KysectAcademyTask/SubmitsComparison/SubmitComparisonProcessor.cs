using KysectAcademyTask.FileComparison;
using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;
using KysectAcademyTask.Submit;
using KysectAcademyTask.Submit.SubmitFilters;
using KysectAcademyTask.Utils.ProgressTracking;

namespace KysectAcademyTask.SubmitsComparison;

internal class SubmitComparisonProcessor
{
    private readonly IReadOnlyList<SubmitInfo> _submits;
    private Filters _filters;
    private readonly string _rootDir;
    private readonly IReadOnlyList<ComparisonAlgorithm.Metrics> _metrics;
    private IProgressBar? _progressBar;

    private delegate void ProgressBarUpdater();

    private event ProgressBarUpdater? ProgressBarUpdate;

    public SubmitComparisonProcessor(string rootDir, Filters? filters,
        IReadOnlyList<ComparisonAlgorithm.Metrics> metrics)
    {
        _submits = new SubmitGetter(rootDir, filters).GetSubmits();
        _filters = filters ?? new Filters();
        _rootDir = rootDir;
        _metrics = metrics;
    }

    public void SetProgressBar(IProgressBar progressBar)
    {
        _progressBar = progressBar;
    }

    public ComparisonResultsTable GetComparisonResults()
    {
        IReadOnlyCollection<(string dirName1, string dirName2)> suitablePairs = GetSuitablePairsDirNames();
        ComparisonResultsTable results = new();

        if (_progressBar is not null)
        {
            ComparisonProgressTracker progressTracker = new(_progressBar, suitablePairs.Count);
            ProgressBarUpdate = progressTracker.IncreaseProgress;
        }

        foreach ((string dirName1, string dirName2) pair in suitablePairs)
        {
            AddComparisonToTable(results, pair);
            OnProgressBarUpdate();
        }

        return results;
    }

    private void AddComparisonToTable(ComparisonResultsTable results, (string dirName1, string dirName2) pairToCompare)
    {
        FileProcessor fileProcessor = new(pairToCompare.dirName1, pairToCompare.dirName2, _filters.FileRequirements,
            _filters.DirectoryRequirements);
        ComparisonResultsTable curSubmitsTable = fileProcessor.GetComparisonResults(_metrics);
        results.AddTable(curSubmitsTable);
    }


    private IReadOnlyCollection<(string dirName1, string dirName2)> GetSuitablePairsDirNames()
    {
        List<(string dirName1, string dirName2)> pairs = new();

        for (int i = 0; i < _submits.Count - 1; ++i)
        {
            for (int j = i; j < _submits.Count; ++j)
            {
                if (!AreSuitable(_submits[i], _submits[j])) continue;
                string dirname1 =
                    new SubmitInfoProcessor().SubmitInfoToDirectoryPath(_submits[i], _rootDir, "yyyyMMddHHmmss");
                string dirname2 =
                    new SubmitInfoProcessor().SubmitInfoToDirectoryPath(_submits[j], _rootDir, "yyyyMMddHHmmss");
                (string dirName1, string dirName2) pair = new(dirname1, dirname2);
                pairs.Add(pair);
            }
        }

        return pairs;
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

    private void OnProgressBarUpdate()
    {
        ProgressBarUpdate?.Invoke();
    }
}