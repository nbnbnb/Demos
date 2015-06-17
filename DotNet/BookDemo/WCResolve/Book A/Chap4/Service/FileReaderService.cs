using Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Service
{
    public class FileReaderService : IFileReader, IDisposable
    {
        private FileStream _stream;
        private byte[] _buffer;

        public IAsyncResult BeginReader(string fileName, AsyncCallback callback, object stateObject)
        {
            _stream = new FileStream(fileName, FileMode.Open);
            _buffer = new byte[_stream.Length];
            return _stream.BeginRead(_buffer, 0, _buffer.Length, callback, stateObject);
        }

        public string EndReader(IAsyncResult asynResult)
        {
            _stream.EndRead(asynResult);
            _stream.Close();
            return Encoding.UTF8.GetString(_buffer);
        }

        public void Dispose()
        {
            if (_stream != null)
            {
                _stream.Close();
            }
        }
    }
}
