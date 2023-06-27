using SearchEngine.InvertedIndex;

namespace SearchEngine.Test;

public class MapGeneratorTest
{
    
    [Fact]
    public void MapGeneratorTestFunc()
    {
        IMapGenerator invertedIndexGenerator = new InvertedIndexGenerator();
        invertedIndexGenerator.AddIndex("HELLO", "doc1.txt");
        invertedIndexGenerator.AddIndex("ALI", "doc1.txt");
        invertedIndexGenerator.AddIndex("CAR", "doc1.txt");
        invertedIndexGenerator.AddIndex("GAME", "doc1.txt");
        invertedIndexGenerator.AddIndex("HELLO", "doc2.txt");
        invertedIndexGenerator.AddIndex("ALI", "doc2.txt");
        invertedIndexGenerator.AddIndex("CAR", "doc2.txt");
        invertedIndexGenerator.AddIndex("GAME", "doc2.txt");
        invertedIndexGenerator.AddIndex("HELLO", "doc3.txt");

        var res1 = invertedIndexGenerator.GetIndex();

        LinkedList<string> hello = new LinkedList<string>();
        hello.AddLast("doc1.txt");
        hello.AddLast("doc2.txt");
        hello.AddLast("doc3.txt");

        LinkedList<string> ali = new LinkedList<string>();
        ali.AddLast("doc1.txt");
        ali.AddLast("doc2.txt");

        LinkedList<string> car = new LinkedList<string>();
        car.AddLast("doc1.txt");
        car.AddLast("doc2.txt");

        LinkedList<string> game = new LinkedList<string>();
        game.AddLast("doc1.txt");
        game.AddLast("doc2.txt");

        Dictionary<string, LinkedList<string>> expected = new Dictionary<string, LinkedList<string>>()
        {
            { "HELLO", hello },
            { "ALI", ali },
            { "CAR", car },
            { "GAME", game }
        };

        Assert.Equal(expected, res1);
    }
}