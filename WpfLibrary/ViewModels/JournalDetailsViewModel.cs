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
    public class JournalDetailsViewModel : ValidatableViewModelBase, IRefreshableViewModel
    {
        [DataType(DataType.NotEmptyString)]
        public string Publisher { get; set; }

        public JournalGenre Genre { get; set; }
        public List<JournalGenre> GenreList { get; set; } = Enum.GetValues(typeof(JournalGenre)).Cast<JournalGenre>().ToList();

        public Frequency SelectedFrequency { get; set; } //TODO: select first if needed
        public List<Frequency> FrequencyList { get; set; } = Enum.GetValues(typeof(Frequency)).Cast<Frequency>().ToList();

        public RelayCommand<IList> SelectionChangedCommand { get; set; }

        public JournalDetailsViewModel()
        {
            SelectionChangedCommand = new RelayCommand<IList>((e) => SelectionChanged(e));
            EnumFlagsValidate(nameof(Genre), (int)Genre);
        }

        private void SelectionChanged(IList e)
        {
            int journalGenre = 0;
            foreach (JournalGenre item in e)
            {
                journalGenre += (int)item;
            }

            Genre = (JournalGenre)journalGenre;
            EnumFlagsValidate(nameof(Genre), (int)Genre);
        }

        public void Refresh()
        {
            Publisher = string.Empty;
            Genre = 0;
            SelectedFrequency = 0;
            EnumFlagsValidate(nameof(Genre), (int)Genre);
        }
    }
}
