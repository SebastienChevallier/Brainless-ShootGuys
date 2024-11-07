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

    public bool isEquipWeapon;

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
    public SkinnedMeshRenderer skullRenderer;
    public MeshRenderer arrowRenderer;
    Weapon playerBasicPistol;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        if(_stats != null)
        {
            _stats = Instantiate(_stats);
            _stats.Init();
        }
        isEquipWeapon = false;

        playerBasicPistol = Instantiate(basicPistol);
        playerBasicPistol.Init();

        Equip(playerBasicPistol, true);
    }

    public void Spawn() { }

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

        if (_weapon && context.started)
            _weapon.Shoot();
    }

    public void GetSkillAction(InputAction.CallbackContext context)
    {
        if (!_CanMove) return;
    }

    public void Dammage(float dmg)
    {
        if(dmg < _stats._CurrentHealth)
        {
            _stats._CurrentHealth -= dmg;
        }
        else
        {
            //Fin de la manche
            Destroy(gameObject);
        }
    }



    #region equipements
    public void Equip(Weapon wpn, bool isBasicPistol = false)
    {
        wpn.enabled = true;
        wpn.playerUse = this;
        _weapon = wpn;
        _weapon.transform.SetParent(_weaponAnchor);
        _weapon.transform.localPosition = Vector3.zero;
        _weapon.transform.localRotation = Quaternion.identity;
        _weapon.transform.localScale = Vector3.one;

        if (isBasicPistol)
            playerBasicPistol.gameObject.SetActive(true);
        else
        {
            _weapon.Init();
            isEquipWeapon = true;
            playerBasicPistol.gameObject.SetActive(false);
        }
    }

    public void UnEquip() 
    {
        Destroy(_weapon.gameObject);

        Equip(playerBasicPistol, true);
        isEquipWeapon = false;
    }

    #endregion
}
