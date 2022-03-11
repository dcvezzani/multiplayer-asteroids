using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustEffect : MonoBehaviour
{

    public ParticleSystem [] particleSystems;
    public SpriteRenderer sr;
    public bool isShowingEffect = false;

    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>(true);
        particleSystems = GetComponentsInChildren<ParticleSystem>(true);
    }

    public void ShowEffect()
    {
        isShowingEffect = true;
        sr.gameObject.SetActive(true);
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Play();
        }
    }

    public void HideEffect()
    {
        isShowingEffect = false;
        sr.gameObject.SetActive(false);
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Stop();
        }
    }
}
