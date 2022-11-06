using System;
using System.Collections;
using UnityEngine;

namespace FSM.RearMirror
{
    [Serializable]
    public class DefaultState : AbstractState
    {
        [SerializeField] private MonoBehaviour _behaviour;
        [SerializeField] private GameObject[] _objects;
        private Coroutine stateRoutine;
        

        public override void StateEnter()
        {
            foreach (var gameObject in _objects)
            {
                gameObject.SetActive(true);
            }
        }

        private IEnumerator StateUpdate()
        {
            yield return null;
        }

        public override void StateExit()
        {
            foreach (var gameObject in _objects)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
