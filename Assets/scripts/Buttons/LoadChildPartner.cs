using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase.Database;
public class LoadChildPartner : MonoBehaviour
{

    public Button Partner;// here the child avatar will be loaded

    private DatabaseReference databaseReference; //will be used to get logged in guardian's stuff

    // Start is called before the first frame update
    void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        Partner.image.sprite = null; //image is null
        LoadPartner();
    }

    public async void LoadPartner()
    {
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        string childId = PlayerPrefs.GetString("LoggedInChild");
        DataSnapshot dataSnapshot = await databaseReference.Child("childProfiles").Child(userId).Child("profiles").Child(childId).GetValueAsync();

        if (dataSnapshot.Exists)
        {
            //  foreach (var childSnapshot in dataSnapshot.Children)
            {
                string partnerName = (string)dataSnapshot.Child("Partner").Value;
                Sprite partnerSprite = Resources.Load<Sprite>("Partners/" + partnerName);
                Partner.image.sprite = partnerSprite;
            }
        }


    }

}
