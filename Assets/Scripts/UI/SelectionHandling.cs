using System;
using Shooting;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SelectionHandling : MonoBehaviour
    {
        [SerializeField] private ProjectilesHandler handler;
        [SerializeField] private Image[] images;

        private void Start()
        {
            handler.OnRowChanged.AddListener(RowChanged);
        }

        private void RowChanged(int index)
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].sprite = handler.GetSelectedSprite(index, i);
            }
        }
    }
}
