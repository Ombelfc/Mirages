using _3DEngine.Algorithms;
using _3DEngine.Components;
using _3DEngine.Infrastructure;
using _3DEngine.Utilities;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Mirages.Utility;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace Mirages.ViewModels
{
    public class SceneLoadingViewModel : ViewModelBase
    {
        private BufferedBitmap bufferedBitmap;
        private Device device;
        private Scene scene;

        #region Binding Fields

        private ImageSource image;

        public ImageSource Image
        {
            get => image;
            set
            {
                image = value;
                RaisePropertyChanged("Image");
            }
        }

        private bool isCameraVisible = false;

        public bool IsCameraVisible
        {
            get => isCameraVisible;
            set
            {
                isCameraVisible = value;
                RaisePropertyChanged("IsCameraVisible");
            }
        }

        public RelayCommand LoadSceneCommand { get; private set; }

        #endregion

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
            SetData();

            LoadSceneCommand = new RelayCommand(OnLoadSceneCommand);

            Messenger.Default.Register<Key>(this, MessengerTokens.KeyPressed, k => OnKeyPressed(k));
            Messenger.Default.Register<int>(this, MessengerTokens.MouseWheenSpin, d => OnMouseWheel(d));
        }

        private void SetData()
        {
            bufferedBitmap = new BufferedBitmap(1355, 460);
            device = new Device(bufferedBitmap);

            scene = SceneImporter.LoadSceneJsonFile(Path.Combine("Resources", "scene.unity.babylon"));
            scene.Meshes.First(m => m.Name == "Plane").Color = _3DEngine.Utilities.Colors.DarkGrey;

            Image = bufferedBitmap.BitmapSource;
            SetCameraParameters(scene.Camera);

            //CompositionTarget.Rendering += CompositionTargetOnRendering;
            //IsCameraVisible = true;
        }

        private void CompositionTargetOnRendering(object sender, EventArgs e)
        {
            if (!IsCameraVisible)
                return;

            bufferedBitmap.Clear(_3DEngine.Utilities.Colors.Black.ToColor32());
            device.Render(scene);
            bufferedBitmap.Present();
        }

        #region ShortCuts Handles

        private void OnKeyPressed(Key k)
        {
            var step = 0.2f;
            var rot = 1f;

            switch (k)
            {
                case Key.W:
                    scene.Camera.Move(new Vector3(0, 0, step));
                    break;
                case Key.S:
                    scene.Camera.Move(new Vector3(0, 0, -step));
                    break;
                case Key.A:
                    scene.Camera.Move(new Vector3(-step, 0, 0));
                    break;
                case Key.D:
                    scene.Camera.Move(new Vector3(step, 0, 0));
                    break;
                case Key.E:
                    scene.Camera.Move(new Vector3(0, step, 0));
                    break;
                case Key.C:
                    scene.Camera.Move(new Vector3(0, -step, 0));
                    break;
                case Key.K:
                    scene.Camera.Rotate(Axis.Y, -rot);
                    break;
                case Key.OemSemicolon:
                    scene.Camera.Rotate(Axis.Y, rot);
                    break;
                case Key.I:
                    scene.Camera.Rotate(Axis.Z, rot);
                    break;
                case Key.P:
                    scene.Camera.Rotate(Axis.Z, -rot);
                    break;
                case Key.O:
                    scene.Camera.Rotate(Axis.X, rot);
                    break;
                case Key.L:
                    scene.Camera.Rotate(Axis.X, -rot);
                    break;
            }

            SetCameraParameters(scene.Camera);
        }

        private void OnMouseWheel(int d)
        {
            var steps = d / 120f;
            var angleDelta = steps * 5;

            scene.Camera.FieldOfView -= angleDelta;
        }

        #endregion

        #region Updating UI Parameters

        private void SetCameraParameters(Camera camera)
        {
            SetCoordinates(camera.Position);
            SetCoordinates(camera.Rotation);
            PZoom = Math.Round(camera.FieldOfView).ToString("0.0", CultureInfo.InvariantCulture);
        }

        private void SetCoordinates(Vector3 position)
        {
            PCamPosX = position.X.ToString("0.000", CultureInfo.InvariantCulture);
            PCamPosY = position.Y.ToString("0.000", CultureInfo.InvariantCulture);
            PCamPosZ = position.Z.ToString("0.000", CultureInfo.InvariantCulture);
        }

        private void SetCoordinates(Quaternion rotation)
        {
            PCamRotX = RadianToDegree(rotation.Yaw).ToString("0.000", CultureInfo.InvariantCulture);
            PCamRotY = RadianToDegree(rotation.Pitch).ToString("0.000", CultureInfo.InvariantCulture);
            PCamRotZ = RadianToDegree(rotation.Roll).ToString("0.000", CultureInfo.InvariantCulture);
        }

        private double RadianToDegree(double angle)
        {
            return Math.Round(angle * (180.0f / Math.PI));
        }

        #endregion

        private void OnLoadSceneCommand()
        {
            
        }
    }
}
