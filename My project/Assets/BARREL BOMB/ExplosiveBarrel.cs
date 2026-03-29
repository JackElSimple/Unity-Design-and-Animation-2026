using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    [Header("Explosion Settings")]
    public float explosionForce = 700f;
    public float explosionRadius = 5f;
    public float upwardModifier = 1f;
    public GameObject explosionEffect; // Partículas de explosión
    public AudioClip explosionSound;
    public float destructionDelay = 2f;
    public GameObject barrelOnePiece;

    private bool hasExploded = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasExploded) return;

        if (other.CompareTag("Player") || other.CompareTag("Projectile"))
        {
           
            GetComponent<Animator>().SetTrigger("Explode");
            Explode();
        }
    }

    void Explode()
    {
        hasExploded = true;

        // Reproduce efecto visual
        if (explosionEffect != null)
        {
            GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(effect, 3f);
        }

        // Reproduce sonido si existe
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        // Aplica fuerza a todos los objetos cercanos
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardModifier, ForceMode.Impulse);
            }
        }

        // Desactivar visual del barril y destruir luego
//        GetComponent<MeshRenderer>().enabled = false;
    //    GetComponent<Collider>().enabled = false;
      //  Destroy(gameObject, destructionDelay);
    }
}
