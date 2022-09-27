using ClassLibraryLibrary;
using ClassLibraryLibrary.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Windows;
using WpfLibrary.Validation;
using WpfLibrary.ViewModelsNavigation;

namespace WpfLibrary.ViewModels
{
    public class AddItemViewModel : ViewModelBase, IRefreshableViewModel
    {
        private AbstractItemDetailsViewModel abstractItemVM;
        private BookDetailsViewModel bookVM;
        private JournalDetailsViewModel journalVM;

        private Library library;
        private Dictionary<Type, (Action action, ValidatableViewModelBase viewModel)> typeToActionAndView;

        public Type ItemType => ViewModelLocator.Worker.SelectedType;
        public string Title => $"Add {ItemType.Name}";

        public ViewModelBase ItemDetailsView => typeToActionAndView[ItemType].viewModel;
        public RelayCommand AddCommand { get; set; }
        public RelayCommand GoBackCommand { get; set; }

        public AddItemViewModel(Library library)
        {
            abstractItemVM = ViewModelLocator.AbstractItemDetails;
            bookVM = ViewModelLocator.BookDetails;
            journalVM = ViewModelLocator.JournalDetails;

            this.library = library;

            typeToActionAndView = new Dictionary<Type, (Action action, ValidatableViewModelBase viewModel)>()
            {
                { typeof(Book), (AddBook, bookVM) },
                { typeof(Journal), (AddJournal, journalVM) }
            };

            AddCommand = new RelayCommand(AddItem, CanAdd);
            GoBackCommand = new RelayCommand(Navigation.Worker);

            abstractItemVM.OnPropertyChanged += AddCommand.RaiseCanExecuteChanged;
            foreach (var (action, viewModel) in typeToActionAndView.Values)
            {
                viewModel.OnPropertyChanged += AddCommand.RaiseCanExecuteChanged;
            }
        }

        private void AddItem()
        {
            typeToActionAndView[ItemType].action.Invoke();
        }

        private bool CanAdd()
        {
            return !(abstractItemVM.HasError || typeToActionAndView[ItemType].viewModel.HasError);
        }

        private void AddBook()
        {
            var success = library.Items.TryAddItem(new Book(abstractItemVM.Name, abstractItemVM.ISBN, double.Parse(abstractItemVM.Price), uint.Parse(abstractItemVM.Quantity),
            bookVM.Author, bookVM.Publisher, bookVM.Genre, int.Parse(bookVM.RelaseYear), bookVM.Edition));

            if (!success) MessageBox.Show("An item with the same ISBN has been already added", "Warning!");
            if (success) MessageBox.Show("Book added successfully!", "Message!");
        }

        private void AddJournal()
        {
            var success = library.Items.TryAddItem(new Journal(abstractItemVM.Name, abstractItemVM.ISBN, double.Parse(abstractItemVM.Price), uint.Parse(abstractItemVM.Quantity),
            journalVM.Publisher, journalVM.Genre, journalVM.SelectedFrequency));

            if (!success) MessageBox.Show("An item with the same ISBN has been already added", "Warning!");
            if (success) MessageBox.Show("Journal added successfully!", "Message!");
        }

        public void Refresh()
        {
            abstractItemVM.Refresh();
            bookVM.Refresh();
            journalVM.Refresh();
            AddCommand.RaiseCanExecuteChanged();
        }
    }
}
