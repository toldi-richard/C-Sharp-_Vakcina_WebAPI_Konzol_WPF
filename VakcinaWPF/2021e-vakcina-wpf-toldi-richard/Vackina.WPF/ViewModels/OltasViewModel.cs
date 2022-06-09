using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vakcina.WPF.Models;
using Vakcina.WPF.Repositories;

namespace Vakcina.WPF.ViewModels
{
    public class OltasViewModel : PagerViewModel
    {
        private OltasRepository _repository;
        public OltasViewModel(OltasRepository repository)
        {
            _repository = repository;
            LoadData();
        }
        public OltasViewModel()
        {

        }

        private ObservableCollection<oltas> _oltasok;
        public ObservableCollection<oltas> Oltasok
        {
            get { return _oltasok; }
            set { SetProperty(ref _oltasok, value); }
        }

        protected override void LoadData()
        {
            var query = _repository.GetAll(page, ItemsPerPage, SearchKey, SortBy, ascending);
            TotalItems = _repository.TotalItems;
            Oltasok = new ObservableCollection<oltas>(query);
        }
    }
}
