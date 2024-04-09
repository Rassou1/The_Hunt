using UnityEngine;


public class P_ClimbingState : P_BaseState
{
    private bool _isClimbing = false;

    public P_ClimbingState(P_StateManager currentContext, P_StateFactory p_StateFactory) : base(currentContext, p_StateFactory)
    {
        _ctx = currentContext;
    }

    public override void EnterState()
    {
        _ctx.Animator.SetBool(_ctx.IsClimbingHash, true);
        _isClimbing = true;
    }

    public override void UpdateState()
    {
        if (_isClimbing)
        {
            float verticalInput = _ctx.CurrentMovementInput.y;
            Vector3 movement = Vector3.up * verticalInput * _ctx.climbspeed * Time.deltaTime;
            _ctx.Rigidbody.MovePosition(_ctx.Rigidbody.position + movement);
        }

    }

    public override void ExitState()
    {
        _ctx.Animator.SetBool(_ctx.IsClimbingHash, false);
        _isClimbing = false;
    }

    public override void CheckSwitchState()
    {
        // Ladder layer?ndaki objelerle temas? kontrol et
        Collider[] colliders = Physics.OverlapSphere(_ctx.transform.position, 0.5f, LayerMask.GetMask("Ladder"));

        if (colliders.Length == 0)
        {
            // E?er merdivene temas kesildiyse veya t?rmanma tu?u b?rak?ld?ysa, Idle durumuna geç
            SwitchState(_factory.Idle());
        }

        // T?rmanma i?lemi s?ras?nda t?rmanma tu?u b?rak?ld?ysa veya t?rmanma i?lemi sonland?r?ld?ysa
        if (!_ctx.IsClimbingPressed || !_isClimbing)
        {
            // T?rmanma durumunu s?f?rla ve yere in
            SwitchState(_factory.Ground());
        }
    }

    public override void InitializeSubState()
    {

    }
}
