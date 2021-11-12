# Vostok.Tracing.Extensions

[![Build & Test & Publish](https://github.com/vostok/tracing.extensions/actions/workflows/ci.yml/badge.svg)](https://github.com/vostok/tracing.extensions/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/Vostok.Tracing.Extensions.svg)](https://www.nuget.org/packages/Vostok.Tracing.Extensions/)

This library contains a set of extensions for common case scenarios (such as [HTTP request tracing](Vostok.Tracing.Extensions/Http/HttpTracerExtensions.cs)). These extensions are intended to be used by instrumentation developers. They contain common boilerplate code to produce relevant span annotations. Reuse of this code eliminates most of code duplication across tracing instrumentations.

See [Vostok.Tracing.Abstractions](https://github.com/vostok/tracing.abstractions) repository for an in-depth dive into span annotations.
