// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using ReactNative;
using ReactNative.Modules.Core;
using ReactNative.Shell;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ReactNative.Bridge;

namespace Playground.Net46
{
    internal class AppReactPage : ReactPage
    {
        public override string MainComponentName => "Playground.Net46";

        public override string JavaScriptMainModuleName => "ReactWindows/Playground.Net46/index.windows";

        public async Task<ReactContext> GetCurrentReactContext()
        {
            return await ReactInstanceManager.GetOrCreateReactContextAsync(CancellationToken.None);
        }

#if BUNDLE
        public override string JavaScriptBundleFile => AppDomain.CurrentDomain.BaseDirectory + "ReactAssets/index.windows.bundle";
#endif

        public override List<IReactPackage> Packages => new List<IReactPackage>
        {
            new MainReactPackage(),
        };

        public override bool UseDeveloperSupport
        {
            get
            {
#if !BUNDLE || DEBUG
                return true;
#else
                return false;
#endif
            }
        }
    }
}
