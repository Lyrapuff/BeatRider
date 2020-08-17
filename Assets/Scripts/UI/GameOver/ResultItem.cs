using UnityEngine;
using UnityEngine.UI;

namespace UI.GameOver
{
    public class ResultItem : MonoBehaviour
    {
        public Text Name => _name;
        public Text Score => _score;
        
        [SerializeField] private Text _name;
        [SerializeField] private Text _score;
    }
}