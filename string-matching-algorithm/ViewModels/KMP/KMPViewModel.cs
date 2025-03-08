using Accessibility;
using NavigationMVVM.ViewModels;
using string_matching_algorithm.Commands;
using string_matching_algorithm.Stores;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace string_matching_algorithm.ViewModels;
public class KMPViewModel : ViewModelBase {
    #region Properties
    //param goc luu tru du lieu

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
    private string _textString ;
    public string TextString
    {
        get => _textString;
        set
        {
            _textString = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<TextItem> _txtList = new();
    public ObservableCollection<TextItem> TxtList { 
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
        set {_patList = value; OnPropertyChanged(nameof(PatternString)); }
    }
    #endregion
    public ICommand NavigateAlgorithmCommand { get; set; }
    public ICommand SearchCommand { get; set; }
    public ICommand StepOverCommand { get; set; }
    public ICommand StartCommand { get; set; }
    public ICommand RefreshCommand { get; set; }
    public ICommand ResultCommand { get; set; }
    public KMPViewModel(NavigationStore navigationStore) {
        NavigateAlgorithmCommand = new NavigateCommand<AlgorithmViewModel>(navigationStore, () => new AlgorithmViewModel(navigationStore));
        SearchCommand = new RelayCommand<object>(Render);
        ResultCommand = new RelayCommand<object>(KMP);
    }
    public void Render(object? parameter = null)
    {
        if (parameter == null)
        {
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
    public void longestPrefixSuffix(string pattern, ref List<int> lps)
    {
        int length = 0;
        int i = 1;
        int m = pattern.Length;
        // algorithm that find matches between prefix and suffix
        // how many times they appear in pattern string
        while (i < m)
        {
            if (pattern[i] == pattern[length])
            {
                length++;
                lps[i] = length;
                i++;
            }
            else
            {
                if (length != 0) length = lps[length - 1];
                else
                {
                    lps[i] = 0;
                    i++;
                }
            }
        }
    }
     public void KMP(object? parameter = null)
    {
        List<int> res = new List<int>();
        int i = 0;
        int j = 0;
        int textLength = TextString.Length;
        int patternLength = PatternString.Length;
        
        List<int> lps = new List<int>(new int[patternLength]);
        //find lps table
        longestPrefixSuffix(PatternString, ref lps);
        while (i < textLength)
        {
            PatList[j].Foreground = Brushes.Red;
            TxtList[i].Foreground = Brushes.Red;
            if (PatList[j].Text == TxtList[i].Text)
            {
                i++;
                j++;
                // found 
                if (j == patternLength)
                {
                    res.Add(i - j); 
                    j = lps[j - 1];
                }
            }
            else
            {
                // if j is not initial value (0), move j to value in lps table
                if (j != 0) j = lps[j - 1];
                else i++;
            }
        }
        foreach (var item in res)
        {
            if(res.Count() == 0)
            {
                MessageBox.Show("Pattern not found");
                return;
            }
            MessageBox.Show(item.ToString() + " ");
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

