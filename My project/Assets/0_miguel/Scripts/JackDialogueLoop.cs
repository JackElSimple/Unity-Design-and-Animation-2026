using UnityEngine;

using System.Collections;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class VoiceLine
{
	public AudioClip clip;
	[TextArea]
	public string subtitle;
}

public class JackDialogueLoop : MonoBehaviour
{
	public AudioSource audioSource;
	public TextMeshProUGUI subtitleText; // UI Text

	public VoiceLine[] lines;
	public float delayBetweenLines = 2f;

	void Start()
	{
		StartCoroutine(PlayDialogue());
	}

	IEnumerator PlayDialogue()
	{
		for (int i = 0; i < lines.Length; i++)
		{
			// reproducir audio
			audioSource.clip = lines[i].clip;
			audioSource.Play();

			// mostrar subtítulo
			subtitleText.text = lines[i].subtitle;

			// esperar duración del audio
			yield return new WaitForSeconds(lines[i].clip.length);

			// limpiar subtítulo
			subtitleText.text = "";

			// pausa entre frases
			yield return new WaitForSeconds(delayBetweenLines);
		}
	}
}
