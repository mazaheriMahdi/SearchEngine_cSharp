using System.Text.RegularExpressions;
using System.Windows.Markup;

namespace SearchEngine.FileUtility;

public class FileSplitter : IFileSplitter
{
    string pattern = "\\s|'|\\.|!|\"|\"|,|:|;|\\?|\\(|\\)|\\[|\\]|\\{|\\}| |\\\\* |\\*|/|\\\\";
    public List<string> Split(string path)
    {
        var data = File.ReadAllText(path);
        var regex = new Regex(pattern);
        var result = regex.Split(data);
        return result.Where(word => 
            !string.IsNullOrEmpty(word.Trim())
            &&
            !StopWords.STOP_WORDS.Contains(word.Trim().ToLower())
            && word.Length > 1
            )
            .Select(word=>word.Trim().ToUpper())
            .ToList();


    }
}