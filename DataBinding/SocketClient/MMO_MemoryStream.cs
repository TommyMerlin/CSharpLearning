// ***********************************************************************
// Author     ：HuYe
// Function   ：数据流处理读取与写入类
// CreateTime ：2019/4/15 11:31:56
// ***********************************************************************

using System;
using System.IO;
using System.Text;

namespace SocketService
{
    /// <summary>
    /// 数据流处理读取与写入类
    /// </summary>
    public class MMO_MemoryStream : MemoryStream
    {
        public MMO_MemoryStream()
        {
        }
        public MMO_MemoryStream(byte[] buffer) : base(buffer)
        {
        }

        #region short,ushort数据读取与写入
        /// <summary>
        /// 从流中读取一个short数据
        /// </summary>
        /// <returns></returns>
        public short ReadShort()
        {
            byte[] arr = new byte[2];
            base.Read(arr, 0, 2);
            return BitConverter.ToInt16(arr, 0);
        }

        /// <summary>
        /// 向流中写入一个short数组
        /// </summary>
        /// <param name="value"></param>
        public void WriteShort(short value)
        {
            byte[] arr = BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }

        /// <summary>
        /// 从流中读取一个UShort数据
        /// </summary>
        /// <returns></returns>
        public ushort ReadUShort()
        {
            byte[] arr = new byte[2];
            base.Read(arr, 0, 2);
            return BitConverter.ToUInt16(arr, 0);
        }
        /// <summary>
        /// 向流中写入一个uShort数组
        /// </summary>
        /// <param name="value"></param>
        public void WriteUShort(ushort value)
        {
            byte[] arr = BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }
        #endregion

        #region int,uint数据读取与写入
        /// <summary>
        /// 从流中读取一个int数据
        /// </summary>
        /// <returns></returns>
        public int ReadInt()
        {
            byte[] arr = new byte[4];
            base.Read(arr, 0, 4);
            return BitConverter.ToInt32(arr, 0);
        }

        /// <summary>
        /// 向流中写入一个int数组
        /// </summary>
        /// <param name="value"></param>
        public void WriteInt(int value)
        {
            byte[] arr = BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }

        /// <summary>
        /// 从流中读取一个Uint数据
        /// </summary>
        /// <returns></returns>
        public uint ReadUInt()
        {
            byte[] arr = new byte[4];
            base.Read(arr, 0, 4);
            return BitConverter.ToUInt32(arr, 0);
        }

        /// <summary>
        /// 向流中写入一个uint数组
        /// </summary>
        /// <param name="value"></param>
        public void WriteUInit(uint value)
        {
            byte[] arr = BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }
        #endregion

        #region long,ulong数据读取与写入
        /// <summary>
        /// 从流中读取一个long数据
        /// </summary>
        /// <returns></returns>
        public long ReadLong()
        {
            byte[] arr = new byte[8];
            base.Read(arr, 0, 8);
            return BitConverter.ToInt64(arr, 0);
        }

        /// <summary>
        /// 向流中写入一个long数组
        /// </summary>
        /// <param name="value"></param>
        public void WriteLong(long value)
        {
            byte[] arr = BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }

        /// <summary>
        /// 从流中读取一个ULong数据
        /// </summary>
        /// <returns></returns>
        public ulong ReadULong()
        {
            byte[] arr = new byte[8];
            base.Read(arr, 0, 8);
            return BitConverter.ToUInt64(arr, 0);
        }

        /// <summary>
        /// 向流中写入一个ULong数组
        /// </summary>
        /// <param name="value"></param>
        public void WriteULong(ulong value)
        {
            byte[] arr = BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }
        #endregion

        #region float,double数据读取与写入
        /// <summary>
        /// 从流中读取一个float数据
        /// </summary>
        /// <returns></returns>
        public float ReadFloat()
        {
            byte[] arr = new byte[4];
            base.Read(arr, 0, 4);
            return BitConverter.ToSingle(arr, 0);
        }

        /// <summary>
        /// 向流中写入一个float数组
        /// </summary>
        /// <param name="value"></param>
        public void WriteFloat(float value)
        {
            byte[] arr = BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }

        /// <summary>
        /// 从流中读取一个Double数据
        /// </summary>
        /// <returns></returns>
        public double ReadDouble()
        {
            byte[] arr = new byte[8];
            base.Read(arr, 0, 8);
            return BitConverter.ToDouble(arr, 0);
        }

        /// <summary>
        /// 向流中写入一个double数组
        /// </summary>
        /// <param name="value"></param>
        public void WriteDouble(double value)
        {
            byte[] arr = BitConverter.GetBytes(value);
            base.Write(arr, 0, arr.Length);
        }
        #endregion

        #region Bool数据读取与写入
        /// <summary>
        /// 从流中读取一个Bool数据
        /// </summary>
        /// <returns></returns>
        public bool ReadBool()
        {
            return base.ReadByte() == 1;
        }
        /// <summary>
        /// 向流中写入一个bool数组
        /// </summary>
        /// <param name="value"></param>
        public void WriteBool(bool value)
        {
            base.WriteByte((byte)(value == true ? 1 : 0));
        }
        #endregion

        #region UTF8String数据读取与写入
        /// <summary>
        /// 从流中读取一个string数组
        /// </summary>
        /// <returns></returns>
        public string ReadUTF8String()
        {
            ushort len = this.ReadUShort();
            byte[] arr = new byte[len];
            base.Read(arr, 0, len);
            return Encoding.UTF8.GetString(arr);
        }
        /// <summary>
        /// 把一个字符串数字写入流
        /// </summary>
        /// <param name="str"></param>
        public void WriteUTF8String(string str)
        {
            if (string.IsNullOrEmpty(str)) return;
            byte[] arr = Encoding.UTF8.GetBytes(str);
            if (arr.Length > 65535)
            {
                throw new InvalidCastException("字符串超出范围");
            }
            this.WriteUShort((ushort)arr.Length);
            base.Write(arr, 0, arr.Length);
        }
        #endregion

        #region Bytes数据读取
        public byte[] ReadBytes(int length)
        {
            byte[] arr = new byte[length];
            base.Read(arr, 0, length);
            return arr;
        }
        #endregion
    }
}
