using System;
using UnityEngine;

namespace Sensor
{
    public abstract class ISensor : MonoBehaviour
    {
        //protected variable
        [SerializeField] protected GizmosSetting _gizmosSetting = new GizmosSetting();
        protected Collider2D[] _collider2Ds;
        
        
        //public variable
        public bool IsTrigger => (_collider2Ds != null && _collider2Ds.Length > 0);
        public Collider2D Content => IsTrigger ? _collider2Ds[0] : null;
        public Collider2D[] Contents => _collider2Ds;
        
        
        //unity callback
        private void OnDrawGizmos()
        {
            if (!_gizmosSetting.IsShow) return;
            Gizmos.color = IsTrigger ? _gizmosSetting.IsTriggerColor : _gizmosSetting.NormalColor;

            var transform = this.transform;
            DrawGizmos(transform.position, transform.localRotation, transform.localScale);
        }

        //public method
        public abstract ISetting GetSetting();
        public abstract void SetSetting(ISetting setting);
        public abstract void OnFixedUpdate(float fixedDeltaTime);
        

        //protected method
        protected abstract void DrawGizmos(Vector3 selfPosition, Quaternion selfLocalRotation, Vector3 selfLocalScale);
        protected float GetAngle() => transform.rotation.eulerAngles.z;


        //model
        [Serializable]
        public class GizmosSetting
        {
            public bool IsShow = true;

            [Space] public Color NormalColor = new Color(0.25f, 1, 0.27f, 0.588f);

            public Color IsTriggerColor = new Color(1, 0.25f, 0.4f, 0.588f);
        }

        public interface ISetting
        {
        }
    }
}