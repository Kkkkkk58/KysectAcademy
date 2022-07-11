using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

namespace KysectAcademyTask.FileComparison;

internal class FileComparer
{
    private readonly FileLoader _fileLoader;
    private readonly string _fileName1;
    private readonly string _fileName2;
    private readonly ComparisonAlgorithm.Metrics _metrics;

    public FileComparer(FileLoader fileLoader, string fileName1, string fileName2, ComparisonAlgorithm.Metrics metrics)
    {
        _fileLoader = fileLoader;
        _fileName1 = fileName1;
        _fileName2 = fileName2;
        _metrics = metrics;
    }

    public ComparisonResult Compare()
    {
        string fileContent1 = _fileLoader.GetFileContent(_fileName1);
        string fileContent2 = _fileLoader.GetFileContent(_fileName2);
        double similarityRate = new ComparisonAlgorithm().SetImplementation(_metrics)
            .GetSimilarityRate(fileContent1, fileContent2);
        return new ComparisonResult(_fileName1, _fileName2, similarityRate);
    }
}