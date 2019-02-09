using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Mirages.ConvolutionFilters;
using Mirages.ElementaryAlgorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mirages.ViewModels
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

        private bool isSobelFilterEnabled = false;

        public bool IsSobelFilterEnabled
        {
            get => isSobelFilterEnabled;
            set
            {
                isSobelFilterEnabled = value;
                RaisePropertyChanged("IsSobelFilterEnabled");
            }
        }

        private bool isRobertsCrossEnabled = false;

        public bool IsRobertsCrossEnabled
        {
            get => isRobertsCrossEnabled;
            set
            {
                isRobertsCrossEnabled = value;
                RaisePropertyChanged("IsRobertsCrossEnabled");
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

        #region Commands

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
                Reset();
            }
        });

        public ICommand ResetImage => new RelayCommand(() =>
        {
            EditedImage = OriginalImage.Clone();
        });

        public ICommand GaussianFilter => new RelayCommand(() =>
        {
            double[,] matrix = new double[,] { { 1, 2, 1 },
                                               { 2, 4, 2 },
                                               { 1, 2, 1 } };

            EditedImage = (OriginalImage.Clone() as BitmapSource).GaussianFilter(matrix);
        });

        public ICommand SharpenFilter => new RelayCommand(() =>
        {
            double[,] matrix = new double[,] { { 0, -1, 0 },
                                               { -1, 4, -1 },
                                               { 0, -1, 0 } };

            EditedImage = (OriginalImage.Clone() as BitmapSource).SharpenFilter(matrix);
        });

        public ICommand EdgeDetection => new RelayCommand(() =>
        {
            double[,] matrix = new double[,] { { 0, -1, 0 },
                                               { -1, 4, -1 },
                                               { 0, -1, 0 } };

            EditedImage = (OriginalImage.Clone() as BitmapSource).EdgeDetectionFilter(matrix);
        });

        public ICommand SobelFilter => new RelayCommand(() =>
        {
            double[,] matrixX = new double[,] { { -1, 0, 1 },
                                                { -2, 0, 2 },
                                                { -1, 0, 1 } };

            double[,] matrixY = new double[,] { { -1, -2, -1 },
                                                { 0, 0, 0 },
                                                { 1, 2, 1 } };

            EditedImage = (OriginalImage.Clone() as BitmapSource).SobelFilter(matrixX, matrixY);
        });

        public ICommand RobertsCross => new RelayCommand(() =>
        {
            double[,] matrixX = new double[,] { { 1, 0 },
                                                { 0, -1 } };

            double[,] matrixY = new double[,] { { 0, 1 },
                                                { -1, 0 } };

            EditedImage = (OriginalImage.Clone() as BitmapSource).RobertsCrossFilter(matrixX, matrixY);
        });

        #endregion

        private void Reset()
        {
            IsGaussianFilterEnabled = true;
            IsSharpenFilterEnabled = true;
            IsEdgeDetectionEnabled = true;
            IsSobelFilterEnabled = true;
            IsRobertsCrossEnabled = true;
            IsResetEnabled = true;
        }
    }
}
