using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Correctbody : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip pickUpClip, dropClip;
    private bool dragging, placed;
    private Vector2 offset, originalPosition;
    private PuzzleSlot _slot;

    public GameObject draggedbody;
    public GameObject hintslot;

    void Awake()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (placed) return;
        if (!dragging) return;

        var mousePosition = GetMousePos();
        transform.position = mousePosition - offset;
    }

    void OnMouseDown()
    {
        dragging = true;
        source.PlayOneShot(pickUpClip);
        offset = GetMousePos() - (Vector2)transform.position;
    }

    void OnMouseUp()
    {
        if (Vector2.Distance(transform.position, hintslot.transform.position) < 1)
        {
            transform.position = hintslot.transform.position;
            source.PlayOneShot(dropClip);
            placed = true;

            StartCoroutine(LoadNextSceneWithDelay());
        }
        else
        {
            transform.position = originalPosition;
            dragging = false;
        }
    }

    IEnumerator LoadNextSceneWithDelay()
    {
        // Wait for the drop sound to finish playing
        yield return new WaitForSeconds(dropClip.length);

        // Get the current scene name
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Extract the numeric part of the scene name (assuming it's in the format "challengeX")
        int currentChallengeNumber;
        if (int.TryParse(currentSceneName.Substring("challenge".Length), out currentChallengeNumber))
        {
            // Calculate the next challenge number
            int nextChallengeNumber = currentChallengeNumber + 1;

            // Form the name of the next scene
            string nextSceneName = "challenge" + nextChallengeNumber;

            // Check if the next scene is within the total number of scenes
            if (SceneExistsInBuildSettings(nextSceneName))
            {
                // Load the next scene with a smooth transition
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                // If the next scene doesn't exist, load the "lastScene"
                SceneManager.LoadScene("lastScene");
            }
        }
        else
        {
            // If the current scene name doesn't follow the expected format, load the "lastScene"
            SceneManager.LoadScene("lastScene");
        }
    }

    bool SceneExistsInBuildSettings(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneNameInBuildSettings = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            if (sceneNameInBuildSettings == sceneName)
            {
                return true;
            }
        }

        return false;
    }

    Vector2 GetMousePos()
    {
        return (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
