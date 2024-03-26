using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase.Database;
public class LoadChildAvatar : MonoBehaviour
{

    public Button Avatar;// here the child avatar will be loaded

    private DatabaseReference databaseReference; //will be used to get logged in guardian's stuff

    // Start is called before the first frame update
    void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        Avatar.image.sprite = null; //image is null
        LoadAvatar();
    }

    public async void LoadAvatar()
    {
        string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        DataSnapshot dataSnapshot = await databaseReference.Child("childProfiles").Child(userId).GetValueAsync();

        if (dataSnapshot.Exists)
        {
            foreach (var childSnapshot in dataSnapshot.Children)
            {
                string avatarName = (string)childSnapshot.Child("Avatar").Value;
                Sprite avatarSprite = Resources.Load<Sprite>("Avatars/" + avatarName);
                Avatar.image.sprite = avatarSprite;
            }
        }


    }

}
