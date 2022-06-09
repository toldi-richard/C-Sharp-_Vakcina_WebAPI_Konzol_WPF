using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vakcina.WPF.ViewModels;

namespace Vakcina.WPF.Views
{
    // TODO: 01. XAML fájl, 58. sortól: Oszlop hozzárendelések javítása
    // TODO: 02.b XAML fájl, 44. sortól: Keresőmező készítése
    // TODO: 03. XAML fájl, 58. sortól: Sorbarendezés társítása
    // TODO: 05. XAML fájl, 76. sortól: Parancsok társítása gombokhoz

    /// <summary>
    /// Interaction logic for OltasokDGView.xaml
    /// </summary>
    public partial class OltasokDGView : UserControl
    {
        private OltasViewModel viewModel;
        public OltasokDGView()
        {
            InitializeComponent();
            viewModel = Ioc.Default.GetService<OltasViewModel>();
            this.DataContext = viewModel;
        }

        private void dataGrid_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            DataGridColumn column = dataGrid.Columns.FirstOrDefault(x => x.SortMemberPath == viewModel.SortBy);
            if (column != null)
            {
                ListSortDirection direction = viewModel.ascending ? ListSortDirection.Ascending : ListSortDirection.Descending;
                column.SortDirection = direction;
            }
        }

        private void dataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            viewModel.SortBy = e.Column.SortMemberPath;
            e.Handled = true;
        }
    }
}
