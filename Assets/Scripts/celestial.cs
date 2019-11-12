
using System.Net;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Celestial
{
    [Serializable]
    public class RawData
    {
        public string pl_hostname;
        public float ra;
        public float dec;
        public float st_dist;
        public string pl_name;
        public float pl_orbsmax;
        public float pl_orbseccen;
        public float pl_orbincl;
        public float pl_orbper;
    }
    [Serializable]
    public class Observed
    {
        public RawData[] rawData;
    }
    public class Star
    {
        public string name;
        public float distance;
        public Vector3 startingPosition;
        public Vector3 location;
        public List<Exoplanet> planets;
    }
    public class Exoplanet
    {
        public string name;
        public float smAxis;
        public float eccentricity;
        public float inclination;
        public float period;
    }
}
  