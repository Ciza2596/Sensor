using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sensor
{
    public abstract class ISensor : MonoBehaviour
    {
        //public
        [ShowInInspector]
        [ReadOnly]
        public bool IsTrigger => (_collider2Ds != null && _collider2Ds.Length > 0);

        [ShowInInspector]
        [ReadOnly]
        public Collider2D Content => IsTrigger ? _collider2Ds[0] : null;

        [ShowInInspector]
        [ReadOnly]
        public Collider2D[] Contents => _collider2Ds;

        public abstract ISetting GetSetting();
        public abstract void     SetSetting(ISetting setting);
        public abstract void     OnFixedUpdate(float  fixedDeltaTime);
        

        //protected
        protected abstract void DrawGizmos(Vector3 selfPosition, Quaternion selfLocalRotation, Vector3 selfLocalScale);

        protected float GetAngle() => transform.rotation.eulerAngles.z;
        
        [SerializeField]
        protected GizmosSetting _gizmosSetting = new GizmosSetting();

        protected Collider2D[] _collider2Ds;
        
    
        //unity callback
        private void OnDrawGizmos() {
            if(!_gizmosSetting.IsShow) return;
            Gizmos.color = IsTrigger ? _gizmosSetting.IsTriggerColor : _gizmosSetting.NormalColor;

            var transform = this.transform;
            DrawGizmos(transform.position, transform.localRotation, transform.localScale);
        }
        

        //model
        [Serializable]
        public class GizmosSetting
        {
            public bool IsShow = true;

            [Space]
            public Color NormalColor = new Color(0.25f, 1, 0.27f, 0.588f);

            public Color IsTriggerColor = new Color(1, 0.25f, 0.4f, 0.588f);
        }

        public interface ISetting { }
    }
}
