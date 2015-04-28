using System;
using System.Reflection;
using System.Diagnostics;

namespace ATP.LoggingTools {

    public class StackInfo {
        #region FIELDS
        private StackTrace stackTrace = new StackTrace();
        private StackFrame frame;
        private MethodBase method;
        private Type classType;
        #endregion

        #region PROPERTIES
        public string MethodName {
            get {
                if (frame != null) {
                    return method.Name;
                }
                return "[Method info is not available]";
            }
        }

        public string MethodSignature {
            get {
                if (frame != null) {
                    return method.ToString();
                }
                return "[Method info is not available]";
            }
        }

        public string ClassName {
            get {
                // Make sure that there's a frame to get the info from.
                if (frame != null) {
                    return classType.Name;
                }
                return "[Class info is not available]";
            }
        }

        public string QualifiedClassName {
            get {
                // Make sure that there's a frame to get the info from.
                if (frame != null) {
                    return classType.ToString();
                }
                return "[Class info is not available]";
            }
        }

        public int FrameCount {
            get {
                return stackTrace.FrameCount;
            }
        }
        #endregion

        #region METHODS
        public StackInfo(int frameIndex) {
            frame = stackTrace.GetFrame(frameIndex);

            // Frame can be null.
            try {
                method = frame.GetMethod();
                classType = method.DeclaringType;
            }
            catch (NullReferenceException e) {
                UnityEngine.Debug.LogWarning("Frame not found: " + e);
            }
        }
        #endregion
    }
}
