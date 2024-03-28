using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CorrectVowelMove : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip pickUpClip, dropClip;
    [SerializeField] private string sceneName; // Store the scene name as a string
    [SerializeField] private float delayBeforeLoading = 0.5f; // Delay in seconds before loading the next scene
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
            StartCoroutine(LoadSceneWithDelay());
        }
        else
        {
            transform.position = originalPosition;
            dragging = false;
        }
    }

    IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoading);

        SceneManager.LoadScene(sceneName); // Load the scene by name
    }

    Vector2 GetMousePos()
    {
        return (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
