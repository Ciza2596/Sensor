using System;
using UnityEngine;
using Matrix4x4 = UnityEngine.Matrix4x4;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Sensor
{
    public class CapsuleSensor : ISensor
    {
        //private
        [SerializeField]
        private Setting _setting = new Setting();


        //public

        public override ISetting GetSetting()                 => _setting;
        public override void     SetSetting(ISetting setting) => _setting = (Setting) setting;

        public override void OnFixedUpdate(float fixedDeltaTime) {
            var position = transform.position;

            var layerMask = _setting.LayerMask;

            var isVertical            = GetIsVerticalDirection();
            var capsuleDirection      = _setting.Direction;
            var height                = _setting.Height;
            var radius                = _setting.Radius;
            var diameter              = radius * 2;
            var heightIncludeDiameter = height + diameter;
            var angle                 = GetAngle();

            var halfHeight              = height / 2;
            var halfHeightIncludeRadius = halfHeight + radius;

            var angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
            var direction = isVertical ? angleAxis         * Vector3.up : angleAxis * Vector3.right;
            var point     = position + (Vector3) direction * halfHeightIncludeRadius;

            var size = isVertical
                           ? new Vector2(diameter,              heightIncludeDiameter)
                           : new Vector2(heightIncludeDiameter, diameter);

            UpdateContents(point, layerMask, capsuleDirection, size, angle);
        }
        

        //protected
        protected override void DrawGizmos(Vector3 selfPosition, Quaternion selfLocalRotation, Vector3 selfLocalScale) {
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(selfPosition, selfLocalRotation, selfLocalScale);
            Gizmos.matrix = rotationMatrix;

            var isVerticalDirection = GetIsVerticalDirection();
            var radius              = _setting.Radius;
            var height              = _setting.Height;

            DrawCapsule(isVerticalDirection, radius, height);
        }


        //private
        private void UpdateContents(Vector3            point,
                                    LayerMask          layerMask,
                                    CapsuleDirection2D capsuleDirection2D,
                                    Vector2            size,
                                    float              angle) {
            var collider2Ds = Physics2D.OverlapCapsuleAll((Vector2) point, size, capsuleDirection2D, angle, layerMask);

            _collider2Ds = collider2Ds;
        }

        private bool GetIsVerticalDirection() => _setting.Direction == CapsuleDirection2D.Vertical;


        private void DrawCapsule(bool isVerticalDirection, float radius, float height) {
            var halfHeight              = height / 2;
            var halfHeightIncludeRadius = halfHeight + radius;
            var offset = isVerticalDirection
                             ? new Vector3(0,                       halfHeightIncludeRadius)
                             : new Vector3(halfHeightIncludeRadius, 0);

            var position = isVerticalDirection ? new Vector3(0, halfHeight) : new Vector3(halfHeight, 0);
            Gizmos.DrawSphere(position + offset, radius);

            var cubeWidth  = radius * 2;
            var cubeHeight = height;
            var cubeSize   = new Vector2(cubeWidth, cubeHeight);

            position = new Vector3(0, 0, 0);
            Gizmos.DrawCube(position + offset, cubeSize);

            position = isVerticalDirection ? new Vector3(0, -halfHeight) : new Vector3(-halfHeight, 0);
            Gizmos.DrawSphere(position + offset, radius);
        }
        
        //model
        [Serializable]
        public class Setting : ISetting
        {
            //private
            [SerializeField]
            private LayerMask _layerMask;

            [Space]
            [SerializeField]
            private CapsuleDirection2D _direction = CapsuleDirection2D.Vertical;

            [SerializeField]
            private float _radius = 0.5f;

            [SerializeField]
            private float _height = 1f;


            //public
            public Setting() { }

            public Setting(LayerMask layerMask) {
                _layerMask = layerMask;
            }

            public Setting(LayerMask layerMask, CapsuleDirection2D direction, float radius, float height) {
                _layerMask = layerMask;
                _direction = direction;
                _radius    = radius;
                _height    = height;
            }

            public LayerMask          LayerMask => _layerMask;
            public CapsuleDirection2D Direction => _direction;
            public float              Radius    => _radius;
            public float              Height    => _height;

        }
        
    }
}
