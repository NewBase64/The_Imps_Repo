using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] Animator credits;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene(0);
        }
        else if(Input.GetButtonDown("Submit"))
        {
            credits.speed = 5;
        }
        else if(Input.GetButtonUp("Submit"))
        {
            credits.speed = 1;
        }
    }
}
