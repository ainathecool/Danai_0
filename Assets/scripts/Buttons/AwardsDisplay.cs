using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AwardsDisplay : MonoBehaviour
{

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
 

    // Start is called before the first frame update
    void Start()
    {
       
        star1.gameObject.SetActive(false);
        star2.gameObject.SetActive(false);
        star3.gameObject.SetActive(false);
    }

    public void DisplayAwards()
    {
        float accuracy = PlayerPrefs.GetFloat("Accuracy");
        Debug.Log(accuracy);

        if (accuracy >= 30 && accuracy < 50)
        {
            star1.gameObject.SetActive(true);
        }
        else if(accuracy >= 50 && accuracy <=70)
        {
             star2.gameObject .SetActive(true);
        }
        else if(accuracy >= 71 && accuracy <= 100)
        {
             star3.gameObject .SetActive(true);
        }
    }

  
}
