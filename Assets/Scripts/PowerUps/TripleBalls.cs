using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.PowerUps
{
    public class TripleBalls : PowerUp
    {
        public override void ResolveEffect()
        {
            FindObjectOfType<BallController>().TripleBalls();
        }
    }
}
