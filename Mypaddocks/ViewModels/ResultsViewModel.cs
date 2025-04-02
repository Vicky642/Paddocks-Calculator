using Microsoft.Win32;
using Mypaddocks.Interfaces;
using Mypaddocks.Models;
using Mypaddocks.Repository;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media;

namespace Mypaddocks.ViewModels
{
    public class ResultsViewModel : ViewModelBase
    {
        private readonly MainViewModel _mainViewModel;
        private readonly ICalculationRepository _calculationRepository;
        private CalculationResult _result;

        public CalculationResult Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PaddockDetails));
                OnPropertyChanged(nameof(PaddockVisuals));
            }
        }

        public IEnumerable<PaddockDetail> PaddockDetails =>
            _calculationRepository.GetPaddockDetails(_result);

        public IEnumerable<PaddockVisual> PaddockVisuals =>
            _calculationRepository.GetPaddockVisuals(_result);

        public ICommand BackCommand { get; }
        public ICommand ExportCommand { get; }

        // Constructor that takes both MainViewModel and repository
        public ResultsViewModel(MainViewModel mainViewModel, ICalculationRepository calculationRepository)
        {
            _mainViewModel = mainViewModel;
            _calculationRepository = calculationRepository;

            // Initialize with empty result
            _result = new CalculationResult
            {
                FarmDimensions = new FarmDimensions(),
                PaddockConfiguration = new PaddockConfiguration()
            };

            BackCommand = new RelayCommand(_ => _mainViewModel.NavigateToFarmDimensions());
            ExportCommand = new RelayCommand(ExportData);
        }
      
        private void ExportData(object parameter)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Export Paddock Data"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                _calculationRepository.ExportToCsv(Result, saveFileDialog.FileName);
            }
        }
    }
}
