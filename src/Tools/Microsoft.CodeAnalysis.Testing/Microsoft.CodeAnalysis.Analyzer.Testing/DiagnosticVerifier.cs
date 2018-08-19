﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Microsoft.CodeAnalysis.Analyzer.Testing
{
    public class DiagnosticVerifier<TAnalyzer, TTest, TVerifier>
           where TAnalyzer : DiagnosticAnalyzer, new()
           where TTest : BaseAnalyzerTest<TVerifier>, new()
           where TVerifier : IVerifier, new()
    {
        public static DiagnosticResult[] EmptyDiagnosticResults { get; } = { };

        public static DiagnosticResult Diagnostic(string diagnosticId = null)
        {
            var analyzer = new TAnalyzer();
            var supportedDiagnostics = analyzer.SupportedDiagnostics;
            if (diagnosticId is null)
            {
                return Diagnostic(supportedDiagnostics.Single());
            }
            else
            {
                return Diagnostic(supportedDiagnostics.Single(i => i.Id == diagnosticId));
            }
        }

        public static DiagnosticResult Diagnostic(DiagnosticDescriptor descriptor) => new DiagnosticResult(descriptor);

        public static DiagnosticResult CompilerError(string errorIdentifier) => new DiagnosticResult(errorIdentifier, DiagnosticSeverity.Error);

        public static Task VerifyDiagnosticAsync(string source, DiagnosticResult expected, CancellationToken cancellationToken = default)
            => VerifyDiagnosticAsync(source, new[] { expected }, cancellationToken);

        public static Task VerifyDiagnosticAsync(string source, DiagnosticResult[] expected, CancellationToken cancellationToken = default)
        {
            var test = new TTest
            {
                TestCode = source,
            };

            test.ExpectedDiagnostics.AddRange(expected);
            return test.RunAsync(cancellationToken);
        }
    }
}
