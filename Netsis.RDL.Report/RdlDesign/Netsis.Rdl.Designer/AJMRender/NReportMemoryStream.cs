using fyiReporting.RDL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace fyiReporting.RdlEngine
{
    public class NReportMemoryStream: IStreamGen, IDisposable
    {

        #region [_FIELDS_]

        MemoryStream _ms = null;
        StreamWriter _sw;

        #endregion

        #region [_CONSTRUCTORS_]

        public NReportMemoryStream()
        {
            this._ms = new MemoryStream();
            this._sw = new StreamWriter(this._ms);
        }

        #endregion

        public byte[] GetBytes()
        {
            if (_ms == null)
                return null;
            this._ms.Flush();
            this._sw.Flush();
            return this._ms.ToArray();
        }

        #region IStreamGen Members

        public System.IO.Stream GetStream()
        {
            return this._ms;
        }

        public System.IO.TextWriter GetTextWriter()
        {
            return this._sw;
        }

        public System.IO.Stream GetIOStream(out string relativeName, string extension)
        {
            relativeName = string.Empty;
            return this._ms;
        }

        public void CloseMainStream()
        {
           /* if (_sw != null)
            {
                _sw.Close();
                _sw = null;
            }
            if (_ms != null)
            {
                _ms.Close();
                _ms = null;
            }*/            
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_sw != null)
            {
                _sw.Flush();
                _sw.Close();
                _sw = null;
            }
            if (_ms != null)
            {
                _ms.Flush();
                _ms.Close();
                _ms = null;
            }
        }

        #endregion
    }
}
