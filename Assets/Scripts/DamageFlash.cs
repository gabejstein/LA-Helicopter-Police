using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    MeshRenderer[] renderers;
    Material[] originalMaterials;
    float flashTime = 0.08f;
    [SerializeField] Material flashMaterial;

    private void Start()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();

        if(renderers==null)
            renderers = GetComponents<MeshRenderer>();

        originalMaterials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
            originalMaterials[i] = renderers[i].material;

    }

    public void StartFlash()
    {
        StartCoroutine(Flash());
    }

    public void StopFlash()
    {
        StopAllCoroutines();
    }

    IEnumerator Flash()
    {
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material = flashMaterial;

        yield return new WaitForSeconds(flashTime);

        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material = originalMaterials[i];

        yield return null;
    }
    
}
