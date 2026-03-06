using UnityEngine;

public class TriggerChest : MonoBehaviour
{
    public Animator chestAnimator;
    public Animator JackAnimator;

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
            chestAnimator.SetBool("abierto", true);
            JackAnimator.SetTrigger("Muerto");

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
}
