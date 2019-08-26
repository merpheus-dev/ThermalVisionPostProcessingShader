using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Serialization;

public class ActivateGoggles : MonoBehaviour
{
    public Renderer[] HotObjects;
    public LayerMask Cold;
    public LayerMask Hot;
    public GameObject PostProcessObject;

    [SerializeField,FormerlySerializedAs("Material")]
    private Material _material;

    private Material SkyboxMaterial;

    private Dictionary<Renderer, Material> HotObjectMaterialCache;
    private bool enabled = false;
    private void Start()
    {
        HotObjectMaterialCache = new Dictionary<Renderer, Material>();
        StartCoroutine(WaitForActivation());
    }

    private IEnumerator WaitForActivation()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Alpha1) && !enabled);
        PostProcessObject.SetActive(true);
        var renderers = FindObjectsOfType<Renderer>().ToList().Except(HotObjects);
        foreach (var renderer in renderers)
            renderer.gameObject.layer = 10;

        _material.SetColor("_MainColor", Color.white);
        foreach (var hotObject in HotObjects)
        {
            HotObjectMaterialCache.Add(hotObject, hotObject.material);
            hotObject.gameObject.layer = 9;
            hotObject.material = _material;
        }

        SkyboxMaterial = RenderSettings.skybox;
        RenderSettings.skybox = null;
        enabled = true;
        yield return WaitForDeactivation();

    }

    private IEnumerator WaitForDeactivation()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Alpha2) && enabled);
        PostProcessObject.SetActive(false);
        var renderers = FindObjectsOfType<Renderer>().ToList().Except(HotObjects);
        foreach (var renderer in renderers)
            renderer.gameObject.layer = 0;

        _material.SetColor("_MainColor", Color.white);
        foreach (var hotObject in HotObjects)
        {
            hotObject.gameObject.layer = 0;
            hotObject.material = HotObjectMaterialCache[hotObject];
        }

        HotObjectMaterialCache.Clear();
        enabled = false;
        RenderSettings.skybox = SkyboxMaterial;

        yield return WaitForActivation();
    }
}
