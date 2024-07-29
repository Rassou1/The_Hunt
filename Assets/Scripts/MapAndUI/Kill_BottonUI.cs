using Alteruna;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Kill_BottonUI : MonoBehaviour
{
    private bool isAttack = false;
    private float attackCooldown = 0.47f; // 47 milliseconds
    public Alteruna.Avatar avatar;
    public GameObject onAttackUI;
    public GameObject offAttackUI;

    public void Awake()
    {
        if (!avatar.IsMe)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void HandleAttack()
    {
        if (isAttack)
        {
            return;
        }

        isAttack = true;
        StartCoroutine(AttackCooldown());
        onAttackUI.SetActive(false);
        offAttackUI.SetActive(true);
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttack = false;
        onAttackUI.SetActive(true);
        offAttackUI.SetActive(false);
    }

}
