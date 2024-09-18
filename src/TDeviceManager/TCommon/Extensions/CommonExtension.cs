﻿using System.Diagnostics;
using System.IO.Ports;
using System.Net.Sockets;
using System.Runtime.Versioning;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TCommon.Extensions
{
    /// <summary>扩展的辅助类方法</summary>
    public static class CommonExtension
    {
        /// <inheritdoc cref="SoftBasic.ByteToHexString(System.Byte[])" />
        public static string ToHexString(this byte[] inBytes) => SoftBasic.ByteToHexString(inBytes);

        /// <inheritdoc cref="SoftBasic.ByteToHexString(System.Byte[],System.Char)" />
        public static string ToHexString(this byte[] inBytes, char segment)
        {
            return SoftBasic.ByteToHexString(inBytes, segment);
        }

        /// <inheritdoc cref="SoftBasic.ByteToHexString(System.Byte[],System.Char,System.Int32,System.String)" />
        public static string ToHexString(
          this byte[] inBytes,
          char segment,
          int newLineCount,
          string format = "{0:X2}")
        {
            return SoftBasic.ByteToHexString(inBytes, segment, newLineCount, format);
        }

        /// <inheritdoc cref="SoftBasic.HexStringToBytes(System.String)" />
        public static byte[] ToHexBytes(this string value) => SoftBasic.HexStringToBytes(value);

        /// <inheritdoc cref="SoftBasic.BoolArrayToByte(System.Boolean[])" />
        public static byte[] ToByteArray(this bool[] array) => SoftBasic.BoolArrayToByte(array);

        /// <inheritdoc cref="SoftBasic.ByteToBoolArray(System.Byte[],System.Int32)" />
        public static bool[] ToBoolArray(this byte[] inBytes, int length)
        {
            return SoftBasic.ByteToBoolArray(inBytes, length);
        }

        /// <summary>
        /// 获取当前数组的倒序数组，这是一个新的实例，不改变原来的数组值<br />
        /// Get the reversed array of the current byte array, this is a new instance, does not change the original array value
        /// </summary>
        /// <param name="value">输入的原始数组</param>
        /// <returns>反转之后的数组信息</returns>
        public static T[] ReverseNew<T>(this T[] value)
        {
            T[]? array = value.CopyArray();
            Debug.Assert(array != null, nameof(array) + " != null");
            Array.Reverse(array);
            return array;
        }

        /// <inheritdoc cref="SoftBasic.ByteToBoolArray(System.Byte[])" />
        public static bool[] ToBoolArray(this byte[] inBytes) => SoftBasic.ByteToBoolArray(inBytes);

        /// <summary>
        /// 获取Byte数组的第 bytIndex 个位置的，boolIndex偏移的bool值<br />
        /// Get the bool value of the bytIndex position of the Byte array and the boolIndex offset
        /// </summary>
        /// <param name="bytes">字节数组信息</param>
        /// <param name="bytIndex">字节的偏移位置</param>
        /// <param name="boolIndex">指定字节的位偏移</param>
        /// <returns>bool值</returns>
        public static bool GetBoolValue(this byte[] bytes, int bytIndex, int boolIndex)
        {
            return SoftBasic.BoolOnByteIndex(bytes[bytIndex], boolIndex);
        }

        /// <summary>
        /// 获取Byte数组的第 boolIndex 偏移的bool值，这个偏移值可以为 10，就是第 1 个字节的 第3位 <br />
        /// Get the bool value of the boolIndex offset of the Byte array. The offset value can be 10, which is the third bit of the first byte
        /// </summary>
        /// <param name="bytes">字节数组信息</param>
        /// <param name="boolIndex">指定字节的位偏移</param>
        /// <returns>bool值</returns>
        public static bool GetBoolByIndex(this byte[] bytes, int boolIndex)
        {
            return SoftBasic.BoolOnByteIndex(bytes[boolIndex / 8], boolIndex % 8);
        }

        /// <summary>
        /// 获取Byte的第 boolIndex 偏移的bool值，比如3，就是第4位 <br />
        /// Get the bool value of Byte's boolIndex offset, such as 3, which is the 4th bit
        /// </summary>
        /// <param name="byt">字节信息</param>
        /// <param name="boolIndex">指定字节的位偏移</param>
        /// <returns>bool值</returns>
        public static bool GetBoolByIndex(this byte byt, int boolIndex)
        {
            return SoftBasic.BoolOnByteIndex(byt, boolIndex % 8);
        }

        /// <summary>
        /// 获取short类型数据的第 boolIndex (从0起始)偏移的bool值，比如3，就是第4位 <br />
        /// Get the bool value of the boolIndex (starting from 0) offset of the short type data, such as 3, which is the 4th bit
        /// </summary>
        /// <param name="value">short数据值</param>
        /// <param name="boolIndex">位偏移索引，从0开始</param>
        /// <returns>bool值</returns>
        public static bool GetBoolByIndex(this short value, int boolIndex)
        {
            return BitConverter.GetBytes(value).GetBoolByIndex(boolIndex);
        }

        /// <summary>
        /// 获取ushort类型数据的第 boolIndex (从0起始)偏移的bool值，比如3，就是第4位 <br />
        /// Get the bool value of the boolIndex (starting from 0) offset of the ushort type data, such as 3, which is the 4th bit
        /// </summary>
        /// <param name="value">ushort数据值</param>
        /// <param name="boolIndex">位偏移索引，从0开始</param>
        /// <returns>bool值</returns>
        public static bool GetBoolByIndex(this ushort value, int boolIndex)
        {
            return BitConverter.GetBytes(value).GetBoolByIndex(boolIndex);
        }

        /// <summary>
        /// 获取int类型数据的第 boolIndex (从0起始)偏移的bool值，比如3，就是第4位 <br />
        /// Get the bool value of the boolIndex (starting from 0) offset of the int type data, such as 3, which is the 4th bit
        /// </summary>
        /// <param name="value">int数据值</param>
        /// <param name="boolIndex">位偏移索引，从0开始</param>
        /// <returns>bool值</returns>
        public static bool GetBoolByIndex(this int value, int boolIndex)
        {
            return BitConverter.GetBytes(value).GetBoolByIndex(boolIndex);
        }

        /// <summary>
        /// 获取uint类型数据的第 boolIndex (从0起始)偏移的bool值，比如3，就是第4位 <br />
        /// Get the bool value of the boolIndex (starting from 0) offset of the uint type data, such as 3, which is the 4th bit
        /// </summary>
        /// <param name="value">uint数据值</param>
        /// <param name="boolIndex">位偏移索引，从0开始</param>
        /// <returns>bool值</returns>
        public static bool GetBoolByIndex(this uint value, int boolIndex)
        {
            return BitConverter.GetBytes(value).GetBoolByIndex(boolIndex);
        }

        /// <summary>
        /// 获取long类型数据的第 boolIndex (从0起始)偏移的bool值，比如3，就是第4位 <br />
        /// Get the bool value of the boolIndex (starting from 0) offset of the long type data, such as 3, which is the 4th bit
        /// </summary>
        /// <param name="value">long数据值</param>
        /// <param name="boolIndex">位偏移索引，从0开始</param>
        /// <returns>bool值</returns>
        public static bool GetBoolByIndex(this long value, int boolIndex)
        {
            return BitConverter.GetBytes(value).GetBoolByIndex(boolIndex);
        }

        /// <summary>
        /// 获取ulong类型数据的第 boolIndex (从0起始)偏移的bool值，比如3，就是第4位 <br />
        /// Get the bool value of the boolIndex (starting from 0) offset of the ulong type data, such as 3, which is the 4th bit
        /// </summary>
        /// <param name="value">ulong数据值</param>
        /// <param name="boolIndex">位偏移索引，从0开始</param>
        /// <returns>bool值</returns>
        public static bool GetBoolByIndex(this ulong value, int boolIndex)
        {
            return BitConverter.GetBytes(value).GetBoolByIndex(boolIndex);
        }

        /// <summary>从字节数组里提取字符串数据，如果碰到0x00字节，就直接结束</summary>
        /// <param name="buffer">原始字节信息</param>
        /// <param name="index">起始的偏移地址</param>
        /// <param name="length">字节长度信息</param>
        /// <param name="encoding">编码</param>
        /// <returns>字符串信息</returns>
        public static string GetStringOrEndChar(
          this byte[] buffer,
          int index,
          int length,
          Encoding encoding)
        {
            for (int index1 = index; index1 < index + length; ++index1)
            {
                if (buffer[index1] == 0)
                {
                    length = index1 - index;
                    break;
                }
            }
            return Encoding.UTF8.GetString(buffer, index, length);
        }

        /// <summary>
        /// 设置Byte的第 boolIndex 位的bool值，可以强制为 true 或是 false, 不影响其他的位<br />
        /// Set the bool value of the boolIndex bit of Byte, which can be forced to true or false, without affecting other bits
        /// </summary>
        /// <param name="byt">字节信息</param>
        /// <param name="boolIndex">指定字节的位偏移</param>
        /// <param name="value">bool的值</param>
        /// <returns>修改之后的byte值</returns>
        public static byte SetBoolByIndex(this byte byt, int boolIndex, bool value)
        {
            return SoftBasic.SetBoolOnByteIndex(byt, boolIndex, value);
        }

        /// <summary>
        /// 设置Byte[]的第 boolIndex 位的bool值，可以强制为 true 或是 false, 不影响其他的位，如果是第 10 位，则表示第 1 个字节的第 2 位（都是从 0 地址开始算的）<br />
        /// Set the bool value of the boolIndex bit of Byte[], which can be forced to true or false, without affecting other bits.
        /// If it is the 10th bit, it means the second bit of the first byte (both starting from the 0 address Calculated)
        /// </summary>
        /// <param name="buffer">字节数组信息</param>
        /// <param name="boolIndex">位偏移的索引</param>
        /// <param name="value">bool的值</param>
        public static void SetBoolByIndex(this byte[] buffer, int boolIndex, bool value)
        {
            buffer[boolIndex / 8] = buffer[boolIndex / 8].SetBoolByIndex(boolIndex % 8, value);
        }

        /// <summary>
        /// 修改short数据的某个位，并且返回修改后的值，不影响原来的值。位索引为 0~15，之外的值会引发异常<br />
        /// Modify a bit of short data and return the modified value without affecting the original value. Bit index is 0~15, values outside will raise an exception
        /// </summary>
        /// <param name="shortValue">等待修改的short值</param>
        /// <param name="boolIndex">位索引，位索引为 0~15，之外的值会引发异常</param>
        /// <param name="value">bool值</param>
        /// <returns>修改之后的short值</returns>
        public static short SetBoolByIndex(this short shortValue, int boolIndex, bool value)
        {
            byte[] bytes = BitConverter.GetBytes(shortValue);
            bytes.SetBoolByIndex(boolIndex, value);
            return BitConverter.ToInt16(bytes, 0);
        }

        /// <summary>
        /// 修改ushort数据的某个位，并且返回修改后的值，不影响原来的值。位索引为 0~15，之外的值会引发异常<br />
        /// Modify a bit of ushort data and return the modified value without affecting the original value. Bit index is 0~15, values outside will raise an exception
        /// </summary>
        /// <param name="ushortValue">等待修改的ushort值</param>
        /// <param name="boolIndex">位索引，位索引为 0~15，之外的值会引发异常</param>
        /// <param name="value">bool值</param>
        /// <returns>修改之后的ushort值</returns>
        public static ushort SetBoolByIndex(this ushort ushortValue, int boolIndex, bool value)
        {
            byte[] bytes = BitConverter.GetBytes(ushortValue);
            bytes.SetBoolByIndex(boolIndex, value);
            return BitConverter.ToUInt16(bytes, 0);
        }

        /// <summary>
        /// 修改int数据的某个位，并且返回修改后的值，不影响原来的值。位索引为 0~31，之外的值会引发异常<br />
        /// Modify a bit of int data and return the modified value without affecting the original value. Bit index is 0~31, values outside will raise an exception
        /// </summary>
        /// <param name="intValue">等待修改的int值</param>
        /// <param name="boolIndex">位索引，位索引为 0~31，之外的值会引发异常</param>
        /// <param name="value">bool值</param>
        /// <returns>修改之后的int值</returns>
        public static int SetBoolByIndex(this int intValue, int boolIndex, bool value)
        {
            byte[] bytes = BitConverter.GetBytes(intValue);
            bytes.SetBoolByIndex(boolIndex, value);
            return BitConverter.ToInt32(bytes, 0);
        }

        /// <summary>
        /// 修改uint数据的某个位，并且返回修改后的值，不影响原来的值。位索引为 0~31，之外的值会引发异常<br />
        /// Modify a bit of uint data and return the modified value without affecting the original value. Bit index is 0~31, values outside will raise an exception
        /// </summary>
        /// <param name="uintValue">等待修改的uint值</param>
        /// <param name="boolIndex">位索引，位索引为 0~31，之外的值会引发异常</param>
        /// <param name="value">bool值</param>
        /// <returns>修改之后的uint值</returns>
        public static uint SetBoolByIndex(this uint uintValue, int boolIndex, bool value)
        {
            byte[] bytes = BitConverter.GetBytes(uintValue);
            bytes.SetBoolByIndex(boolIndex, value);
            return BitConverter.ToUInt32(bytes, 0);
        }

        /// <summary>
        /// 修改long数据的某个位，并且返回修改后的值，不影响原来的值。位索引为 0~63，之外的值会引发异常<br />
        /// Modify a bit of long data and return the modified value without affecting the original value. Bit index is 0~63, values outside will raise an exception
        /// </summary>
        /// <param name="longValue">等待修改的long值</param>
        /// <param name="boolIndex">位索引，位索引为 0~63，之外的值会引发异常</param>
        /// <param name="value">bool值</param>
        /// <returns>修改之后的long值</returns>
        public static long SetBoolByIndex(this long longValue, int boolIndex, bool value)
        {
            byte[] bytes = BitConverter.GetBytes(longValue);
            bytes.SetBoolByIndex(boolIndex, value);
            return BitConverter.ToInt64(bytes, 0);
        }

        /// <summary>
        /// 修改ulong数据的某个位，并且返回修改后的值，不影响原来的值。位索引为 0~63，之外的值会引发异常<br />
        /// Modify a bit of ulong data and return the modified value without affecting the original value. Bit index is 0~63, values outside will raise an exception
        /// </summary>
        /// <param name="ulongValue">等待修改的ulong值</param>
        /// <param name="boolIndex">位索引，位索引为 0~63，之外的值会引发异常</param>
        /// <param name="value">bool值</param>
        /// <returns>修改之后的ulong值</returns>
        public static ulong SetBoolByIndex(this ulong ulongValue, int boolIndex, bool value)
        {
            byte[] bytes = BitConverter.GetBytes(ulongValue);
            bytes.SetBoolByIndex(boolIndex, value);
            return BitConverter.ToUInt64(bytes, 0);
        }

        /// <inheritdoc cref="SoftBasic.ArrayRemoveDouble{T}(T[],System.Int32,System.Int32)" />
        public static T[] RemoveDouble<T>(this T[] value, int leftLength, int rightLength)
        {
            return SoftBasic.ArrayRemoveDouble(value, leftLength, rightLength);
        }

        /// <inheritdoc cref="SoftBasic.ArrayRemoveBegin{T}(T[],System.Int32)" />
        public static T[] RemoveBegin<T>(this T[] value, int length)
        {
            return SoftBasic.ArrayRemoveBegin(value, length);
        }

        /// <inheritdoc cref="SoftBasic.ArrayRemoveLast{T}(T[],System.Int32)" />
        public static T[] RemoveLast<T>(this T[] value, int length)
        {
            return SoftBasic.ArrayRemoveLast(value, length);
        }

        /// <inheritdoc cref="SoftBasic.ArraySelectMiddle{T}(T[],System.Int32,System.Int32)" />
        public static T[] SelectMiddle<T>(this T[] value, int index, int length)
        {
            return SoftBasic.ArraySelectMiddle(value, index, length);
        }

        /// <inheritdoc cref="SoftBasic.ArraySelectBegin{T}(T[],System.Int32)" />
        public static T[] SelectBegin<T>(this T[] value, int length)
        {
            return SoftBasic.ArraySelectBegin(value, length);
        }

        /// <inheritdoc cref="SoftBasic.ArraySelectLast{T}(T[],System.Int32)" />
        public static T[] SelectLast<T>(this T[] value, int length)
        {
            return SoftBasic.ArraySelectLast(value, length);
        }

        /// <inheritdoc cref="SoftBasic.GetValueFromJsonObject{T}(Newtonsoft.Json.Linq.JObject,System.String,T)" />
        public static T GetValueOrDefault<T>(JObject jObject, string name, T defaultValue)
        {
            return SoftBasic.GetValueFromJsonObject(jObject, name, defaultValue);
        }

        /// <inheritdoc cref="SoftBasic.SpliceArray{T}(T[][])" />
        public static T[] SpliceArray<T>(this T[] value, params T[][] arrays)
        {
            List<T[]> objArrayList = new List<T[]>(arrays.Length + 1);
            objArrayList.Add(value);
            objArrayList.AddRange(arrays);
            return SoftBasic.SpliceArray(objArrayList.ToArray());
        }

        /// <summary>
        /// 移除指定字符串数据的最后 length 个字符。如果字符串本身的长度不足 length，则返回为空字符串。<br />
        /// Remove the last "length" characters of the specified string data. If the length of the string itself is less than length,
        /// an empty string is returned.
        /// </summary>
        /// <param name="value">等待操作的字符串数据</param>
        /// <param name="length">准备移除的长度信息</param>
        /// <returns>移除之后的数据信息</returns>
        public static string? RemoveLast(this string? value, int length)
        {
            if (value == null)
                return null;
            return value.Length < length ? string.Empty : value.Remove(value.Length - length);
        }

        /// <summary>将指定的数据添加到数组的每个元素上去，会改变每个元素的值</summary>
        /// <param name="array">原始数组</param>
        /// <param name="value">值</param>
        /// <returns>修改后的数组信息</returns>
        public static byte[]? EveryByteAdd(this byte[]? array, int value)
        {
            if (array == null)
                return null;
            for (int index = 0; index < array.Length; ++index)
                array[index] = (byte)(array[index] + (uint)value);
            return array;
        }

        /// <summary>拷贝当前的实例数组，是基于引用层的浅拷贝，如果类型为值类型，那就是深度拷贝，如果类型为引用类型，就是浅拷贝</summary>
        /// <typeparam name="T">类型对象</typeparam>
        /// <param name="value">数组对象</param>
        /// <returns>拷贝的结果内容</returns>
        public static T[]? CopyArray<T>(this T[]? value)
        {
            if (value == null)
                return null;
            var destinationArray = new T[value.Length];
            Array.Copy(value, destinationArray, value.Length);
            return destinationArray;
        }

        /// <inheritdoc cref="SoftBasic.ArrayFormat{T}(T[])" />
        public static string ToArrayString<T>(this T[] value) => SoftBasic.ArrayFormat(value);

        /// <inheritdoc cref="SoftBasic.ArrayFormat{T}(T,System.String)" />
        public static string ToArrayString<T>(this T[] value, string format)
        {
            return SoftBasic.ArrayFormat(value, format);
        }

        /// <summary>
        /// 将字符串数组转换为实际的数据数组。例如字符串格式[1,2,3,4,5]，可以转成实际的数组对象<br />
        /// Converts a string array into an actual data array. For example, the string format [1,2,3,4,5] can be converted into an actual array object
        /// </summary>
        /// <typeparam name="T">类型对象</typeparam>
        /// <param name="value">字符串数据</param>
        /// <param name="selector">转换方法</param>
        /// <returns>实际的数组</returns>
        public static T[] ToStringArray<T>(this string value, Func<string, T> selector)
        {
            if (value.IndexOf('[') >= 0)
                value = value.Replace("[", "");
            if (value.IndexOf(']') >= 0)
                value = value.Replace("]", "");
            return value.Split(new char[2]
            {
                ',',
                ';'
            }, StringSplitOptions.RemoveEmptyEntries).Select(selector).ToArray();
        }

        /// <summary>
        /// 将字符串数组转换为实际的数据数组。支持byte,sbyte,bool,short,ushort,int,uint,long,ulong,float,double，使用默认的十进制，例如字符串格式[1,2,3,4,5]，可以转成实际的数组对象<br />
        /// Converts a string array into an actual data array. Support byte, sbyte, bool, short, ushort, int, uint, long, ulong, float, double, use the default decimal,
        /// such as the string format [1,2,3,4,5], which can be converted into an actual array Object
        /// </summary>
        /// <typeparam name="T">类型对象</typeparam>
        /// <param name="value">字符串数据</param>
        /// <returns>实际的数组</returns>
        public static T[]? ToStringArray<T>(this string value)
        {
            Type type = typeof(T);
            if (type == typeof(byte))
                return value.ToStringArray(byte.Parse) as T[];
            if (type == typeof(sbyte))
                return value.ToStringArray(sbyte.Parse) as T[];
            if (type == typeof(bool))
                return value.ToStringArray(bool.Parse) as T[];
            if (type == typeof(short))
                return value.ToStringArray(short.Parse) as T[];
            if (type == typeof(ushort))
                return value.ToStringArray(ushort.Parse) as T[];
            if (type == typeof(int))
                return value.ToStringArray(int.Parse) as T[];
            if (type == typeof(uint))
                return value.ToStringArray(uint.Parse) as T[];
            if (type == typeof(long))
                return value.ToStringArray(long.Parse) as T[];
            if (type == typeof(ulong))
                return value.ToStringArray(ulong.Parse) as T[];
            if (type == typeof(float))
                return value.ToStringArray(float.Parse) as T[];
            if (type == typeof(double))
                return value.ToStringArray(double.Parse) as T[];
            if (type == typeof(DateTime))
                return value.ToStringArray(DateTime.Parse) as T[];
            if (type == typeof(Guid))
                return value.ToStringArray(Guid.Parse) as T[];
            if (type == typeof(string))
                return value.ToStringArray(m => m) as T[];
            throw new Exception("use ToArray<T>(Func<string,T>) method instead");
        }

        /// <summary>
        /// 启动接收数据，需要传入回调方法，传递对象<br />
        /// To start receiving data, you need to pass in a callback method and pass an object
        /// </summary>
        /// <param name="socket">socket对象</param>
        /// <param name="callback">回调方法</param>
        /// <param name="obj">数据对象</param>
        /// <returns>是否启动成功</returns>
        public static OperateResult BeginReceiveResult(
          this Socket socket,
          AsyncCallback callback,
          object obj)
        {
            try
            {
                socket.BeginReceive(new byte[0], 0, 0, SocketFlags.None, callback, obj);
                return OperateResult.CreateSuccessResult();
            }
            catch (Exception ex)
            {
                socket.Close();
                return new OperateResult(ex.Message);
            }
        }

        /// <summary>
        /// 启动接收数据，需要传入回调方法，传递对象默认为socket本身<br />
        /// To start receiving data, you need to pass in a callback method. The default object is the socket itself.
        /// </summary>
        /// <param name="socket">socket对象</param>
        /// <param name="callback">回调方法</param>
        /// <returns>是否启动成功</returns>
        public static OperateResult BeginReceiveResult(this Socket socket, AsyncCallback callback)
        {
            return socket.BeginReceiveResult(callback, socket);
        }

        /// <summary>
        /// 结束挂起的异步读取，返回读取的字节数，如果成功的情况。<br />
        /// Ends the pending asynchronous read and returns the number of bytes read, if successful.
        /// </summary>
        /// <param name="socket">socket对象</param>
        /// <param name="ar">回调方法</param>
        /// <returns>是否启动成功</returns>
        public static OperateResult<int> EndReceiveResult(this Socket socket, IAsyncResult ar)
        {
            try
            {
                return OperateResult.CreateSuccessResult(socket.EndReceive(ar));
            }
            catch (Exception ex)
            {
                socket.Close();
                return new OperateResult<int>(ex.Message);
            }
        }

        /// <summary>
        /// 根据英文小数点进行切割字符串，去除空白的字符<br />
        /// Cut the string according to the English decimal point and remove the blank characters
        /// </summary>
        /// <param name="str">字符串本身</param>
        /// <returns>切割好的字符串数组，例如输入 "100.5"，返回 "100", "5"</returns>
        public static string[] SplitDot(this string str)
        {
            return str.Split(new char[1] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 获取当前对象的JSON格式表示的字符串。<br />
        /// Gets the string represented by the JSON format of the current object.
        /// </summary>
        /// <returns>字符串对象</returns>
        public static string ToJsonString(this object obj, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(obj, formatting);
        }

        /// <inheritdoc cref="M:System.IO.MemoryStream.Write(System.Byte[],System.Int32,System.Int32)" />
        public static void Write(this MemoryStream ms, byte[]? buffer)
        {
            if (buffer == null)
                return;
            ms.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// 将<see cref="T:System.UInt16" />数据写入到字节流，字节顺序为相反<br />
        /// Write <see cref="T:System.UInt16" /> data to the byte stream, the byte order is reversed
        /// </summary>
        /// <param name="ms">字节流</param>
        /// <param name="value">等待写入的值</param>
        public static void WriteReverse(this MemoryStream ms, ushort value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            (bytes[0], bytes[1]) = (bytes[1], bytes[0]);
            ms.Write((ReadOnlySpan<byte>)bytes);
        }

        /// <summary>
        /// 设置套接字的活动时间和活动间歇时间，此值会设置到socket低级别的控制中，传入值如果为负数，则表示不使用 KeepAlive 功能。<br />
        /// Set the active time and active intermittent time of the socket. This value will be set to the low-level control of the socket.
        /// If the incoming value is a negative number, it means that the KeepAlive function is not used.
        /// </summary>
        /// <param name="socket">套接字对象</param>
        /// <param name="keepAliveTime">保持活动时间</param>
        /// <param name="keepAliveInterval">保持活动的间歇时间</param>
        /// <returns>返回获取的参数的字节</returns>
        [SupportedOSPlatform("Windows")]
        public static int SetKeepAlive(this Socket socket, int keepAliveTime, int keepAliveInterval)
        {
            byte[] optionInValue = new byte[12];
            BitConverter.GetBytes(keepAliveTime < 0 ? 0 : 1).CopyTo(optionInValue, 0);
            BitConverter.GetBytes(keepAliveTime).CopyTo(optionInValue, 4);
            BitConverter.GetBytes(keepAliveInterval).CopyTo(optionInValue, 8);
            try
            {
                return socket.IOControl(IOControlCode.KeepAliveValues, optionInValue, null);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 使用格式化的串口参数信息来初始化串口的参数，举例：9600-8-N-1，分别表示波特率，数据位，奇偶校验，停止位，当然也可以携带串口名称，例如：COM3-9600-8-N-1，linux环境也是支持的。<br />
        /// Use the formatted serial port parameter information to initialize the serial port parameters, for example: 9600-8-N-1, which means baud rate, data bit, parity,
        /// stop bit, of course, can also carry the serial port name, for example: COM3- 9600-8-N-1, linux environment is also supported.
        /// </summary>
        /// <remarks>
        /// 其中奇偶校验的字母可选，N:无校验，O：奇校验，E:偶校验，停止位可选 0, 1, 2, 1.5 四种选项<br />
        /// Among them, the letters of the parity check are optional, N: no check, O: odd check, E: even check, stop bit optional 0, 1, 2, 1.5 four options
        /// </remarks>
        /// <param name="serialPort">串口对象信息</param>
        /// <param name="format">格式化的参数内容，例如：9600-8-N-1</param>
        public static void IniSerialByFormatString(this SerialPort serialPort, string format)
        {
            string[] strArray1 = format.Split(['-', ';'], StringSplitOptions.RemoveEmptyEntries);
            if (strArray1.Length == 0)
                return;
            int num1 = 0;
            if (!Regex.IsMatch(strArray1[0], "^[0-9]+$"))
            {
                serialPort.PortName = strArray1[0];
                num1 = 1;
            }
            if (num1 < strArray1.Length)
                serialPort.BaudRate = Convert.ToInt32(strArray1[num1++]);
            if (num1 < strArray1.Length)
                serialPort.DataBits = Convert.ToInt32(strArray1[num1++]);
            if (num1 < strArray1.Length)
            {
                string upper = strArray1[num1++].ToUpper();
                SerialPort serialPort1 = serialPort;
                int num2;
                switch (upper)
                {
                    case "N":
                        num2 = 0;
                        break;
                    case "O":
                        num2 = 1;
                        break;
                    case "E":
                        num2 = 2;
                        break;
                    default:
                        num2 = 4;
                        break;
                }
                serialPort1.Parity = (Parity)num2;
            }
            if (num1 >= strArray1.Length)
                return;
            string[] strArray2 = strArray1;
            int index = num1;
            string str = strArray2[index];
            SerialPort serialPort2 = serialPort;
            int num4;
            switch (str)
            {
                case "1":
                    num4 = 1;
                    break;
                case "2":
                    num4 = 2;
                    break;
                case "0":
                    num4 = 0;
                    break;
                default:
                    num4 = 3;
                    break;
            }
            serialPort2.StopBits = (StopBits)num4;
        }

        /// <summary>
        /// 将一个串口对象的基本配置参数进行格式化字符串，例如 COM3-9600-8-N-1<br />
        /// Format the basic configuration parameters of a serial port object as a string, for example, COM3-9600-8-N-1
        /// </summary>
        /// <remarks>
        /// 其中奇偶校验的字母可选，N:无校验，O：奇校验，E:偶校验，停止位可选 0, 1, 2, 1.5 四种选项<br />
        /// Among them, the letters of the parity check are optional, N: no check, O: odd check, E: even check, stop bit optional 0, 1, 2, 1.5 four options
        /// </remarks>
        /// <param name="serialPort">串口对象信息</param>
        /// <returns>串口对的格式化字符串信息</returns>
        public static string ToFormatString(this SerialPort serialPort)
        {
            //return ToFormatString(serialPort.PortName, serialPort.BaudRate, serialPort.DataBits, serialPort.Parity, serialPort.StopBits);
            var portName = serialPort.PortName;
            var baudRate = serialPort.BaudRate;
            var dataBits = serialPort.DataBits;
            var parity = serialPort.Parity;
            var stopBits = serialPort.StopBits;

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(portName);
            stringBuilder.Append("-");
            stringBuilder.Append(baudRate.ToString());
            stringBuilder.Append("-");
            stringBuilder.Append(dataBits.ToString());
            stringBuilder.Append("-");
            switch (parity)
            {
                case Parity.None:
                    stringBuilder.Append("N");
                    break;
                case Parity.Odd:
                    stringBuilder.Append("O");
                    break;
                case Parity.Even:
                    stringBuilder.Append("E");
                    break;
                case Parity.Space:
                    stringBuilder.Append("S");
                    break;
                default:
                    stringBuilder.Append("M");
                    break;
            }
            stringBuilder.Append("-");
            switch (stopBits)
            {
                case StopBits.None:
                    stringBuilder.Append("0");
                    break;
                case StopBits.One:
                    stringBuilder.Append("1");
                    break;
                case StopBits.Two:
                    stringBuilder.Append("2");
                    break;
                default:
                    stringBuilder.Append("1.5");
                    break;
            }
            return stringBuilder.ToString();
        }


        /// <summary>
        /// 根据指定的字节长度信息，获取到随机的字节信息<br />
        /// Obtain random byte information according to the specified byte length information
        /// </summary>
        /// <param name="random">随机数对象</param>
        /// <param name="length">字节的长度信息</param>
        /// <returns>原始字节数组</returns>
        public static byte[] GetBytes(this Random random, int length)
        {
            byte[] buffer = new byte[length];
            random.NextBytes(buffer);
            return buffer;
        }

        ///<inheritdoc cref="SoftBasic.BytesReverseByWord(System.Byte[])"/>
        public static byte[] ReverseByWord(this byte[] inBytes)
        {
            return SoftBasic.BytesReverseByWord(inBytes);
        }
    }
}
