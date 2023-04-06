using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGame : MonoBehaviour
{
    public TextMeshProUGUI endingNameText;
    public GameObject pressAnyKeyToRestartText;
    public float timeWhenRestartIsAllowed = 1;

    private void Awake()
    {
        endingNameText.text = "Ending:\n" + GameManager.endingName;
        pressAnyKeyToRestartText.SetActive(false);
        endingNameText.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(timeWhenRestartIsAllowed);
        pressAnyKeyToRestartText.SetActive(true);
        endingNameText.gameObject.SetActive(true);
;   }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && Time.timeSinceLevelLoad >= timeWhenRestartIsAllowed)
        {
            SceneManager.LoadScene("Game");
        }
    }
}
