using GalaSoft.MvvmLight;
using Mirages.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Mirages.ViewModels
{
    public class SceneLoadingViewModel : ViewModelBase
    {
        private ImageSource scene;

        public ImageSource Scene
        {
            get => scene;
            set
            {
                scene = value;
                RaisePropertyChanged("Scene");
            }
        }

        #region Parameters

        private string pZoom;

        public string PZoom
        {
            get => pZoom;
            set
            {
                pZoom = value;
                RaisePropertyChanged("PZoom");
            }
        }

        private string pFPS;

        public string PFPS
        {
            get => pFPS;
            set
            {
                pFPS = value;
                RaisePropertyChanged("PFPS");
            }
        }

        private string pFPSlow;

        public string PFPSlow
        {
            get => pFPSlow;
            set
            {
                pFPSlow = value;
                RaisePropertyChanged("PFPSlow");
            }
        }

        private string pFPSHigh;

        public string PFPSHigh
        {
            get => pFPSHigh;
            set
            {
                pFPSHigh = value;
                RaisePropertyChanged("PFPSHigh");
            }
        }

        #endregion

        #region Positions

        private string pCamPosX;

        public string PCamPosX
        {
            get => pCamPosX;
            set
            {
                pCamPosX = value;
                RaisePropertyChanged("PCamPosX");
            }
        }

        private string pCamPosY;

        public string PCamPosY
        {
            get => pCamPosY;
            set
            {
                pCamPosY = value;
                RaisePropertyChanged("PCamPosY");
            }
        }

        private string pCamPosZ;

        public string PCamPosZ
        {
            get => pCamPosZ;
            set
            {
                pCamPosZ = value;
                RaisePropertyChanged("PCamPosZ");
            }
        }

        #endregion

        #region Rotations

        private string pCamRotX;

        public string PCamRotX
        {
            get => pCamRotX;
            set
            {
                pCamRotX = value;
                RaisePropertyChanged("PCamRotX");
            }
        }

        private string pCamRotY;

        public string PCamRotY
        {
            get => pCamRotY;
            set
            {
                pCamRotY = value;
                RaisePropertyChanged("PCamRotY");
            }
        }

        private string pCamRotZ;

        public string PCamRotZ
        {
            get => pCamRotZ;
            set
            {
                pCamRotZ = value;
                RaisePropertyChanged("PCamRotZ");
            }
        }

        #endregion

        public SceneLoadingViewModel()
        {
            SceneImporter.LoadSceneJsonFile(Path.Combine("Resources", "scene.unity.babylon"));
        }
    }
}
