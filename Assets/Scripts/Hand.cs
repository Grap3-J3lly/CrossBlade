using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{

    //------------------------------------------------------
    //                  ANIMATION VARIABLES
    //------------------------------------------------------

    public float animationSpeed;
    Animator animator;
    private float gripTarget;
    private float triggerTarget;
    private float gripCurrent;
    private float triggerCurrent;
    private string animatorGripParam = "Grip";
    private string animatortriggerParam = "Trigger";
    
    //------------------------------------------------------
    //                  PHYSICS MOVEMENT VARIABLES
    //------------------------------------------------------
    [SerializeField] private GameObject followObject;
    [SerializeField] private float followSpeed = 30f;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;

    private Transform followTarget;
    private Rigidbody body;


    //------------------------------------------------------
    //                  NEW VARIABLES
    //------------------------------------------------------

    // Physics Movement
	[Space]
	[SerializeField] private ActionBasedController controller;
	[SerializeField] private Transform palm;
	[SerializeField] private float reachDistance = 0.1f, joinDistance = 0.05f;
	[SerializeField] private LayerMask grabbableLayer;

	private bool _isGrabbing; 
	private GameObject _heldObject; 
	private Transform _grabPoint; 
	private FixedJoint _joint1, _joint2;

    //------------------------------------------------------
    //                  STANDARD METHODS
    //------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        // Animation
        animator = GetComponent<Animator>();

        // Physics Movement
        followTarget = followObject.transform;
        body = GetComponent<Rigidbody>();
        body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        body.interpolation = RigidbodyInterpolation.Interpolate;
        body.mass = 20f;
        body.MaxAngularVelocity = 20f;

        // Inputs Setup
        controller.selectAction.action.started += Grab;
        controller.selectAction.action.canceled += Release;

        // Teleport Hands
        body.position = followTarget.position;
        body.rotation = followTarget.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();

        PhysicsMove();
    }

    //------------------------------------------------------
    //                  PHYSICS METHODS
    //------------------------------------------------------

    private void PhysicsMove() {
        // Position
        var positionWithOffset = followTarget.position + positionOffset;
        var distance = Vector3.Distance(positionWithOffset, transform.position);
        body.velocity = (positionWithOffset - transform.position).normalized * (followSpeed * distance);

        // Rotation
        var rotationWithOffset = followTarget.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        body.angularVelocity = axis * angle * Mathf.Deg2Rad * rotateSpeed;
    }

    //------------------------------------------------------
    //                  ANIMATION METHODS
    //------------------------------------------------------

    internal void SetGrip(float v) {
        gripTarget = v;
    }

    internal void SetTrigger(float v) {
        triggerTarget = v;
    }

    void AnimateHand() {
        if(gripCurrent != gripTarget) {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(animatorGripParam, gripCurrent);
        }
        if(triggerCurrent != triggerTarget) {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(animatortriggerParam, triggerCurrent);
        }
    }

    //------------------------------------------------------
    //                  NEW METHODS
    //------------------------------------------------------

    private void Grab(InputAction.CallbackContext context)
    {
		if (_isGrabbing || (bool)_heldObject) return;

		Collider[] grabbableColliders = Physics.OverlapSphere(palm.position, reachDistance, (int)grabbableLayer); 
		if (grabbablecolliders.Length < 1) return;

		var objectToGrab = grabbableColliders[0].transform.gameObject;

		var objectBody = objectToGrab.GetComponent<Rigidbody>();

		if (objectBody != null)
		{
			_heldObject = objectBody.gameObject;
		}
        else
        {
			objectBody = objectToGrab.GetComponentInParent();
			if (objectBody != null)
			{
				_heldObject = objectBody.gameObject;
			}
            else
            {
				return;
			}
		}
		StartCoroutine(Grabobject(grabbableColliders[0], objectBody));
	}

	private IEnumerator Grabobject(Collider collider, Rigidbody targetBody)
	{
		LisGrabbing = true;

		// Create a grab point
		_grabPoint = new GameObject().transform;
		_grabPoint.position = collider.closestPoint(palm.position);
		_grabPoint.parent = _heldObject.transform;

		// Move hand to grab point
		followTarget = _grabPoint;

		// Wait for hand to reach grab point
		while (_grabPoint != null && Vector3.Distance(_grabPoint.position, palm.position) > joinDistance && isGrabbing)
        {
			yield return new WaitForEndOfFrame();
		}

		// Freeze hand and object motion
		body.velocity = Vector3.zero; 
		body.angularVelocity = Vector3.zero;
		targetBody.velocity = Vector3.zero;
		targetBody.angularVelocity = Vector3.zero;

		targetBody.collisionDetectionMode = CollisionDetectionMode.Continuous; 
		targetBody.interpolation = RigidbodyInterpolation.Interpolate;

		// Attach joints
		_joint1 = gameObject.AddComponent<FixedJoint>();
		_jointi.connectedBody = targetBody;
		_joint1.breakForce = float.PositiveInfinity;
		_jointi.breakTorque = float.PositiveInfinity;

		_jointi.connectedMassScale = 1;
		_joint1.massScale = 1;
		_jointi.enableCollision = false;
		_joint1.enablePreprocessing = false;

		_joint2 = _heldObject.AddComponent<FixedJoint>();
		_joint2.connectedBody = body;
		_joint2.breakForce = float.PositiveInfinity;
		_joint2.breakTorque = float.PositiveInfinity;
			  
		_joint2.connectedMassScale = 1;
		_joint2.massScale = 1;
		_joint2.enableCollision = false;
		_joint2.enablePreprocessing = false;

		// Reset follow target
		followTarget = controller.gameObject.transform;
	}

	private void Release(InputAction.CallbackContext context)
	{
		if (_joint1 != null)
			Destroy(_joint1);
		if (_joint2 != null)
			Destroy(_joint2);
		if (_grabPoint != null)
			Destroy(_grabPoint.gameObject);

		if (_heldobject != null)
		{
			var targetBody = _heldObject.GetComponent<Rigidbody>();
			targetBody.collisionDetectionMode = CollisionDetectionMode.Discrete;
			targetBody.interpolation = RigidbodyInterpolation.None;
			_heldobject = null;
		}
	
		_isGrabbing = false; 
		followTarget = controller.gameObject.transform;
	}

}
