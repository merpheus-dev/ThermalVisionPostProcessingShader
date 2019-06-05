using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTheGuy : MonoBehaviour
{
    private Animator Animator;
    void Start()
    {
        Animator = GetComponent<Animator>();
        StartCoroutine(WaitForPress());
    }

    private IEnumerator WaitForPress()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.P));
        Animator.SetTrigger("Kill");
        yield return new WaitForSeconds(2f);
        ActivateAllHeatDowngrades();
    }

    private void ActivateAllHeatDowngrades()
    {
        foreach (var ht in FindObjectsOfType<Heat>())
            StartCoroutine(ht.HeatWait());
    }
}

