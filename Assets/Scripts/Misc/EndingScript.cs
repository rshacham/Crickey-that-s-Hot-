using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingScript : MonoBehaviour
{
    [SerializeField] private TextMeshPro scoreText;
    [SerializeField] private ScoreHolder scoreHolder;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = scoreHolder.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Application.OpenURL("https://donate.wwf.org.au/adopt/adopt-an-aussie-species?t=AD2122O01&f=41140-227&gclid=CjwKCAjwloCSBhAeEiwA3hVo_YIp5kJTCaHEChYUTkRO9A5UzTWyJ0bJ66aNm2aleBcugqxHJrh6HhoCBygQAvD_BwE");    
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
}
