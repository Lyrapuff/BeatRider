using General.Behaviours;
using General.UI.Popups.CanvasManagement;
using Game.Services;
using Game.Services.Implementations;
using UnityEngine;

namespace UI.CanvasManagement
{
    public class OpenCanvasOnBack : ExtendedBehaviour
    {
        [SerializeField] private string _canvasName;

        private BackService _back;
        private ICanvasSwitcher _canvasSwitcher;

        private void Awake()
        {
            _back = Toolbox.Instance.GetService<BackService>();
            _canvasSwitcher = FindComponentOfInterface<ICanvasSwitcher, NullCanvasSwitcher>();
        }

        private void OnEnable()
        {
            _back.Set(() =>
            {
                _canvasSwitcher.Open(_canvasName);
            });
        }
    }
}