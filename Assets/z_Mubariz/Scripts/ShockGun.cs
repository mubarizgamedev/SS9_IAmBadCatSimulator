using UnityEngine;

public class ShockGun : MonoBehaviour
{
    [SerializeField] GameObject currentGameObject;
    [SerializeField] GameObject fireGameObject;
    [SerializeField] GameObject beeGameObject;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float throwForce;
    public void ShootCurrent()
    {
        GameObject current = Instantiate(currentGameObject, spawnPoint.position, Quaternion.identity);
        Rigidbody rb = current.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        Debug.LogError("current bullet spawned");
    }

    public void ShootFire()
    {
        GameObject current = Instantiate(fireGameObject, spawnPoint.position, Quaternion.identity);
        Rigidbody rb = current.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        Debug.LogError("fire bullet spawned");
    }
    public void ShootBee()
    {
        GameObject current = Instantiate(beeGameObject, spawnPoint.position, Quaternion.identity);
        Rigidbody rb = current.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        Debug.LogError("bee bullet spawned");
    }
}
