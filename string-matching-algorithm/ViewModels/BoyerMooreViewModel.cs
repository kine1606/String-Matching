using NavigationMVVM.ViewModels;
using string_matching_algorithm.Commands;
using string_matching_algorithm.Stores;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

namespace string_matching_algorithm.ViewModels;
public class BoyerMooreViewModel : ViewModelBase {
    #region Properties
    public int NO_OF_CHARS = 256;
    public Brush FOREGROUND_DEFAULT = Brushes.Black;
    private string _animationSpeed = "1000";
    public string AnimationSpeed {
        get => _animationSpeed;
        set {
            if (_animationSpeed != value) {
                _animationSpeed = value;
                OnPropertyChanged();
            }
        }
    }
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

    public string ResultText {
        get => _resultText;
        set {
            if (_resultText != value) {
                _resultText = value;
                OnPropertyChanged();
            }
        }
    }
    private string _resultText;

    public ObservableCollection<TextItem> TxtList {
        get => _txtList;
        set { _txtList = value; OnPropertyChanged(nameof(TxtList)); }
    }
    private ObservableCollection<TextItem> _txtList = new();

    public ObservableCollection<TextItem> PatList {
        get => _patList;
        set { _patList = value; OnPropertyChanged(nameof(PatList)); }
    }
    private ObservableCollection<TextItem> _patList = new();

    #endregion

    #region Command
    public ICommand NavigateAlgorithmCommand { get; set; }
    public ICommand ResultCommand { get; set; }
    public ICommand SearchCommand { get; set; }
    public ICommand RandomTextCommand { get; set; }
    public ICommand RandomPatternCommand { get; set; }

    #endregion
    public BoyerMooreViewModel(NavigationStore navigationStore) {
        NavigateAlgorithmCommand = new NavigateCommand<AlgorithmViewModel>(navigationStore, () => new AlgorithmViewModel(navigationStore));

        ResultCommand = new RelayCommandAsync(async () => SearchAsync());

        SearchCommand = new RelayCommand<object>(Render);

        RandomTextCommand = new RelayCommand<object>((o) => { Txt = RandomString(); });
        RandomPatternCommand = new RelayCommand<object>((o) => { Pattern = RandomString(3); });
    }
    public string RandomString(int length = 10) {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return new string(Enumerable.Repeat(chars, length)
                                   .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public void Render(object? parameter = null) {
        if (parameter == null) {
            TxtList.Clear();
            PatList.Clear();
            if (!(string.IsNullOrWhiteSpace(Txt) || string.IsNullOrWhiteSpace(Pattern))) {
                foreach (var item in Txt) {
                    TxtList.Add(new TextItem { Text = item.ToString(), Foreground = FOREGROUND_DEFAULT });
                }
                foreach (var item in Pattern) {
                    PatList.Add(new TextItem { Text = item.ToString(), Foreground = FOREGROUND_DEFAULT });
                }
            }
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
    public async Task SearchAsync(object? parameter = null) {
        ResultText = string.Empty;
        int m = PatList.Count;
        int n = TxtList.Count;

        int[] badchar = new int[NO_OF_CHARS];
        //find last character
        badCharHeuristic(PatList.Select(item => item.Text[0]).ToArray(), m, badchar);
        int s = 0;

        while (s <= (n - m)) {
            int j = m - 1;
            TxtList[s].Foreground = Brushes.Blue;
            TxtList[s + j].Foreground = Brushes.Red;
            PatList[j].Foreground = Brushes.Red;
            OnPropertyChanged(nameof(TxtList));
            await Task.Delay(int.Parse(AnimationSpeed));

            //find bad character
            while (j >= 0 && PatList[j].Text[0] == TxtList[s + j].Text[0]) {
                TxtList[s + j].Foreground = Brushes.Red;
                PatList[j].Foreground = Brushes.Red;
                OnPropertyChanged(nameof(TxtList));
                j--;
                await Task.Delay(int.Parse(AnimationSpeed));
            }
            if (j < 0) {
                // Match found
                ResultText += $"Pattern occurs at shift = {s}\n";
                OnPropertyChanged(nameof(ResultText));
                await Task.Delay(3000);
                s += (s + m < n) ? m - badchar[TxtList[s + m].Text[0]] : 1;
            }
            else {
                // Mismatch
                s += Math.Max(1, j - badchar[TxtList[s + j].Text[0]]);
            }

            //reset foreground pattern
            foreach (var item in PatList.Where(p => p.Foreground != Brushes.Black)) {
                item.Foreground = Brushes.Black;
            }
            //reset foreground text
            foreach (var item in TxtList.Where(p => p.Foreground != Brushes.Black)) {
                item.Foreground = Brushes.Black;
            }
        }
    }

    public class TextItem : INotifyPropertyChanged {
        private string _text;
        private Brush _foreground = Brushes.Black;

        public string Text {
            get => _text;
            set {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public Brush Foreground {
            get => _foreground;
            set {
                _foreground = value;
                OnPropertyChanged(nameof(Foreground));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
