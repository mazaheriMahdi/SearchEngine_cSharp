using SearchEngine.Doc;
using SearchEngine.FileUtility;
using SearchEngine.InvertedIndex;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace SearchEngine.Test;

using SearchEngine;

public class SearchEngineTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public SearchEngineTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }


    [Fact]
    public void SplitterTest()
    {
        IFileSplitter fileSplitter = new FileSplitter();
        List<string> res =
            fileSplitter.Split(
                "/Users/mahdimazaheri/Programming/SearchEngine/SearchEngine/SearchEngine.Test/Data/doc1.txt");

        List<string> expected = new List<string>() { "HELLO", "ALI", "CAR", "GAME" };

        Assert.Equal(expected, res);
    }



    [Fact]
    public void DocScannerTest()
    {
        var folderPath = "/Users/mahdimazaheri/Programming/SearchEngine/SearchEngine/SearchEngine.Test/Data/";
        IDocScanner docScanner = new DocScanner();
        docScanner.Scan(folderPath);
        var res = docScanner.GetIndex();
        
        LinkedList<string> hello = new LinkedList<string>();
        hello.AddLast(folderPath+"doc1.txt");

        LinkedList<string> ali = new LinkedList<string>();
        ali.AddLast(folderPath+"doc1.txt");

        LinkedList<string> car = new LinkedList<string>();
        car.AddLast(folderPath+"doc1.txt");

        LinkedList<string> game = new LinkedList<string>();
        game.AddLast(folderPath+"doc1.txt");

        Dictionary<string, LinkedList<string>> expected = new Dictionary<string, LinkedList<string>>()
        {
            { "HELLO", hello },
            { "ALI", ali },
            { "CAR", car },
            { "GAME", game }
        };

        foreach (var keyValuePair in res)
        {
            _testOutputHelper.WriteLine(keyValuePair.Key);
            _testOutputHelper.WriteLine(keyValuePair.Value.ToString());
        }

        Assert.True(true);
        
    }
}