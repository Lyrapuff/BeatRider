using System;
using General.Behaviours;
using General.Storage;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    [RequireComponent(typeof(Text))]
    public class PointsDisplay : ExtendedBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
            HandleChanged(GameDataStorage.Instance.GetData());
        }

        private void OnEnable()
        {
            GameDataStorage.Instance.OnChanged += HandleChanged;
        }

        private void OnDisable()
        {
            GameDataStorage.Instance.OnChanged -= HandleChanged;
        }

        private void HandleChanged(GameData gameData)
        {
            _text.text = gameData.Points.ToString();
        }
    }
}