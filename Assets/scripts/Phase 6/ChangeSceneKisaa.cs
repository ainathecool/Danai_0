using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneKisaa : MonoBehaviour
{
    public void MoveToScene()
    {
        SceneManager.LoadScene("VowelScreen2");
    }
    public void MoveToScene2()
    {
        SceneManager.LoadScene("VowelScreen3");
    }

}