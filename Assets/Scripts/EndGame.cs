using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGame : MonoBehaviour
{
    public TextMeshProUGUI endingNameText;
    public TextMeshProUGUI reachedEndingAmountText;

    public GameObject pressAnyKeyToRestartText;

    private bool isRestartAllowed = false;

    private void Awake()
    {
        endingNameText.text = "Ending:\n" + GameManager.endingName;
        int reachedEndingAmount = GameManager.GetReachedEndingAmount();
        int possibleEndingAmount = GameManager.possibleEndingAmount;

        reachedEndingAmountText.text = "Endings reached:\n(" + reachedEndingAmount + "/" + possibleEndingAmount + ")";

        pressAnyKeyToRestartText.SetActive(false);
        endingNameText.gameObject.SetActive(false);
        reachedEndingAmountText.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        endingNameText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        reachedEndingAmountText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        
        pressAnyKeyToRestartText.SetActive(true);

        isRestartAllowed = true;
;   }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && isRestartAllowed)
        {
            SceneManager.LoadScene("Game");
        }
    }
}
