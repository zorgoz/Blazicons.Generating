using Blazicons.Generating;

namespace Tests;

[TestClass]
public class DownloaderTest
{
    [TestMethod]
    [DataRow("https://github.com/Templarian/MaterialDesign-SVG/archive/refs/heads/master.zip")]
    public async Task TestMethod(string url)
    {
        var downloader = new RepoDownloader(new Uri(url));

        var downloaded = await downloader!.Download();

        Assert.IsTrue(downloaded.Count > 0, "There are files downloaded");
        Assert.IsTrue(downloaded.All(x => File.Exists(x)), "All files exist");

        downloader?.CleanUp();
    }
}