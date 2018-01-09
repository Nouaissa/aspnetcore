﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Reflection;
using Xunit.Sdk;

namespace Microsoft.AspNetCore.Razor.Design.IntegrationTests
{
    public class InitializeTestProjectAttribute : BeforeAfterTestAttribute
    {
        private readonly string _projectName;
        private readonly string[] _additionalProjects;

        public InitializeTestProjectAttribute(string projectName, params string[] additionalProjects)
        {
            _projectName = projectName;
            _additionalProjects = additionalProjects;
        }

        public override void Before(MethodInfo methodUnderTest)
        {
            if (!typeof(MSBuildIntegrationTestBase).GetTypeInfo().IsAssignableFrom(methodUnderTest.DeclaringType.GetTypeInfo()))
            {
                throw new InvalidOperationException($"This should be used on a class derived from {typeof(MSBuildIntegrationTestBase)}");
            }

            MSBuildIntegrationTestBase.Project = ProjectDirectory.Create(_projectName, _additionalProjects);
        }

        public override void After(MethodInfo methodUnderTest)
        {
            if (!typeof(MSBuildIntegrationTestBase).GetTypeInfo().IsAssignableFrom(methodUnderTest.DeclaringType.GetTypeInfo()))
            {
                throw new InvalidOperationException($"This should be used on a class derived from {typeof(MSBuildIntegrationTestBase)}");
            }

            MSBuildIntegrationTestBase.Project.Dispose();
            MSBuildIntegrationTestBase.Project = null;
        }
    }
}
