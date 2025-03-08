using NavigationMVVM.ViewModels;
using string_matching_algorithm.Commands;
using string_matching_algorithm.Stores;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace string_matching_algorithm.ViewModels;

class CodeBoyerMooreViewModel : ViewModelBase 
{
    public string CodeBoyerMoore {
        get => _codeBoyerMoore;
        set {
            _codeBoyerMoore = value;
            OnPropertyChanged();
        }
    }
    private string _codeBoyerMoore;

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
    public  CodeBoyerMooreViewModel(NavigationStore navigationStore) {
        NavigateBoyerMooreCommand = new NavigateCommand<BoyerMooreViewModel>(navigationStore, () => new BoyerMooreViewModel(navigationStore));
        cs();
        CodeJavaCommand = new RelayCommand<object>(java);
        CodeCSCommand = new RelayCommand<object>(cs);
        CodeCPPCommand = new RelayCommand<object>(cpp);
        CodePythonCommand = new RelayCommand<object>(py);
        
    }
    public void java(object? sender = null) {
        Language = "Code with Java";
        Img = "/Assets/java.png";

        CodeBoyerMoore = @"

        static void badCharHeuristic(char[] str, int size, int badchar[])
        {
 
            for (int i = 0; i < NO_OF_CHARS; i++)
                badchar[i] = -1;
 
            for (int i = 0; i < size; i++)
                badchar[(int)str[i]] = i;
        }
 
        static void search(char txt[], char pat[])
        {
            int m = pat.length;
            int n = txt.length;
 
            int badchar[] = new int[NO_OF_CHARS];
 
            badCharHeuristic(pat, m, badchar);
 
            int s = 0; 

            while (s <= (n - m)) {
                int j = m - 1;
 
                while (j >= 0 && pat[j] == txt[s + j])
                    j--;
 
                if (j < 0) {
                    //find match
 
                    s += (s + m < n) ? m - badchar[txt[s + m]] : 1;
                }
 
                else
                    s += max(1, j - badchar[txt[s + j]]);
            }
        }
        ";
    }
    public void py(object? sender = null) {
        Language = "Code with Python3";
        Img = "/Assets/python-logo.png";

        CodeBoyerMoore = @"

        def badCharHeuristic(string, size):
 
            badChar = [-1]*NO_OF_CHARS
 
            for i in range(size):
                badChar[ord(string[i])] = i
 
            return badChar
 
 
        def search(txt, pat):
            
            m = len(pat)
            n = len(txt)
 
            badChar = badCharHeuristic(pat, m)
 
            s = 0
            while(s <= n-m):
                j = m-1
 
                while j >= 0 and pat[j] == txt[s+j]:
                    j -= 1
 
                if j < 0:

                    //find match
                    
                    s += (m-badChar[ord(txt[s+m])] if s+m < n else 1)
                else:
                    s += max(1, j-badChar[ord(txt[s+j])])

        ";
    }
    public void cs(object? sender = null) {
        Language = "Code with C#";
        Img = "/Assets/c-sharp.png";

        CodeBoyerMoore = @"

        static int NO_OF_CHARS = 256;
 
        static int max(int a, int b) { return (a > b) ? a : b; }
 
        static void badCharHeuristic(char[] str, int size, int[] badchar)
        {
            int i;
 
            for (i = 0; i < NO_OF_CHARS; i++)
                badchar[i] = -1;
 
            for (i = 0; i < size; i++)
                badchar[(int)str[i]] = i;
        }
 
        static void search(char[] txt, char[] pat)
        {
            int m = pat.Length;
            int n = txt.Length;
 
            int[] badchar = new int[NO_OF_CHARS];
 
            badCharHeuristic(pat, m, badchar);
 
            int s = 0; 

            while (s <= (n - m)) {
                int j = m - 1;
 
                
                while (j >= 0 && pat[j] == txt[s + j])
                    j--;
 
                
                if (j < 0) {

                    //find match
                    
                    s += (s + m < n) ? m - badchar[txt[s + m]] : 1;
                }
 
                else
                    s += max(1, j - badchar[txt[s + j]]);
            }
        }

        ";
    }
    public void cpp(object? sender = null) {
        Language = "Code with C++";
        Img = "/Assets/cpp_logo.png";

        CodeBoyerMoore = @"

        void badCharHeuristic(string str, int size, int badchar[NO_OF_CHARS])
        {
            int i;

            for (i = 0; i < NO_OF_CHARS; i++)
                badchar[i] = -1;
 
            for (i = 0; i < size; i++)
                badchar[(int)str[i]] = i;
        }
 
        void search(string txt, string pat)
        {
            int m = pat.size();
            int n = txt.size();
 
            int badchar[NO_OF_CHARS];
 
            badCharHeuristic(pat, m, badchar);
 
            int s = 0; 
                       
            while (s <= (n - m)) {
                int j = m - 1;
 
                while (j >= 0 && pat[j] == txt[s + j])
                    j--;
 
                if (j < 0) {

                    //find match
                    
                    s += (s + m < n) ? m - badchar[txt[s + m]] : 1;
                }
 
                else
                    s += max(1, j - badchar[txt[s + j]]);
            }
        }

        ";
    }
}
