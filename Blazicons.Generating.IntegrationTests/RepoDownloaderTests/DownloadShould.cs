using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blazicons.Generating.IntegrationTests;

[TestClass]
public class DownloadShould
{
    [DataTestMethod]
    [DataRow("https://github.com/Templarian/MaterialDesign-SVG/archive/refs/heads/master.zip")]
    public async Task DownloadAndExtractAllFilesGivenValidUrl(string url)
    {
        var downloader = new RepoDownloader(new Uri(url));

        var downloaded = await downloader.Download();

        Assert.IsTrue(downloaded.Count > 0, "No files were downloaded.");
        Assert.IsTrue(downloaded.All(x => File.Exists(x)), "Not all files exist locally.");

        downloader.CleanUp();
    }
}