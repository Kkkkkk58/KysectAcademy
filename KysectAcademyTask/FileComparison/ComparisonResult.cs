using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

namespace KysectAcademyTask.FileComparison;

public readonly struct ComparisonResult
{
    public string FileName1 { get; }
    public string FileName2 { get; }
    public ComparisonAlgorithm.Metrics Metrics { get; }
    public double SimilarityRate { get; }

    public ComparisonResult(string fileName1, string fileName2, ComparisonAlgorithm.Metrics metrics,
        double similarityRate)
    {
        FileName1 = fileName1;
        FileName2 = fileName2;
        Metrics = metrics;
        SimilarityRate = similarityRate;
    }

    public ComparisonResult(DataAccess.Models.Entities.ComparisonResult dataAccessModel)
    {
        FileName1 = dataAccessModel.File1Navigation.Path;
        FileName2 = dataAccessModel.File2Navigation.Path;
        Metrics = Enum.Parse<ComparisonAlgorithm.Metrics>(dataAccessModel.Metrics);
        SimilarityRate = dataAccessModel.SimilarityRate;
    }

    public override string ToString()
    {
        return $"|{FileName1}\n|{FileName2}\n|{{using {Metrics} metrics}}\n=> {SimilarityRate:0.##}";
    }
}