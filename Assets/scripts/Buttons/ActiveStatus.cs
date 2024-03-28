using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveStatus : MonoBehaviour
{
    public GameObject statusObj;
    public string status;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    public void ChangeStatus()
    {
        if(status == "true")
        {
            statusObj.SetActive(true);
        }
        else if (status == "false")
        {
            statusObj.SetActive(false);
            SceneManager.LoadScene("childHome");
        }
      
    }
}
