using System.IO.Compression;
using System.Text.RegularExpressions;

namespace Blazicons.Generating;

public class RepoDownloader
{
    private static readonly HttpClient client = new();

    public RepoDownloader(Uri address)
    {
        Address = address;
        var parts = Address.AbsolutePath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
        AuthorName = parts[0];
        RepoName = parts[1];
        BranchName = Path.GetFileNameWithoutExtension(parts.Last());
    }

    public Uri Address { get; }

    public string? RootFolder { get; set; }

    public string ExtractedFolder => Path.Combine(RootFolder, "files");

    public void CleanUp()
    {
        Directory.Delete(RootFolder, true);
    }

    public string AuthorName { get; }

    public string RepoName { get; }

    public string BranchName { get; }

    public async Task Download(string entriesFilter = @"\.svg")
    {
        var fileName = Path.GetFileName(Address.AbsolutePath);

        var bytes = await client.GetByteArrayAsync(Address).ConfigureAwait(false);

        if (string.IsNullOrEmpty(RootFolder))
        {
            RootFolder = Path.Combine(Path.GetTempPath(), $"{RepoName}-{Guid.NewGuid().ToString("N")}");
            Directory.CreateDirectory(RootFolder);
        }

        var zipFileName = Path.Combine(RootFolder, fileName);
        File.WriteAllBytes(zipFileName, bytes);

        using var archive = ZipFile.OpenRead(zipFileName);
        var entries = archive.Entries.Where(x =>
            !string.IsNullOrEmpty(x.Name)
            && Regex.IsMatch(x.FullName.Substring(RepoName.Length + 1 + BranchName.Length +1), entriesFilter));

        foreach (var entry in entries)
        {
            var extractedName = Path.Combine(ExtractedFolder, entry.FullName.Replace("/", "\\"));
            if (extractedName.Contains(".."))
            {
                throw new InvalidOperationException($"Invalid file name '{extractedName}'");
            }

            var dir = Path.GetDirectoryName(extractedName);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            entry.ExtractToFile(extractedName);
        }
    }
}

