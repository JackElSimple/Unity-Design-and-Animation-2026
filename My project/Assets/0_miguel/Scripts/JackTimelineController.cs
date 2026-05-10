using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using System.Collections;

public class JackTimelineController : MonoBehaviour
{
    [Header("Objetos de Timeline")]
    public GameObject objetoTimelineInicial; 
    public GameObject objetoTimelineFinal;

    [Header("Animators")]
    public Animator timelineAnimator;
    public Animator gameplayAnimator;
    [Header("Sonido y Animacion")]
    public AudioSource audioSource; 
    public AudioClip sonidoFinal;
    [Header("Controles")]
    public PlayerInput playerInput;
    public CharacterController characterController;

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
            DialogueManager.Instance.PlayLine(timelineClip, timelineSubtitle);
        }
    }

    public void IniciarCinematica()
    {
        if (gameplayAnimator != null) gameplayAnimator.enabled = false;
        if (timelineAnimator != null)
        {
            timelineAnimator.enabled = true;
            timelineAnimator.applyRootMotion = true;
        }

        if (characterController != null) characterController.enabled = false;
        if (playerInput != null) playerInput.enabled = false;

        if (robotCamera != null) robotCamera.SetActive(false);
        if (playerFollowCamera != null) playerFollowCamera.SetActive(false);
    }

    public void FinalizarCinematica()
    {
        if (timelineAnimator != null) timelineAnimator.enabled = false;
        StartCoroutine(ActivarPersonaje());
    }

    private IEnumerator ActivarPersonaje()
    {
        if (gameplayAnimator != null)
        {
            gameplayAnimator.enabled = true;
            gameplayAnimator.applyRootMotion = false;
        }

        if (robotCamera != null) robotCamera.SetActive(true);
        if (playerFollowCamera != null) playerFollowCamera.SetActive(true);

        yield return new WaitForSeconds(6f);

        if (characterController != null) characterController.enabled = true;
        if (playerInput != null) playerInput.enabled = true;

        if (triggerAudio != null)
        {
            triggerAudio.SetActive(true);
        }
    }

    public void IniciarCinematicaFinal()
    {
        if (objetoTimelineInicial != null)
        {
            PlayableDirector directorInicial = objetoTimelineInicial.GetComponent<PlayableDirector>();
            if (directorInicial != null) directorInicial.Stop();

            objetoTimelineInicial.SetActive(false);
        }

        if (characterController != null) characterController.enabled = false;
        if (playerInput != null) playerInput.enabled = false;

        if (gameplayAnimator != null)
        {
            gameplayAnimator.applyRootMotion = false;
            gameplayAnimator.enabled = false;
        }

        if (timelineAnimator != null)
        {
            timelineAnimator.enabled = true;
            timelineAnimator.applyRootMotion = true;
        }

        if (objetoTimelineFinal != null)
        {
            objetoTimelineFinal.SetActive(true);
        }

        if (robotCamera != null) robotCamera.SetActive(false);
        if (playerFollowCamera != null) playerFollowCamera.SetActive(false);
        if (audioSource != null && sonidoFinal != null)
        {
            audioSource.PlayOneShot(sonidoFinal);
        }
    }

    public void FinalizarCinematicaFinal()
    {
        if (timelineAnimator != null) timelineAnimator.enabled = false;
        StartCoroutine(ActivarPersonajeFinal());
    }

    private IEnumerator ActivarPersonajeFinal()
    {
        if (gameplayAnimator != null)
        {
            gameplayAnimator.enabled = true;
            gameplayAnimator.applyRootMotion = false;
            gameplayAnimator.Rebind();
        }

        if (robotCamera != null) robotCamera.SetActive(true);
        if (playerFollowCamera != null) playerFollowCamera.SetActive(true);

        yield return new WaitForSeconds(6f);

        if (characterController != null) characterController.enabled = true;
        if (playerInput != null) playerInput.enabled = true;
    }
}
