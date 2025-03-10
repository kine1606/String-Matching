using NavigationMVVM.ViewModels;
using string_matching_algorithm.Commands;
using string_matching_algorithm.Stores;
using System.Windows.Input;

namespace string_matching_algorithm.ViewModels;

class CodeKMPViewModel : ViewModelBase {
    public string CodeKMP {
        get => _codeKMP;
        set {
            _codeKMP = value;
            OnPropertyChanged();
        }
    }
    private string _codeKMP;

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
    public CodeKMPViewModel(NavigationStore navigationStore) {
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

        CodeKMP = @"

        static void constructLps(String pat, int[] lps) {
        
        int len = 0;

        // lps[0] is always 0
        lps[0] = 0;

        int i = 1;
        while (i < pat.length()) {
            
            if (pat.charAt(i) == pat.charAt(len)) {
                len++;
                lps[i] = len;
                i++;
            }
            
            // If there is a mismatch
            else {
                if (len != 0) {
                    len = lps[len - 1];
                } 
                else {
                    // If no matching prefix found, set lps[i] to 0
                    lps[i] = 0;
                    i++;
                }
            }
        }

        static ArrayList<Integer> search(String pat, String txt) {
            int n = txt.length();
            int m = pat.length();

            int[] lps = new int[m];
            ArrayList<Integer> res = new ArrayList<>();

            constructLps(pat, lps);

            int i = 0;
            int j = 0;

            while (i < n) {
                if (txt.charAt(i) == pat.charAt(j)) {
                    i++;
                    j++;

                    if (j == m) {
                        res.add(i - j);
                        j = lps[j - 1];
                    }
                }
            
                // If there is a mismatch
                else {
                    if (j != 0)
                        j = lps[j - 1];
                    else
                        i++;
                }
            }
            return res; 
        }

        ";
    }
    public void py(object? sender = null) {
        Language = "Code with Python3";
        Img = "/Assets/python-logo.png";

        CodeKMP = @"

        def constructLps(pat, lps):
    
            len_ = 0
            m = len(pat)
    
            lps[0] = 0

            i = 1
            while i < m:
                if pat[i] == pat[len_]:
                    len_ += 1
                    lps[i] = len_
                    i += 1
        
                # If there is a mismatch
                else:
                    if len_ != 0:
                        len_ = lps[len_ - 1]
                    else:
                
                        lps[i] = 0
                        i += 1

        def search(pat, txt):
            n = len(txt)
            m = len(pat)

            lps = [0] * m
            res = []

            constructLps(pat, lps)

            i = 0
            j = 0

            while i < n:
                if txt[i] == pat[j]:
                    i += 1
                    j += 1

                    if j == m:
                        res.append(i - j)
                
                        j = lps[j - 1]
        
                # If there is a mismatch
                else:
            
                    if j != 0:
                        j = lps[j - 1]
                    else:
                        i += 1
            return res 
        ";
    }
    public void cs(object? sender = null) {
        Language = "Code with C#";
        Img = "/Assets/c-sharp.png";

        CodeKMP = @"

        static void ConstructLps(string pat, int[] lps) {
                int len = 0;

                lps[0] = 0;

                int i = 1;
                while (i < pat.Length) {
          
                    if (pat[i] == pat[len]) {
                        len++;
                        lps[i] = len;
                        i++;
                    }
          
                    // If there is a mismatch
                    else {
                        if (len != 0) {
                  
                            len = lps[len - 1];
                        }
                        else {
                  
                            lps[i] = 0;
                            i++;
                        }
                    }
                }
            }

            static List<int> search(string pat, string txt) {
                int n = txt.Length;
                int m = pat.Length;

                int[] lps = new int[m];
                List<int> res = new List<int>();

                ConstructLps(pat, lps);

                int i = 0;
                int j = 0;

                while (i < n) {
          
                    if (txt[i] == pat[j]) {
                        i++;
                        j++;

                        if (j == m) {
                            res.Add(i - j);
                            j = lps[j - 1];
                        }
                    }
            
                    // If there is a mismatch
                    else {
            
                        if (j != 0)
                            j = lps[j - 1];
                        else
                            i++;
                    }
                }
                return res;
            }

        ";
    }
    public void cpp(object? sender = null) {
        Language = "Code with C++";
        Img = "/Assets/cpp_logo.png";

        CodeKMP = @"

        void constructLps(string &pat, vector<int> &lps) {

            int len = 0;

            // lps[0] is always 0
            lps[0] = 0;

            int i = 1;
            while (i < pat.length()) {

                if (pat[i] == pat[len]) {
                    len++;
                    lps[i] = len;
                    i++;
                }

                // If there is a mismatch
                else {
                    if (len != 0) {
                        len = lps[len - 1];
                    }
                    else {
                        lps[i] = 0;
                        i++;
                    }
                }
            }
        }

        vector<int> search(string &pat, string &txt) {
            int n = txt.length();
            int m = pat.length();

            vector<int> lps(m);
            vector<int> res;

            constructLps(pat, lps);

            int i = 0;
            int j = 0;

            while (i < n) {

                if (txt[i] == pat[j]) {
                    i++;
                    j++;

                    if (j == m) {
                        res.push_back(i - j);
                        j = lps[j - 1];
                    }
                }

                else {

                    if (j != 0)
                        j = lps[j - 1];
                    else
                        i++;
                }
            }
            return res;
        }

        ";
    }
}
