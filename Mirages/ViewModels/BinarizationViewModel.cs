using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Mirages.Binarizations;
using Mirages.ElementaryAlgorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Mirages.ViewModels
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

        private PointCollection histogramPoints;

        public PointCollection HistogramPoints
        {
            get => histogramPoints;
            set
            {
                histogramPoints = value;
                RaisePropertyChanged("HistogramPoints");
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

        private bool isHistogramEnabled = false;

        public bool IsHistogramEnabled
        {
            get => isHistogramEnabled;
            set
            {
                isHistogramEnabled = value;
                RaisePropertyChanged("IsHistogramEnabled");
            }
        }

        private bool isNHistogramEnabled = false;

        public bool IsNHistogramEnabled
        {
            get => isNHistogramEnabled;
            set
            {
                isNHistogramEnabled = value;
                RaisePropertyChanged("IsNHistogramEnabled");
            }
        }

        #endregion

        #region IsVisible Booleans

        private Visibility isOriginalImageVisible = Visibility.Visible;

        public Visibility IsOriginalImageVisible
        {
            get => isOriginalImageVisible;
            set
            {
                isOriginalImageVisible = value;
                RaisePropertyChanged("IsOriginalImageVisible");
            }
        }

        private Visibility isHistogramVisible = Visibility.Hidden;

        public Visibility IsHistogramVisible
        {
            get => isHistogramVisible;
            set
            {
                isHistogramVisible = value;
                RaisePropertyChanged("IsHistogramVisible");
            }
        }

        #endregion

        #region Commands

        public ICommand LoadImage => new RelayCommand(() =>
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Select a picture",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                                    "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                    "Portable Network Graphic (*.png)|*.png"
            };

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
            ThresholdValue = 0;

            IsOriginalImageVisible = Visibility.Visible;
            IsHistogramVisible = Visibility.Hidden;
        });

        public ICommand Gthreshold => new RelayCommand(() =>
        {
            if (ThresholdValue < 1) return;

            EditedImage = (OriginalImage.Clone() as BitmapSource).ToGrayScale();
            EditedImage = (EditedImage as BitmapSource).ToGThreshold(ThresholdValue);
        });

        public ICommand Hthreshold => new RelayCommand(() =>
        {
            if (ThresholdValue < 1) return;

            EditedImage = (OriginalImage.Clone() as BitmapSource).ToGrayScale();
            EditedImage = (EditedImage as BitmapSource).ToHThreshold(ThresholdValue);
        });

        public ICommand Lthreshold => new RelayCommand(() =>
        {
            if (ThresholdValue < 1) return;

            EditedImage = (OriginalImage.Clone() as BitmapSource).ToGrayScale();
            EditedImage = (EditedImage as BitmapSource).ToBernsen(ThresholdValue);
        });

        public ICommand Histogram => new RelayCommand(() =>
        {
            EditedImage = (OriginalImage.Clone() as BitmapSource).ToGrayScale();
            var points = (EditedImage as BitmapSource).GenerateHistogram();

            HistogramPoints = ConvertToPointCollection(points);
            IsOriginalImageVisible = Visibility.Hidden;
            IsHistogramVisible = Visibility.Visible;
        });

        public ICommand NHistogram => new RelayCommand(() =>
        {
            EditedImage = (OriginalImage.Clone() as BitmapSource).ToGrayScale();
            var points = (EditedImage as BitmapSource).GenerateNormalizeHistogram();

            EditedImage = points.Item2;
            HistogramPoints = ConvertToPointCollection(points.Item1);
            IsOriginalImageVisible = Visibility.Hidden;
            IsHistogramVisible = Visibility.Visible;
        });

        #endregion

        #region Helpers

        private void Reset()
        {
            IsGthresholdEnabled = true;
            IsHthresholdEnabled = true;
            IsLthresholdEnabled = true;
            IsHistogramEnabled = true;
            IsNHistogramEnabled = true;
            IsResetEnabled = true;

            ThresholdValue = 0;

            IsOriginalImageVisible = Visibility.Visible;
            IsHistogramVisible = Visibility.Hidden;
        }

        private PointCollection ConvertToPointCollection(int[] points)
        {
            int max = points.Max();
            var collection = new PointCollection
            {
                new Point(0, max)
            };

            for (int i = 0; i < points.Length; i++)
            {
                collection.Add(new Point(i, max - points[i]));
            }

            collection.Add(new Point(points.Length - 1, max));

            return collection;
        }

        #endregion
    }
}
