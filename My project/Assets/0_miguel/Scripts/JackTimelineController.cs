using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using System.Collections;

public class JackTimelineController : MonoBehaviour
{
    [Header("Objetos de Timeline")]
    public GameObject objetoTimelineInicial; // Arrastra el objeto de la primera cinematica
    public GameObject objetoTimelineFinal;

    [Header("Animators")]
    public Animator timelineAnimator;
    public Animator gameplayAnimator;
    [Header("Sonido y Animacion")]
    public AudioSource audioSource; // El componente que emite el sonido
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

        Debug.Log("Cinematica iniciada: Control transferido al Timeline.");
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
            Debug.Log($"<color=green>LOG: Activando {triggerAudio.name}</color>");
        }

        Debug.Log("Cinematica finalizada.");
    }

    public void IniciarCinematicaFinal()
    {
        // 1. Apagamos la primera para liberar el control del personaje
        if (objetoTimelineInicial != null)
        {
            PlayableDirector directorInicial = objetoTimelineInicial.GetComponent<PlayableDirector>();
            if (directorInicial != null) directorInicial.Stop();

            objetoTimelineInicial.SetActive(false);
            Debug.Log("Timeline Inicial desactivada.");
        }

        // 2. Preparacion de componentes (Fisica e Input)
        if (characterController != null) characterController.enabled = false;
        if (playerInput != null) playerInput.enabled = false;

        // La Timeline final esta enlazada al Animator raiz, no al Animator de gameplay.
        // Dejamos un solo Animator al mando para que no compitan entre si.
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

        // 3. Encendemos la cinematica final
        if (objetoTimelineFinal != null)
        {
            objetoTimelineFinal.SetActive(true);
            Debug.Log("Timeline Final activada.");
        }

        if (robotCamera != null) robotCamera.SetActive(false);
        if (playerFollowCamera != null) playerFollowCamera.SetActive(false);
        // 2. Ejecutar Sonido
        if (audioSource != null && sonidoFinal != null)
        {
            audioSource.PlayOneShot(sonidoFinal);
            Debug.Log("Sonido reproducido.");
        }
    }

    public void FinalizarCinematicaFinal()
    {
        if (timelineAnimator != null) timelineAnimator.enabled = false;
        StartCoroutine(ActivarPersonajeFinal());
    }

    private IEnumerator ActivarPersonajeFinal()
    {
        Debug.Log("<color=cyan>DEBUG: Iniciando reactivacion final...</color>");

        if (gameplayAnimator != null)
        {
            gameplayAnimator.enabled = true;
            gameplayAnimator.applyRootMotion = false;
            gameplayAnimator.Rebind();
            Debug.Log("<color=green>LOG: Animator reseteado con Rebind.</color>");
        }

        if (robotCamera != null) robotCamera.SetActive(true);
        if (playerFollowCamera != null) playerFollowCamera.SetActive(true);

        yield return new WaitForSeconds(6f);

        if (characterController != null) characterController.enabled = true;
        if (playerInput != null) playerInput.enabled = true;

        Debug.Log("<color=magenta>Cinematica FINAL terminada. Control devuelto.</color>");
    }
}
