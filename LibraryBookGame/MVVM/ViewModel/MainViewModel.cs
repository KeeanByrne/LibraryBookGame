using LibraryBookGame.Core;
using LibraryBookGame.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBookGame.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {

        public  RelayCommand ReplacingBooksCommand { get; set; }
        public RelayCommand IdentifyingAreasCommand { get; set; }
        public RelayCommand FindingCallNumbersCommand { get; set; }


        
        public ReplacingBooksViewModel ReplacingBooksVM { get; set; }
        public IdentifyingAreasViewModel IdentifyingAreasVM { get; set; }
        public FindingCallNumbersViewModel FindingCallNumbersVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value;
                onPropertyChanged();
            }   
        }
        public MainViewModel()
        {
            ReplacingBooksVM = new ReplacingBooksViewModel();
            IdentifyingAreasVM = new IdentifyingAreasViewModel();
            FindingCallNumbersVM = new FindingCallNumbersViewModel();
            
            CurrentView = ReplacingBooksVM;

            ReplacingBooksCommand = new RelayCommand(o =>
            {

                CurrentView = ReplacingBooksVM;

            });

            IdentifyingAreasCommand = new RelayCommand(o =>
            {

                CurrentView = IdentifyingAreasVM;

            });

            FindingCallNumbersCommand = new RelayCommand(o =>
            {
                CurrentView = FindingCallNumbersVM;
            });
        }
    }
}
