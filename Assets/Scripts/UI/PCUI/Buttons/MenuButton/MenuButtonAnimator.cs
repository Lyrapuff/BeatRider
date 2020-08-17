using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.PCUI.Buttons.MenuButton
{
    [RequireComponent(typeof(RectTransform))]
    public class MenuButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Size")]
        [SerializeField] private VerticalLayoutGroup _layoutGroup;
        [SerializeField] private float _idleSpeed;
        [SerializeField] private float _hoverSpeed;
        [SerializeField] private Vector2 _idleSize;
        [SerializeField] private Vector2 _hoverSize;
        [Header("Shine")] 
        [SerializeField] private float _shineAmount;
        [SerializeField] private float _shineSpeed;
        [SerializeField] private Image _shinyImage;

        private Coroutine _animation;
        private RectTransform _rectTransform;
        private Material _cachedMaterial;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            
            _cachedMaterial = Instantiate(_shinyImage.material);
            _shinyImage.material = _cachedMaterial;

            _layoutGroup = GetComponentInParent<VerticalLayoutGroup>();
        }

        private void OnEnable()
        {
            OnPointerExit(null);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_animation != null)
            {
                StopCoroutine(_animation);
            }
            
            _animation = StartCoroutine(AnimateSize(_hoverSize, _hoverSpeed));
            
            _cachedMaterial.SetFloat("_ShineAmount", _shineAmount);
            _cachedMaterial.SetFloat("_ShineSpeed", _shineSpeed);
            _shinyImage.material = _cachedMaterial;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_animation != null)
            {
                StopCoroutine(_animation);
            }
            
            _animation = StartCoroutine(AnimateSize(_idleSize, _idleSpeed));
            
            _cachedMaterial.SetFloat("_ShineAmount", 0f);
            _shinyImage.material = _cachedMaterial;
        }

        private IEnumerator AnimateSize(Vector2 goalSize, float speed)
        {
            Vector2 size = _rectTransform.sizeDelta;
            float time = 0f;

            while (time <= 1f)
            {
                _rectTransform.sizeDelta = Vector2.Lerp(size, goalSize, time);
                
                _layoutGroup.CalculateLayoutInputHorizontal();
                _layoutGroup.CalculateLayoutInputVertical();
                _layoutGroup.SetLayoutHorizontal();
                _layoutGroup.SetLayoutVertical();
                
                time += Time.fixedDeltaTime * speed;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}