﻿[assembly: BenchmarkDotNet.Attributes.Config(typeof(Gu.State.Benchmarks.MemoryDiagnoserConfig))]
namespace Gu.State.Benchmarks
{
    using BenchmarkDotNet.Configs;

    public class MemoryDiagnoserConfig : ManualConfig
    {
        public MemoryDiagnoserConfig()
        {
            this.Add(new BenchmarkDotNet.Diagnosers.MemoryDiagnoser());
        }
    }
}
