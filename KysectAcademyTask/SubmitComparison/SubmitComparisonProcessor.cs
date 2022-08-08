using KysectAcademyTask.ComparisonResult;
using KysectAcademyTask.ComparisonResult.Transformer;
using KysectAcademyTask.FileComparison;
using KysectAcademyTask.Submit;
using KysectAcademyTask.Utils.ProgressTracking.ProgressBar.Base;
using KysectAcademyTask.Utils.ProgressTracking.ProgressTracker;

namespace KysectAcademyTask.SubmitComparison;

public class SubmitComparisonProcessor
{
    private readonly IReadOnlyList<SubmitInfo> _submits;
    private readonly SubmitInfoProcessor _submitInfoProcessor;
    private readonly SubmitSuitabilityChecker _submitSuitabilityChecker;
    private readonly FileProcessor _fileProcessor;
    private readonly ComparisonResultsTable<SubmitComparisonResult> _cache;

    private IProgressBar _progressBar;

    private event Action ProgressBarUpdate;

    public SubmitComparisonProcessor(IReadOnlyList<SubmitInfo> submits, SubmitInfoProcessor submitInfoProcessor,
        SubmitSuitabilityChecker submitSuitabilityChecker, FileProcessor fileProcessor, ComparisonResultsTable<SubmitComparisonResult> cache)
    {
        _submits = submits;
        _submitInfoProcessor = submitInfoProcessor;
        _submitSuitabilityChecker = submitSuitabilityChecker;
        _fileProcessor = fileProcessor;
        _cache = cache;
    }

    public void SetProgressBar(IProgressBar progressBar)
    {
        _progressBar = progressBar;
    }

    public ComparisonResultsTable<SubmitComparisonResult> GetComparisonResults()
    {
        IReadOnlyCollection<(SubmitInfo submit1, SubmitInfo submit2)> suitablePairs = GetSuitablePairsOfSubmits();
        ConnectProgressBarEventIfNeeded(suitablePairs.Count);

        foreach ((SubmitInfo submit1, SubmitInfo submit2) pair in suitablePairs)
        {
            AddComparisonToTable(_cache, pair);
            OnProgressBarUpdate();
        }

        return _cache;
    }

    private void AddComparisonToTable(ComparisonResultsTable<SubmitComparisonResult> results, (SubmitInfo submit1, SubmitInfo submit2) pairToCompare)
    {
        (string dirName1, string dirName2) = GetPairOfDirNames(pairToCompare);

        ComparisonResultsTable<FileComparisonResult> curSubmitsFilesComparison =
            _fileProcessor.CompareDirectories(dirName1, dirName2);

        SubmitComparisonResult curSubmitsComparison = new FilesToSubmitComparisonTransformer(pairToCompare.submit1, pairToCompare.submit2)
            .Transform(curSubmitsFilesComparison);

        results.AddComparisonResult(curSubmitsComparison);
    }

    private IReadOnlyCollection<(SubmitInfo submit1, SubmitInfo submit2)> GetSuitablePairsOfSubmits()
    {
        var pairs = new List<(SubmitInfo submit1, SubmitInfo submit2)>();

        for (int i = 0; i < _submits.Count - 1; ++i)
        {
            for (int j = i; j < _submits.Count; ++j)
            {
                if (!AreSuitable(_submits[i], _submits[j]))
                    continue;

                (SubmitInfo submit1, SubmitInfo submit2) pair = (_submits[i], _submits[j]);
                pairs.Add(pair);
            }
        }

        return pairs;
    }

    private bool AreSuitable(SubmitInfo submit1, SubmitInfo submit2)
    {
        return _submitSuitabilityChecker.AreSuitable(submit1, submit2);
    }

    private (string dirName1, string dirName2) GetPairOfDirNames((SubmitInfo submit1, SubmitInfo submit2) submitPair)
    {
        string dirname1 =
            _submitInfoProcessor.SubmitInfoToDirectoryPath(submitPair.submit1);
        string dirname2 =
            _submitInfoProcessor.SubmitInfoToDirectoryPath(submitPair.submit2);

        return new ValueTuple<string, string>(dirname1, dirname2);
    }

    private void ConnectProgressBarEventIfNeeded(int workToDo)
    {
        if (_progressBar is null)
            return;

        ComparisonProgressTracker progressTracker = new(_progressBar, workToDo);
        ProgressBarUpdate = progressTracker.IncreaseProgress;
    }

    private void OnProgressBarUpdate()
    {
        ProgressBarUpdate?.Invoke();
    }
}