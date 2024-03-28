
using UnityEngine;
using UnityEngine.SceneManagement;

public class Phase1MIRT : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      
        CheckPhase01MIRT(); 
       
    }
    public void CheckPhase01MIRT()
    {
        float accuracy = PlayerPrefs.GetFloat("Accuracy");
        if(accuracy == 100)
        {
            SceneManager.LoadScene("WelcometoGames"); //yaani k phase 2 unlock kardain. 
        }
    }
}
