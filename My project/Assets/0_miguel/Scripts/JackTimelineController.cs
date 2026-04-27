using UnityEngine;
using UnityEngine.InputSystem; // No olvides esta línea para el PlayerInput
using System.Collections;
public class JackTimelineController : MonoBehaviour
{
	[Header("Animators")]
	public Animator timelineAnimator; // El de JackSkellingtonMove Variant
	public Animator gameplayAnimator; // El de Robot

	[Header("Controles")]
	public PlayerInput playerInput; // Arrástralo desde el inspector

	public CharacterController characterController; // Arrástralo desde el inspector

	[Header("Camaras")]
	public GameObject robotCamera; 
	public GameObject playerFollowCamera;

	[Header("Triggers")]
	public GameObject triggerAudio;

	[Header("Dialogue Signal")]
	public AudioClip timelineClip;

	[TextArea]
	public string timelineSubtitle;
	public void LanzarDialogoTimeline()
	{
		if (DialogueManager.Instance != null)
		{
			DialogueManager.Instance.PlayLine(
				timelineClip,
				timelineSubtitle
			);
		}
	}
	public void IniciarCinematica()
	{

		if (gameplayAnimator != null) gameplayAnimator.enabled = false;
		if (characterController != null) characterController.enabled = false;

		if (playerInput != null) playerInput.enabled = false;


		if (robotCamera != null) robotCamera.SetActive(false);
		if (playerFollowCamera != null) playerFollowCamera.SetActive(false);


		Debug.Log("Cinematica iniciada: Control transferido al Timeline.");

	}
	public void FinalizarCinematica()
	{
		// 1. Apagamos el Animator que usó el Timeline para que no bloquee al personaje
		if (timelineAnimator != null) timelineAnimator.enabled = false;

		StartCoroutine(ActivarPersonaje());
		
	}

	private IEnumerator ActivarPersonaje()
	{
        // 2. Encendemos el Animator del Robot (el que tiene el StarterAssets Controller)
        if (gameplayAnimator != null) gameplayAnimator.enabled = true;
        if (robotCamera != null) robotCamera.SetActive(true);
        if (playerFollowCamera != null) playerFollowCamera.SetActive(true);

        yield return new WaitForSeconds(6f);

        // 3. Activamos la física y el control del jugador
        if (characterController != null) characterController.enabled = true;
        if (playerInput != null) playerInput.enabled = true;
		triggerAudio.SetActive(true);

		Debug.Log("Cinemática finalizada: Control transferido al Robot.");
    }
}