﻿namespace Appium.Integration.Tests.Properties {

    [global::System.CodeDom.Compiler.GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCode()]
    [global::System.Runtime.CompilerServices.CompilerGenerated()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Appium.Integration.Tests.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static byte[] PathToLinuxNode {
            get {
                object obj = ResourceManager.GetObject("PathToLinuxNode", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        internal static byte[] PathToMacOSNode {
            get {
                object obj = ResourceManager.GetObject("PathToMacOSNode", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        internal static byte[] PathToWindowsNode {
            get {
                object obj = ResourceManager.GetObject("PathToWindowsNode", resourceCulture);
                return ((byte[])(obj));
            }
        }
    }
}
