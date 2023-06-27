using System.Runtime.CompilerServices;

namespace SearchEngine.InvertedIndex;

public class InvertedIndexGenerator : IMapGenerator
{
    private Dictionary<string, LinkedList<string>> _invertedIndex;
    private LinkedList<Thread> _threads = new LinkedList<Thread>();

    public InvertedIndexGenerator()
    {
        _invertedIndex = new Dictionary<string, LinkedList<string>>();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void AddIndex(string word, string docName)
    {
        Thread thread = new Thread(() =>
        {
            if (_invertedIndex.TryGetValue(word, out var value))
            {
                value.AddLast(docName);
            }
            else
            {
                LinkedList<string> docList = new LinkedList<string>();
                docList.AddLast(docName);
                _invertedIndex.Add(word, docList);
            }
        });
        _threads.AddLast(thread);
        thread.Start();
    }

    public Dictionary<string, LinkedList<string>> GetIndex()
    {
        return _invertedIndex;
    }

    public LinkedList<Thread> GetThreads()
    {
        return _threads;
    }
}