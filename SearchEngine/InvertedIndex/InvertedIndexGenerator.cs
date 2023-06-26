namespace SearchEngine.InvertedIndex;

public class InvertedIndexGenerator: IMapGenerator
{
    private static Dictionary<string , LinkedList<string>> _invertedIndex = new Dictionary<string, LinkedList<string>>();
    public void AddIndex(string word, string docName)
    {
        if (_invertedIndex.Keys.Contains(word))
        {
            _invertedIndex[word].AddLast(docName);
        }
        else
        {
            LinkedList<string> docList = new LinkedList<string>();
            docList.AddLast(docName);
            _invertedIndex.Add(word , docList);
        }
    }

    public Dictionary<string, LinkedList<string>> GetIndex()
    {
        return  _invertedIndex;
    }
    
}