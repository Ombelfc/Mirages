using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Mirages.ElementaryAlgorithms;
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
    public class ElementaryViewModel : ViewModelBase
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

        private int brightnessValue;

        public int BrightnessValue
        {
            get => brightnessValue;
            set
            {
                brightnessValue = value;
                RaisePropertyChanged("BrightnessValue");
            }
        }

        private int contrastValue;
        
        public int ContrastValue
        {
            get => contrastValue;
            set
            {
                contrastValue = value;
                RaisePropertyChanged("ContrastValue");
            }
        }

        #endregion

        #region IsEnabled Booleans

        private bool isGrayscaleEnabled = false;

        public bool IsGrayscaleEnabled
        {
            get => isGrayscaleEnabled;
            set
            {
                isGrayscaleEnabled = value;
                RaisePropertyChanged("IsGrayscaleEnabled");
            }
        }

        private bool isInversionEnabled = false;

        public bool IsInversionEnabled
        {
            get => isInversionEnabled;
            set
            {
                isInversionEnabled = value;
                RaisePropertyChanged("IsInversionEnabled");
            }
        }

        private bool isBrightnessEnabled = false;

        public bool IsBrightnessEnabled
        {
            get => isBrightnessEnabled;
            set
            {
                isBrightnessEnabled = value;
                RaisePropertyChanged("IsBrightnessEnabled");
            }
        }

        private bool isContrastEnabled = false;

        public bool IsContrastEnabled
        {
            get => isContrastEnabled;
            set
            {
                isContrastEnabled = value;
                RaisePropertyChanged("IsContrastEnabled");
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

            if(openFileDialog.ShowDialog() == true)
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
            BrightnessValue = 0;
            ContrastValue = 0;
        });

        public ICommand GrayScale => new RelayCommand(() =>
        {
            EditedImage = (OriginalImage.Clone() as BitmapSource).ToGrayScale();
        });

        public ICommand Inversion => new RelayCommand(() =>
        {
            EditedImage = (OriginalImage.Clone() as BitmapSource).Invert();
        });

        public ICommand Brightness => new RelayCommand(() =>
        {
            EditedImage = (OriginalImage.Clone() as BitmapSource).InhanceBrightness(BrightnessValue);
        });

        public ICommand Contrast => new RelayCommand(() =>
        {
            if(ContrastValue > 1) 
                EditedImage = (OriginalImage.Clone() as BitmapSource).InhanceContrast(ContrastValue);
        });

        #endregion

        private void Reset()
        {
            IsGrayscaleEnabled = true;
            IsInversionEnabled = true;
            IsBrightnessEnabled = true;
            IsContrastEnabled = true;
            IsResetEnabled = true;

            BrightnessValue = 0;
            ContrastValue = 0;
        }
    }
}
