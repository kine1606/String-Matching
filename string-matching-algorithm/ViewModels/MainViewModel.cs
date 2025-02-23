using NavigationMVVM.ViewModels;
using string_matching_algorithm.Stores;
using string_matching_algorithm.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace string_matching_algorithm.ViewModels {
    public class MainViewModel : ViewModelBase {

        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore navigationStore) {
            _navigationStore = navigationStore;


            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged() {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}