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
    private readonly string _relativeRootPath = @$"FilesForTests{Path.DirectorySeparatorChar}ReportTests{Path.DirectorySeparatorChar}RootDirectory";

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
        InitPaths(_relativeRootPath, ReportType.Json);

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

        IReadOnlyCollection<TestSubmitComparisonResult> results = GetResults();

        bool condition = results.Any(result => blackList.Any(name =>
            result.SubmitInfo1.AuthorName.Equals(name, StringComparison.OrdinalIgnoreCase)
            || result.SubmitInfo2.AuthorName.Equals(name, StringComparison.OrdinalIgnoreCase)));

        Assert.False(condition);
    }

    [Fact]
    public void AuthorFilter_WhiteList_ReportContainsAuthors()
    {
        InitPaths(_relativeRootPath, ReportType.Json);

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

        IReadOnlyCollection<TestSubmitComparisonResult> results = GetResults();

        bool condition = results.All(result => whiteList.Any(name =>
            result.SubmitInfo1.AuthorName.Equals(name, StringComparison.OrdinalIgnoreCase)
            || result.SubmitInfo2.AuthorName.Equals(name, StringComparison.OrdinalIgnoreCase)));

        Assert.True(condition);
    }

    //[Fact]
    //public void DirectoryFilter_BlackList_ReportDoesNotContainFilesFromDirs()
    //{
    //    InitPaths(_relativeRootPath, ReportType.Json);

    //    var blackList = new List<string> { "M3236", "M3235", "20191118202349" };
    //    var directoryFilter = new DirectoryFilter(null, blackList);
    //    var filters = new Filters
    //    {
    //        DirectoryRequirements = new DirectoryRequirements
    //        {
    //            DirectoryFilter = directoryFilter
    //        }
    //    };

    //    AppSettingsConfig config = GetConfig(filters);
    //    RunApplication(config);

    //    IReadOnlyCollection<TestSubmitComparisonResult> results = GetResults();
    //    Assert.False(DoesReportContain(results, blackList));
        
    //}

    //[Fact]
    //public void DirectoryFilter_WhiteList_ReportContainsFilesFromDirs()
    //{
    //    InitPaths(_relativeRootPath, ReportType.Json);

    //    var whiteList = new List<string> { "20191118202349", "M3234", "M3237", "4. INI файл" };
    //    var directoryFilter = new DirectoryFilter(whiteList, null);
    //    var filters = new Filters
    //    {
    //        DirectoryRequirements = new DirectoryRequirements
    //        {
    //            DirectoryFilter = directoryFilter
    //        }
    //    };

    //    AppSettingsConfig config = GetConfig(filters);
    //    RunApplication(config);

    //    IReadOnlyCollection<TestSubmitComparisonResult> results = GetResults();
    //    Assert.True(DoesReportConsistOf(results, whiteList));

    //}


    //[Fact]
    //public void FileExtensionFilter_BlackList_ReportDoesNotContainFilesWithExtensions()
    //{
    //    InitPaths(_relativeRootPath, ReportType.Json);

    //    var blackList = new List<string> { ".java" };
    //    var fileExtensionFilter = new FileExtensionFilter(null, blackList);
    //    var filters = new Filters
    //    {
    //        FileRequirements = new FileRequirements
    //        {
    //            FileExtensionFilter = fileExtensionFilter
    //        }
    //    };

    //    AppSettingsConfig config = GetConfig(filters);
    //    RunApplication(config);

    //    IReadOnlyCollection<TestSubmitComparisonResult> results = GetResults();
    //    Assert.False(DoesReportContain(results, blackList));

    //}

    //[Fact]
    //public void FileExtensionFilter_WhiteList_ReportContainsFilesWithExtensions()
    //{
    //    InitPaths(_relativeRootPath, ReportType.Json);

    //    var whiteList = new List<string> { ".cs" };
    //    var fileExtensionFilter = new FileExtensionFilter(whiteList, null);
    //    var filters = new Filters
    //    {
    //        FileRequirements = new FileRequirements
    //        {
    //            FileExtensionFilter = fileExtensionFilter
    //        }
    //    };

    //    AppSettingsConfig config = GetConfig(filters);
    //    RunApplication(config);

    //    IReadOnlyCollection<TestSubmitComparisonResult> results = GetResults();
    //    Assert.True(DoesReportConsistOf(results, whiteList));

    //}

    //[Fact]
    //public void FileNameFilter_BlackList_ReportDoesNotContainFiles()
    //{
    //    InitPaths(_relativeRootPath, ReportType.Json);

    //    var blackList = new List<string> { "main.py", "Program.cs" };
    //    var fileNameFilter = new FileNameFilter(null, blackList);
    //    var filters = new Filters
    //    {
    //        FileRequirements = new FileRequirements
    //        {
    //            FileNameFilter = fileNameFilter
    //        }
    //    };

    //    AppSettingsConfig config = GetConfig(filters);
    //    RunApplication(config);

    //    IReadOnlyCollection<TestSubmitComparisonResult> results = GetResults();
    //    Assert.False(DoesReportContain(results, blackList));

    //}

    //[Fact]
    //public void FileNameFilter_WhiteList_ReportContainsFiles()
    //{
    //    InitPaths(_relativeRootPath, ReportType.Json);

    //    var whiteList = new List<string> { "main.py", "Program.cs" };
    //    var fileNameFilter = new FileNameFilter(whiteList, null);
    //    var filters = new Filters
    //    {
    //        FileRequirements = new FileRequirements
    //        {
    //            FileNameFilter = fileNameFilter
    //        }
    //    };

    //    AppSettingsConfig config = GetConfig(filters);
    //    RunApplication(config);

    //    IReadOnlyCollection<TestSubmitComparisonResult> results = GetResults();
    //    Assert.True(DoesReportConsistOf(results, whiteList));

    //}

    [Fact]
    public void GroupFilter_BlackList_ReportDoesNotContainSubmitsFromGroups()
    {
        InitPaths(_relativeRootPath, ReportType.Json);

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

        IReadOnlyCollection<TestSubmitComparisonResult> results = GetResults();

        bool condition = results.Any(result => blackList.Any(group =>
            result.SubmitInfo1.GroupName.Equals(group, StringComparison.OrdinalIgnoreCase)
            || result.SubmitInfo2.GroupName.Equals(group, StringComparison.OrdinalIgnoreCase)));

        Assert.False(condition);

    }

    [Fact]
    public void GroupFilter_WhiteList_ReportContainsSubmitsFromGroups()
    {
        InitPaths(_relativeRootPath, ReportType.Json);

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

        IReadOnlyCollection<TestSubmitComparisonResult> results = GetResults();

        bool condition = results.All(result => whiteList.Any(group =>
            result.SubmitInfo1.GroupName.Equals(group, StringComparison.OrdinalIgnoreCase)
            || result.SubmitInfo2.GroupName.Equals(group, StringComparison.OrdinalIgnoreCase)));

        Assert.True(condition);

    }

    [Fact]
    public void HomeWorkFilter_BlackList_ReportDoesNotContainSubmitsWithHomeWorks()
    {
        InitPaths(_relativeRootPath, ReportType.Json);

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

        IReadOnlyCollection<TestSubmitComparisonResult> results = GetResults();

        bool condition = results.Any(result => blackList.Any(homeWork =>
            result.SubmitInfo1.AuthorName.Equals(homeWork, StringComparison.OrdinalIgnoreCase)
            || result.SubmitInfo2.AuthorName.Equals(homeWork, StringComparison.OrdinalIgnoreCase)));

        Assert.False(condition);

    }

    [Fact]
    public void HomeWorkFilter_WhiteList_ReportContainsSubmitsWithHomeWorks()
    {
        InitPaths(_relativeRootPath, ReportType.Json);

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

        IReadOnlyCollection<TestSubmitComparisonResult> results = GetResults();

        bool condition = results.All(result => whiteList.Any(homeWork =>
            result.SubmitInfo1.HomeworkName.Equals(homeWork, StringComparison.OrdinalIgnoreCase)
            || result.SubmitInfo2.HomeworkName.Equals(homeWork, StringComparison.OrdinalIgnoreCase)));

        Assert.True(condition);

    }

    [Fact]
    public void DateFilter_BlackList_ReportDoesNotContainSubmitsWithDates()
    {
        InitPaths(_relativeRootPath, ReportType.Json);

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

        IReadOnlyCollection<TestSubmitComparisonResult> results = GetResults();

        bool condition = results.Any(result => blackList.Any(date =>
            result.SubmitInfo1.SubmitDate.Equals(date)
            || result.SubmitInfo2.SubmitDate.Equals(date)));

        Assert.False(condition);

    }

    [Fact]
    public void DateFilter_WhiteList_ReportContainsSubmitsWithDates()
    {
        InitPaths(_relativeRootPath, ReportType.Json);

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

        IReadOnlyCollection<TestSubmitComparisonResult> results = GetResults();

        bool condition = results.All(result => whiteList.Any(date =>
            result.SubmitInfo1.SubmitDate.Equals(date)
            || result.SubmitInfo2.SubmitDate.Equals(date)));

        Assert.True(condition);

    }

    private AppSettingsConfig GetConfig(Filters filters)
    {
        return new AppSettingsConfig
        {
            DbConfig = new DbConfig(null, null),
            ReportConfig = new ReportConfig(ReportType.Json, ResultPath),
            SubmitConfig = new SubmitConfig(RootPath, filters, DefaultMetrics, DefaultDateTimeFormat, DefaultDirDepth),
            ProgressBarConfig = new ProgressBarConfig(false)
        };
    }

    //private bool DoesReportConsistOf(IEnumerable<TestSubmitComparisonResult> results, IReadOnlyCollection<string> whiteList)
    //{
    //    return results
    //        .All(r =>
    //            whiteList.Any(el => r.FileName1.Contains(el) || r.FileName2.Contains(el)));
    //}

    //private bool DoesReportContain(IEnumerable<TestSubmitComparisonResult> results, IReadOnlyCollection<string> blackList)
    //{
    //    return results
    //        .Any(r =>
    //            blackList.Any(el => r.FileName1.Contains(el) || r.FileName2.Contains(el)));
    //}
}