using KysectAcademyTask.FileComparison;
using KysectAcademyTask.Submit;
using KysectAcademyTask.Submit.SubmitFilters;
using KysectAcademyTask.Utils.ProgressTracking;

namespace KysectAcademyTask.SubmitComparison;

internal class SubmitComparisonProcessor
{
    private readonly IReadOnlyList<SubmitInfo> _submits;
    private readonly SubmitConfig _submitConfig;
    private IProgressBar? _progressBar;

    private delegate void ProgressBarUpdater();

    private event ProgressBarUpdater? ProgressBarUpdate;

    public SubmitComparisonProcessor(SubmitConfig submitConfig)
    {
        _submitConfig = submitConfig;
        _submits = new SubmitGetter(_submitConfig).GetSubmits();
    }

    public void SetProgressBar(IProgressBar progressBar)
    {
        _progressBar = progressBar;
    }

    public ComparisonResultsTable GetComparisonResults()
    {
        IReadOnlyCollection<(string dirName1, string dirName2)> suitablePairs = GetSuitablePairsOfDirNames();
        ComparisonResultsTable results = new();
        ConnectProgressBarEventIfNeeded(suitablePairs.Count);

        foreach ((string dirName1, string dirName2) pair in suitablePairs)
        {
            AddComparisonToTable(results, pair);
            OnProgressBarUpdate();
        }

        return results;
    }

    private void AddComparisonToTable(ComparisonResultsTable results, (string dirName1, string dirName2) pairToCompare)
    {
        FileProcessor fileProcessor = new(pairToCompare.dirName1, pairToCompare.dirName2,
            _submitConfig.Filters?.FileRequirements,
            _submitConfig.Filters?.DirectoryRequirements);
        ComparisonResultsTable curSubmitsTable = fileProcessor.GetComparisonResults(_submitConfig.Metrics);
        results.AddTable(curSubmitsTable);
    }

    private IReadOnlyCollection<(string dirName1, string dirName2)> GetSuitablePairsOfDirNames()
    {
        var pairs = new List<(string dirName1, string dirName2)>();

        for (int i = 0; i < _submits.Count - 1; ++i)
        {
            for (int j = i; j < _submits.Count; ++j)
            {
                if (!AreSuitable(_submits[i], _submits[j])) continue;
                string dirname1 =
                    new SubmitInfoProcessor().SubmitInfoToDirectoryPath(_submits[i], _submitConfig.RootDir,
                        _submitConfig.SubmitTimeFormat);
                string dirname2 =
                    new SubmitInfoProcessor().SubmitInfoToDirectoryPath(_submits[j], _submitConfig.RootDir,
                        _submitConfig.SubmitTimeFormat);
                (string dirName1, string dirName2) pair = new(dirname1, dirname2);
                pairs.Add(pair);
            }
        }

        return pairs;
    }

    private bool AreSuitable(SubmitInfo submit1, SubmitInfo submit2)
    {
        return IsAnyAuthorFromWhiteList(submit1, submit2)
               && AreSubmitsFromDifferentAuthors(submit1, submit2)
               && IsSameHomework(submit1, submit2);
    }

    private bool IsAnyAuthorFromWhiteList(SubmitInfo submit1, SubmitInfo submit2)
    {
        return _submitConfig.Filters is null
               || ((Filters)_submitConfig.Filters).IsAuthorNameNullOrIsContainedInWhiteList(submit1.AuthorName)
               || ((Filters)_submitConfig.Filters).IsAuthorNameNullOrIsContainedInWhiteList(submit2.AuthorName);
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

    private void ConnectProgressBarEventIfNeeded(int workToDo)
    {
        if (_progressBar is not null)
        {
            ComparisonProgressTracker progressTracker = new(_progressBar, workToDo);
            ProgressBarUpdate = progressTracker.IncreaseProgress;
        }
    }

    private void OnProgressBarUpdate()
    {
        ProgressBarUpdate?.Invoke();
    }
}