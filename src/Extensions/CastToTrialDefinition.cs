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
public class CastToTrialDefinition
{
    public IObservable<TrialDefinition> Process(IObservable<Object> source)
    {
        return source.Select(value => (TrialDefinition)value);
    }
}
