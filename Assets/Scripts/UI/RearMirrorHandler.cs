using System;
using System.Collections;
using System.Linq;
using MyBox;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace UI
{
    public class RearMirrorHandler : MonoBehaviour
    {
        [SerializeField] private ScoreHandler scoreHandler;
        
        public GameObjectContainer[] stateObjects;
        
        public UnityEvent<RearMirrorState> OnStateChanged;

        [SerializeField, ReadOnly] private GameObjectContainer currentState;
        

        private Coroutine _stateChangeRoutine;

        private void Start()
        {
            currentState = stateObjects.FirstOrDefault(x => x.state == RearMirrorState.Default);
            scoreHandler.onPlanetHitCorrect.AddListener( () =>
            {
                var rng = Random.value;
                if (rng > 0.5f)
                {
                    ChangeState(RearMirrorState.Happy);
                }
                else
                {
                    ChangeState(RearMirrorState.Drinking);
                }
            });
            scoreHandler.onPlanetHitWrong.AddListener(()=>ChangeState(RearMirrorState.Sad));
            currentState.Activate();
        }

        public void ChangeState(RearMirrorState state)
        {
            if (_stateChangeRoutine != null) return;
            currentState.Deactivate();
            currentState = stateObjects.FirstOrDefault(x => x.state == state);
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
            OnStateChanged?.Invoke(state);
        }

        private void StateCleanup()
        {
            _stateChangeRoutine = null;
        }

        private IEnumerator DefaultStateRoutine()
        {
            currentState.Activate();
            yield return null;
            StateCleanup();
        }

        private IEnumerator HappyStateRoutine()
        {
            currentState.Activate();
            yield return new WaitForSeconds(2);
            StateCleanup();
            ChangeState(RearMirrorState.Default);
        }

        private IEnumerator SadStateRoutine()
        {
            currentState.Activate();
            yield return new WaitForSeconds(2);
            StateCleanup();
            ChangeState(RearMirrorState.Default);
        }

        private IEnumerator DrinkingStateRoutine()
        {
            currentState.Activate();
            yield return new WaitForSeconds(2);
            StateCleanup();
            ChangeState(RearMirrorState.Default);
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
