using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fading : MonoBehaviour {

    public static Fading fading;

    public Image fadeImage;

    void Awake()
    {
        fading = this;
    }

    public IEnumerator Fade()
    {
        float percent = 0f;
        float fadeSpeed = 2f;

        while(percent <= 1)
        {
            percent += fadeSpeed * Time.deltaTime;

            fadeImage.color = Color.Lerp(Color.black, Color.clear, percent);

            yield return null;
        }
    }
}
