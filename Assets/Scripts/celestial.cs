
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
        public string name; // Name of star
        public float distance; // Distance from the Earth
        public Vector3 startingPosition; // Globe position
        public Vector3 location; // Location in field-mode
        public List<Exoplanet> planets; // planets in orbit of this star.
    }
    public class Exoplanet
    {
        public string name;  // Planet Name
        public float smAxis;  // Semi-Major Axis in AU
        public float eccentricity;  // Orbit Eccentricity
        public float inclination; // Orbit inclination in degrees
        public float period; // Orbital period in days
    }
}
  