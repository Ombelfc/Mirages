﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Mirages.Binarizations;
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
    public class BinarizationViewModel : ViewModelBase
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

        private int thresholdValue;

        public int ThresholdValue
        {
            get => thresholdValue;
            set
            {
                thresholdValue = value;
                RaisePropertyChanged("ThresholdValue");
            }
        }

        #endregion

        #region IsEnabled Booleans

        private bool isGthresholdEnabled;

        public bool IsGthresholdEnabled
        {
            get => isGthresholdEnabled;
            set
            {
                isGthresholdEnabled = value;
                RaisePropertyChanged("IsGthresholdEnabled");
            }
        }

        private bool isHthresholdEnabled;

        public bool IsHthresholdEnabled
        {
            get => isHthresholdEnabled;
            set
            {
                isHthresholdEnabled = value;
                RaisePropertyChanged("IsHthresholdEnabled");
            }
        }

        private bool isLthresholdEnabled;

        public bool IsLthresholdEnabled
        {
            get => isLthresholdEnabled;
            set
            {
                isLthresholdEnabled = value;
                RaisePropertyChanged("IsLthresholdEnabled");
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
                IsGthresholdEnabled = true;
                IsHthresholdEnabled = true;
                IsLthresholdEnabled = true;
                IsResetEnabled = true;
            }
        });

        public ICommand ResetImage => new RelayCommand(() =>
        {
            EditedImage = OriginalImage.Clone();
            ThresholdValue = 0;
        });

        public ICommand Gthreshold => new RelayCommand(() =>
        {
            if (ThresholdValue > 1)
                EditedImage = (OriginalImage.Clone() as BitmapSource).ToGrayScale();
                EditedImage = (EditedImage as BitmapSource).ToGThreshold(ThresholdValue);
        });

        public ICommand Hthreshold => new RelayCommand(() =>
        {
            if (ThresholdValue > 1)
                EditedImage = (OriginalImage.Clone() as BitmapSource).ToGrayScale();
                EditedImage = (EditedImage as BitmapSource).ToHThreshold(ThresholdValue);
        });

        public ICommand Lthreshold => new RelayCommand(() =>
        {
            if (ThresholdValue > 1)
                EditedImage = (OriginalImage.Clone() as BitmapSource).ToGrayScale();
                EditedImage = (EditedImage as BitmapSource).Brensen(ThresholdValue);
        });
    }
}
