namespace SearchEngine.Doc;

public interface IDocScanner
{
    void Scan(string folderPath);
    Dictionary<string, LinkedList<string>> GetIndex();
    Tuple<int , int > GetProgress();
    Boolean IsFinished();
}