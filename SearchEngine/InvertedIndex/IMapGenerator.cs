namespace SearchEngine.InvertedIndex;

public interface IMapGenerator
{
    void AddIndex(string word, string docName);
    Dictionary<string, LinkedList<string>> GetIndex();
    LinkedList<Thread> GetThreads();

}