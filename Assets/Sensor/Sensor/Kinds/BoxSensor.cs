using System;
using UnityEngine;


namespace Sensor
{
    public class BoxSensor : ISensor
    {
        //private variable
        [SerializeField]
        private Setting _setting = new Setting();

        //public variable
        public override ISetting GetSetting()                  => _setting;
        
        
        //public method
        public override void     SetSetting(ISetting setting) => _setting = (Setting) setting;

        public override void OnFixedUpdate(float fixedDeltaTime) {
            var position = transform.position;
            var angle    = GetAngle();
            
            var layerMask = _setting.LayerMask;
            var size      = _setting.Size;

            UpdateContents(position, layerMask, size, angle);
        }
        
        //protected method
        protected override void DrawGizmos(Vector3 selfPosition, Quaternion selfLocalRotation, Vector3 selfLocalScale) {
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(selfPosition, selfLocalRotation, selfLocalScale);
            Gizmos.matrix = rotationMatrix;

            var size = _setting.Size;

            Gizmos.DrawCube(Vector3.zero, size);
        }


        //private method
        private void UpdateContents(Vector3   point,
            LayerMask layerMask,
            Vector3   size,
            float     angle) {
            var collider2Ds = Physics2D.OverlapBoxAll(point, size, angle, layerMask.value);
            
            _collider2Ds = collider2Ds;
        }


        //model
        [Serializable]
        public class Setting: ISetting
        {
            //private variable
            [SerializeField]
            private LayerMask _layerMask;

            [SerializeField]
            private Vector2   _size = new Vector2(1, 1);

            //public variable

            public LayerMask LayerMask => _layerMask;
            public Vector2   Size      => _size;

            //public method

            public Setting() { }
            public Setting(LayerMask layerMask) {
                _layerMask = layerMask;
            }
            public Setting(LayerMask layerMask, Vector2 size) {
                _layerMask = layerMask;
                _size      = size;
            }
        }
    }
}
