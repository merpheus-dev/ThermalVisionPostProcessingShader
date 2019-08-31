using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatController : MonoBehaviour
{
    public IEnumerator DecreaseHeat()
    {
        var slide = 0f;
        while (slide <1f)
        {
            slide += Time.deltaTime*.1f;
            var materials = GetComponent<SkinnedMeshRenderer>().materials;
            materials[0].SetColor("_MainColor", Color.Lerp(Color.white, Color.black, slide));
            GetComponent<SkinnedMeshRenderer>().materials = materials;
            yield return null;
        }
    }
}
