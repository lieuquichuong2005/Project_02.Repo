using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneEffect : MonoBehaviour
{
    public TMP_Text nameAreaText;

    private void Awake()
    {
        nameAreaText.text = PlayerMovement.currentScene;
        StartCoroutine(RunLoadingSceneEffect());
    }
    IEnumerator RunLoadingSceneEffect()
    {
        yield return new WaitForSeconds(2);
        this.gameObject.SetActive(false);
    }    
}
