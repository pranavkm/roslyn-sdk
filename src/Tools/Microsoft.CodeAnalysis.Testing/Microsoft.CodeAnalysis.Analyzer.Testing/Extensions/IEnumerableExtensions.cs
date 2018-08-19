﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.CodeAnalysis.Analyzer.Testing
{
    internal static class IEnumerableExtensions
    {
        public static DiagnosticResult[] ToOrderedArray(this IEnumerable<DiagnosticResult> diagnosticResults)
        {
            return diagnosticResults
                .OrderBy(diagnosticResult => diagnosticResult.Spans.FirstOrDefault().Path, StringComparer.Ordinal)
                .ThenBy(diagnosticResult => diagnosticResult.Spans.FirstOrDefault().Span.Start.Line)
                .ThenBy(diagnosticResult => diagnosticResult.Spans.FirstOrDefault().Span.Start.Character)
                .ThenBy(diagnosticResult => diagnosticResult.Spans.FirstOrDefault().Span.End.Line)
                .ThenBy(diagnosticResult => diagnosticResult.Spans.FirstOrDefault().Span.End.Character)
                .ThenBy(diagnosticResult => diagnosticResult.Id, StringComparer.Ordinal)
                .ToArray();
        }
    }
}
