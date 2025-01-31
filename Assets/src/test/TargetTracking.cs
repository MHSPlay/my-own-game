using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class TargetTracking : MonoBehaviour
{
    public Rig Rig;
    public float transitionSpeed = 4f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(ChangeWeight(1f));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(ChangeWeight(0f));
        }
    }

    private IEnumerator ChangeWeight(float targetWeight)
    {
        float currentWeight = Rig.weight;
        while (Mathf.Abs(currentWeight - targetWeight) > 0.01f)
        {
            currentWeight = Mathf.Lerp(currentWeight, targetWeight, Time.deltaTime * transitionSpeed);
            Rig.weight = currentWeight;
            yield return null;
        }

        Rig.weight = targetWeight;
    }

}
