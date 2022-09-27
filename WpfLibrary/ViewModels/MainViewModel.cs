using GalaSoft.MvvmLight;

namespace WpfLibrary.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase currentView;

        public ViewModelBase CurrentView
        {
            get => currentView;
            set => Set(ref currentView, value);
        }

        public MainViewModel()
        {
            CurrentView = ViewModelLocator.Welcome;
        }
    }
}