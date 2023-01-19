using UnityEngine;

namespace Sources.Logic.Effect
{
    public class EffectExplosion : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _effect;

        public void Explode(Color color)
        {
            _effect.startColor = color;
            _effect.Play();
            Invoke(nameof(DisableEffect),2f);
        }

        public void DisableEffect()
        {
            gameObject.SetActive(false);
        }
    }
}