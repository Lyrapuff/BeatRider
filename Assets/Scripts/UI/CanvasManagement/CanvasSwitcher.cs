using System.Collections.Generic;
using System.Linq;
using General.Behaviours;
using General.UI.CanvasManagement;
using UnityEngine;

namespace UI.CanvasManagement
{
    public class CanvasSwitcher : ExtendedBehaviour, ICanvasSwitcher
    {
        [SerializeField] private string _defaultCanvas;
        
        private readonly List<SwitchableCanvas> _canvases = new List<SwitchableCanvas>();
        private SwitchableCanvas _currentCanvas;

        private float _lastOpenTime;
        private string _openedName;

        private void Awake()
        {
            foreach (Transform child in transform)
            {
                SwitchableCanvas canvas = child.GetComponent<SwitchableCanvas>();

                if (canvas != null)
                {
                    if (canvas.Name == _defaultCanvas)
                    {
                        canvas.Open();
                        _currentCanvas = canvas;
                        _openedName = canvas.Name;
                    }
                    else
                    {
                        canvas.CloseHard();
                    }

                    _canvases.Add(canvas);
                }
            }
        }

        public void Open(string canvasName)
        {
            if (canvasName == _openedName)
            {
                return;
            }
            
            if (Time.time - _lastOpenTime <= 0.25f)
            {
                return;
            }
            
            SwitchableCanvas canvas = _canvases.FirstOrDefault(c => c.Name == canvasName);

            if (canvas != null)
            {
                _openedName = canvasName;
                
                _lastOpenTime = Time.time;
                _currentCanvas.Close();
                canvas.Open();
                _currentCanvas = canvas;
            }
        }
    }
}