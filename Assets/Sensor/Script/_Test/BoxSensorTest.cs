using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace Sensor.Test
{
    public class BoxSensorTest : BaseSensorTest
    {
        private BoxSensor _sensor;

        [SetUp]
        public void SetUp() {
            var boxSensorObject = new GameObject();
            _sensor = boxSensorObject.AddComponent<BoxSensor>();

            _testObject = new GameObject();
            _testObject.AddComponent<BoxCollider2D>();
        }


        [Test]
        [TestCase(1, 1, true)]
        [TestCase(2, 2, true)]
        public void When_SameLayerAndSamePosition_Then_SensorTrigger(int  sensorLayer,
                                                                     int  testObjectLayer,
                                                                     bool isTrigger) {
            //arrange
            var layerMask = new LayerMask();
            layerMask = 1 << sensorLayer;
            var setting = new BoxSensor.Setting(layerMask);
            _sensor.SetSetting(setting);
            _sensor.gameObject.transform.position = _testObject.transform.position;

            var testObjectName = Time.time.ToString();
            _testObject.name  = testObjectName;
            _testObject.layer = testObjectLayer;

            //act
            _sensor.OnFixedUpdate(0);

            //assert
            var contents = _sensor.Contents.ToList();
            var Object   = contents.Find(data => data.gameObject.name == testObjectName);
            Assert.AreEqual( Object != null, isTrigger);
        }

        [Test]
        [TestCase(1, 0, false)]
        [TestCase(2, 3, false)]
        public void When_DifferentLayerAndSamePosition_Then_SensorDontTrigger(int  sensorLayer,
                                                                              int  testObjectLayer,
                                                                              bool isTrigger) {
            //arrange
            var layerMask = new LayerMask();
            layerMask = 1 << sensorLayer;
            var setting = new BoxSensor.Setting(layerMask);
            _sensor.SetSetting(setting);
            _sensor.gameObject.transform.position = _testObject.transform.position;

            var testObjectName = Time.time.ToString();
            _testObject.name  = testObjectName;
            _testObject.layer = testObjectLayer;

            //act
            _sensor.OnFixedUpdate(0);

            //assert
            var contents = _sensor.Contents.ToList();
            var Object   = contents.Find(data => data.gameObject.name == testObjectName);
            Assert.AreEqual( Object != null, isTrigger);
        }

        [Test]
        [TestCase(1, 1, false)]
        [TestCase(2, 2, false)]
        public void When_SameLayerAndDifferentPosition_Then_SensorDontTrigger(int  sensorLayer,
                                                                              int  testObjectLayer,
                                                                              bool isTrigger) {
            //arrange
            var layerMask = new LayerMask();
            layerMask = 1 << sensorLayer;
            var setting = new BoxSensor.Setting(layerMask);
            _sensor.SetSetting(setting);
            _sensor.gameObject.transform.position = _distantPosition;

            var testObjectName = Time.time.ToString();
            _testObject.name  = testObjectName;
            _testObject.layer = testObjectLayer;

            //act
            _sensor.OnFixedUpdate(0);

            //assert
            var contents = _sensor.Contents.ToList();
            var Object   = contents.Find(data => data.gameObject.name == testObjectName);
            Assert.AreEqual( Object != null, isTrigger);
        }
    }
}
