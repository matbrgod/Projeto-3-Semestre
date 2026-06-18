using UnityEngine;
using TMPro;

public class CréditosFinais : MonoBehaviour
{
    public TextMeshProUGUI creditsText;
    public float speed = 80f;
    public float startY = -800f;
    public float endY = 800f;
    public bool loop = false;
    public float delayBeforeStart = 0.5f;
    public bool autoDisable = true;

    RectTransform rect;

    void Start()
    {
        if (creditsText == null)
            creditsText = GetComponentInChildren<TextMeshProUGUI>();

        if (creditsText == null)
        {
            Debug.LogWarning("CréditosFinais: No TextMeshProUGUI assigned or found in children.");
            enabled = false;
            return;
        }

        rect = creditsText.rectTransform;

        Vector2 pos = rect.anchoredPosition;
        pos.y = startY;
        rect.anchoredPosition = pos;

        if (delayBeforeStart > 0f)
        {
            enabled = false;
            Invoke(nameof(Begin), delayBeforeStart);
        }
        else enabled = true;
    }

    void Begin() => enabled = true;

    void Update()
    {
        if (rect == null) return;

        rect.anchoredPosition += Vector2.up * (speed * Time.deltaTime);

        if (rect.anchoredPosition.y >= endY)
        {
            if (loop)
            {
                Vector2 pos = rect.anchoredPosition;
                pos.y = startY;
                rect.anchoredPosition = pos;
            }
            else
            {
                if (autoDisable) enabled = false;
            }
        }
    }
}
