using NavigationMVVM.ViewModels;
using string_matching_algorithm.Commands;
using string_matching_algorithm.Stores;
using System.Windows.Input;

namespace string_matching_algorithm.ViewModels;

class CodeRabinKarpViewModel : ViewModelBase {
    public string CodeRabinKarp {
        get => _codeRabinKarp;
        set {
            _codeRabinKarp = value;
            OnPropertyChanged();
        }
    }
    private string _codeRabinKarp;

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
    public ICommand NavigateBoyerMooreCommand { get; set; }
    public ICommand CodeJavaCommand { get; set; }
    public ICommand CodeCSCommand { get; set; }
    public ICommand CodeCPPCommand { get; set; }
    public ICommand CodePythonCommand { get; set; }
    public CodeRabinKarpViewModel(NavigationStore navigationStore) {
        NavigateBoyerMooreCommand = new NavigateCommand<RabinKarpVIewModel>(navigationStore, () => new RabinKarpVIewModel(navigationStore));
        cs();
        CodeJavaCommand = new RelayCommand<object>(java);
        CodeCSCommand = new RelayCommand<object>(cs);
        CodeCPPCommand = new RelayCommand<object>(cpp);
        CodePythonCommand = new RelayCommand<object>(py);

    }
    public void java(object? sender = null) {
        Language = "Code with Java";
        Img = "/Assets/java.png";

        CodeRabinKarp = @"

        public final static int d = 256;

        static void search(String pat, String txt, int q)
        {
            int M = pat.length();
            int N = txt.length();
            int i, j;
            int p = 0; // hash value for pattern
            int t = 0; // hash value for txt
            int h = 1;

            for (i = 0; i < M - 1; i++)
                h = (h * d) % q;

            for (i = 0; i < M; i++) {
                p = (d * p + pat.charAt(i)) % q;
                t = (d * t + txt.charAt(i)) % q;
            }

            for (i = 0; i <= N - M; i++) {

                if (p == t) {
                    for (j = 0; j < M; j++) {
                        if (txt.charAt(i + j) != pat.charAt(j))
                            break;
                    }

                    // if p == t and pat[0...M-1] = txt[i, i+1,
                    // ...i+M-1]
                    if (j == M)
                        //find match
                }

                if (i < N - M) {
                    t = (d * (t - txt.charAt(i) * h)
                         + txt.charAt(i + M))
                        % q;

                    if (t < 0)
                        t = (t + q);
                }
            }
        }

        ";
    }
    public void py(object? sender = null) {
        Language = "Code with Python3";
        Img = "/Assets/python-logo.png";

        CodeRabinKarp = @"

        def search(pat, txt, q):
            M = len(pat)
            N = len(txt)
            i = 0
            j = 0
            p = 0    # hash value for pattern
            t = 0    # hash value for txt
            h = 1

            for i in range(M-1):
                h = (h*d) % q

            for i in range(M):
                p = (d*p + ord(pat[i])) % q
                t = (d*t + ord(txt[i])) % q

            for i in range(N-M+1):
                if p == t:
                    for j in range(M):
                        if txt[i+j] != pat[j]:
                            break
                        else:
                            j += 1

                    if j == M:
                        # find match

                if i < N-M:
                    t = (d*(t-ord(txt[i])*h) + ord(txt[i+M])) % q

                    if t < 0:
                        t = t+q
       
        ";
    }
    public void cs(object? sender = null) {
        Language = "Code with C#";
        Img = "/Assets/c-sharp.png";

        CodeRabinKarp = @"

        public readonly static int d = 256;

        static void search(String pat, String txt, int q)
        {
            int M = pat.Length;
            int N = txt.Length;
            int i, j;
            int p = 0; // hash value for pattern
            int t = 0; // hash value for txt
            int h = 1;

            for (i = 0; i < M - 1; i++)
                h = (h * d) % q;

            for (i = 0; i < M; i++) {
                p = (d * p + pat[i]) % q;
                t = (d * t + txt[i]) % q;
            }

            for (i = 0; i <= N - M; i++) {

                if (p == t) {
                    for (j = 0; j < M; j++) {
                        if (txt[i + j] != pat[j])
                            break;
                    }

                    // if p == t and pat[0...M-1] = txt[i, i+1,
                    // ...i+M-1]
                    if (j == M)
                        //find match
                }

                if (i < N - M) {
                    t = (d * (t - txt[i] * h) + txt[i + M]) % q;

                    if (t < 0)
                        t = (t + q);
                }
            }
        }

        ";
    }
    public void cpp(object? sender = null) {
        Language = "Code with C++";
        Img = "/Assets/cpp_logo.png";

        CodeRabinKarp = @"

        void search(string pat, string txt, int q)
        {
            int M = pat.size();
            int N = txt.size();
            int i, j;
            int p = 0; // hash value for pattern
            int t = 0; // hash value for txt
            int h = 1;
            int d = 256; // d is the number of characters in the input alphabet 
            for (i = 0; i < M - 1; i++)
                h = (h * d) % q;

            for (i = 0; i < M; i++) {
                p = (d * p + pat[i]) % q;
                t = (d * t + txt[i]) % q;
            }

            for (i = 0; i <= N - M; i++) {
                if (p == t) {
                    /* Check for characters one by one */
                    for (j = 0; j < M; j++) {
                        if (txt[i + j] != pat[j]) {
                            break;
                        }
                    }

                    // if p == t and pat[0...M-1] = txt[i, i+1,
                    // ...i+M-1]

                    if (j == M)
                        //find match
                }

                if (i < N - M) {
                    t = (d * (t - txt[i] * h) + txt[i + M]) % q;
                    if (t < 0)
                        t = (t + q);
                }
            }
        }

        ";
    }
}

