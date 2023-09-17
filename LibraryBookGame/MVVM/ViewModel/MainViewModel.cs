using LibraryBookGame.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBookGame.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {


        public ReplacingBooksViewModel ReplacingBooksVM { get; set; }

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
            CurrentView = ReplacingBooksVM;

        }



    }
}
