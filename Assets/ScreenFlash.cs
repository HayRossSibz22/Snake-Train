using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    public float fadeInDuration = 0.1f;
    public float fadeOutDuration = 0.1f;
    private Image panelImage;

    private void Start()
    {
        panelImage = GetComponent<Image>();
    }

    public void FlashGreen()
    {
        StartCoroutine(Flash(Color.green));
    }

    public void FlashRed()
    {
        StartCoroutine(Flash(Color.red));
    }

    private IEnumerator Flash(Color flashColor)
    {
        // Fade in
        float elapsedTime = 0;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 0.5f, elapsedTime / fadeInDuration);
            panelImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha);
            yield return null;
        }

        // Fade out
        elapsedTime = 0;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0.5f, 0, elapsedTime / fadeOutDuration);
            panelImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha);
            yield return null;
        }

        // Reset color to transparent
        panelImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0f);
    }
}