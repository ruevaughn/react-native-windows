// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Windows.Media.Effects;

namespace ReactNative.UIManager
{
    /// <summary>
    /// Simple structural type for blur effect settings.
    /// </summary>
    public class BlurEffectSettings
    {
        /// <summary>
        /// The Y-coordinate.
        /// </summary>
        public KernelType KernelType { get; set; }

        /// <summary>
        /// The blur radius.
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// The rendering bias.
        /// </summary>
        public RenderingBias RenderingBias { get; set; }
    }
}
