using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.Interpolation;
using Bonsai;
using System.Reactive.Linq;
using MathNet.Numerics.Distributions;
using System.ComponentModel;

namespace AindPavlovianConditioningDataSchema
{
    public interface IDistribution
    {
        double Sample();

        double[] Samples(double[] arr);
    }

    public class ContinuousDistributionWrapper : IDistribution
    {
        private readonly IContinuousDistribution _distribution;

        public ContinuousDistributionWrapper(IContinuousDistribution distribution)
        {
            _distribution = distribution;
        }

        public double Sample()
        {
            return _distribution.Sample();
        }

        public double[] Samples(double[] arr)
        {
            _distribution.Samples(arr);
            return arr;
        }
    }

    partial class Distribution
    {
        private const uint SampleSize = 1000;

        public virtual double SampleDistribution(Random random)
        {
            throw new NotImplementedException();
        }

        public virtual IDistribution GetDistribution(Random random)
        {
            throw new NotImplementedException();
        }

        private static double ApplyScaleAndOffset(double value, ScalingParameters scalingParameters)
        {
            return scalingParameters == null ? value : value * scalingParameters.Scale + scalingParameters.Offset;
        }

        public double DrawSample(IDistribution distribution, ScalingParameters scalingParameters, TruncationParameters truncationParameters)
        {
            if (truncationParameters == null)
            {
                return ApplyScaleAndOffset(distribution.Sample(), scalingParameters);
            }

            ValidateTruncationParameters(truncationParameters);

            switch (truncationParameters.TruncationMode)
            {
                case TruncationParametersTruncationMode.Clamp:
                    var sample = ApplyScaleAndOffset(distribution.Sample(), scalingParameters);
                    return Math.Min(Math.Max(sample, truncationParameters.Min), truncationParameters.Max);
                case TruncationParametersTruncationMode.Exclude:
                    double[] samples = new double[SampleSize];
                    distribution.Samples(samples);
                    var scaledSamples = samples.Select(x => ApplyScaleAndOffset(x, scalingParameters)).ToArray();
                    return ValidateTruncationExcludeMode(scaledSamples, truncationParameters);
                default:
                    throw new ArgumentException("Invalid truncation mode.");
            }
        }

        private static double ValidateTruncationExcludeMode(double[] drawnSamples, TruncationParameters truncationParameters)
        {
            double outValue;
            var average = drawnSamples.Average();
            var truncatedSamples = drawnSamples.Where(x => x >= truncationParameters.Min && x <= truncationParameters.Max);
         
            if (truncatedSamples.Count() <= 0)
            {
                if (average <= truncationParameters.Min)
                {
                    outValue = truncationParameters.Min;
                }
                else if (average >= truncationParameters.Max)
                {
                    outValue = truncationParameters.Max;
                }
                else
                {
                    throw new ArgumentException("Truncation heuristic has failed. Please check your truncation parameters.");
                }
            }
            else
            {
                outValue = truncatedSamples.First();
            }
            return outValue;
        }

        private static void ValidateTruncationParameters(TruncationParameters truncationParameters)
        {
            if (truncationParameters == null) { return; }
            if (truncationParameters.Min > truncationParameters.Max)
            {
                throw new ArgumentException("Invalid truncation parameters. Min must be lower than Max");
            }
        }
    }

    partial class ExponentialDistribution
    {
        public override IDistribution GetDistribution(Random random)
        {
            return new ContinuousDistributionWrapper(new Exponential(DistributionParameters.Rate, random));
        }

        public override double SampleDistribution(Random random)
        {
            return DrawSample(GetDistribution(random), ScalingParameters, TruncationParameters);
        }
    }

    [Combinator]
    [Description("Samples a value for a known distribution.")]
    [WorkflowElementCategory(ElementCategory.Transform)]

    public class SampleDistribution
    {

        private Random randomSource;
        public Random RandomSource
        {
            get { return randomSource; }
            set { randomSource = value; }
        }

        public IObservable<double> Process(IObservable<Distribution> source)
        {
            return source.Select(value => value.SampleDistribution(RandomSource));
        }
    }
}

