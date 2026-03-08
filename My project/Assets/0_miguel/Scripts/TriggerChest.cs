using UnityEngine;
using System.Collections;
public class TriggerChest : MonoBehaviour
{
    public Animator chestAnimator;
    public Animator JackAnimator;
	public AudioSource sonidoApertura;
	public AudioSource sonidoExplosion;
	private float tiempoEsperaExplosion = 1f; // Tiempo en segundos para esperar antes de reproducir el sonido de explosión
											 // Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Triggered when something enters the trigger collider
    void OnTriggerEnter(Collider other)
    {
        // Use == for comparison, or better yet, CompareTag()
        if (other.CompareTag("Player"))
        {
			Debug.Log("Player entered the chest trigger!");
			chestAnimator.SetBool("abierto", true);
			StartCoroutine(ReproducirSonidos());
		}
    }

    // Triggered when something leaves the trigger collider
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chestAnimator.SetBool("abierto", false);
        }
    }


	IEnumerator ReproducirSonidos()
	{
		sonidoApertura.Play();
		// Espera el tiempo exacto del primer clip (o los segundos que tú quieras)
		yield return new WaitForSeconds(sonidoApertura.clip.length);

		sonidoExplosion.Play();
		yield return new WaitForSeconds(tiempoEsperaExplosion);
		JackAnimator.SetTrigger("Muerto");


	}

	
}
