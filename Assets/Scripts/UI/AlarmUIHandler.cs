using System;
using System.Collections;
using Shooting;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AlarmUIHandler : MonoBehaviour
    {
        [SerializeField] private ProjectilesHandler handler;
        [SerializeField] private Image[] toActivate;
        [SerializeField] private Image[] toDeactivate;
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
            if (_animRoutine != null) return;
            _animRoutine = StartCoroutine(AlarmUI());
        }
        
        private IEnumerator AlarmUI()
        {
            foreach (var obj in toDeactivate)
            {
                obj.enabled = false;
            }

            foreach (var obj in toActivate)
            {
                obj.enabled = true;
            }

            yield return new WaitForSeconds(0.3f);
            
            foreach (var obj in toActivate)
            {
                obj.enabled = false;
            }
            foreach (var obj in toDeactivate)
            {
                obj.enabled = true;
            }

            _animRoutine = null;
        }
    }
}
