using Sources.Logic.Effect;
using UnityEngine;

namespace Sources.Spawner
{
    public class EffectSpawner : Spawner<EffectExplosion>
    {
        public EffectExplosion GetEffect() => GetObject();
    }
}