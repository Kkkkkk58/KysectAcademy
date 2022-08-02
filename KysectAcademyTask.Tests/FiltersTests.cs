using System.Text.Json;
using System.Text.Json.Serialization;
using KysectAcademyTask.AppSettings;
using KysectAcademyTask.DbInteraction.Configuration;
using KysectAcademyTask.Report;
using KysectAcademyTask.Submit.SubmitFilters;
using KysectAcademyTask.SubmitComparison;
using KysectAcademyTask.Tests.Base;
using KysectAcademyTask.Tests.TestModels;
using KysectAcademyTask.Utils.ProgressTracking.ProgressBar;
using Xunit;

namespace KysectAcademyTask.Tests;

public class FiltersTests : BaseTests
{

    [Fact]
    public void AnyFilter_IntersectionBetweenWhiteAndBlackLists_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var blackList = new List<string> { "Jane Doe", "John Doe", "Robin Robinson" };
            var whiteList = new List<string> { "John Johnson", "Sony Ericsson", "John Doe" };
            _ = new AuthorFilter(whiteList, blackList);
        });
    }

    [Fact]
    public void AuthorFilter_BlackList_ReportDoesNotContainAuthors()
    {
        var blackList = new List<string> { "Betty Padilla", "Andrew Gray", "Barbara Jones", "Alex Lane", "Stephen Brown" };
        var authorFilter = new AuthorFilter(null, blackList);
        var filters = new Filters
        {
            SubmitInfoRequirements = new SubmitInfoRequirements
            {
                AuthorFilter = authorFilter
            }
        };

        AppSettingsConfig config = GetConfig(filters);
        RunApplication(config);

        IReadOnlyCollection<Result> results = GetResults();
        Assert.False(DoesReportContain(results, blackList));
    }

    [Fact]
    public void AuthorFilter_WhiteList_ReportContainsAuthors()
    {
        var whiteList = new List<string> { "Betty Padilla", "Andrew Gray" };
        var authorFilter = new AuthorFilter(whiteList, null);
        var filters = new Filters
        {
            SubmitInfoRequirements = new SubmitInfoRequirements
            {
                AuthorFilter = authorFilter
            }
        };

        AppSettingsConfig config = GetConfig(filters);
        RunApplication(config);

        IReadOnlyCollection<Result> results = GetResults();
        Assert.True(DoesReportConsistOf(results, whiteList));
    }

    [Fact]
    public void DirectoryFilter_BlackList_ReportDoesNotContainFilesFromDirs()
    {
        var blackList = new List<string> { "M3236", "M3235", "20191118202349" };
        var directoryFilter = new DirectoryFilter(null, blackList);
        var filters = new Filters
        {
            DirectoryRequirements = new DirectoryRequirements
            {
                DirectoryFilter = directoryFilter
            }
        };

        AppSettingsConfig config = GetConfig(filters);
        RunApplication(config);

        IReadOnlyCollection<Result> results = GetResults();
        Assert.False(DoesReportContain(results, blackList));
    }

    [Fact]
    public void DirectoryFilter_WhiteList_ReportContainsFilesFromDirs()
    {
        var whiteList = new List<string> { "20191118202349", "M3234", "M3237", "4. INI файл" };
        var directoryFilter = new DirectoryFilter(whiteList, null);
        var filters = new Filters
        {
            DirectoryRequirements = new DirectoryRequirements
            {
                DirectoryFilter = directoryFilter
            }
        };

        AppSettingsConfig config = GetConfig(filters);
        RunApplication(config);

        IReadOnlyCollection<Result> results = GetResults();
        Assert.True(DoesReportConsistOf(results, whiteList));
    }


    [Fact]
    public void FileExtensionFilter_BlackList_ReportDoesNotContainFilesWithExtensions()
    {
        var blackList = new List<string> { ".java" };
        var fileExtensionFilter = new FileExtensionFilter(null, blackList);
        var filters = new Filters
        {
            FileRequirements = new FileRequirements
            {
                FileExtensionFilter = fileExtensionFilter
            }
        };

        AppSettingsConfig config = GetConfig(filters);
        RunApplication(config);

        IReadOnlyCollection<Result> results = GetResults();
        Assert.False(DoesReportContain(results, blackList));
    }

    [Fact]
    public void FileExtensionFilter_WhiteList_ReportContainsFilesWithExtensions()
    {
        var whiteList = new List<string> { ".cs" };
        var fileExtensionFilter = new FileExtensionFilter(whiteList, null);
        var filters = new Filters
        {
            FileRequirements = new FileRequirements
            {
                FileExtensionFilter = fileExtensionFilter
            }
        };

        AppSettingsConfig config = GetConfig(filters);
        RunApplication(config);

        IReadOnlyCollection<Result> results = GetResults();
        Assert.True(DoesReportConsistOf(results, whiteList));
    }

    [Fact]
    public void FileNameFilter_BlackList_ReportDoesNotContainFiles()
    {
        var blackList = new List<string> { "main.py", "Program.cs" };
        var fileNameFilter = new FileNameFilter(null, blackList);
        var filters = new Filters
        {
            FileRequirements = new FileRequirements
            {
                FileNameFilter = fileNameFilter
            }
        };

        AppSettingsConfig config = GetConfig(filters);
        RunApplication(config);

        IReadOnlyCollection<Result> results = GetResults();
        Assert.False(DoesReportContain(results, blackList));
    }

    [Fact]
    public void FileNameFilter_WhiteList_ReportContainsFiles()
    {
        var whiteList = new List<string> { "main.py", "Program.cs" };
        var fileNameFilter = new FileNameFilter(whiteList, null);
        var filters = new Filters
        {
            FileRequirements = new FileRequirements
            {
                FileNameFilter = fileNameFilter
            }
        };

        AppSettingsConfig config = GetConfig(filters);
        RunApplication(config);

        IReadOnlyCollection<Result> results = GetResults();
        Assert.True(DoesReportConsistOf(results, whiteList));
    }

    [Fact]
    public void GroupFilter_BlackList_ReportDoesNotContainSubmitsFromGroups()
    {
        var blackList = new List<string> { "M3235", "M3236" };
        var groupFilter = new GroupFilter(null, blackList);
        var filters = new Filters
        {
            SubmitInfoRequirements = new SubmitInfoRequirements
            {
                GroupFilter = groupFilter
            }
        };

        AppSettingsConfig config = GetConfig(filters);
        RunApplication(config);

        IReadOnlyCollection<Result> results = GetResults();
        Assert.False(DoesReportContain(results, blackList));
    }

    [Fact]
    public void GroupFilter_WhiteList_ReportContainsSubmitsFromGroups()
    {
        var whiteList = new List<string> { "M3235", "M3236" };
        var groupFilter = new GroupFilter(whiteList, null);
        var filters = new Filters
        {
            SubmitInfoRequirements = new SubmitInfoRequirements
            {
                GroupFilter = groupFilter
            }
        };

        AppSettingsConfig config = GetConfig(filters);
        RunApplication(config);

        IReadOnlyCollection<Result> results = GetResults();
        Assert.True(DoesReportConsistOf(results, whiteList));
    }

    [Fact]
    public void HomeWorkFilter_BlackList_ReportDoesNotContainSubmitsWithHomeWorks()
    {
        var blackList = new List<string> { "4. INI файл", "1. Ввод-вывод" };
        var homeWorkFilter = new HomeworkFilter(null, blackList);
        var filters = new Filters
        {
            SubmitInfoRequirements = new SubmitInfoRequirements
            {
                HomeworkFilter = homeWorkFilter
            }
        };

        AppSettingsConfig config = GetConfig(filters);
        RunApplication(config);

        IReadOnlyCollection<Result> results = GetResults();
        Assert.False(DoesReportContain(results, blackList));
    }

    [Fact]
    public void HomeWorkFilter_WhiteList_ReportContainsSubmitsWithHomeWorks()
    {
        var whiteList = new List<string> { "6. Знакомство с паттернами" };
        var homeWorkFilter = new HomeworkFilter(whiteList, null);
        var filters = new Filters
        {
            SubmitInfoRequirements = new SubmitInfoRequirements
            {
                HomeworkFilter = homeWorkFilter
            }
        };

        AppSettingsConfig config = GetConfig(filters);
        RunApplication(config);

        IReadOnlyCollection<Result> results = GetResults();
        Assert.True(DoesReportConsistOf(results, whiteList));
    }

    [Fact]
    public void DateFilter_BlackList_ReportDoesNotContainSubmitsWithDates()
    {
        var blackList = new List<DateTime>
        {
            new(2019, 11, 18, 20, 23, 49),  // Andrew Gray - 4. INI файл
            new(2019, 12, 2, 17, 56, 12)    // Barbara Jones - 4. INI файл
        };
        var submitDateFilter = new SubmitDateFilter(null, blackList);
        var filters = new Filters
        {
            SubmitInfoRequirements = new SubmitInfoRequirements
            {
                SubmitDateFilter = submitDateFilter
            }
        };

        AppSettingsConfig config = GetConfig(filters);
        RunApplication(config);

        IReadOnlyCollection<Result> results = GetResults();
        IReadOnlyCollection<string> datesToFormat = blackList
            .Select(d => d.ToString(DefaultDateTimeFormat))
            .ToList();
        Assert.False(DoesReportContain(results, datesToFormat));
    }

    [Fact]
    public void DateFilter_WhiteList_ReportContainsSubmitsWithDates()
    {
        var whiteList = new List<DateTime>
        {
            new(2019, 11, 18, 20, 23, 49),  // Andrew Gray - 4. INI файл
            new(2019, 12, 2, 17, 56, 12)    // Barbara Jones - 4. INI файл
        };
        var submitDateFilter = new SubmitDateFilter(whiteList, null);
        var filters = new Filters
        {
            SubmitInfoRequirements = new SubmitInfoRequirements
            {
                SubmitDateFilter = submitDateFilter
            }
        };

        AppSettingsConfig config = GetConfig(filters);
        RunApplication(config);

        IReadOnlyCollection<Result> results = GetResults();
        IReadOnlyCollection<string> datesToFormat = whiteList
            .Select(d => d.ToString(DefaultDateTimeFormat))
            .ToList();
        Assert.True(DoesReportConsistOf(results, datesToFormat));
    }

    private AppSettingsConfig GetConfig(Filters filters)
    {
        string rootPath = GetRootPath(@"FilesForTests\ReportTests\RootDirectory\");
        string resultPath = GetResultPath(rootPath, ReportType.Json);

        return new AppSettingsConfig
        {
            DbConfig = new DbConfig(null),
            ReportConfig = new ReportConfig(ReportType.Json, resultPath),
            SubmitConfig = new SubmitConfig(rootPath, filters, DefaultMetrics, DefaultDateTimeFormat, DefaultDirDepth),
            ProgressBarConfig = new ProgressBarConfig(false)
        };
    }

    private IReadOnlyCollection<Result> GetResults()
    {
        string rootPath = GetRootPath(@"FilesForTests\ReportTests\RootDirectory\");
        string resultPath = GetResultPath(rootPath, ReportType.Json);

        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
        string jsonString = File.ReadAllText(resultPath);
        Result[] results = JsonSerializer.Deserialize<Result[]>(jsonString, options);

        return results;
    }

    private bool DoesReportConsistOf(IEnumerable<Result> results, IReadOnlyCollection<string> whiteList)
    {
        return results
            .All(r =>
                whiteList.Any(el => r.FileName1.Contains(el) || r.FileName2.Contains(el)));
    }

    private bool DoesReportContain(IEnumerable<Result> results, IReadOnlyCollection<string> blackList)
    {
        return results
            .Any(r =>
                blackList.Any(el => r.FileName1.Contains(el) || r.FileName2.Contains(el)));
    }
}