using System.Collections.Generic;
using UnityEngine;

namespace Sensor {
    public class SensorExecutor : MonoBehaviour {

        //private variable
        [SerializeField]
        private List<ISensor> _sensors = new List<ISensor>();

        //unity callback
        private void FixedUpdate() {
            var fixedDeltaTime = Time.fixedDeltaTime;
            
            if(fixedDeltaTime <= 0)
                return;
            
            foreach (var sensor in _sensors) {
                if(sensor.gameObject.activeSelf) 
                    sensor.OnFixedUpdate(fixedDeltaTime);
            }
        }


        //public method
        public void AddSensor(ISensor sensor) => _sensors.Add(sensor);
        
        public void RemoveSensor(ISensor sensor) => _sensors.Remove(sensor);
    }
}