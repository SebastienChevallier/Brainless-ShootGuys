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

    public Transform _weaponAnchor;
    public Transform _visualTranform;
    public PlayerStats _stats;
    public DeviceType _deviceType;
    public LayerMask _layerMask;
    public Transform _cameraOrigin;
    public Camera _camera;
    public Animator _animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        if(_stats != null)
        {
            _stats.Init();
        }
    }

    private void Update()
    {
        UpdateAnimatorParameters();
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDirection * _stats._Speed * Time.deltaTime, ForceMode.Impulse);
    }

    public void GetMoveDirection(InputAction.CallbackContext context)
    {
        Vector3 tempVec = context.ReadValue<Vector2>();
        moveDirection = new Vector3(tempVec.x, 0, tempVec.y).normalized;
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
                aimDirection = point - transform.position;
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
        // Calculer la rotation pour aligner l'axe Z sur la direction de visée
        Quaternion aimRotation = Quaternion.LookRotation(aimDirection, Vector3.up);

        // Transformer la direction de déplacement en utilisant la rotation de visée comme référence
        Vector3 relativeMoveDirection = aimRotation * moveDirection;

        // Assigner Move_X et Move_Y pour piloter le Blend Tree
        _animator.SetFloat("Aim_X", relativeMoveDirection.x);
        _animator.SetFloat("Aim_Y", relativeMoveDirection.z);
    }

    public void GetShootAction(InputAction.CallbackContext context) 
    {

    }

    public void GetSkillAction(InputAction.CallbackContext context)
    {

    }

    #region equipements
    void Equip()
    {

    }

    void UnEquip() 
    {

    }

    #endregion
}
