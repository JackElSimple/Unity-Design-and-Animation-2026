using UnityEngine;
using UnityEngine.Playables;

public class TimelineTrigger : MonoBehaviour
{
	public PlayableDirector director;

	private bool activated = false;

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Algo entró: " + other.name);

		if (!other.CompareTag("Player")) return;

		Debug.Log("Jugador entró al trigger");
		Debug.Log(director.playableAsset);
		director.Play();

		Destroy(gameObject);
	}
}