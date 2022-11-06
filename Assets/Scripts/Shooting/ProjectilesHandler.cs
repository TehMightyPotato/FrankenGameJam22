using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Shooting
{
    [CreateAssetMenu(fileName = "ProjectilesHandler", menuName = "Projectile/Handler")]
    public class ProjectilesHandler : ScriptableObject
    {
        public List<ProjectileRow> projectiles;
        public UnityEvent<int> OnRowChanged;

        public void Init()
        {
            OnRowChanged.RemoveAllListeners();
        }
        public void RowChanged(int index)
        {
            OnRowChanged?.Invoke(index);
        }
        
        public GameObject GetSelectedPrefab(int row, int index)
        {
            return projectiles[row].projectiles[index].prefab;
        }

        public Sprite GetSelectedSprite(int row, int index)
        {
            return projectiles[row].projectiles[index].sprite;
        }
    }
}
