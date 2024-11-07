using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour, IHealth
{
    private Rigidbody rb;
    private Vector3 moveDirection;
    private Vector3 aimDirection;
    private PlayerInput playerInput;
    private Vector2 mousePos;

    [Header("Player Info")]
    public PlayerStats _stats;
    public Weapon _weapon;
    public bool _CanMove = false;
    public LayerMask _layerMask;

    [Header("Referencies")]
    public Transform _weaponAnchor;
    public Transform _visualTranform;
    public Transform _cameraOrigin;
    public Camera _camera;
    public Animator _animator;
    public Weapon basicPistol;
    public UIGaugeHandler healthGauge;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        if(_stats != null)
        {
            _stats.Init();
        }
        
        Equip(basicPistol);
    }

    private void Update()
    {
        if (!_CanMove) return;
        UpdateAnimatorParameters();
    }

    private void FixedUpdate()
    {
        if (!_CanMove) return;
        rb.AddForce(moveDirection * _stats._Speed * Time.deltaTime, ForceMode.Impulse);
    }

    public void GetMoveDirection(InputAction.CallbackContext context)
    {
        Vector3 tempVec = context.ReadValue<Vector2>();
        moveDirection = new Vector3(tempVec.x, 0, tempVec.y).normalized;

        if(context.canceled)
        {            
            rb.linearVelocity = Vector3.zero;
        }
    }

    public void GetAim(InputAction.CallbackContext context)
    {       
        if (playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            Ray aimOrigin = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if(Physics.Raycast(aimOrigin, out rayHit, 100f, _layerMask))
            {
                Vector3 point = new Vector3(rayHit.point.x, 0, rayHit.point.z);
                aimDirection = (point - transform.position).normalized;
            }

            _cameraOrigin.localPosition = aimDirection * 0.1f;
        }
        else if(playerInput.currentControlScheme == "Gamepad")
        {
            if (context.ReadValue<Vector2>().magnitude > 0.4f)
                mousePos = context.ReadValue<Vector2>();

            aimDirection = new Vector3(mousePos.x, 0, mousePos.y).normalized;
            _cameraOrigin.localPosition = aimDirection * 0.5f;
        }

        _visualTranform.rotation = Quaternion.LookRotation(aimDirection);        
    }

    private void UpdateAnimatorParameters()
    {
        Vector2 playerDir = new Vector2(_visualTranform.forward.x, _visualTranform.forward.z);
        float angleDiff = Vector2.SignedAngle(Vector2.up, playerDir);
        float radians = -angleDiff * Mathf.Deg2Rad;

        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);
        float tx = moveDirection.x;
        float ty = moveDirection.z;

        Vector2 final = new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);

        _animator.SetFloat("Aim_X", final.x);
        _animator.SetFloat("Aim_Y", final.y);
    }

    public void GetShootAction(InputAction.CallbackContext context) 
    {
        if (!_CanMove) return;
    }

    public void GetSkillAction(InputAction.CallbackContext context)
    {
        if (!_CanMove) return;
    }

    public void Dammage(float dmg)
    {
        healthGauge.UpdateUISlider(_stats._CurrentHealth);
        if (dmg < _stats._CurrentHealth)
        {
            _stats._CurrentHealth -= dmg;
        }
        else
        {
            //Fin de la manche
        }
    }

    #region equipements
    public void Equip(Weapon wpn)
    {
        _weapon = wpn;
        _weapon.enabled = true;
        _weapon.transform.SetParent(_weaponAnchor);
    }

    void UnEquip() 
    {

    }

    

    #endregion
}
