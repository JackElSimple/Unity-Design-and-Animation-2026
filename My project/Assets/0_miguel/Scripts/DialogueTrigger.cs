using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
	public AudioClip clip;
	[TextArea] public string subtitle;


	private void OnTriggerEnter(Collider other)
	{
		DialogueManager.Instance.PlayLine(clip, subtitle);
		Destroy(gameObject);
	}
}