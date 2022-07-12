using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

namespace KysectAcademyTask.FileComparison;

internal class FileComparer
{
    private readonly FileLoader _fileLoader;
    private readonly ComparisonAlgorithm.Metrics _metrics;

    public FileComparer(FileLoader fileLoader, ComparisonAlgorithm.Metrics metrics)
    {
        _fileLoader = fileLoader;
        _metrics = metrics;
    }

    public ComparisonResult Compare(string fileName1, string fileName2)
    {
        string fileContent1 = _fileLoader.GetFileContent(fileName1);
        string fileContent2 = _fileLoader.GetFileContent(fileName2);
        double similarityRate = new ComparisonAlgorithm().SetImplementation(_metrics)
            .GetSimilarityRate(fileContent1, fileContent2);
        return new ComparisonResult(fileName1, fileName2, similarityRate);
    }
}