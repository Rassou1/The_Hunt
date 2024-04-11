using UnityEngine;



public class P_ClimbingState : P_BaseState
{
    float lerpTime;
    //private P_StateManager stateManager;

    public P_ClimbingState(P_StateManager stateManager, P_StateFactory p_StateFactory) : base(stateManager, p_StateFactory)
    {
        this._ctx = stateManager;
       
    }

   

    public override void EnterState()
    {
        _ctx.Animator.SetBool(_ctx.IsClimbingHash, true);
        lerpTime = 0f;
       
        // Eğer tırmanırken yerçekimi veya diğer fiziksel etkileri değiştirmeniz gerekiyorsa, burada yapabilirsiniz.
    }

    public override void UpdateState()
    {
        // Tırmanma hareketi ve hızının lerp edilmesi
        // Bu örnek tırmanma için basit bir yukarı hareket varsayar. İhtiyacınıza göre ayarlayın.
        if (Input.GetKey(KeyCode.E)) // Yukarı ok tuşu ile tırmanma örneği
        {
            float verticalInput = 1f; // Veya Input.GetAxis("Vertical") kullanılabilir
            Vector3 move = Vector3.up * verticalInput * _ctx.climbspeed * Time.deltaTime;
            _ctx.Rigidbody.MovePosition(_ctx.transform.position + move);

            _ctx.StateMagnitude = Mathf.Lerp(_ctx.ActualMagnitude, _ctx.climbspeed, lerpTime);
            lerpTime += Time.deltaTime;
        }
        else
        {
            CheckSwitchState();
        }
    }

    public override void ExitState()
    {
        _ctx.Animator.SetBool(_ctx.IsClimbingHash, false);
        // Tırmanmayı bitirdiğinizde yerçekimi veya diğer fiziksel etkileri eski haline getirin.
    }

    public override void CheckSwitchState()
    {
        // Durum geçişi koşulları. Örneğin, tırmanma bitirme koşulu.
        if (_ctx.IsClimbingPressed) // Tırmanma tuşuna basılmadığında tırmanmayı durdur.
        {
            SwitchState(_factory.Idle()); // Tırmanma durdurulduğunda Idle durumuna geç.
        }
        // Diğer durum geçişleri koşulları burada eklenebilir.
    }

    public override void InitializeSubState()
    {
        // Tırmanma durumu için alt durumların başlatılmasına gerek yoksa, bu boş bırakılabilir.
    }
}


