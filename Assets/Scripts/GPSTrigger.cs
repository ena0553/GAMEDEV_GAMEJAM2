using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class SubtitleTrigger : MonoBehaviour
{
    [TextArea] public string subtitle = "subtitle here";
    public AudioClip clip;
    public float duration = 10f;
    public bool triggerOnce = true;

    AudioSource audioSource;
    bool used;
    Coroutine routine;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Reset() { GetComponent<Collider>().isTrigger = true; }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (triggerOnce && used) return;
        used = true;

        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(Show());
    }

    IEnumerator Show()
    {
        SubtitleUI.Instance.Show(subtitle);

        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }

        float time = duration > 0f ? duration : (clip != null ? clip.length : 5f);
        yield return new WaitForSeconds(time);

        SubtitleUI.Instance.Hide();
    }
}
