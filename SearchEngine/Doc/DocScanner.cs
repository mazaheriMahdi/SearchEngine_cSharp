using SearchEngine.FileUtility;
using SearchEngine.InvertedIndex;

namespace SearchEngine.Doc
{
    public class DocScanner : IDocScanner
    {
        private readonly IFileSplitter _fileSplitter;
        private readonly IMapGenerator _mapGenerator;
        private int _numberOfFiles = -1;
        private int _numberOfFilesScanned = 0 ;
        public Boolean IsFinished => _numberOfFiles == _numberOfFilesScanned;
        public DocScanner()
        {
            _mapGenerator = new InvertedIndexGenerator();
            _fileSplitter = new FileSplitter();
        }

        public void Scan(string folderPath)
        {
            var files = Directory.GetFiles(folderPath).ToList();

            _numberOfFiles = files.Count;
        
            files.ForEach(
                file =>
                {
                    
                    var words = _fileSplitter.Split(file);
                    words.ForEach(word => _mapGenerator.AddIndex(word, file));
                    _mapGenerator.GetThreads().ToList().ForEach(thread => thread.Join());
                    // _mapGenerator.GetThreads().Clear();
                    _numberOfFilesScanned++;
                }
            );
        }

        public Dictionary<string, LinkedList<string>> GetIndex()
        {
            return _mapGenerator.GetIndex();
        }


        public Tuple<int, int> GetProgress()
        {
            return Tuple.Create(_numberOfFiles, _numberOfFilesScanned);
        }

        bool IDocScanner.IsFinished()
        {
            return IsFinished;
        }
    }
}

