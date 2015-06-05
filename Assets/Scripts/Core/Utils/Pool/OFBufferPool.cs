#region

using System.Collections.Generic;

#endregion

namespace Assets.Scripts.Core.Utils.Pool {
    public class OFBufferPool {
        #region Fields

        private static readonly OFBufferPool _instance = new OFBufferPool();
        private readonly Dictionary<int, Queue<byte[]>> _pool = new Dictionary<int, Queue<byte[]>>();

        #endregion

        #region Properties

        public static OFBufferPool Instance {
            get { return _instance; }
        }

        #endregion

        #region Methods

        public byte[] Pop(int length) {
            Queue<byte[]> queue;
            if (!_pool.TryGetValue(length, out queue)) {
                queue = new Queue<byte[]>();
            }
            byte[] bytes = queue.Count > 0 ? queue.Dequeue() : new byte[length];
            return bytes;
        }

        public void Push(byte[] bytes) {
            Queue<byte[]> queue;
            if (!_pool.TryGetValue(bytes.Length, out queue)) {
                queue = new Queue<byte[]>();
            }
            queue.Enqueue(bytes);
        }

        #endregion
    }
}