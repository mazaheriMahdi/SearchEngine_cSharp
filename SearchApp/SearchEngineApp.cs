using SearchEngine.Doc;

namespace SearchApp;

static class SearchEngineApp
{
    public static void Main()
    {
        Console.Write("Enter the path of the folder you want to scan: ");
        var path = Console.ReadLine();
        IDocScanner iDocScanner = new DocScanner();
        Thread thread = new Thread(o => { iDocScanner.Scan(path); });
        thread.Start();
        
        
        ProgressBar progressBar = new ProgressBar(30, 1000, 0);
        while (!iDocScanner.IsFinished())
        {
            progressBar.Update(iDocScanner.GetProgress().Item2);
            Thread.Sleep(200);
        }

        Console.WriteLine();

        thread.Join();
        var res = iDocScanner.GetIndex();
        while (true)
        {
            Console.WriteLine("Enter the word you want to search: ");
            var word = Console.ReadLine().Trim().ToUpper();
            if (word.Equals("EXIT")) break;
            if (res.ContainsKey(word))
            {
                Console.WriteLine("The word is in these files: ");
                var index = 0;
                foreach (var doc in res[word])
                {
                    Console.WriteLine(index++ + "-->" + doc);
                }
            }
            else
            {
                Console.WriteLine("The word is not in any file");
            }
        }
    }
}