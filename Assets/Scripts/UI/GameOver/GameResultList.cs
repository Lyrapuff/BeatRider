using Game.GameTracker;
using SmallTail.Localization;
using UnityEngine;

namespace UI.GameOver
{
    public class GameResultList : MonoBehaviour
    {
        [SerializeField] private ResultTracker _resultTracker;

        private void OnEnable()
        {
            GameResult gameResult = _resultTracker.GameResult;

            if (gameResult != null)
            {
                SetResult(0, LocalizationService.GetValue("ui_gameover_coins"), gameResult.CoinCount.ToString());
                SetResult(1, LocalizationService.GetValue("ui_gameover_distance"), gameResult.DistanceDriven.ToString("0.00"));
                SetResult(2, LocalizationService.GetValue("ui_gameover_repeats"), gameResult.RepeatCount.ToString());
                SetResult(3, LocalizationService.GetValue("ui_gameover_firsttoday"), "200");
            }
        }

        private void SetResult(int index, string name, string score)
        {
            ResultItem resultItem = transform.GetChild(index)?.GetComponent<ResultItem>();

            if (resultItem != null)
            {
                resultItem.Name.text = name;
                resultItem.Score.text = score;
            }
        }
    }
}