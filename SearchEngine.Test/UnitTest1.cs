using SearchEngine.FileUtility;

namespace SearchEngine.Test;
using SearchEngine;
public class SearchEngineTest
{
    [Fact]
    public void Test1()
    {
        IFileSplitter fileSplitter = new FileSplitter();
        List<string> res  =  fileSplitter.split("");

        List<string> expected = new List<string>() { "HELLO", "ALI" , "CAR" , "GAME" };
        
        Assert.Equal(expected , res);
    }
}