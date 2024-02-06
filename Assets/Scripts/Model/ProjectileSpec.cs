using System;

namespace DefaultNamespace.Model
{
    [Serializable]
    public struct ProjectileSpec
    {
        public int Damage;
        public float Velocity;
        public int Lifespan;
        public float Radius;
    }
}