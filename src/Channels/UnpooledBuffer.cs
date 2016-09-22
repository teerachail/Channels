﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Channels
{
    public class UnpooledBuffer : IBuffer
    {
        private ArraySegment<byte> _buffer;

        public UnpooledBuffer(ArraySegment<byte> buffer)
        {
            _buffer = buffer;
        }

        public Span<byte> Data => new Span<byte>(_buffer.Array, _buffer.Offset, _buffer.Count);

        public void Dispose()
        {
            // GC works
        }

        public IBuffer Preserve(int offset, int length)
        {
            var buffer = new byte[length];
            Buffer.BlockCopy(_buffer.Array, _buffer.Offset + offset, buffer, 0, length);
            return new UnpooledBuffer(new ArraySegment<byte>(buffer));
        }
    }
}