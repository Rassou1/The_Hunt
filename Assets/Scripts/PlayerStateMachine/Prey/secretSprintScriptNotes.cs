//using UnityEngine;

//public class P_RunningState : P_BaseState
//{
//    float totalMagnitude;
//    float sprintMagnitude;
//    public P_RunningState(P_StateManager currentContext, P_StateFactory p_StateFactory) : base(currentContext, p_StateFactory)
//    {

//    }
//    public override void EnterState()
//    {
//        _ctx.Animator.SetBool(_ctx.IsWalkingHash, true);
//        _ctx.Animator.SetBool(_ctx.IsSprintingHash, true);
//    }

//    public override void UpdateState()
//    {

//        _ctx.StateMagnitude = 20f;
//        CheckSwitchState();
//    }

//    public override void ExitState()
//    {

//    }

//    public override void CheckSwitchState()
//    {
//        if (_ctx.IsSlidePressed)
//        {
//            SwitchState(_factory.Slide());
//        }
//        else if (!_ctx.IsMovementPressed)
//        {
//            SwitchState(_factory.Idle());
//        }
//        else if (_ctx.IsMovementPressed && !_ctx.IsSprintPressed)
//        {
//            SwitchState(_factory.Walk());
//        }

//    }

//    public override void InitializeSubState()
//    {

//    }
//}