using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using AindPavlovianConditioningDataSchema;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class SelectBlock
{
    private Random rng = new Random();

    // 
    public IObservable<Block> Process(IObservable<List<Block>> source)
    {
        return source.Select(value => {
            return value.OrderBy(_ => rng.Next()).ToList()[0];
        }); 
    }

    public IObservable<Block> Process(IObservable<Tuple<List<Block>, Block>> source)
    {
        return source.Select(value =>
        {
            List<Block> availableBlocks = value.Item1;
            Block currentBlock = value.Item2;

            if (availableBlocks.Count == 1)
            {
                return availableBlocks[0];
            }

            var shuffledBlocks = availableBlocks.OrderBy(_ => rng.Next()).ToList();
            foreach (Block block in shuffledBlocks)
            {
                if (block != currentBlock)
                {
                    return block;
                }
            }

            return currentBlock;
        });
    }
}
