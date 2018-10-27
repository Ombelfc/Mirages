using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Mirages.ConvolutionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mirages.ViewModel
{
    public class FiltersViewModel : ViewModelBase
    {
        #region Fields

        private ImageSource originalImage;

        public ImageSource OriginalImage
        {
            get => originalImage;
            set
            {
                originalImage = value;
                RaisePropertyChanged("OriginalImage");
            }
        }

        private ImageSource editedImage;

        public ImageSource EditedImage
        {
            get => editedImage;
            set
            {
                editedImage = value;
                RaisePropertyChanged("EditedImage");
            }
        }

        #endregion

        #region IsEnabled Booleans

        private bool isHistogramNormalizationEnabled = false;

        public bool IsHistogramNormalizationEnabled
        {
            get => isHistogramNormalizationEnabled;
            set
            {
                isHistogramNormalizationEnabled = value;
                RaisePropertyChanged("IsHistogramNormalizationEnabled");
            }
        }

        private bool isGaussianFilterEnabled = false;

        public bool IsGaussianFilterEnabled
        {
            get => isGaussianFilterEnabled;
            set
            {
                isGaussianFilterEnabled = value;
                RaisePropertyChanged("IsGaussianFilterEnabled");
            }
        }

        private bool isSharpenFilterEnabled = false;

        public bool IsSharpenFilterEnabled
        {
            get => isSharpenFilterEnabled;
            set
            {
                isSharpenFilterEnabled = value;
                RaisePropertyChanged("IsSharpenFilterEnabled");
            }
        }

        private bool isEdgeDetectionEnabled = false;

        public bool IsEdgeDetectionEnabled
        {
            get => isEdgeDetectionEnabled;
            set
            {
                isEdgeDetectionEnabled = value;
                RaisePropertyChanged("IsEdgeDetectionEnabled");
            }
        }

        private bool isRobertCrossEnabled = false;

        public bool IsRobertCrossEnabled
        {
            get => isRobertCrossEnabled;
            set
            {
                isRobertCrossEnabled = value;
                RaisePropertyChanged("IsRobertCrossEnabled");
            }
        }

        private bool isResetEnabled = false;

        public bool IsResetEnabled
        {
            get => isResetEnabled;
            set
            {
                isResetEnabled = value;
                RaisePropertyChanged("IsResetEnabled");
            }
        }

        #endregion

        public ICommand LoadImage => new RelayCommand(() =>
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a picture";
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                                    "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                    "Portable Network Graphic (*.png)|*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                var image = new BitmapImage(new Uri(openFileDialog.FileName));
                OriginalImage = image;
                EditedImage = image;
                IsHistogramNormalizationEnabled = true;
                IsGaussianFilterEnabled = true;
                IsSharpenFilterEnabled = true;
                IsEdgeDetectionEnabled = true;
                IsRobertCrossEnabled = true;
                IsResetEnabled = true;
            }
        });

        public ICommand ResetImage => new RelayCommand(() =>
        {
            EditedImage = OriginalImage.Clone();
        });

        public ICommand HistogramNormalization => new RelayCommand(() =>
        {
            
        });

        public ICommand GaussianFilter => new RelayCommand(() =>
        {
            double[,] matrix = new double[,] { { 1, 2, 1 }, { 2, 4, 2 }, { 1, 2, 1 } };
            EditedImage = (EditedImage as BitmapSource).GaussianFilter(matrix);
        });

        public ICommand SharpenFilter => new RelayCommand(() =>
        {

        });

        public ICommand EdgeDetection => new RelayCommand(() =>
        {

        });

        public ICommand RobertCross => new RelayCommand(() =>
        {

        });
    }
}
