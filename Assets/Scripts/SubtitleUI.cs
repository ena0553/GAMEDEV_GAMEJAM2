using UnityEngine;
using TMPro;

public class SubtitleUI : MonoBehaviour
{
    public static SubtitleUI Instance;
    public TMP_Text text;

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Show(string message)
    {
        text.text = message;
        gameObject.SetActive(true);
    }

    public void Hide() => gameObject.SetActive(false);
}
