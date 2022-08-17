using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillBrain : MonoBehaviour
{
    public GameObject countdownCanvas;
    public TMP_Text roomText;
    

    private void OnDestroy()
    {
        roomText.text = "Escape";
        
    }
}
