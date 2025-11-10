using ServiceContracts.DTOs.ManufacturersDTOs;
using ServiceContracts.ServiceContracts;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WarehouseApp.Views.ManufacturersViews;

namespace WarehouseApp.ViewModels.ManufacturersViewModels
{
    public class ManufacturersViewModel : BaseViewModel
    {
        public string Title => "📦 Manufacturers page";


        private readonly HttpClient _httpClient;
        private readonly IManufacturersService _manufacturersService;

        private ObservableCollection<ManufacturerResponse> _manufacturers = new();
        public ObservableCollection<ManufacturerResponse> Manufacturers
        {
            get => _manufacturers;
            set
            {
                _manufacturers = value;
                OnPropertyChanged();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        private ObservableCollection<ManufacturerResponse> _allManufacturers = new();

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        private ManufacturerResponse _selectedManufacturer;
        public ManufacturerResponse SelectedManufacturer
        {
            get => _selectedManufacturer;
            set
            {
                _selectedManufacturer = value;
                OnPropertyChanged();
            }
        }

        public ManufacturersViewModel()
        {
            _httpClient = new HttpClient();
            _manufacturersService = new ManufacturersService();

            AddCommand = new RelayCommand(async _ => await AddManufacturer());
            UpdateCommand = new RelayCommand(async _ => await UpdateManufacturer(), _ => SelectedManufacturer != null);
            DeleteCommand = new RelayCommand(async _ => await DeleteManufacturer(), _ => SelectedManufacturer != null);
            RefreshCommand = new RelayCommand(async _ => await LoadManufacturers());

            Task.Run(LoadManufacturers);
        }

        private async Task LoadManufacturers()
        {
            try
            {
                List<ManufacturerResponse> manufacturers = await _manufacturersService.GetAllManufacturers();

                if (manufacturers != null)
                {
                    for (int i = 0; i < manufacturers.Count; i++)
                        manufacturers[i].RowNumber = i + 1;

                    _allManufacturers = new ObservableCollection<ManufacturerResponse>(manufacturers);
                    ApplyFilter();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error with downloading manufacturers: {ex.Message}");
            }
        }

        private void ApplyFilter()
        {
            IEnumerable<ManufacturerResponse> filtered;

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = _allManufacturers;
            }
            else
            {
                filtered = _allManufacturers.Where(c =>
                    c.ManufacturerName != null &&
                    c.ManufacturerName.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            }

            var resultList = filtered.Select((c, index) =>
            {
                c.RowNumber = index + 1;
                return c;
            }).ToList();

            Manufacturers = new ObservableCollection<ManufacturerResponse>(resultList);
        }


        private async Task DeleteManufacturer()
        {
            var window = new ManufacturerDeleteView();
            var vm = new ManufacturerDeleteViewModel(window, SelectedManufacturer.ManufacturerName ?? "this manufacturer", async confirmed =>
            {
                if (confirmed)
                {
                    try
                    {
                        await _manufacturersService.DeleteManufacturer(SelectedManufacturer.ManufacturerID);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting manufacturer: {ex.Message}");
                    }
                    await LoadManufacturers();
                }
            });
            window.DataContext = vm;
            window.ShowDialog();
        }



        private async Task AddManufacturer()
        {
            var window = new ManufacturerAddEditView();
            var vm = new ManufacturerAddEditViewModel(window, async result =>
            {
                if (result != null)
                {
                    var addReq = new ManufacturerAddRequest { ManufacturerName = result.ManufacturerName };
                    try
                    {
                        await _manufacturersService.AddManufacturer(addReq);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error adding manufacturer: {ex.Message}");
                    }

                    await LoadManufacturers();
                }
            });
            window.DataContext = vm;
            window.ShowDialog();
        }

        private async Task UpdateManufacturer()
        {
            if (SelectedManufacturer == null) return;

            var window = new ManufacturerAddEditView();
            var vm = new ManufacturerAddEditViewModel(window, async result =>
            {
                if (result != null)
                {
                    var updateRequest = new ManufacturerUpdateRequest
                    {
                        ManufacturerID = SelectedManufacturer.ManufacturerID,
                        ManufacturerName = result.ManufacturerName
                    };

                    try
                    {
                        await _manufacturersService.UpdateManufacturer(updateRequest);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error adding manufacturer: {ex.Message}");
                    }

                    await LoadManufacturers();
                }
            }, SelectedManufacturer);

            window.DataContext = vm;
            window.ShowDialog();
        }
    }
}
