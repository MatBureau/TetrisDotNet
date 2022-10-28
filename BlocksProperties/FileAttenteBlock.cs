using TetrisDotNet.Blocks;
using System;

namespace TetrisDotNet.BlocksProperties
{
    public class FileAttenteBlock
    {
        private readonly Block[] blocks = new Block[]
        {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new OBlock(),
            new SBlock(),
            new TBlock(),
            new ZBlock()
        };

        private readonly Random random = new Random();

        public Block BlockSuivant { get; private set; }
        
        public FileAttenteBlock()
        {
            BlockSuivant = BlockAleatoire();
        }

        private Block BlockAleatoire()
        {
            return blocks[random.Next(blocks.Length)];
        }

        public Block GetEtUpdate()
        {
            Block block = BlockSuivant;

            do
            {
                BlockSuivant = BlockAleatoire();
            }
            while (block.Id == BlockSuivant.Id);

            return block;
        }
    }
}
