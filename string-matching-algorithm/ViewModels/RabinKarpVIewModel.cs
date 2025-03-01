﻿using NavigationMVVM.ViewModels;
using string_matching_algorithm.Commands;
using string_matching_algorithm.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace string_matching_algorithm.ViewModels;
public class RabinKarpVIewModel : ViewModelBase
{
    #region Properties
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
    #endregion
    public ICommand NavigateAlgorithmCommand { get; set; }
    public ICommand SearchCommand { get; set; }
    public ICommand StepOverCommand { get; set; }
    public ICommand StartCommand { get; set; }
    public ICommand RefreshCommand { get; set; }
    public ICommand ResultCommand { get; set; }

    public RabinKarpVIewModel(NavigationStore navigationStore)
    {
        NavigateAlgorithmCommand = new NavigateCommand<AlgorithmViewModel>(navigationStore, () => new AlgorithmViewModel(navigationStore));
        ResultCommand = new RelayCommand<object>(rabinKarp);
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
        for (i = 0; i<m; i++)
        {
            p_hash = (d* p_hash + PatternString[i]) % prime;
            t_hash = (d* t_hash + TextString[i]) % prime;
        }
        
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
                if(j==m)
                {
                    MessageBox.Show("Pattern found at: " + i.ToString());
                }
        }
        else
        {
            if (i < n - m)
            {
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
}
