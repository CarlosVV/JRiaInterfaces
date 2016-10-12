using System;
using System.IO;

namespace CES.CoreApi.CallLog.Shared.SOAP.WSExtension
{
    /// <summary>
    /// Special switchable stream
    /// </summary>
    public class TraceExtensionStream : Stream
    {
        //@@2012-06-18 lb SCR# 665511 Created

        #region Fields

        private Stream _innerStream;
        private readonly Stream _originalStream;

        #endregion

        #region New public members

        /// <summary>
        /// Creates a new memory stream and makes it active 
        /// </summary>
        public void SwitchToNewStream()
        {
            _innerStream = new MemoryStream();
        }

        /// <summary>
        /// Copies data from the old stream to the new in-memory stream 
        /// </summary>
        public void CopyOldToNew()
        {
            //_innerStream = new MemoryStream((int)_originalStream.Length);
            copy(_originalStream, _innerStream);
            _innerStream.Position = 0;
        }

        /// <summary>
        /// Copies data from the new stream to the old stream
        /// </summary>
        public void CopyNewToOld()
        {
            copy(_innerStream, _originalStream);
        }

        /// <summary>
        /// Returns <c>true</c> if the active inner stream is a new stream, i.e. <see cref="SwitchToNewStream"/> has been called
        /// </summary>
        public bool IsNewStream
        {
            get
            {
                return (_innerStream != _originalStream);
            }
        }

        /// <summary>
        /// A link to the active inner stream
        /// </summary>
        public Stream InnerStream
        {
            get { return _innerStream; }
        }

        #endregion

        #region Private members

        private static void copy(Stream from, Stream to)
        {
            const int size = 4096;

            byte[] bytes = new byte[4096];

            int numBytes;

            while ((numBytes = from.Read(bytes, 0, size)) > 0)
                to.Write(bytes, 0, numBytes);
        }

        #endregion

        #region csontructors

        /// <summary>
        /// Constructs an instance of the stream wrapping the original stream into it
        /// </summary>
        internal TraceExtensionStream(Stream _originalStream)
        {
            _innerStream = this._originalStream = _originalStream;
        }

        #endregion

        #region Overridden members

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                    _innerStream.Close();
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return _innerStream.BeginRead(buffer, offset, count, callback, state);
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            return _innerStream.EndRead(asyncResult);
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return _innerStream.BeginWrite(buffer, offset, count, callback, state);
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            _innerStream.EndWrite(asyncResult);
        }

        public override int ReadByte()
        {
            return _innerStream.ReadByte();
        }

        public override void WriteByte(byte value)
        {
            _innerStream.WriteByte(value);
        }

        public override int WriteTimeout
        {
            get
            {
                return _innerStream.WriteTimeout;
            }
            set
            {
                _innerStream.WriteTimeout = value;
            }
        }

        public override bool CanRead
        {
            get { return _innerStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return _innerStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return _innerStream.CanWrite; }
        }

        public override void Flush()
        {
            _innerStream.Flush();
        }

        public override long Length
        {
            get { return _innerStream.Length; }
        }

        public override long Position
        {
            get
            {
                return _innerStream.Position;
            }
            set
            {
                _innerStream.Position = value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _innerStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _innerStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _innerStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _innerStream.Write(buffer, offset, count);
        }
        
        #endregion
    }
}
