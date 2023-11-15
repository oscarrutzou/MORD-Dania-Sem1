using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MordSem1OOP
{
    internal class NumberPillar 
    {
        private int setLoopAmount = AnimatedCounter.random.Next(3, 6);

        public int SetLoopAmount { get => setLoopAmount; set => setLoopAmount = value; }

        public NumberPillar(int setLoopAmount)
        {
            this.setLoopAmount = setLoopAmount;
        }
    }
}
