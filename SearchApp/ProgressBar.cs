namespace SearchApp;

public class ProgressBar
{
    private int _length;
    private int _maxValue;
    private int _minValue;
    private char _completedChar;
    private char _incompleteChar;
    private char[] _spinners;
    private int _spinnerIndex;

    public ProgressBar(int length, int maxValue, int minValue, char completedChar = '#', char incompleteChar = '-')
    {
        _length = length;
        _maxValue = maxValue;
        _minValue = minValue;
        _completedChar = completedChar;
        _incompleteChar = incompleteChar;
        _spinners = new char[] { '◐', '◓', '◑', '◒' };
        _spinnerIndex = 0;
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

        string progressBar = new string(_completedChar, completedWidth) + new string(_incompleteChar, incompleteWidth);
        char spinner = _spinners[_spinnerIndex];

        _spinnerIndex = (_spinnerIndex + 1) % _spinners.Length;

        Console.Write("\r[{0}] {1}% {2}", progressBar, (int)(percentage * 100), spinner);
    }
}