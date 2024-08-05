using System;
using UnityEngine;

namespace Assets.Script
{
    [Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int amount;
        public float rate;
    }
}