using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void loadLevel_1()
    {
        StartCoroutine(loadLevel(2));
    }

    IEnumerator loadLevel(int sceneIndex)
    {
        animator.SetTrigger("start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneIndex);
    }
}
