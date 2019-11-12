
using System.Net;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Celestial
{
    [Serializable]
    public class Star
    {
        public string pl_hostname;
        public float ra;
        public float dec;
        public float st_dist;
        public Vector3 startingPosition;
        public Vector3 location;
        //public Exoplanet [] planets;
    }
     [Serializable]
     public class Observed
     {
         public Star[] stars;
     }



//   public class Exoplanet
//   {
//       public string name;
//   }
}
  