using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisDotNet.Blocks
{
    public class ZBlock : Block
    {
        private readonly Position[][] tuiles = new Position[][]
        {
            new Position[] {new(0,0), new(0, 1), new(1, 1), new(1, 2)},
            new Position[] {new(0, 2), new(1, 1), new(1, 2), new(2, 1)},
            new Position[] {new(1, 0), new(1, 1), new(2, 1), new(2, 2)},
            new Position[] {new(0, 1), new(1, 0), new(1,1), new (2,0)}
        };

        public override int Id => 7;
        protected override Position DecalageDeDepart => new Position(0, 3);
        protected override Position[][] Tuiles => tuiles;
    }
}
