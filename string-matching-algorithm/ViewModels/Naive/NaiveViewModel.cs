using NavigationMVVM.ViewModels;
using string_matching_algorithm.Commands;
using string_matching_algorithm.Stores;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace string_matching_algorithm.ViewModels; 
//brute force : vet' can.
public class NaiveViewModel : ViewModelBase {

    #region Properties
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
    private bool _isButtonEnabled = true;
    public bool IsButtonEnabled {
        get { return _isButtonEnabled; }
        set {
            _isButtonEnabled = value;
            OnPropertyChanged(nameof(IsButtonEnabled));
        }
    }

    #endregion


    private ObservableCollection<string> _textList = new();
    public ObservableCollection<string> TextList {
        get => _textList;
        set { _textList = value; OnPropertyChanged(nameof(TextList)); }
    }
    #region Command
    public ICommand NavigateAlgorithmCommand { get; set; }
    public ICommand NavigateCodeCommand { get; set; }
    public ICommand SearchCommand { get; set; }
    public ICommand ResultCommand { get; set; }
    public ICommand RandomTextCommand { get; set; }
    public ICommand RandomPatternCommand { get; set; }

    #endregion

    public NaiveViewModel(NavigationStore navigationStore) {
        NavigateAlgorithmCommand = new NavigateCommand<AlgorithmViewModel>(navigationStore, () => new AlgorithmViewModel(navigationStore));
        NavigateCodeCommand = new NavigateCommand<CodeNaiveViewModel>(navigationStore, () => new CodeNaiveViewModel(navigationStore));
        
        SearchCommand = new RelayCommand<object>(Render);

        ResultCommand = new RelayCommandAsync(async () => SearchAsync());

        SearchCommand = new RelayCommand<object>(Render);

        RandomTextCommand = new RelayCommand<object>((o) => { Txt = RandomString(); });
        RandomPatternCommand = new RelayCommand<object>((o) => { Pattern = RandomString(12); });

    }
    public string RandomString(int length = 36) {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return new string(Enumerable.Repeat(chars, length)
                                   .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public void Render(object? sender = null) {
        if (sender == null) {
            try {
                ResultText = string.Empty;
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
            catch (Exception) {
                MessageBox.Show("Có lỗi xảy ra vui lòng thử lại");
            }
            
        }
    }

    public async Task SearchAsync(object? parameter = null) {
        try {
            IsButtonEnabled = false;
            ResultText = string.Empty;
            int M = PatList.Count;
            int N = TxtList.Count;

            for (int i = 0; i <= N - M; i++) {
                int j;

                for (j = 0; j < M; j++) {
                    TxtList[i + j].Foreground = Brushes.Blue;
                    PatList[j].Foreground = Brushes.Blue;
                    OnPropertyChanged(nameof(TxtList));
                    await Task.Delay(int.Parse(AnimationSpeed));
                    if (TxtList[i + j].Text[0] != PatList[j].Text[0]) {
                        break;
                    }
                }

                if (j == M) {
                    ResultText += $"Pattern occurs at shift = {i}\n";
                    OnPropertyChanged(nameof(ResultText));
                    await Task.Delay(int.Parse(AnimationSpeed) * 2);
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
        catch (Exception) {
            MessageBox.Show("Có lỗi xảy ra vui lòng thử lại");
        }
        IsButtonEnabled = true;
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
