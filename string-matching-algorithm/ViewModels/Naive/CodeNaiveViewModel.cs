using NavigationMVVM.ViewModels;
using string_matching_algorithm.Commands;
using string_matching_algorithm.Stores;
using System.Windows.Input;

namespace string_matching_algorithm.ViewModels;

class CodeNaiveViewModel : ViewModelBase
{
    public string CodeNaive {
        get => _codeNaive;
        set {
            _codeNaive = value;
            OnPropertyChanged();
        }
    }
    private string _codeNaive;
    public string Language {
        get => _language;
        set {
            _language = value;
            OnPropertyChanged();
        }
    }
    private string _language;
    public string Img {
        get => _img;
        set {
            _img = value;
            OnPropertyChanged();
        }
    }
    private string _img;

    public ICommand NavigateNaiveCommand { get; set; }
    public ICommand CodeJavaCommand { get; set; }
    public ICommand CodeCSCommand { get; set; }
    public ICommand CodeCPPCommand { get; set; }
    public ICommand CodePythonCommand { get; set; }
    public CodeNaiveViewModel(NavigationStore navigationStore) {
        NavigateNaiveCommand = new NavigateCommand<NaiveViewModel>(navigationStore, () => new NaiveViewModel(navigationStore));
        cs();
        CodeJavaCommand = new RelayCommand<object>(java);
        CodeCSCommand = new RelayCommand<object>(cs);
        CodeCPPCommand = new RelayCommand<object>(cpp);
        CodePythonCommand = new RelayCommand<object>(py);
    }
    public void java(object? sender = null) {
        Language = "Code with Java";
        Img = "/Assets/java.png";

        CodeNaive = @"
        public static void search(String pat, String txt) 
        {
            int M = pat.length();
            int N = txt.length();

            for (int i = 0; i <= N - M; i++) {
                int j;

                for (j = 0; j < M; j++) {
                    if (txt.charAt(i + j) != pat.charAt(j)) {
                        break;
                    }
                }

                if (j == M) {
                    // find matching
                }
            }
        }    
        ";
    }
    public void cs(object? sender = null) {
        Language = "Code with C#";
        Img = "/Assets/c-sharp.png";

        CodeNaive = @"
        static void Search(string pat, string txt)
        {
            int M = pat.Length;
            int N = txt.Length;

            for (int i = 0; i <= N - M; i++)
            {
                int j;

                for (j = 0; j < M; j++)
                {
                    if (txt[i + j] != pat[j])
                    {
                        break;
                    }
                }

                if (j == M)
                {
                    // find matching
                }
            }
        }   
        ";
    }
    public void cpp(object? sender = null) {
        Language = "Code with C++";
        Img = "/Assets/cpp_logo.png";

        CodeNaive = @"
        void search(string& pat, string& txt) 
        {
            int M = pat.size();
            int N = txt.size();

            for (int i = 0; i <= N - M; i++) {
                int j;

                for (j = 0; j < M; j++) {
                    if (txt[i + j] != pat[j]) {
                        break;
                    }
                }

                if (j == M) {
                    //find matching
                }
            }
        }      
    ";
    }
    public void py(object? sender = null) {
        Language = "Code with Python3";
        Img = "/Assets/python-logo.png";

        CodeNaive = @"
        def search_pattern(pattern, text):

        m = len(pattern)
        n = len(text)

        for i in range(n - m + 1):
            j = 0
            while j < m and text[i + j] == pattern[j]:
                j += 1
        
            if j == m:
                # find matching
        ";
    }
}
