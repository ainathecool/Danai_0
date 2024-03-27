using UnityEngine;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;

public class ChangePassword : MonoBehaviour
{
    FirebaseAuth auth;
    public TextMeshProUGUI newPass;
    public TextMeshProUGUI confirmNewPass;
    // public TextMeshProUGUI email;
    public TextMeshProUGUI status;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void ResetPassword()
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        string newPassword = newPass.text;
        string confirmNewPassword = confirmNewPass.text;

        if (string.IsNullOrEmpty(newPassword))
        {
            status.text = "Please enter a password";
            status.color = Color.red;
        }
        else if (string.IsNullOrEmpty(newPassword))
        {
            status.text = "Please confirm password";
            status.color = Color.red;
        }
        else if (newPassword != confirmNewPassword)
        {
            status.text = "Passwords don't match";
            status.color = Color.red;
        }
        else if (newPassword.Length < 6)
        {
            status.text = "Passwords must be at least 6 characters long";
            status.color = Color.red;
        }
        else if (user != null)
        {
            user.UpdatePasswordAsync(newPassword).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdatePasswordAsync was canceled.");
                    status.text = "Cancelled";
                    status.color = Color.red;
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdatePasswordAsync encountered an error: " + task.Exception);
                    status.text = "Failed. Try Again!";
                    status.color = Color.red;
                    return;
                }

              
            });
            Debug.Log("Password updated successfully.");
            status.text = "Successfull!";
            status.color = Color.green;
            SceneManager.LoadScene("GuardianLogin");
        }
    }

    /*    public void ChangePasswordThroughEmail()
      {
          FirebaseUser user = auth.CurrentUser;

          string emailAddress = email.text;
          Debug.Log(emailAddress);
          if (user != null)
          {
              auth.SendPasswordResetEmailAsync(emailAddress).ContinueWith(task => {
                  if (task.IsCanceled)
                  {
                      Debug.LogError("SendPasswordResetEmailAsync was canceled.");
                      status.text = "Cancelled";
                      status.color = Color.red;
                      return;
                  }
                  if (task.IsFaulted)
                  {
                      Debug.LogError("SendPasswordResetEmailAsync encountered an error: " + task.Exception);
                      status.text = "Error. Try Again.";
                      status.color = Color.red;
                      return;
                  }

                  Debug.Log("Password reset email sent successfully.");
                  status.text = "Password Reset Email Sent Successfully.";
                  status.color = Color.green;
                  SceneManager.LoadScene("GuardianLogin");
              });
          }
      }

   
}
    */
}
