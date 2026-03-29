using UnityEngine;

public class BallGun : MonoBehaviour
{
    public Vector3 origin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        origin = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < -10 || this.transform.position.y > 100)
        {
            this.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            this.transform.position = origin;
        }
    }
}
