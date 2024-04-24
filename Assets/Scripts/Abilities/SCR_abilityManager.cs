using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_abilityManager : MonoBehaviour
{
    // All multi jump variables
    public bool AB_canDoubleJump = true;

    public int AB_jumpsTotal = 1;
    public int AB_jumpsLeft = 1;
    private float AB_jumpHeight = 5f;

    // All Dash variables
    public bool AB_canDash = true;

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
    public void CheckDoubleJump(ref P_StateManager _ctx)
    {
        if (_ctx.IsJumpPressed && _ctx._pow.AB_jumpsLeft > 0 && _ctx._pow.AB_canDoubleJump)
        {
            _ctx.VertMagnitude = AB_jumpHeight;
            _ctx._pow.AB_jumpsLeft--;
        }

    }
    public void CheckDash(ref P_StateManager _ctx)
    {
        if (_ctx.IsDashPressed && _ctx.IsDashReleased && _ctx._pow.AB_dashCharges > 0 && _ctx._pow.AB_canDash)
        {
            _ctx._dashFactor = 3;
        }
        else
        {
            _ctx._dashFactor = 0;
        }
        //Debug.Log($"{_ctx.IsDashPressed} {_ctx.IsDashReleased} {_ctx._pow.AB_dashCharges}");
    }
}
