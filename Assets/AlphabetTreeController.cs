using UnityEngine;

public class AlphabetTreeController : MonoBehaviour
{
    public GameObject alphabetButtonPrefab;
    public Transform treeTransform;

    void Start()
    {
        string[] alphabet = { "A", "B", "C", "D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};
        ShuffleArray(alphabet);

        for (int i = 0; i < alphabet.Length; i++)
        {
            GameObject button = Instantiate(alphabetButtonPrefab, treeTransform);
            button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = alphabet[i];
            button.transform.localPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-2f, 2f), 0f);
        }
    }

    void ShuffleArray<T>(T[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n; i++)
        {
            int r = i + Random.Range(0, n - i);
            T temp = array[r];
            array[r] = array[i];
            array[i] = temp;
        }
    }
}
