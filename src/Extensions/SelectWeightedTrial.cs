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
public class SelectWeightedTrial
{
    public IObservable<WeightedTrial> Process(IObservable<Tuple<List<WeightedTrial>, double>> source)
    {
        return source.Select(value => {
            var weightedTrials = value.Item1;
            var selectThreshold = value.Item2;
            var orderedTrials = weightedTrials.OrderByDescending(x => x.Weight);

            double cumulative = 0;
            foreach (var trial in orderedTrials)
            {
                cumulative += trial.Weight;
                if (selectThreshold < cumulative)
                {
                    return trial;
                }
            }

            return null;
        });
    }
}
