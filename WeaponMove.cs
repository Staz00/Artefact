using UnityEngine;
using System.Collections;

public class WeaponMove : MonoBehaviour {

    public float weaponDamage = 5f;

    public bool stillMoving
    {
        get; private set;
    }

    PlayerController pc;
    BoxCollider weaponCollider;

    void Start()
    {
        pc = GetComponentInParent<PlayerController>();
        weaponCollider = GetComponent<BoxCollider>();

        weaponCollider.isTrigger = true;
        weaponCollider.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Enemy targettedEnemy = other.GetComponent<Enemy>();
            targettedEnemy.TakeDamage(weaponDamage);
        }
    }

    public IEnumerator SwingWeapon()
    {
        pc.currentState = PlayerController.PlayerState.Attacking;

        weaponCollider.enabled = true;

        Vector3 originalPos = transform.position;
        Vector3 swingPos = originalPos + transform.forward;

        float swingSpeed = 6f;
        float percent = 0f;

        while(percent <= 1)
        {
            stillMoving = true;
            percent += Time.deltaTime * swingSpeed;

            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPos, swingPos, interpolation);

            yield return null;
        }

        pc.currentState = PlayerController.PlayerState.Moving;

        weaponCollider.enabled = false;
        stillMoving = false;
        transform.localPosition = transform.InverseTransformPoint(originalPos + transform.forward * .5f);
        
    }

}
