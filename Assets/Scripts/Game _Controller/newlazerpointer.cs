//using UnityEngine;

//public class newLaserPointer : MonoBehaviour
//{
//   [SerializeField]
//    private LineRenderer lineRenderer;
//    [SerializeField]
//    private GameObject dot;

//    private IControllerInput controller;
//    private Transform controllerTransform;

//    private RaycastTargetButton currentButton = null;
//    private bool wasTriggerPressedLastFrame = false;

//    public void Initialize(IControllerInput controller)
//    {
//        this.controller = controller;
//        controllerTransform = controller.GetTransform();
//        dot.SetActive(false);
//    }

//    private void LateUpdate()
//    {
//        transform.position = controllerTransform.position;
//        transform.rotation = controllerTransform.rotation;

//        RaycastTargetButton newButton = null;
//        ExplodeOnTouch explodeTarget = null;

//        var ray = new Ray(transform.position, transform.forward);
//        if (Physics.Raycast(ray, out var hitInfo))
//        {
            // Adjust length of the laser line
//            Vector3 localHitPoint = Vector3.forward * hitInfo.distance;
//            lineRenderer.SetPosition(1, localHitPoint);

            // Update laser dot
//            dot.transform.localPosition = localHitPoint;
//            dot.SetActive(true);

            // Check for RaycastTargetButton
//            newButton = hitInfo.collider.GetComponentInParent<RaycastTargetButton>();

            // Check for ExplodeOnTouch
//            explodeTarget = hitInfo.collider.GetComponentInParent<ExplodeOnTouch>();
//        }
//        else
//        {
//            dot.SetActive(false);
//            lineRenderer.SetPosition(1, Vector3.forward * 100f);
//        }

        // Update which button is in focus
//        if (newButton != currentButton)
//        {
//            if (currentButton != null)
//            {
//                currentButton.OnHoverExit();
//            }

//            currentButton = newButton;

//            if (currentButton != null)
//            {
//                currentButton.OnHoverEnter();
//            }
//        }

        // Check if the trigger was pressed this frame
//        if (controller.IsTriggerPressed() && !wasTriggerPressedLastFrame)
//        {
//            if (currentButton != null)
//            {
//                currentButton.OnClicked();
//            }

            // Trigger explosion if ExplodeOnTouch is hit
//            if (explodeTarget != null)
//            {
//                explodeTarget.Explode();
//            }
//        }

//        wasTriggerPressedLastFrame = controller.IsTriggerPressed();
//    }
//}
