using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class JackTimelineController : MonoBehaviour
{
    [Header("Objetos de Timeline")]
    public GameObject objetoTimelineInicial; // Arrastra el objeto de la primera cinemática
    public GameObject objetoTimelineFinal;
    public string nombreAnimacion = "FinalAnimation";
    [Header("Animators")]
    public Animator timelineAnimator;
    public Animator gameplayAnimator;
    [Header("Sonido y Animación")]
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
        if (gameplayAnimator != null) gameplayAnimator.enabled = true;
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

        Debug.Log("Cinemática finalizada.");
    }

    public void IniciarCinematicaFinal()
    {
        // 1. Apagamos la primera para liberar el control del personaje
        if (objetoTimelineInicial != null)
        {
            objetoTimelineInicial.SetActive(false);
            Debug.Log("Timeline Inicial desactivada.");
        }

        // 2. Preparación de componentes (Física e Input)
        if (characterController != null) characterController.enabled = false;
        if (playerInput != null) playerInput.enabled = false;

        // IMPORTANTE: No apagues el gameplayAnimator si la Timeline lo usa, 
        // pero haz un Rebind para que acepte la nueva posición de la Timeline Final
        if (gameplayAnimator != null)
        {
            gameplayAnimator.enabled = true;
            gameplayAnimator.Rebind();
        }

        // 3. Encendemos la cinemática final
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

        // 3. Ejecutar Animación
        if (gameplayAnimator != null)
        {
            gameplayAnimator.enabled = true;
            gameplayAnimator.Play(nombreAnimacion);

            // Si la animación debe desplazar al personaje en el espacio:
            gameplayAnimator.applyRootMotion = true;

            Debug.Log($"Ejecutando animación: {nombreAnimacion}");
        }
    }

    public void FinalizarCinematicaFinal()
    {
        if (timelineAnimator != null) timelineAnimator.enabled = false;
        StartCoroutine(ActivarPersonajeFinal());
    }

    private IEnumerator ActivarPersonajeFinal()
    {
        Debug.Log("<color=cyan>DEBUG: Iniciando reactivación final...</color>");

        if (gameplayAnimator != null)
        {
            gameplayAnimator.enabled = true;
            gameplayAnimator.Rebind();
            Debug.Log("<color=green>LOG: Animator reseteado con Rebind.</color>");
        }

        if (robotCamera != null) robotCamera.SetActive(true);
        if (playerFollowCamera != null) playerFollowCamera.SetActive(true);

        yield return new WaitForSeconds(6f);

        if (characterController != null) characterController.enabled = true;
        if (playerInput != null) playerInput.enabled = true;

        Debug.Log("<color=magenta>Cinemática FINAL terminada. Control devuelto.</color>");
    }
}