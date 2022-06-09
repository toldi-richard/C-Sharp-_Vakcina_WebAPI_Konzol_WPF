using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.Generic;

namespace Vakcina.WPF.ViewModels
{
    public abstract class PagerViewModel : ObservableObject
    {
        private string _searchKey;
        public string SearchKey
        {
            get { return _searchKey; }
            set { _searchKey = value; page = 1; }
        }

        private string _sortBy;
        public string SortBy
        {
            get { return _sortBy; }
            set
            {
                if (value == _sortBy)
                {
                    ascending = !ascending;
                }
                _sortBy = value;
                LoadData();
            }
        }
        public bool ascending = true;

        #region Oldaltördelés
        private string _currentPage;
        public string CurrentPage
        {
            get { return _currentPage; }
            set { SetProperty(ref _currentPage, value); }
        }

        public List<int> IPPList { get; }

        private int itemsPerPage = 20;
        public int ItemsPerPage
        {
            get { return itemsPerPage; }
            set
            {
                SetProperty(ref itemsPerPage, value);
                LoadData();
            }
        }
        protected int page = 1;
        private int pageCount;

        private int _totalItems;
        public int TotalItems
        {
            get { return _totalItems; }
            set
            {
                pageCount = (value - 1) / itemsPerPage + 1;
                CurrentPage = page + "/" + pageCount;
                SetProperty(ref _totalItems, value);
                if (page > pageCount)
                {
                    page = pageCount;
                }
            }
        }

        public RelayCommand FirstPageCmd { get; }
        public RelayCommand PreviousPageCmd { get; }
        public RelayCommand NextPageCmd { get; }
        public RelayCommand LastPageCmd { get; }
        #endregion

        public RelayCommand LoadDataCmd { get; }

        public PagerViewModel()
        {
            LoadDataCmd = new RelayCommand(LoadData);
            FirstPageCmd = new RelayCommand(FirstPage);
            PreviousPageCmd = new RelayCommand(PrevPage);
            NextPageCmd = new RelayCommand(NextPage);
            LastPageCmd = new RelayCommand(LastPage);
            IPPList = new List<int>() { 10, 20, 50 };
        }
        protected abstract void LoadData();

        protected void FirstPage()
        {
            // TODO: 04.a Első oldalra lépés
            page = 1;
            LoadData();
        }

        protected void PrevPage()
        {
            // TODO: 04.b Előző oldal
            if (page != 1)
            {
                page--;
                LoadData();
            }
        }

        protected void NextPage()
        {
            // TODO: 04.c Következő oldal
            if (page != pageCount)
            {
                page++;
                LoadData();
            }
        }

        protected void LastPage()
        {
            // TODO: 04.d Utolsó oldal
            page = pageCount;
            LoadData();
        }
    }
}
