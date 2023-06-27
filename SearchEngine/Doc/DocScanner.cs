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

        public async Task Scan(string folderPath)
        {
            var files = Directory.GetFiles(folderPath).ToList();

            _numberOfFiles = files.Count;

            object lockObject = new object();

            List<Task> tasks = new List<Task>();
            files.ForEach(file=>
            {
                Task task = new Task(() =>
                {
                    var words = _fileSplitter.Split(file);
                    words.ForEach(word=>_mapGenerator.AddIndex(word, file));
                    Interlocked.Increment(ref _numberOfFilesScanned);
                });
                tasks.Add(task);
                task.Start();
            });
            await Task.WhenAll(tasks);
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

