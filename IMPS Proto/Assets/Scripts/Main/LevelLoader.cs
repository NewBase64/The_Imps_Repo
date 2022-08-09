using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Animator transition;
    [SerializeField] int transitionTimer;

    public IEnumerator Load(int scene)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTimer);
        SceneManager.LoadScene(scene);
    }
}
