using System;
using System.Collections;
using Shooting;
using UnityEngine;

namespace UI
{
    public class AlarmUIHandler : MonoBehaviour
    {
        [SerializeField] private ProjectilesHandler handler;
        [SerializeField] private GameObject[] toActivate;
        [SerializeField] private GameObject[] toDeactivate;
        private Coroutine _animRoutine;

        private void Start()
        {
            handler.OnRowChanged.AddListener(x =>
            {
                Trigger();
            });
        }

        private void Trigger()
        {
            if (_animRoutine != null)
            {
                _animRoutine = StartCoroutine(AlarmUI());
            }
        }
        
        private IEnumerator AlarmUI()
        {
            foreach (var obj in toDeactivate)
            {
                obj.SetActive(false);
            }

            foreach (var obj in toActivate)
            {
                obj.SetActive(true);
            }

            yield return new WaitForSeconds(0.2f);
            
            foreach (var obj in toActivate)
            {
                obj.SetActive(false);
            }
            foreach (var obj in toDeactivate)
            {
                obj.SetActive(true);
            }

            _animRoutine = null;
        }
    }
}
