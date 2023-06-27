namespace SearchApp;

public class ProgressBar
{
    private int _length;
    private int _maxValue;
    private int _minValue;
    private string _completedColor;
    private string _incompleteColor;
    private char[] _spinners;
    private int _spinnerIndex;
    private DateTime _startTime;

    public ProgressBar(int length, int maxValue, int minValue, string completedColor = "\u001b[42m", string incompleteColor = "\u001b[41m")
    {
        _length = length;
        _maxValue = maxValue;
        _minValue = minValue;
        _completedColor = completedColor;
        _incompleteColor = incompleteColor;
        _spinners = new char[] { '◐', '◓', '◑', '◒' };
        _spinnerIndex = 0;
        _startTime = DateTime.Now;
    }

    public void Update(int currentValue)
    {
        if (currentValue < _minValue || currentValue > _maxValue)
        {
            throw new ArgumentOutOfRangeException("currentValue", $"The value must be between {_minValue} and {_maxValue}.");
        }

        float percentage = (float)(currentValue - _minValue) / (_maxValue - _minValue);
        int completedWidth = (int)(_length * percentage);
        int incompleteWidth = _length - completedWidth;

        string progressBar = $"{_completedColor}{new string(' ', completedWidth)}\u001b[0m{_incompleteColor}{new string(' ', incompleteWidth)}\u001b[0m";
        char spinner = _spinners[_spinnerIndex];

        _spinnerIndex = (_spinnerIndex + 1) % _spinners.Length;

        TimeSpan elapsedTime = DateTime.Now - _startTime;
        string elapsedTimeString = elapsedTime.ToString(@"hh\:mm\:ss");

        Console.Write("\r[{0}] {1}% {2} Elapsed Time: {3}", progressBar, (int)(percentage * 100), spinner, elapsedTimeString);
    }
}