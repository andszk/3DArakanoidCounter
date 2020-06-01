using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.PowerUps
{
    class DoublePadSize : PowerUp
    {
        public override void ResolveEffect()
        {
            var pad  = FindObjectOfType<PadMovement>();
            pad.DoubleScaleForTime(15);
        }
    }
}
