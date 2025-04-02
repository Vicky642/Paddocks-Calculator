using GalaSoft.MvvmLight.Messaging;
using Mypaddocks.Models;
using Mypaddocks.Repository;
using System.Windows.Input;

namespace Mypaddocks.ViewModels
{
  public class FarmDimensionsViewModel : ViewModelBase
    {
        private FarmDimensionRepository _repository;

        private FarmDimensions _farmDimensions;
        public FarmDimensions FarmDimensions
        {
            get => _farmDimensions;
            private set
            {
                if (_farmDimensions != value)
                {
                    _farmDimensions = value;
                    OnPropertyChanged();
                    // Notify the properties after changing FarmDimensions
                    OnPropertyChanged(nameof(Length));
                    OnPropertyChanged(nameof(Width));
                    OnPropertyChanged(nameof(Area));
                }
            }
        }

        private bool _hasAttemptedCalculation = false;

        public bool ShowLengthError => _hasAttemptedCalculation && !_repository.ValidateLength();
        public bool ShowWidthError => _hasAttemptedCalculation && !_repository.ValidateWidth();

        private bool _isAreaCalculated;
        public bool IsAreaCalculated
        {
            get => _isAreaCalculated;
            private set
            {
                if (_isAreaCalculated != value)
                {
                    _isAreaCalculated = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand CalculateAreaCommand { get; }

        public int Length
        {
            get => _farmDimensions?.Length ?? 0;
            set
            {
                if (_farmDimensions.Length != value)
                {
                    _farmDimensions.Length = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ShowLengthError)); // Update error visibility
                    //CalculateArea(); // Automatically recalculate area when length changes
                }
            }
        }

        public int Width
        {
            get => _farmDimensions?.Width ?? 0;
            set
            {
                if (_farmDimensions.Width != value)
                {
                    _farmDimensions.Width = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ShowWidthError)); // Update error visibility
                    //CalculateArea(); // Automatically recalculate area when width changes
                }
            }
        }

        public int Area
        {
            get => _farmDimensions?.Area ?? 0;
            set
            {
                if (_farmDimensions.Area != value)
                {
                    _farmDimensions.Area = value;
                    OnPropertyChanged();
                }
            }
        }

        public FarmDimensionsViewModel(FarmDimensionRepository repository)
        {
            _repository = repository;
            _farmDimensions = _repository.GetFarmDimensions(); // Get the current global dimensions

            CalculateAreaCommand = new RelayCommand(_ => CalculateArea());
        }
        
        private void CalculateArea()
        {
            _hasAttemptedCalculation = true;
            OnPropertyChanged(nameof(ShowLengthError));
            OnPropertyChanged(nameof(ShowWidthError));

            // Check if validation passes
            if (_repository.ValidateLength() && _repository.ValidateWidth())
            {
                _farmDimensions.Area = _farmDimensions.Length * _farmDimensions.Width; // Calculate and store area
                IsAreaCalculated = true;
                OnPropertyChanged(nameof(Area)); // Notify that Area has been recalculated
                

            }
            else
            {
                IsAreaCalculated = false;
            }
            OnPropertyChanged(nameof(IsAreaCalculated));
            OnPropertyChanged(nameof(Area)); // Notify that Area has been recalculated
            // Store the calculated area in AppState
            AppState.FarmArea = _farmDimensions.Area; // Store area globally
            //store Length and width globally
            AppState.FarmLength = _farmDimensions.Length;
            AppState.FarmWidth = _farmDimensions.Width;
        }

        public class FarmAreaUpdatedMessage
        {
            public int FarmArea { get; }

            public FarmAreaUpdatedMessage(int farmArea)
            {
                FarmArea = farmArea;
            }
        }

    }
}
