using KysectAcademyTask.FileComparison;
using KysectAcademyTask.Submit;
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
                var submitSuitabilityChecker = new SubmitSuitabilityChecker(_submitConfig.Filters);
                if (!submitSuitabilityChecker.AreSuitable(_submits[i], _submits[j]))
                {
                    continue;
                }

                (string dirName1, string dirName2) pair = GetPairOfDirNames(_submits[i], _submits[j]);
                pairs.Add(pair);
            }
        }

        return pairs;
    }

    private (string dirName1, string dirName2) GetPairOfDirNames(SubmitInfo submit1, SubmitInfo submit2)
    {

        string dirname1 =
            new SubmitInfoProcessor().SubmitInfoToDirectoryPath(submit1, _submitConfig.RootDir,
                _submitConfig.SubmitTimeFormat);
        string dirname2 =
            new SubmitInfoProcessor().SubmitInfoToDirectoryPath(submit2, _submitConfig.RootDir,
                _submitConfig.SubmitTimeFormat);

        return new ValueTuple<string, string>(dirname1, dirname2);
    }

    private void ConnectProgressBarEventIfNeeded(int workToDo)
    {
        if (_progressBar is null)
        {
            return;
        }

        ComparisonProgressTracker progressTracker = new(_progressBar, workToDo);
        ProgressBarUpdate = progressTracker.IncreaseProgress;
    }

    private void OnProgressBarUpdate()
    {
        ProgressBarUpdate?.Invoke();
    }
}