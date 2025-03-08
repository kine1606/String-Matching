using NavigationMVVM.ViewModels;
using string_matching_algorithm.Commands;
using string_matching_algorithm.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

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
    public ICommand NavigateAlgorithmCommand { get; set; }

    public ICommand SearchCommand { get; set; }
    public ICommand ResultCommand { get; set; }
    public ICommand RandomTextCommand { get; set; }
    public ICommand RandomPatternCommand { get; set; }
    public RabinKarpVIewModel(NavigationStore navigationStore)
    {
        NavigateAlgorithmCommand = new NavigateCommand<AlgorithmViewModel>(navigationStore, () => new AlgorithmViewModel(navigationStore));
        ResultCommand = new RelayCommand<object>(rabinKarp);

        SearchCommand = new RelayCommand<object>(Render);

        RandomTextCommand = new RelayCommand<object>((o) => { TextString = RandomString(); });
        RandomPatternCommand = new RelayCommand<object>((o) => { PatternString = RandomString(4); });
    }

    public string RandomString(int length = 12)
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return new string(Enumerable.Repeat(chars, length)
                                   .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public void Render(object? parameter = null)
    {
        if (parameter == null)
        {
            List<int> lps = new List<int>(new int[PatternString.Length]);
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
    public void rabinKarp(object? parameter = null)
    {
        // breaking pattern into every single char
        // turn these char to int (ASCII) -> hash to a unique number
        // the same with text, compare the value 
        // if both value are equal, compare strings themself
        // else move and use rolling hash for saving time 
        int i, j;
        int n = TextString.Length;
        int m = PatternString.Length;

        if(n < m)
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
            if (p_hash == t_hash)
            {
                for (j = 0; j < m; j++)
                {
                    // spurious hit occurs
                    // when value are both same but the order is not true 
                    // or mismatch like 'abd' and 'bbc' 
                    if (PatternString[j] != TextString[i + j]) break;
                }
                // exact match 
                if (j == m)
                {
                    MessageBox.Show("Pattern found at: " + i.ToString());
                }
            }
            else
            {
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
            }
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
