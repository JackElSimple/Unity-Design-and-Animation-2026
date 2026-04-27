using UnityEngine;
using System.Collections;

public class ActivateObjectsWithDelay : MonoBehaviour
{
	[Header("Objects to activate (start disabled)")]
	public GameObject[] objectsToActivate;

	[Header("Delay between each object")]
	public float delayBetweenObjects = 0.5f;
	void Start()
	{
		StartCoroutine(ActivateRoutine());
	}
	private IEnumerator ActivateRoutine()
	{
		for (int i = 0; i < objectsToActivate.Length; i++)
		{
			if (objectsToActivate[i] != null)
				objectsToActivate[i].SetActive(true);

			yield return new WaitForSeconds(delayBetweenObjects);
		}
	}
}