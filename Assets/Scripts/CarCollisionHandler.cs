using UnityEngine;
using System.Collections;

public class CarCollisionHandler : MonoBehaviour
{
    private bool canTakeDamage = true;
    public float damageCooldown = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("obstacle")) return;
        if (!canTakeDamage) return;
        if (GameManager.instance == null) return;

        GameManager.instance.LoseLife();
        StartCoroutine(DamageCooldown());
    }

    private IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
}