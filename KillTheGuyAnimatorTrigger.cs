using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTheGuyAnimatorTrigger : MonoBehaviour
{
    private Animator animator;
    private HeatController[] _heatControllers;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _heatControllers = FindObjectsOfType<HeatController>();
        StartCoroutine(WaitForPress());
    }
    

    private IEnumerator WaitForPress()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.P));
        animator.SetTrigger("Kill");
        yield return new WaitForSeconds(2f);
        ActivateAllHeatDowngrades();
    }

    private void ActivateAllHeatDowngrades()
    {
        foreach (var heatController in _heatControllers)
            StartCoroutine(heatController.DecreaseHeat());
    }
}

