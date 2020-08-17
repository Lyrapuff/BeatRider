using General.Behaviours;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    [RequireComponent(typeof(Text))]
    public class RepeatCount : ExtendedBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();

            RepetitiveGame.OnRepeat += HandleRepeat;
        }

        private void HandleRepeat(int count)
        {
            _text.text = "x" + count;
        }
    }
}