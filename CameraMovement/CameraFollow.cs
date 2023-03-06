using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Virtuesky.common
{
    public class CameraFollow : MonoBehaviour
    {
        public TypeCamFollow TypeCamFollow;
        public GameObject TargetFollow;
        private Vector3 offset;
        [SerializeField] private float smooth = 0.1f;
        private Vector3 velocity = Vector3.zero;
        public bool isOnFollow;

        void Start()
        {
            Setup();
        }

        private void Setup()
        {
            transform.position =
                new Vector3(TargetFollow.transform.position.x, transform.position.y, transform.position.z);
            offset = transform.position - TargetFollow.transform.position;
        }

        private void Update()
        {
            if (isOnFollow)
            {
                switch (TypeCamFollow)
                {
                    case TypeCamFollow.Normal:
                        transform.position = TargetFollow.transform.position + offset;
                        break;
                    case TypeCamFollow.UseSmoothDamp:
                        transform.position = Vector3.SmoothDamp(transform.position,
                            TargetFollow.transform.position + offset,
                            ref velocity,
                            smooth * Time.deltaTime);
                        break;
                    case TypeCamFollow.UseLerp:
                        Vector3 target = TargetFollow.transform.position + offset;
                        if (transform.position != target)
                        {
                            Vector3 targetPos = new Vector3(target.x, transform.position.y, target.z);
                            transform.position = Vector3.Lerp(transform.position, targetPos, smooth);
                        }

                        break;
                }
            }
        }
    }

    public enum TypeCamFollow
    {
        Normal,
        UseSmoothDamp,
        UseLerp,
    }
}