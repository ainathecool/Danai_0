using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Hint : MonoBehaviour
{

    public GameObject hintA;
    public GameObject writingA;

    private int count;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void HintPhase2()
    {
        count = PlayerPrefs.GetInt("Phase2Hints");
        Debug.Log("hints: " + count);
        StartCoroutine(OnHintsButtonClicked());


        //store hints count in playerprefs
        PlayerPrefs.SetInt("Phase2Hints", count + 1);

    }

    
    public IEnumerator OnHintsButtonClicked()
    {
        writingA.SetActive(false);
        hintA.SetActive(true);

        yield return new WaitForSeconds(3f);

        hintA.SetActive(false);
        writingA.SetActive(true);

    }
}
