using System;
using UnityEngine;

[Serializable]
public class JsonRadarogramReader : MonoBehaviour
{
    [Serializable]
    public class images
    {
        [SerializeField]
        public int[] jpg0/* = new int[2] {1504, 300}*/;
        [SerializeField]
        public int[] jpg1/* = new int[2] {1504, 300}*/;
        [SerializeField]
        public int[] jpg2/* = new int[2] {1504, 300}*/;
        [SerializeField]
        public int[] jpg3;
        public int[] jpg4;
        public int[] jpg5;
        public int[] jpg6;
        public int[] jpg7;
        public int[] jpg8;
        public int[] jpg9;
        public int[] jpg10;
    }

    [Serializable]
    public class CommonData
    {
        [SerializeField]
        public images images;

        [SerializeField]
        public double time/* = 1623911573.2986324*/;

        [SerializeField]
        public string id;
    }

}
