using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopsUpEffect : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    private float fadeRate = 0.2f;
    float t = 0;
    float startingYPos;
    void Start()
    {
        startingYPos = transform.position.y;
        StartCoroutine(FadingEffect());
    }

    void Update()
    {
        t += Time.deltaTime;
        float lerpValue = Mathf.Lerp(startingYPos, startingYPos + 1.5f, t);
        gameObject.transform.position = new Vector3(transform.position.x, lerpValue, transform.position.z);
    }
    IEnumerator FadingEffect()
    {
        while(canvasGroup.alpha > 0)
        {
            yield return new WaitForSeconds(0.1f);
            canvasGroup.alpha -= fadeRate;
        }
        Destroy(gameObject);
    }
}
