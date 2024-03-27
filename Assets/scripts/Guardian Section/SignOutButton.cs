using UnityEngine;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;

public class SignOutButton : MonoBehaviour
{
    public TextMeshProUGUI LogoutError;
    public void SignOutOnClick()
    {
       
    // Get the default instance of FirebaseAuth
    FirebaseAuth auth = FirebaseAuth.DefaultInstance;

        // Sign out the current user
        auth.SignOut();

         
        SceneManager.LoadScene("GuardianLogin");
 
        
    }
}
