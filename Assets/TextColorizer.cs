using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextColorizer : MonoBehaviour
{
    public TextMeshProUGUI alienSurvivor;
    public Outline outline;
    public float speed = 0.2f; // Speed of the color change
    public float scaleSpeed;
    public float scaleAmount;

    private float time;
    public float speedOutline;


    public GameObject playButton;


    void Update()
    {
        time += Time.deltaTime * speed;

        alienSurvivor.color = RandomizeColor(time);

        outline.effectColor = RandomizeColor(time / speedOutline);

        playButton.transform.localScale = Vector3.one * (1 + Mathf.Sin(Time.time * scaleSpeed) * scaleAmount);
    }

    Color RandomizeColor(float time)
    {
        float r = Mathf.Sin(time) * 0.5f + 0.5f;
        float g = Mathf.Sin(time + 2 * Mathf.PI / 3) * 0.5f + 0.5f;
        float b = Mathf.Sin(time + 4 * Mathf.PI / 3) * 0.5f + 0.5f;

        return new Color(r, g, b);
    }
}
