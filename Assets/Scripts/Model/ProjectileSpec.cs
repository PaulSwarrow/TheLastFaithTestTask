using System;

namespace DefaultNamespace.Model
{
    [Serializable]
    public struct ProjectileSpec
    {
        public int Damage;
        public int Velocity;
        public int Lifespan;
        public float Radius;
    }
}