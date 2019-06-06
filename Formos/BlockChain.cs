using System;
using System.Collections.Generic;
using System.Text;

namespace Formos {
    [Serializable]
    public class Blockchain {
        public List<Block> Chain { set; get; }
        public string origin { set; get; }

        public Blockchain()
        {
            InitializeChain();
            AddGenesisBlock();
        }

        public void InitializeChain()
        {
            Chain = new List<Block>();
        }

        public Block CreateGenesisBlock()
        {
            return new Block(null, "{}");
        }

        public void AddGenesisBlock()
        {           
            Chain.Add(CreateGenesisBlock());
        }

        public Block GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }

        public void AddBlock(Block block)
        {
            Block latestBlock = GetLatestBlock();
            block.Index = latestBlock.Index + 1;
            block.PreviousHash = latestBlock.Hash;
            block.Hash = block.CalculateHash();
            block.Date = DateTime.Now;
            Chain.Add(block);
        }
    }
}
