using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireSlider : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private List<Color> myColors = new List<Color>();

    [SerializeField] private Image image;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = GameManager._shared.CurFires / GameManager._shared.maxFires;
        if (slider.value < 0.50f)
        {
            image.color = myColors[0];
        }
        else if (slider.value >= 0.50f && slider.value <= 0.75f)
        {
            image.color = myColors[1];
        }
        
        else
        {
            image.color = myColors[2];
        }
    }
}
