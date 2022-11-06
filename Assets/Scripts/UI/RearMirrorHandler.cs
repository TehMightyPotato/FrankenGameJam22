using System;
using System.Collections;
using MyBox;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class RearMirrorHandler : MonoBehaviour
    {
        [Separator("Default State")] public GameObject defaultStateObj;
        
        public UnityEvent<RearMirrorState> OnStateChanged;

        [SerializeField] private RearMirrorState currentState;

        private Coroutine _stateChangeRoutine;
        
        public void ChangeState(RearMirrorState state)
        {
            if (_stateChangeRoutine != null) return;
            switch (state)
            {
                case RearMirrorState.Default:
                    _stateChangeRoutine = StartCoroutine(DefaultStateRoutine());
                    break;
                case RearMirrorState.Happy:
                    _stateChangeRoutine = StartCoroutine(HappyStateRoutine());
                    break;
                case RearMirrorState.Sad:
                    _stateChangeRoutine = StartCoroutine(SadStateRoutine());
                    break;
                case RearMirrorState.Drinking:
                    _stateChangeRoutine = StartCoroutine(DrinkingStateRoutine());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }

            currentState = state;
            OnStateChanged?.Invoke(state);
        }

        private void StateCleanup()
        {
            _stateChangeRoutine = null;
        }

        private IEnumerator DefaultStateRoutine()
        {
            
        }

        private IEnumerator HappyStateRoutine()
        {
            
        }

        private IEnumerator SadStateRoutine()
        {
            
        }

        private IEnumerator DrinkingStateRoutine()
        {
            
        }
    }

    public enum RearMirrorState
    {
        Default,
        Happy,
        Sad,
        Drinking
    }
}
