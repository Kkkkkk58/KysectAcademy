using KysectAcademyTask.DataAccess.EfStructures;
using KysectAcademyTask.DataAccess.Models.Entities;
using KysectAcademyTask.DataAccess.Repos.Interfaces;
using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;
using KysectAcademyTask.Submit.SubmitFilters;
using KysectAcademyTask.Utils;

namespace KysectAcademyTask.FileComparison;

internal class FileProcessor
{
    private readonly FileRequirements? _fileRequirements;
    private readonly DirectoryRequirements? _directoryRequirements;
    private readonly IReadOnlyCollection<ComparisonAlgorithm.Metrics> _metrics;
    private readonly IComparisonResultRepo _resultRepo;
    private readonly FileComparisonDbContext _context;

    public FileProcessor(FileRequirements? fileRequirements,
        DirectoryRequirements? directoryRequirements, IReadOnlyCollection<ComparisonAlgorithm.Metrics> metrics, IComparisonResultRepo resultRepo, FileComparisonDbContext context)
    {
        _fileRequirements = fileRequirements;
        _directoryRequirements = directoryRequirements;
        _metrics = metrics;
        _resultRepo = resultRepo;
        _context = context;
    }

    public ComparisonResultsTable CompareDirectories(string directory1, string directory2)
    {
        FileNamesGetter fileNamesGetter = new(_fileRequirements, _directoryRequirements);
        string[] fileNames1 = fileNamesGetter.GetFileNamesSatisfyingRequirements(directory1);
        string[] fileNames2 = fileNamesGetter.GetFileNamesSatisfyingRequirements(directory2);

        List<FileEntity> fileNamesToAdd = new();
        fileNamesToAdd.AddRange(fileNames1.Where(s => !_context.Files.Any(f => f.Path == s)).Select(f => new FileEntity{ Path = f}));
        fileNamesToAdd.AddRange(fileNames2.Where(s => !_context.Files.Any(f => f.Path == s)).Select(f => new FileEntity { Path = f }));
        _context.Files.AddRange(fileNamesToAdd);

        FileLoader loader = GetCombinedLoader(fileNames1, fileNames2);

        return GetComparisonResults(fileNames1, fileNames2, loader);
    }


    private FileLoader GetCombinedLoader(string[] fileNames1, string[] fileNames2)
    {
        FileLoader fileLoader1 = new(fileNames1);
        FileLoader fileLoader2 = new(fileNames2);
        FileLoader commonLoader = new FileLoadersCombiner().Combine(fileLoader1, fileLoader2);

        return commonLoader;
    }

    private ComparisonResultsTable GetComparisonResults(string[] fileNames1, string[] fileNames2, FileLoader loader)
    {
        ComparisonResultsTable comparisonResultsTable = new();
        foreach (ComparisonAlgorithm.Metrics metric in _metrics)
        {
            ComparisonResultsTable resultsUsingMetrics = PerformFilesComparison(fileNames1, fileNames2, loader, metric);
            comparisonResultsTable.AddTable(resultsUsingMetrics);
        }

        return comparisonResultsTable;
    }

    private ComparisonResultsTable PerformFilesComparison(string[] fileNames1, string[] fileNames2, FileLoader loader,
        ComparisonAlgorithm.Metrics metrics)
    {
        ComparisonResultsTable comparisonResultsTable = new();

        FileComparer fileComparer = new(loader, metrics);

        List<DataAccess.Models.Entities.ComparisonResult> notStoredResults = new();
        foreach (string fileName1 in fileNames1)
        {
            foreach (string fileName2 in fileNames2)
            {
                IQueryable<DataAccess.Models.Entities.ComparisonResult> query = _resultRepo.GetResultOfFilesWithMetricsQuery(fileName1, fileName2, metrics.ToString());

                ComparisonResult comparisonResult;
                if (query.Any())
                {
                    DataAccess.Models.Entities.ComparisonResult comparisonResultData = query.First();

                    comparisonResult = new ComparisonResult(comparisonResultData);
                }
                else
                {
                    comparisonResult = fileComparer.Compare(fileName1, fileName2);
                    notStoredResults.Add(comparisonResult.ToDataAccessModel());
                }

                comparisonResultsTable.AddComparisonResult(comparisonResult);
            }
        }

        _resultRepo.AddRange(notStoredResults);
        return comparisonResultsTable;
    }
}