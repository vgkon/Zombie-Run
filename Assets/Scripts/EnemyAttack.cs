using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float damage = 40f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    public void AttackHitEvent()
    {
        if (target == null) return;
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(damage);
        target.GetComponent<DisplayDamage>().ShowDamageImpact();
    }

    public void OnDamageTaken()
    {
        Debug.Log("I also know that we took damage");
    }

}
