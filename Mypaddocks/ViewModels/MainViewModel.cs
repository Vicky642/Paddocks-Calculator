using Mypaddocks.Interfaces;
using Mypaddocks.Models;
using Mypaddocks.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mypaddocks.ViewModels
{
   public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentView;
        private readonly ICalculationRepository _calculationRepository;
        private FarmDimensions _farmDimensions;
        private PaddockConfiguration _paddockConfiguration;
        private FarmDimensionRepository _farmDimensionRepository;
        private PaddockConfigurationRepository _paddockConfigurationRepository;
        public ViewModelBase CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
        }

        public FarmDimensions FarmDimensions
        {
            get => _farmDimensions;
            set { _farmDimensions = value; OnPropertyChanged(); }
        }

        public PaddockConfiguration PaddockConfiguration
        {
            get => _paddockConfiguration;
            set { _paddockConfiguration = value; OnPropertyChanged(); }
        }

        public FarmDimensionsViewModel FarmDimensionsVM { get; }
        public PaddockConfigurationViewModel PaddockConfigurationVM { get; }

        public ICommand FarmDimensionsCommand { get; }
        public ICommand PaddockConfigurationCommand { get; }
        public ICommand ResultsCommand { get; }

        public MainViewModel()
        {
            // Initialize repositories
            _calculationRepository = new CalculationRepository();
            _farmDimensionRepository = new FarmDimensionRepository();
            _paddockConfigurationRepository = new PaddockConfigurationRepository();
            // Initialize models
            FarmDimensions = new FarmDimensions();
            PaddockConfiguration = new PaddockConfiguration();

            // Initialize view models
            FarmDimensionsVM = new FarmDimensionsViewModel(_farmDimensionRepository);
            PaddockConfigurationVM = new PaddockConfigurationViewModel(_paddockConfigurationRepository);

            CurrentView = FarmDimensionsVM;

            // Initialize commands
            FarmDimensionsCommand = new RelayCommand(_ => NavigateToFarmDimensions());
            PaddockConfigurationCommand = new RelayCommand(_ => NavigateToPaddockConfiguration());
            ResultsCommand = new RelayCommand(_ => NavigateToResults());
        }

        public void NavigateToFarmDimensions()
        {
            CurrentView = FarmDimensionsVM;
        }

        public void NavigateToPaddockConfiguration()
        {
            CurrentView = PaddockConfigurationVM;
        }

        public void NavigateToResults()
        {
            var result = _calculationRepository.CalculatePaddockLayout(FarmDimensions, PaddockConfiguration);
            CurrentView = new ResultsViewModel(this, _calculationRepository)
            {
                Result = result
            };
        }
        //private object _currentView;
        //private readonly CalculationRepository _calculationRepository;
        //public object CurrentView
        //{
        //    get => _currentView;
        //    set { _currentView = value; OnPropertyChanged(); }
        //}

        //public FarmDimensionsViewModel FarmDimensionsVM { get; }
        //public PaddockConfigurationViewModel PaddockConfigurationVM { get; }
        //public ResultsViewModel ResultsVM { get; }

        //public ICommand FarmDimensionsCommand { get; }
        //public ICommand PaddockConfigurationCommand { get; }

        //public ICommand ResultsCommand { get; }

        //public MainViewModel()
        //{
        //    // Create an instance of the repository
        //    var farmDimensionsRepository = new FarmDimensionRepository();
        //    var paddockConfigurationRepository = new PaddockConfigurationRepository();
        //    var calculationRepository = new CalculationRepository();

        //    // Pass the repository to the view model
        //    FarmDimensionsVM = new FarmDimensionsViewModel(farmDimensionsRepository);
        //    PaddockConfigurationVM = new PaddockConfigurationViewModel(paddockConfigurationRepository);
        //    ResultsVM = new ResultsViewModel(calculationRepository);

        //    CurrentView = FarmDimensionsVM;

        //    // Using RelayCommand with Action<object>
        //    FarmDimensionsCommand = new RelayCommand(param => NavigateToFarmDimensions(), param => true);
        //    PaddockConfigurationCommand = new RelayCommand(param => NavigateToPaddockConfiguration(), param => true);
        //    ResultsCommand = new RelayCommand(param => NavigateToResults(), param => true);
        //}

        //public void NavigateToFarmDimensions()
        //{
        //    CurrentView = FarmDimensionsVM;
        //}

        //public void NavigateToPaddockConfiguration()
        //{
        //    CurrentView = PaddockConfigurationVM;
        //}
        //public void NavigateToResults()
        //{
        //    // Perform calculation when navigating to results
        //   var result = _calculationRepository.CalculatePaddockLayout(FarmDimensions, PaddockConfiguration);
        //    CurrentView = new ResultsViewModel(this, _calculationRepository)
        //    {
        //        Result = result
        //    };
        //}
    }
}
