using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP
{
    public enum ProjectileTypes
    {
        Arrow,
        Missile
    }

    public interface IProjectile
    {

        public int Damage { get; set; }
        public int Speed { get; set; }
        public GameObject Target { get; set; }
        public ProjectileTypes Type { get; set; }

    }
}
