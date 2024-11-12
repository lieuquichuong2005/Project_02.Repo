using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class LoadingTextEffect : MonoBehaviour
{
    public TMP_Text loadingText; 
    private string baseText = "Loading";
    private int dotCount = 0;
    private float delay = 0.5f;

    private void Start()
    {
        StartCoroutine(UpdateLoadingText());
    }

    private IEnumerator UpdateLoadingText()
    {
        while (true)
        {
            loadingText.text = baseText + new string('.', dotCount);
            dotCount = (dotCount + 1) % 6; 
            yield return new WaitForSeconds(delay);
        }
    }
}