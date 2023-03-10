using Sirenix.OdinInspector;
using UnityEngine;

namespace Virtuesky
{
    public class CameraMovement : MonoBehaviour
    {
        public Camera Camera;
        protected Plane Plane;
        public bool Rotate;

        #region Scroll

        public bool isScroll;

        [ShowIf(nameof(isScroll), true)] [SerializeField]
        private TypeLimitCamera TypeLimitCamera;

        [ShowIf(nameof(TypeLimitCamera), TypeLimitCamera.LimitCoordinate)] [SerializeField]
        private bool isLimitX;

        [ShowIf(nameof(isLimitX), true)] [SerializeField]
        private float maxX;

        [ShowIf(nameof(isLimitX), true)] [SerializeField]
        private float minX;

        [ShowIf(nameof(TypeLimitCamera), TypeLimitCamera.LimitCoordinate)] [SerializeField]
        private bool isLimitY;

        [ShowIf(nameof(isLimitY), true)] [SerializeField]
        private float maxY;

        [ShowIf(nameof(isLimitY), true)] [SerializeField]
        private float minY;

        [ShowIf(nameof(TypeLimitCamera), TypeLimitCamera.LimitCoordinate)] [SerializeField]
        private bool isLimitZ;

        [ShowIf(nameof(isLimitZ), true)] [SerializeField]
        private float maxZ;

        [ShowIf(nameof(isLimitZ), true)] [SerializeField]
        private float minZ;

        [ShowIf(nameof(TypeLimitCamera), TypeLimitCamera.LimitCircle)] [SerializeField]
        private Transform transformCenter;

        [ShowIf(nameof(TypeLimitCamera), TypeLimitCamera.LimitCircle)] [SerializeField]
        private float radius = 5;

        [ShowIf(nameof(TypeLimitCamera), TypeLimitCamera.LimitCircle)] [SerializeField]
        private bool isLockAtCenter;

        #endregion

        #region Pinch

        public bool isPinch;

        [ShowIf(nameof(isPinch), true)] [SerializeField]
        private int minZoom = 0;

        [ShowIf(nameof(isPinch), true)] [SerializeField]
        private int maxZoom = 10;

        #endregion


        private void Update()
        {
            //Update Plane
            if (Input.touchCount >= 1)
                Plane.SetNormalAndPosition(transform.up, transform.position);

            var Delta1 = Vector3.zero;
            var Delta2 = Vector3.zero;

            //Scroll
            if (Input.touchCount >= 1 && isScroll)
            {
                Delta1 = PlanePositionDelta(Input.GetTouch(0));
                switch (TypeLimitCamera)
                {
                    case TypeLimitCamera.None:
                        break;
                    case TypeLimitCamera.LimitCoordinate:
                        var newPosition = Camera.transform.position + Delta1;
                        if (isLimitX)
                        {
                            if (newPosition.x > maxX || newPosition.x < minX) return;
                        }

                        if (isLimitY)
                        {
                            if (newPosition.y > maxY || newPosition.y < minY) return;
                        }

                        if (isLimitZ)
                        {
                            if (newPosition.z > maxZ || newPosition.z < minZ) return;
                        }

                        if (Input.GetTouch(0).phase == TouchPhase.Moved)
                        {
                            Camera.transform.Translate(Delta1, Space.World);
                        }

                        break;
                    case TypeLimitCamera.LimitCircle:
                        float distance = Vector3.Distance(Camera.transform.position, transformCenter.position);
                        if (distance > radius)
                        {
                            Camera.transform.position = transformCenter.position +
                                                        (Camera.transform.position - transformCenter.position)
                                                        .normalized *
                                                        radius;
                            if (isLockAtCenter)
                            {
                                Camera.transform.LookAt(transformCenter.position);
                            }
                        }
                        else
                        {
                            if (Input.GetTouch(0).phase == TouchPhase.Moved)
                            {
                                Camera.transform.Translate(Delta1, Space.World);
                            }
                        }

                        break;
                }
            }

            //Pinch
            if (Input.touchCount >= 2 && isPinch)
            {
                var pos1 = PlanePosition(Input.GetTouch(0).position);
                var pos2 = PlanePosition(Input.GetTouch(1).position);
                var pos1b = PlanePosition(Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition);
                var pos2b = PlanePosition(Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition);

                //calc zoom
                var zoom = Vector3.Distance(pos1, pos2) /
                           Vector3.Distance(pos1b, pos2b);

                //edge case
                if (zoom == minZoom || zoom > maxZoom)
                    return;

                //Move cam amount the mid ray
                Camera.transform.position = Vector3.LerpUnclamped(pos1, Camera.transform.position, 1 / zoom);

                if (Rotate && pos2b != pos2)
                    Camera.transform.RotateAround(pos1, Plane.normal,
                        Vector3.SignedAngle(pos2 - pos1, pos2b - pos1b, Plane.normal));
            }
        }

        protected Vector3 PlanePositionDelta(Touch touch)
        {
            //not moved
            if (touch.phase != TouchPhase.Moved)
                return Vector3.zero;

            //delta
            var rayBefore = Camera.ScreenPointToRay(touch.position - touch.deltaPosition);
            var rayNow = Camera.ScreenPointToRay(touch.position);
            if (Plane.Raycast(rayBefore, out var enterBefore) && Plane.Raycast(rayNow, out var enterNow))
                return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);

            //not on plane
            return Vector3.zero;
        }

        protected Vector3 PlanePosition(Vector2 screenPos)
        {
            //position
            var rayNow = Camera.ScreenPointToRay(screenPos);
            if (Plane.Raycast(rayNow, out var enterNow))
                return rayNow.GetPoint(enterNow);

            return Vector3.zero;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, transform.position + transform.up);
        }
    }

    public enum TypeLimitCamera
    {
        None,
        LimitCoordinate,
        LimitCircle,
    }
}