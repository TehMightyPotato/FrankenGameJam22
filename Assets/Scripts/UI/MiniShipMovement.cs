using System;
using Extensions;
using UnityEngine;

namespace UI
{
    public class MiniShipMovement : MonoBehaviour
    {
        [SerializeField] 
        private RectTransform shipTransform;

        [SerializeField] private RectTransform startTransform;
        
        [SerializeField] private RectTransform endTransform;

        [SerializeField] private GameTick ticker;
        private void Start()
        {
            ticker.gameTick.AddListener(Scale);
        }

        private void Scale(float t)
        {
            var x = t.Map(0, 1, startTransform.position.x, endTransform.position.x);
            shipTransform.position = new Vector3(x, shipTransform.position.y, shipTransform.position.z);

        }
    }
}
