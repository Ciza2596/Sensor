using System;
using UnityEngine;

namespace Sensor
{
    public class CircleSensor : ISensor
    {
        //private
        
        [SerializeField]
        private Setting _setting = new Setting();
        
        
        //public

        public override ISetting GetSetting() => _setting;

        public override void SetSetting(ISetting setting) => _setting = (Setting) setting;

        public override void OnFixedUpdate(float fixedDeltaTime) {
            var position = transform.position;

            var layerMask = _setting.LayerMask;
            var radius    = _setting.Radius;

            UpdateContents(position, layerMask, radius);
        }
        

        //protected
        
        protected override void DrawGizmos(Vector3 selfPosition, Quaternion selfLocalRotation, Vector3 selfLocalScale) {
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(selfPosition, selfLocalRotation, selfLocalScale);
            Gizmos.matrix = rotationMatrix;

            var radius = _setting.Radius;

            Gizmos.DrawSphere(Vector3.zero, radius);
        }

        
        //private
        private void UpdateContents(Vector3   point,
                                    LayerMask layerMask,
                                    float     radius) {
            var collider2Ds = Physics2D.OverlapCircleAll(point, radius, layerMask);

            _collider2Ds = collider2Ds;
        }
        
        //model

        [Serializable]
        public class Setting : ISetting
        {
            //private
            [SerializeField]
            private LayerMask _layerMask;

            private float _radius = 0.5f;
            
            
            //public
            public Setting() { }

            public Setting(LayerMask layerMask) {
                _layerMask = layerMask;
            }

            public Setting(LayerMask layerMask, float radius) {
                _layerMask = layerMask;
                _radius    = radius;
            }
            
            public LayerMask LayerMask => _layerMask;
            public float     Radius    => _radius;
            
            
        }
        
    }
}
