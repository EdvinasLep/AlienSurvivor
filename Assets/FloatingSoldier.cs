using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSoldier : MonoBehaviour
{
    public float speed = 200.0f;       // Speed of the soldier
    public float spinSpeed = 360.0f; // Spin speed in degrees per second

    private RectTransform rectTransform;
    private Vector2 direction;

    private float minX, maxX, minY, maxY;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        // Calculate canvas boundaries
        Canvas canvas = GetComponentInParent<Canvas>();
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        minX = canvasRect.rect.xMin + rectTransform.rect.width / 2;
        maxX = canvasRect.rect.xMax - rectTransform.rect.width / 2;
        minY = canvasRect.rect.yMin + rectTransform.rect.height / 2;
        maxY = canvasRect.rect.yMax - rectTransform.rect.height / 2;
    }

    void Update()
    {
        // Rotate the soldier
        rectTransform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);

        // Move the soldier
        rectTransform.anchoredPosition += direction * speed * Time.deltaTime;

        // Check for collisions with canvas boundaries and bounce
        Vector2 pos = rectTransform.anchoredPosition;
        if (pos.x < minX || pos.x > maxX)
        {
            direction.x = -direction.x;
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
        }

        if (pos.y < minY || pos.y > maxY)
        {
            direction.y = -direction.y;
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
        }

        rectTransform.anchoredPosition = pos;
    }
}
