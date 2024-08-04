using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// Ability manager written by Hamdi.
/// This script takes care of all the abilities of the characters.
/// </summary>
public class SCR_abilityManager : MonoBehaviour
{
    // All multi jump variables
    public bool AB_canDoubleJump = true;
    public int AB_jumpsTotal = 1;
    public int AB_jumpsLeft = 1;
    //private float AB_jumpHeight = 10f;

    // All Dash variables
    public bool AB_canDash = true;

    [SerializeField]
    public int AB_dashCharges = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
    public void ResetJumps()
    {
        AB_jumpsLeft = AB_jumpsTotal;
    }
    //public void CheckDoubleJump(ref P_StateManager _ctx)
    //{
    //    if (_ctx.IsJumpPressed && _ctx.Pow.AB_jumpsLeft > 0 && _ctx.Pow.AB_canDoubleJump)
    //    {
    //        _ctx.VertMagnitude = AB_jumpHeight;
    //        _ctx.Pow.AB_jumpsLeft--;
    //    }
    //}
    //public void CheckDash(ref P_StateManager _ctx)
    //{
    //    if (_ctx.IsDashPressed && _ctx.IsDashReleased && _ctx.Pow.AB_dashCharges > 0 && _ctx.Pow.AB_canDash)
    //    {
    //        _ctx._dashFactor = 15;
    //        _ctx.Pow.AB_dashCharges--;
    //    }
    //    else
    //    {
    //        _ctx._dashFactor = 0.5f;
    //    }
    //    //Debug.Log($"{_ctx.IsDashPressed} {_ctx.IsDashReleased} {_ctx.Pow.AB_dashCharges}");
    //}
}
