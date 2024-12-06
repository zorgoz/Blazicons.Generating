using System.IO.Compression;
using System.Text.RegularExpressions;

namespace Blazicons.Generating;

/// <summary>
/// Provides functionality to download and extract a zipped repository.
/// </summary>
public class RepoDownloader
{
    private static readonly HttpClient client = new();
    private static readonly char[] separator = ['/'];

    /// <summary>
    /// Initializes a new instance of the <see cref="RepoDownloader"/> class.
    /// </summary>
    /// <param name="address">The URL of the .zip file containing the repository contents.</param>
    public RepoDownloader(Uri address)
    {
        Address = address;
        var parts = Address.AbsolutePath.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        AuthorName = parts[0];
        RepoName = parts[1];
        BranchName = Path.GetFileNameWithoutExtension(parts[parts.Length - 1]);
    }

    /// <summary>
    /// Gets the URL of the zip file that contains the repository contents.
    /// </summary>
    public Uri Address { get; }

    /// <summary>
    /// Gets or sets the target folder where the repository will be extracted.
    /// </summary>
    public string? RootFolder { get; set; }

    /// <summary>
    /// Gets the full path to the folder where the repository was extracted. This is a folder named 'files' inside the <see cref="RootFolder"/>.
    /// </summary>
    public string ExtractedFolder => Path.Combine(RootFolder, "files");

    /// <summary>
    /// Deletes the <see cref="ExtractedFolder"/>.
    /// </summary>
    public void CleanUp()
    {
        Directory.Delete(RootFolder, true);
    }

    /// <summary>
    /// Gets the name of the target repository author.
    /// </summary>
    public string AuthorName { get; }

    /// <summary>
    /// Gets the name of the target repository.
    /// </summary>
    public string RepoName { get; }

    /// <summary>
    /// Gets the name of the target repository branch.
    /// </summary>
    public string BranchName { get; }

    /// <summary>
    /// Downloads and extracts the repository contents
    /// </summary>
    /// <param name="entriesFilter">An optional regular expression filter. Only files matching the pattern in the .zip archive will be extracted.</param>
    /// <returns>A list of the file names that were extracted.</returns>
    public async Task<IReadOnlyList<string>> Download(string entriesFilter = @"\.svg")
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

        var downloadedFiles = new List<string>();

        foreach (var entry in entries)
        {
            var extractedName = Path.Combine(ExtractedFolder, entry.FullName.Replace('/', Path.DirectorySeparatorChar));
            if (extractedName.Contains(".."))
            {
                throw new InvalidOperationException($"Invalid file name '{extractedName}'");
            }

            var dir = Path.GetDirectoryName(extractedName);

            Directory.CreateDirectory(dir);

            downloadedFiles.Add(extractedName);

            entry.ExtractToFile(extractedName);
        }

        return downloadedFiles;
    }
}

