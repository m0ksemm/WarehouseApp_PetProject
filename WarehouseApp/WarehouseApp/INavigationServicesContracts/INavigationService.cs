using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.ViewModels;

namespace ServiceContracts
{
    public interface INavigationService
    {
        BaseViewModel CurrentViewModel { get; }
        void NavigateTo<TViewModel>() where TViewModel : BaseViewModel;
        public void NavigateTo(BaseViewModel viewModel);

        public event Action CurrentViewModelChanged;

    }
}
