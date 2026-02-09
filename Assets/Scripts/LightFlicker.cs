using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour
{
    private Light spotlight;

    public int minFlickers = 2;
    public int maxFlickers = 5;
    public float flickerSpeed = 0.1f;

    public float minWaitBetweenBursts = 2.0f;
    public float maxWaitBetweenBursts = 5.0f;

    void Start()
    {
        spotlight = GetComponent<Light>();
        StartCoroutine(BurstRoutine());
    }

    IEnumerator BurstRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitBetweenBursts, maxWaitBetweenBursts));

            int flickersInThisBurst = Random.Range(minFlickers, maxFlickers);

            for (int i = 0; i < flickersInThisBurst; i++)
            {
                spotlight.enabled = !spotlight.enabled;
                yield return new WaitForSeconds(flickerSpeed);

                spotlight.enabled = !spotlight.enabled;
                yield return new WaitForSeconds(flickerSpeed);
            }

            spotlight.enabled = true;
        }
    }
}