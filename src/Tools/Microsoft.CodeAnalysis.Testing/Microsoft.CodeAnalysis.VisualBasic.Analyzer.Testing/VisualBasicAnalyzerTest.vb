﻿Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.Analyzer.Testing
Imports Microsoft.CodeAnalysis.Diagnostics
Imports Microsoft.CodeAnalysis.VisualBasic

Public Class VisualBasicAnalyzerTest(Of TAnalyzer As {DiagnosticAnalyzer, New}, TVerifier As {IVerifier, New})
    Inherits BaseAnalyzerTest(Of TVerifier)

    Public Overrides ReadOnly Property Language As String
        Get
            Return LanguageNames.VisualBasic
        End Get
    End Property

    Protected Overrides ReadOnly Property DefaultFileExt As String
        Get
            Return "vb"
        End Get
    End Property

    Protected Overrides Function CreateCompilationOptions() As CompilationOptions
        Return New VisualBasicCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
    End Function

    Protected Overrides Function GetDiagnosticAnalyzers() As IEnumerable(Of DiagnosticAnalyzer)
        Return New TAnalyzer() {New TAnalyzer()}
    End Function
End Class
