using KysectAcademyTask.FileComparison;
using KysectAcademyTask.Submit;
using KysectAcademyTask.Utils.ProgressTracking;

namespace KysectAcademyTask.SubmitComparison;

internal class SubmitComparisonProcessor
{
    private readonly IReadOnlyList<SubmitInfo> _submits;
    private readonly SubmitInfoProcessor _submitInfoProcessor;
    private readonly SubmitSuitabilityChecker _submitSuitabilityChecker;
    private readonly FileProcessorBuilder _fileProcessorBuilder;
    private IProgressBar _progressBar;

    private event Action ProgressBarUpdate;

    public SubmitComparisonProcessor(SubmitGetter submitGetter, SubmitInfoProcessor submitInfoProcessor,
        SubmitSuitabilityChecker submitSuitabilityChecker, FileProcessorBuilder fileProcessorBuilder)
    {
        _submits = submitGetter.GetSubmits();
        _submitInfoProcessor = submitInfoProcessor;
        _submitSuitabilityChecker = submitSuitabilityChecker;
        _fileProcessorBuilder = fileProcessorBuilder;
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
        FileProcessorBuilder completedFileBuilder = _fileProcessorBuilder
            .BuildDirectory1(pairToCompare.dirName1)
            .BuildDirectory2(pairToCompare.dirName2);
        FileProcessor fileProcessor = completedFileBuilder.GetFileProcessorInstance();
        ComparisonResultsTable curSubmitsTable = fileProcessor.GetComparisonResults();
        results.AddTable(curSubmitsTable);
    }

    private IReadOnlyCollection<(string dirName1, string dirName2)> GetSuitablePairsOfDirNames()
    {
        var pairs = new List<(string dirName1, string dirName2)>();

        for (int i = 0; i < _submits.Count - 1; ++i)
        {
            for (int j = i; j < _submits.Count; ++j)
            {
                if (!_submitSuitabilityChecker.AreSuitable(_submits[i], _submits[j]))
                    continue;

                (string dirName1, string dirName2) pair = GetPairOfDirNames(_submits[i], _submits[j]);
                pairs.Add(pair);
            }
        }

        return pairs;
    }

    private (string dirName1, string dirName2) GetPairOfDirNames(SubmitInfo submit1, SubmitInfo submit2)
    {
        string dirname1 =
            _submitInfoProcessor.SubmitInfoToDirectoryPath(submit1);
        string dirname2 =
            _submitInfoProcessor.SubmitInfoToDirectoryPath(submit2);

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