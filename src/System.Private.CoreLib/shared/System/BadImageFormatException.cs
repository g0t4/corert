// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/*============================================================
**
**
**
** Purpose: Exception to an invalid dll or executable format.
**
** 
===========================================================*/

using System.Globalization;
using System.IO;
using System.Runtime.Serialization;

namespace System
{
    public partial class BadImageFormatException : SystemException
    {
        private String _fileName;  // The name of the corrupt PE file.
        private String _fusionLog = null;  // fusion log (when applicable)

        public BadImageFormatException()
            : base(SR.Arg_BadImageFormatException)
        {
            HResult = __HResults.COR_E_BADIMAGEFORMAT;
        }

        public BadImageFormatException(String message)
            : base(message)
        {
            HResult = __HResults.COR_E_BADIMAGEFORMAT;
        }

        public BadImageFormatException(String message, Exception inner)
            : base(message, inner)
        {
            HResult = __HResults.COR_E_BADIMAGEFORMAT;
        }

        public BadImageFormatException(String message, String fileName) : base(message)
        {
            HResult = __HResults.COR_E_BADIMAGEFORMAT;
            _fileName = fileName;
        }

        public BadImageFormatException(String message, String fileName, Exception inner)
            : base(message, inner)
        {
            HResult = __HResults.COR_E_BADIMAGEFORMAT;
            _fileName = fileName;
        }

        protected BadImageFormatException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            throw new PlatformNotSupportedException();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        public override String Message
        {
            get
            {
                SetMessageField();
                return _message;
            }
        }

        private void SetMessageField()
        {
            if (_message == null)
            {
                if ((_fileName == null) &&
                    (HResult == __HResults.COR_E_EXCEPTION))
                    _message = SR.Arg_BadImageFormatException;

                else
                    _message = FileLoadException.FormatFileLoadExceptionMessage(_fileName, HResult);
            }
        }

        public String FileName
        {
            get { return _fileName; }
        }

        public override String ToString()
        {
            String s = GetType().ToString() + ": " + Message;

            if (_fileName != null && _fileName.Length != 0)
                s += Environment.NewLine + SR.Format(SR.IO_FileName_Name, _fileName);

            if (InnerException != null)
                s = s + " ---> " + InnerException.ToString();

            if (StackTrace != null)
                s += Environment.NewLine + StackTrace;

            if (_fusionLog != null)
            {
                if (s == null)
                    s = " ";
                s += Environment.NewLine;
                s += Environment.NewLine;
                s += _fusionLog;
            }

            return s;
        }

        public String FusionLog
        {
            get { return _fusionLog; }
        }
    }
}
