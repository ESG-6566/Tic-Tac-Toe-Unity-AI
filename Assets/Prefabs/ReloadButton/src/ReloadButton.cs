using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadButton : MonoBehaviour
{
    public void Play_again(){
        Debug.Log("clicked");
        SceneManager.LoadScene("SampleScene");
    }
}
