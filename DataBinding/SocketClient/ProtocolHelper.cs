// ***********************************************************************
// Author     ：HuYe
// Function   ：
// CreateTime ：2019/4/11 11:03:59
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SocketService
{
    /// <summary>
    /// 发送消息的数据类型
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// 信息类型
        /// </summary>
        infomation = 0,

        /// <summary>
        /// 命令类型
        /// </summary>
        command = 1,

        clientName = 2,
    }

    class ProtocolHelper
    {
        //用于存储剩余未解析的字节数
        private List<byte> LBuff = new List<byte>(2);
        //默认是Unicode的编码格式
        private UnicodeEncoding unicode = new UnicodeEncoding();

        //包头1
        const Int16 head1 = 0x55;
        //包头2
        const Int16 head2 = 0xAA;
        //字节数常量 两个包头4个字节，一个消息id4个字节，封装消息长度 int32 4个字节
        const Int32 ConstLength = 8;

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool flag)
        {
            if (flag)
            {
                if (this.unicode is IDisposable disposable) { disposable.Dispose(); }
            }
        }

        /// <summary>
        /// 封包
        /// </summary>
        /// <param name="data">待处理的数据</param>
        /// <returns></returns>
        public byte[] PackData(TSocketMessage msg, MessageType msgType)
        {
            // 封包后返回的数据
            byte[] retBuffer = null;
            // 待封包的数据
            byte[] msgBuffer = msg.MsgBuffer;
            // 数据类型
            byte messageType = (byte)msgType;
            using (MMO_MemoryStream ms = new MMO_MemoryStream())
            {
                //封装包头
                ms.WriteShort(head1);
                ms.WriteShort(head2);
                //包协议
                if (msgBuffer != null)
                {
                    // 写入数据类型
                    ms.WriteByte(messageType);
                    // 写入包体长度信息
                    ms.WriteInt((int)(msgBuffer.Length + 2));
                    // 获取经异或加密后的数据
                    msgBuffer = SecurityUtil.Xor(msgBuffer);
                    // 获取 Crc 冗余校验码
                    ushort crc = Crc16.CalculateCrc16(msgBuffer);
                    // 写入 Crc 冗余校验码
                    ms.WriteUShort(crc);
                    // 写入 Xor 加密后的数据
                    ms.Write(msgBuffer, 0, msgBuffer.Length);
                }
                else
                {
                    ms.WriteInt(0);
                }
                // 获取处理后的完整数据包
                retBuffer = ms.ToArray();
            }
            return retBuffer;
        }

        /// <summary>
        /// 拆包
        /// </summary>
        /// <param name="buff">缓存区数据</param>
        /// <param name="len">数据长度</param>
        /// <returns></returns>
        public List<TSocketMessage> UnpackData(byte[] buff, int len)
        {
            // 拷贝本次有效的字节
            byte[] _b = new byte[len];
            Array.Copy(buff, 0, _b, 0, _b.Length);
            buff = _b;

            if (this.LBuff.Count > 0)
            {
                // 拷贝之前遗留的字节
                this.LBuff.AddRange(_b);
                buff = this.LBuff.ToArray();
                this.LBuff.Clear();
                this.LBuff = new List<byte>(2);
            }

            List<TSocketMessage> list = new List<TSocketMessage>();

            // 存放 data 的 Crc 校验码
            ushort crc = 0;

            try
            {
                // 获取实际数据包体
                using (MMO_MemoryStream ms = new MMO_MemoryStream(buff))
                {
                    byte[] _buff;
                Label_00983:

                    #region 包头读取
                    // 循环读取包头
                    // 判断本次解析的字节是否满足常量字节数
                    if (ms.Length - ms.Position < ConstLength)
                    {
                        _buff = ms.ReadBytes((int)(ms.Length - ms.Position));
                        this.LBuff.AddRange(_buff);
                        return list;
                    }
                    short head11 = ms.ReadShort();
                    short head22 = ms.ReadShort();

                    // 如果未找到包头，则将字节流从上一次读取的位置向后移动一字节
                    if (!(head1 == head11 && head2 == head22))
                    {
                        long newPosition = ms.Seek(-3, SeekOrigin.Current);
                        goto Label_00983;
                    }
                    #endregion

                    // 数据类型
                    byte msgType = (byte)ms.ReadByte();
                    // 数据体长度
                    int offset = ms.ReadInt();

                    #region 包解析
                    // 剩余字节数大于本次需要读取的字节数
                    if (offset <= (ms.Length - ms.Position))
                    {
                        crc = ms.ReadUShort();
                        _buff = ms.ReadBytes(offset - 2);
                        int newCrc = Crc16.CalculateCrc16(_buff);
                        if (newCrc == crc)
                        {
                            _buff = SecurityUtil.Xor(_buff);
                            list.Add(new TSocketMessage(_buff, msgType));
                        }
                    }
                    // 剩余字节数刚好小于本次读取的字节数,先存起来,等待接受剩余字节数一起解析
                    else
                    {
                        _buff = ms.ReadBytes((int)(ms.Length - ms.Position));
                        this.LBuff.AddRange(_buff);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw new Exception("数据读取错误:" + ex.Message);
            }
            return list;
        }

        public sealed class SecurityUtil
        {
            private static readonly byte[] xorScale = new byte[]
            {
                45, 66, 38, 55, 23, 254, 9, 165, 90, 19, 41, 45, 201, 58, 55, 37, 254, 185, 165, 169, 19, 171
            };

            public static byte[] Xor(byte[] data)
            {
                int iScaleLen = xorScale.Length;
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = (byte)(data[i] ^ xorScale[i % iScaleLen]);
                }
                return data;
            }
        }

        public class Crc16
        {
            // Table of CRC values for high-order byte
            private static readonly byte[] _auchCRCHi = new byte[] { 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40 };
            // Table of CRC values for low-order byte
            private static readonly byte[] _auchCRCLo = new byte[] { 0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 0x07, 0xC7, 0x05, 0xC5, 0xC4, 0x04, 0xCC, 0x0C, 0x0D, 0xCD, 0x0F, 0xCF, 0xCE, 0x0E, 0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09, 0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9, 0x1B, 0xDB, 0xDA, 0x1A, 0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 0x1D, 0x1C, 0xDC, 0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3, 0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32, 0x36, 0xF6, 0xF7, 0x37, 0xF5, 0x35, 0x34, 0xF4, 0x3C, 0xFC, 0xFD, 0x3D, 0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A, 0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 0x28, 0xE8, 0xE9, 0x29, 0xEB, 0x2B, 0x2A, 0xEA, 0xEE, 0x2E, 0x2F, 0xEF, 0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26, 0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1, 0x63, 0xA3, 0xA2, 0x62, 0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4, 0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F, 0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 0x69, 0xA9, 0xA8, 0x68, 0x78, 0xB8, 0xB9, 0x79, 0xBB, 0x7B, 0x7A, 0xBA, 0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5, 0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0, 0x50, 0x90, 0x91, 0x51, 0x93, 0x53, 0x52, 0x92, 0x96, 0x56, 0x57, 0x97, 0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C, 0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E, 0x5A, 0x9A, 0x9B, 0x5B, 0x99, 0x59, 0x58, 0x98, 0x88, 0x48, 0x49, 0x89, 0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C, 0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83, 0x41, 0x81, 0x80, 0x40 };
            /// <summary>
            /// 获得CRC16效验码
            /// </summary>
            /// <param name="buffer"></param>
            /// <returns></returns>
            public static ushort CalculateCrc16(byte[] buffer)
            {
                byte crcHi = 0xff;  // high crc byte initialized
                byte crcLo = 0xff;  // low crc byte initialized
                for (int i = 0; i < buffer.Length; i++)
                {
                    int crcIndex = crcHi ^ buffer[i];
                    // calculate the crc lookup index
                    crcHi = (byte)(crcLo ^ _auchCRCHi[crcIndex]);
                    crcLo = _auchCRCLo[crcIndex];
                }
                return (ushort)(crcHi << 8 | crcLo);
            }
        }

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
            public string ReadUnicodeString()
            {
                ushort len = this.ReadUShort();
                byte[] arr = new byte[len];
                base.Read(arr, 0, len);
                return Encoding.Unicode.GetString(arr);
            }
            /// <summary>
            /// 把一个字符串数字写入流
            /// </summary>
            /// <param name="str"></param>
            public void WriteUnicodeString(string str)
            {
                if (string.IsNullOrEmpty(str)) return;
                byte[] arr = Encoding.Unicode.GetBytes(str);
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
}
