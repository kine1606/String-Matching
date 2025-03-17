using NavigationMVVM.ViewModels;
using string_matching_algorithm.Commands;
using string_matching_algorithm.Stores;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace string_matching_algorithm.ViewModels;
public class RabinKarpVIewModel : ViewModelBase
{
    public int NO_OF_CHARS = 256;
    public Brush FOREGROUND_DEFAULT = Brushes.Black;
    private string _animationSpeed = "1000";
    public string AnimationSpeed
    {
        get => _animationSpeed;
        set
        {
            if (_animationSpeed != value)
            {
                _animationSpeed = value;
                OnPropertyChanged();
            }
        }
    }

    private string _patternString;
    public string PatternString
    {
        get => _patternString;
        set
        {
            _patternString = value;
            OnPropertyChanged();
        }
    }
    private string _textString;
    public string TextString
    {
        get => _textString;
        set
        {
            _textString = value;
            OnPropertyChanged();
        }
    }
    private string _valueText;
    public string ValueText
    {
        get => _valueText;
        set
        {
            _valueText = value;
            OnPropertyChanged();
        }
    }

    private string _valuePattern;
    public string ValuePattern
    {
        get => _valuePattern;
        set
        {
            _valuePattern = value;
            OnPropertyChanged();
        }
    }

    private string _timeComplexity;
    public string TimeComplexity
    {
        get { return _timeComplexity; }
        set
        {
            _timeComplexity = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<TextItem> _txtList = new();
    public ObservableCollection<TextItem> TxtList
    {
        get => _txtList;
        set
        {
            _txtList = value;
            OnPropertyChanged(nameof(TextString));
        }
    }

    private ObservableCollection<TextItem> _patList = new();
    public ObservableCollection<TextItem> PatList
    {
        get { return _patList; }
        set { _patList = value; OnPropertyChanged(nameof(PatternString)); }
    }


    public string ResultText
    {
        get => _resultText;
        set
        {
            if (_resultText != value)
            {
                _resultText = value;
                OnPropertyChanged();
            }
        }
    }
    private string _resultText;

    private bool _isButtonEnabled = true;
    public bool IsButtonEnabled
    {
        get { return _isButtonEnabled; }
        set
        {
            _isButtonEnabled = value;
            OnPropertyChanged(nameof(IsButtonEnabled));
        }
    }
    public ICommand NavigateAlgorithmCommand { get; set; }
    public ICommand NavigateCodeCommand { get; set; }
    public ICommand SearchCommand { get; set; }
    public ICommand ResultCommand { get; set; }
    public ICommand RandomTextCommand { get; set; }
    public ICommand RandomPatternCommand { get; set; }
    public RabinKarpVIewModel(NavigationStore navigationStore)
    {
        NavigateAlgorithmCommand = new NavigateCommand<AlgorithmViewModel>(navigationStore, () => new AlgorithmViewModel(navigationStore));
        NavigateCodeCommand = new NavigateCommand<CodeRabinKarpViewModel>(navigationStore, () => new CodeRabinKarpViewModel(navigationStore));

        ResultCommand = new RelayCommandAsync(async () => rabinKarp());

        SearchCommand = new RelayCommand<object>(Render);

        RandomTextCommand = new RelayCommand<object>((o) => { TextString = RandomString(); });
        RandomPatternCommand = new RelayCommand<object>((o) => { PatternString = RandomString(12); });
    }

    public string RandomString(int length = 36)
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return new string(Enumerable.Repeat(chars, length)
                                   .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public void Render(object? parameter = null)
    {
        try
        {
            if (parameter == null)
            {
                ResultText = string.Empty;
                TxtList.Clear();
                PatList.Clear();
                if (!(string.IsNullOrWhiteSpace(TextString) || string.IsNullOrWhiteSpace(PatternString)))
                {
                    foreach (var item in TextString)
                    {
                        TxtList.Add(new TextItem { Text = item.ToString(), Foreground = Brushes.Black });
                    }
                    foreach (var item in PatternString)
                    {
                        PatList.Add(new TextItem { Text = item.ToString(), Foreground = Brushes.Black });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Có lỗi xảy ra vui lòng thử lại");
        }
    }
    public async Task rabinKarp(object? parameter = null)
    {
        try
        {
            IsButtonEnabled = false;
            ResultText = string.Empty;
            // breaking pattern into every single char
            // turn these char to int (ASCII) -> hash to a unique number
            // the same with text, compare the value 
            // if both value are equal, compare strings themself
            // else move and use rolling hash for saving time 
            int i, j;
            int n = TextString.Length;
            int m = PatternString.Length;

            if (n < m)
            {
                // can't compare here
                return;
            }
            // h is largest pow number like 10^99 if pattern has 100 elements
            int h = 1;
            int prime = 101;

            // d is the amount of character in ASCII, can be declined to 26 (alphabet)
            // or any number (range of bound that you gave to it) 
            int d = 256;

            int p_hash = 0;
            int t_hash = 0;

            // 10^99 ??? too large 
            // 10^2 % 101 = (10%101 * 10%101 )% 101
            // 10^3 $ 101 = (10^2 % 101) * (10%101) %101
            // ... 
            // 10^99 = (10^98 % 101) * (10%101)) % 101
            // -> (a*b)%c = ((a%c) * (b%c)) %c

            for (i = 0; i < m - 1; i++)
            {
                h = (h * d) % prime;
            }
            // pow(p_hash, d), d = 10 
            // i.g pattern = abc a-1 b-2 c-3
            // 10*0 + 1 ) %101
            // 10*1 + 2 ) %101
            for (i = 0; i < m; i++)
            {
                p_hash = (d * p_hash + PatternString[i]) % prime;
                t_hash = (d * t_hash + TextString[i]) % prime;
            }

            ValuePattern = p_hash.ToString();
            ValueText = t_hash.ToString();

            for (i = 0; i < n - m + 1; i++)
            {

                for (int k = 0; k < m; k++)
                {
                    PatList[k].Foreground = Brushes.Blue;
                    TxtList[k + i].Foreground = Brushes.Blue;
                }
                await Task.Delay(int.Parse(AnimationSpeed));


                if (p_hash == t_hash)
                {

                    await Task.Delay(int.Parse(AnimationSpeed));
                    for (j = 0; j < m; j++)
                    {
                        PatList[j].Foreground = Brushes.Red;
                        TxtList[i + j].Foreground = Brushes.Red;
                        await Task.Delay(int.Parse(AnimationSpeed));
                        // spurious hit occurs
                        // when value are both same but the order is not true 
                        // or mismatch like 'abd' and 'bbc' 
                        if (PatternString[j] != TextString[i + j])
                        {
                            //reset foreground pattern
                            foreach (var item in PatList.Where(p => p.Foreground != Brushes.Black))
                            {
                                item.Foreground = Brushes.Black;
                            }
                            //reset foreground text
                            foreach (var item in TxtList.Where(p => p.Foreground != Brushes.Black))
                            {
                                item.Foreground = Brushes.Black;
                            }
                            break;
                        }
                    }
                    // exact match 
                    if (j == m)
                    {
                        ResultText += $"Pattern occurs at shift = {i}\n";
                        OnPropertyChanged(nameof(ResultText));
                        await Task.Delay(int.Parse(AnimationSpeed) * 2);
                        //reset foreground pattern
                        foreach (var item in PatList.Where(p => p.Foreground != Brushes.Black))
                        {
                            item.Foreground = Brushes.Black;
                        }
                        //reset foreground text
                        foreach (var item in TxtList.Where(p => p.Foreground != Brushes.Black))
                        {
                            item.Foreground = Brushes.Black;
                        }
                    }

                }
                if (i < n - m)
                {
                    //rolling hash 
                    t_hash = (d * (t_hash - TextString[i] * h) + (TextString[i + m])) % prime;
                    if (t_hash < 0)
                    {
                        // guarantee if t_hash is negative, converting it to positive
                        t_hash += prime;
                    }
                }
                foreach (var item in PatList.Where(p => p.Foreground != Brushes.Black))
                {
                    item.Foreground = Brushes.Black;
                }
                //reset foreground text
                foreach (var item in TxtList.Where(p => p.Foreground != Brushes.Black))
                {
                    item.Foreground = Brushes.Black;
                }
                ValueText = t_hash.ToString();
            }


            //measure timecomplexity
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            // measure time complexity
            for (i = 0; i < 10; i++)
            {
                rabinKarp_timeComplexity();

            }
            double totaltime = 0;
            for (i = 0; i < 100; i++)
            {
                Stopwatch sw = Stopwatch.StartNew();
                rabinKarp_timeComplexity();
                sw.Stop();
                totaltime += sw.Elapsed.TotalMilliseconds;
            }
            totaltime /= 100;
            totaltime = Math.Truncate(totaltime * 10000000) / 10000000;
            ResultText += "Time Complexity: " + totaltime.ToString() + "s";
        }
        catch (Exception ex)
        {
            MessageBox.Show("Có lỗi xảy ra vui lòng thử lại");
        }
        IsButtonEnabled = true;
    }

    public void rabinKarp_timeComplexity()
    {
        try
        {

            int i, j;
            int n = TextString.Length;
            int m = PatternString.Length;
            if (n < m)
            {
                return;
            }
            int h = 1;
            int prime = 101;
            int d = 256;
            int p_hash = 0;
            int t_hash = 0;
            for (i = 0; i < m - 1; i++)
            {
                h = (h * d) % prime;
            }
            for (i = 0; i < m; i++)
            {
                p_hash = (d * p_hash + PatternString[i]) % prime;
                t_hash = (d * t_hash + TextString[i]) % prime;
            }
            for (i = 0; i < n - m + 1; i++)
            {


                if (p_hash == t_hash)
                {
                    for (j = 0; j < m; j++)
                    {
                        if (PatternString[j] != TextString[i + j])
                        {
                            break;
                        }
                    }
                    // match
                }
                if (i < n - m)
                {
                    t_hash = (d * (t_hash - TextString[i] * h) + (TextString[i + m])) % prime;
                    if (t_hash < 0)
                    {
                        t_hash += prime;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show("Có lỗi xảy ra vui lòng thử lại");
        }
    }
    public class TextItem : INotifyPropertyChanged
    {
        private string _text;
        private Brush _foreground = Brushes.Black;

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public Brush Foreground
        {
            get => _foreground;
            set
            {
                _foreground = value;
                OnPropertyChanged(nameof(Foreground));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
