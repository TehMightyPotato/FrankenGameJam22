using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Planets;
using Planets.Needs;
using Shooting;
using UnityEngine;
using UnityEngine.Events;

public sealed class ProjectileEnteredEventArgs
{
    public Planet Planet { get; }
    public Projectile Projectile { get; }

    public ProjectileEnteredEventArgs(Planet planet, Projectile projectile)
    {
        Planet = planet;
        Projectile = projectile;
    }
}

[CreateAssetMenu(fileName = "PlanetHitHandler", menuName = "PlanetHitHandler")]
public class PlanetHitHandler : ScriptableObject
{
    public UnityEvent<ProjectileEnteredEventArgs> OnProjectileEntered;

    public void Init()
    {
        OnProjectileEntered.RemoveAllListeners();
    }

    public void ProjectileEntered(Planet planet, Projectile projectile)
    {
        OnProjectileEntered?.Invoke(new ProjectileEnteredEventArgs(planet, projectile));
    }
}