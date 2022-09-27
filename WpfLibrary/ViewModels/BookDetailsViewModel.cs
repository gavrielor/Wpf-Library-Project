using ClassLibraryLibrary.Enums;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WpfLibrary.Validation;
using WpfLibrary.ViewModelsNavigation;

namespace WpfLibrary.ViewModels
{
    public class BookDetailsViewModel : ValidatableViewModelBase, IRefreshableViewModel
    {
        [DataType(DataType.NotEmptyString)]
        public string Author { get; set; }

        [DataType(DataType.NotEmptyString)]
        public string Publisher { get; set; }

        public BookGenre Genre { get; set; }
        public List<BookGenre> GenreList { get; set; } = Enum.GetValues(typeof(BookGenre)).Cast<BookGenre>().ToList();

        [DataType(DataType.YearUntilToday)]
        public string RelaseYear { get; set; } = DateTime.Today.Year.ToString();

        [DataType(DataType.NotEmptyString)]
        public string Edition { get; set; }

        public RelayCommand<IList> SelectionChangedCommand { get; set; }

        public BookDetailsViewModel()
        {
            SelectionChangedCommand = new RelayCommand<IList>((e) => SelectionChanged(e));
            EnumFlagsValidate(nameof(Genre), (int)Genre);
        }

        private void SelectionChanged(IList e)
        {
            int bookGenre = 0;
            foreach (BookGenre item in e)
            {
                bookGenre += (int)item;
            }

            Genre = (BookGenre)bookGenre;
            EnumFlagsValidate(nameof(Genre), (int)Genre);
        }

        public void Refresh()
        {
            Author = Publisher = Edition = string.Empty;
            RelaseYear = DateTime.Today.Year.ToString();
            Genre = 0;
            EnumFlagsValidate(nameof(Genre), (int)Genre);
        }
    }
}
