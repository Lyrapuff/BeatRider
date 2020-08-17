using General.Behaviours;
using General.Services.Back;
using Game.Services;
using Game.Services.Implementations;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class AnimatorStateButton : ExtendedBehaviour
{
    [SerializeField] private string _stateName;
    [SerializeField] private InputField _focus;

    private BackService _back;
    private Animator _animator;
    private int _hash;
    private bool _noRemoving;

    private void Awake()
    {
        _back = Toolbox.Instance.GetService<BackService>();
        
        _animator = GetComponent<Animator>();
        _hash = Animator.StringToHash(_stateName);
    }

    public void SetState(bool state)
    {
        _animator.SetBool(_hash, state);

        if (state)
        {
            _back.Add(() =>
            {
                _noRemoving = true;
                SetState(false);
            });

            _noRemoving = false;
            
            _focus.Select();
            _focus.ActivateInputField();
        }
        else if(!_noRemoving)
        {
            _back.RemoveLast();
        }
    }
}
