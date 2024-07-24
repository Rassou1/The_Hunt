using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Kill_BottonUI : MonoBehaviour
{
    private bool isAttack = false;
    private float attackCooldown = 0.35f; // 35 milliseconds

    public GameObject onAttackUI;
    public GameObject offAttackUI;


    public void HandleAttack()
    {
        if (isAttack)
        {
            return;
        }

        isAttack = true;
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttack = false;
    }

    public void Update()
    {
        onAttackUI.SetActive(!isAttack);
        offAttackUI.SetActive(isAttack);
    }
}
