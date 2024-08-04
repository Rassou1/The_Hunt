using Alteruna;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

//Copy of the kill UI button code made to work for singleplayer just by removing the awake function of getting the avatar. - Love
public class SP_KillButtonUI : MonoBehaviour
{
    private bool isAttack = false;
    private float attackCooldown = 0.45f;
    public GameObject onAttackUI;
    public GameObject offAttackUI;


    public void HandleAttack()
    {
        if (isAttack) return;

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
