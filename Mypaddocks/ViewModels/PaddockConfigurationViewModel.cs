using GalaSoft.MvvmLight.Messaging;
using Mypaddocks.Models;
using Mypaddocks.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mypaddocks.ViewModels
{
    public class PaddockConfigurationViewModel : ViewModelBase
    {
        private readonly PaddockConfigurationRepository _repository;
        private PaddockConfiguration _configuration;
        private bool _isResultVisible;
        

        public PaddockConfiguration Configuration
        {
            get => _configuration;
            set
            {
                _configuration = value;
                OnPropertyChanged();
            }
        }

        public bool IsResultVisible
        {
            get => _isResultVisible;
            set
            {
                _isResultVisible = value;
                OnPropertyChanged();
            }
        }

        public ICommand GetNumberOfPaddocksCommand { get; }

        public PaddockConfigurationViewModel(PaddockConfigurationRepository repository)
        {
            _repository = repository;
            _configuration = new PaddockConfiguration();  // Initialize the configuration model
            // Retrieve the area from AppState when entering the tab
            _configuration.FarmArea = AppState.FarmArea; // Use the stored farm area
            GetNumberOfPaddocksCommand = new RelayCommand(_ => CalculatePaddocks());

        }

        private void CalculatePaddocks()
        {
            if (_configuration.FarmArea > 0 && _configuration.CowsPerPaddock > 0)
            {
                // Calculate the number of paddocks using the repository
                int numberOfPaddocks = _repository.CalculateNumberOfPaddocks((int)_configuration.FarmArea, _configuration.CowsPerPaddock);

                // Set the result in the model
                _configuration.NumberOfPaddocks = numberOfPaddocks;
                IsResultVisible = true;
                OnPropertyChanged(nameof(Configuration));  // Notify the UI that the configuration has changed
                AppState.CowsPerPaddock = _configuration.CowsPerPaddock;
            }
        }

    }
}
