using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ParticleEffectConfig
{
    public ParticleSystem particleSystem;
    public float minSize = 0.1f;
    public float maxSize = 1.0f;
    public Color startColor = Color.red;
}
public class ParticleEffectsController : MonoBehaviour
{
    [SerializeField]
    private List<ParticleEffectConfig> effectConfigs;

    private Dictionary<string, ParticleEffectConfig> effects = new Dictionary<string, ParticleEffectConfig>();

    private void Start()
    {
        //Populating the dictionary
        foreach(var config in effectConfigs)
        {
            effects[config.particleSystem.gameObject.name] = config; //Uses the gameObject as the key
        }
    }
    public void TriggerEffect(string effectName)
    {
       if (effects.TryGetValue(effectName, out ParticleEffectConfig effect))
        {
            var main = effect.particleSystem.main;
            main.startSize = Random.Range(effect.minSize, effect.maxSize);
            main.startColor = new ParticleSystem.MinMaxGradient(effect.startColor);
            effect.particleSystem.Play();
        }
    }

    //Check if an effect is available in the controller
    public bool HasEffect(string effectName)
    {
        return effects.ContainsKey(effectName);
    }
}
