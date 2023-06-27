namespace SearchEngine.Doc;

public interface IDocScanner
{
    Task Scan(string folderPath);
    Dictionary<string, LinkedList<string>> GetIndex();
    Tuple<int , int > GetProgress();
    Boolean IsFinished();
}