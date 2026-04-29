using UnityEngine;

public class TimelineTrigger : MonoBehaviour
{
	public GameObject timelineObject;

	private bool activated;

	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Player")) return;
		if (activated) return;

		activated = true;

		timelineObject.SetActive(true);

		Destroy(gameObject);
	}
}