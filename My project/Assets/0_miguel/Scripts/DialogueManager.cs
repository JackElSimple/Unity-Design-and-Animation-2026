using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
	public static DialogueManager Instance;

	public AudioSource audioSource;
	public TextMeshProUGUI subtitleUI;

	private bool isPlaying = false;

	void Awake()
	{
		Instance = this;
	}

	public void PlayLine(AudioClip clip, string subtitle)

	{
		Debug.Log("Intento de PlayLine con: " + clip.name + " | isPlaying = " + isPlaying);
		if (isPlaying) return;

		StartCoroutine(PlayRoutine(clip, subtitle));
	}

	IEnumerator PlayRoutine(AudioClip clip, string subtitle)
	{
		isPlaying = true;

		audioSource.PlayOneShot(clip);
		subtitleUI.text = subtitle;

		yield return new WaitForSeconds(clip.length);

		subtitleUI.text = "";
		isPlaying = false;
	}
}