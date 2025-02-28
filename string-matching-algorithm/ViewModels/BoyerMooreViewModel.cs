using NavigationMVVM.ViewModels;
using string_matching_algorithm.Commands;
using string_matching_algorithm.Stores;
using System.Collections.ObjectModel;
using System.Numerics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.TextFormatting;

namespace string_matching_algorithm.ViewModels; 
public class BoyerMooreViewModel : ViewModelBase {
    #region Properties
    public int NO_OF_CHARS = 256;

    public string Txt {
        get => _txt;
        set {
            if (_txt != value) {
                _txt = value;
                OnPropertyChanged();
            }
        }
    }
    private string _txt;
    public string Pattern {
        get => _pattern;
        set {
            if (_pattern != value) {
                _pattern = value;
                OnPropertyChanged();
            }
        }
    }
    private string _pattern;

    public ObservableCollection<string> TxtList {
        get => _txtList;
        set { _txtList = value; OnPropertyChanged(nameof(TxtList)); }

    }
    private ObservableCollection<string> _txtList = new();

    public ObservableCollection<string> PatList{
        get => _patList;
        set { _patList = value; OnPropertyChanged(nameof(PatList)); }

    }
    private ObservableCollection<string> _patList = new();

    #endregion
    public ICommand NavigateAlgorithmCommand { get; set; }
    public ICommand ResultCommand { get; set; }
    public ICommand StepCommand { get; set; }
    public ICommand StartCommand { get; set; }
    public ICommand RefreshCommand { get; set; }
    public ICommand SearchCommand { get; set; }
    public BoyerMooreViewModel(NavigationStore navigationStore) {
        NavigateAlgorithmCommand = new NavigateCommand<AlgorithmViewModel>(navigationStore, () => new AlgorithmViewModel(navigationStore));

        ResultCommand = new RelayCommand<object>(search);
        SearchCommand = new RelayCommand<object>(Render);
    }
    public void Render(object? parameter = null) {
        if (parameter == null) {
            TxtList = new ObservableCollection<string>(Txt.Select(c => c.ToString()));
            PatList = new ObservableCollection<string>(Pattern.Select(c => c.ToString()));
        }
    }
    public int max(int a, int b) { return (a > b) ? a : b; }
    public void badCharHeuristic(char[] str, int size,
                                int[] badchar) {
        int i;
        for (i = 0; i < NO_OF_CHARS; i++)
            badchar[i] = -1;

        for (i = 0; i < size; i++)
            badchar[(int)str[i]] = i;
    }
    public void search(object? parameter = null) {
        char[] txt = Txt.ToCharArray();
        char[] pat = Pattern.ToCharArray();
        List<int> index = new List<int>();

        int m = pat.Length;
        int n = txt.Length;
        int[] badchar = new int[NO_OF_CHARS];
        badCharHeuristic(pat, m, badchar);
        int s = 0;
        while (s <= (n - m)) {
            int j = m - 1;
            while (j >= 0 && pat[j] == txt[s + j]) j--;
            if (j < 0) {
                //find match
                index.Add(s);
                s += (s + m < n) ? m - badchar[txt[s + m]] : 1;
            }
            else
                //mismatch
                s += max(1, j - badchar[txt[s + j]]);
        }
        foreach (var item in index) {
            MessageBox.Show(item.ToString() + " ");
        }
    }
}
