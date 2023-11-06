using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP
{
    public interface IProjectile
    {
        public int Damage { get; set; }
        public int Speed { get; set; }
        public string Type { get; set; }

        public void OnCollision();
    }
}
