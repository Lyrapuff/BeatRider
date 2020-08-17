using System.Collections;
using General.Behaviours;
using General.Services.GameStatus;
using Game.Services;
using Game.Services.Implementations;
using UnityEngine;

namespace Game.Camera
{
    [RequireComponent(typeof(FollowPlayerCamera))]
    public class CameraSwitcher : ExtendedBehaviour
    {
        [Header("In-game")]
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private Vector3 _startRotation;
        [Header("In-menu")]
        [SerializeField] private Vector3 _endPosition;
        [SerializeField] private Vector3 _endRotation;
        [Header("Common")]
        [SerializeField] private float _speed;
        
        private GameStatusService _gameStatus;
        private FollowPlayerCamera _followPlayerCamera;

        private void Awake()
        {
            _gameStatus = Toolbox.Instance.GetService<GameStatusService>();
            
            _followPlayerCamera = GetComponent<FollowPlayerCamera>();

            _gameStatus.OnChanged += HandleGameStatusChanged;
        }

        private void HandleGameStatusChanged(GameStatusChangeType changeType, object dataOverload)
        {
            switch (changeType)
            {
                case GameStatusChangeType.Started:
                    StartCoroutine(StartAnimation());
                    break;
                case GameStatusChangeType.Ended:
                    StartCoroutine(EndAnimation());
                    break;
            }
        }

        private IEnumerator StartAnimation()
        {
            float time = 0f;

            Quaternion qN = transform.rotation;
            Quaternion qT = Quaternion.Euler(_startRotation);

            Vector3 pN = transform.position;
            Vector3 pT = _startPosition;
            
            while (time <= 1f)
            {
                transform.position = Vector3.Lerp(pN, pT, time);
                transform.rotation = Quaternion.Lerp(qN, qT, time);
                
                time += Time.fixedDeltaTime * _speed;
                yield return new WaitForFixedUpdate();
            }
            
            _followPlayerCamera.enabled = true;
            _followPlayerCamera.Offset = pT;
        }

        private IEnumerator EndAnimation()
        {
            _followPlayerCamera.enabled = false;
            
            float time = 0f;

            Quaternion qN = transform.rotation;
            Quaternion qT = Quaternion.Euler(_endRotation);

            Vector3 pN = transform.position;
            Vector3 pT = _endPosition;
            
            while (time <= 1f)
            {
                transform.position = Vector3.Lerp(pN, pT, time);
                transform.rotation = Quaternion.Lerp(qN, qT, time);
                
                time += Time.fixedDeltaTime * _speed;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}