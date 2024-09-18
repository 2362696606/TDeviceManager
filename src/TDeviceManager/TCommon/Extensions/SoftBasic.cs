#nullable disable
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace TCommon.Extensions
{
    /// <summary>
    /// 一个软件基础类，提供常用的一些静态方法，比如字符串转换，字节转换的方法<br />
    /// A software-based class that provides some common static methods，Such as string conversion, byte conversion method
    /// </summary>
    public static class SoftBasic
    {
        /// <summary>
        /// 获取文件的md5码<br />
        /// Get the MD5 code of the file
        /// </summary>
        /// <param name="filePath">文件的路径，既可以是完整的路径，也可以是相对的路径 -&gt; The path to the file</param>
        /// <returns>Md5字符串</returns>
        public static string CalculateFileMd5(string filePath)
        {
            string fileMd5;
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                fileMd5 = SoftBasic.CalculateStreamMd5(fileStream);
            return fileMd5;
        }

        /// <summary>
        /// 获取数据流的md5码<br />
        /// Get the MD5 code for the data stream
        /// </summary>
        /// <param name="stream">数据流，可以是内存流，也可以是文件流</param>
        /// <returns>Md5字符串</returns>
        public static string CalculateStreamMd5(Stream stream)
        {
            byte[] numArray;
            using (MD5 md5 = MD5.Create())
                numArray = md5.ComputeHash(stream);

            return BitConverter.ToString(numArray).Replace("-", "");
        }

        /// <summary>
        /// 获取文本字符串信息的Md5码，编码为UTF8<br />
        /// Get the Md5 code of the text string information, using the utf-8 encoding
        /// </summary>
        /// <param name="data">文本数据信息</param>
        /// <returns>Md5字符串</returns>
        public static string CalculateStreamMd5(string data)
        {
            return SoftBasic.CalculateStreamMd5(data, Encoding.UTF8);
        }

        /// <summary>
        /// 获取文本字符串信息的Md5码，使用指定的编码<br />
        /// Get the Md5 code of the text string information, using the specified encoding
        /// </summary>
        /// <param name="data">文本数据信息</param>
        /// <param name="encode">编码信息</param>
        /// <returns>Md5字符串</returns>
        public static string CalculateStreamMd5(string data, Encoding encode)
        {
            string streamMd5;
            using MD5 md5 = MD5.Create();
            streamMd5 = BitConverter.ToString(md5.ComputeHash(encode.GetBytes(data))).Replace("-", "");
            return streamMd5;
        }

        /// <summary>
        /// 从一个字节大小返回带单位的描述，主要是用于显示操作<br />
        /// Returns a description with units from a byte size, mainly for display operations
        /// </summary>
        /// <param name="size">实际的大小值</param>
        /// <returns>最终的字符串值</returns>
        public static string GetSizeDescription(long size)
        {
            if (size < 1000L)
                return size.ToString() + " B";
            if (size < 1000000L)
                return (size / 1024f).ToString("F2") + " Kb";
            return size < 1000000000L ? ((float)(size / 1024.0 / 1024.0)).ToString("F2") + " Mb" : ((float)(size / 1024.0 / 1024.0 / 1024.0)).ToString("F2") + " Gb";
        }

        /// <summary>
        /// 将数组格式化为显示的字符串的信息，支持所有的类型对象<br />
        /// Formats the array into the displayed string information, supporting all types of objects
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="array">数组信息</param>
        /// <returns>最终显示的信息</returns>
        public static string ArrayFormat<T>(T[] array) => SoftBasic.ArrayFormat(array, string.Empty);

        /// <summary>
        /// 将数组格式化为显示的字符串的信息，支持所有的类型对象<br />
        /// Formats the array into the displayed string information, supporting all types of objects
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="array">数组信息</param>
        /// <param name="format">格式化的信息</param>
        /// <returns>最终显示的信息</returns>
        public static string ArrayFormat<T>(T[] array, string format)
        {
            if (array == null)
                return "NULL";
            StringBuilder stringBuilder = new StringBuilder("[");
            for (int index = 0; index < array.Length; ++index)
            {
                stringBuilder.Append(string.IsNullOrEmpty(format) ? array[index].ToString() : string.Format(format, array[index]));
                if (index != array.Length - 1)
                    stringBuilder.Append(",");
            }
            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 将数组格式化为显示的字符串的信息，支持所有的类型对象<br />
        /// Formats the array into the displayed string information, supporting all types of objects
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="array">数组信息</param>
        /// <returns>最终显示的信息</returns>
        public static string ArrayFormat<T>(T array) => SoftBasic.ArrayFormat(array, string.Empty);

        /// <summary>
        /// 将数组格式化为显示的字符串的信息，支持所有的类型对象<br />
        /// Formats the array into the displayed string information, supporting all types of objects
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="array">数组信息</param>
        /// <param name="format">格式化的信息</param>
        /// <returns>最终显示的信息</returns>
        public static string ArrayFormat<T>(T array, string format)
        {
            StringBuilder stringBuilder = new StringBuilder("[");
            if (array is Array array1)
            {
                foreach (object obj in array1)
                {
                    stringBuilder.Append(string.IsNullOrEmpty(format) ? obj.ToString() : string.Format(format, obj));
                    stringBuilder.Append(",");
                }
                if (array1.Length > 0 && stringBuilder[^1] == ',')
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
            }
            else
                stringBuilder.Append(string.IsNullOrEmpty(format) ? array.ToString() : string.Format(format, array));
            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 一个通用的数组新增个数方法，会自动判断越界情况，越界的情况下，会自动的截断或是填充<br />
        /// A common array of new methods, will automatically determine the cross-border situation, in the case of cross-border, will be automatically truncated or filled
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="array">原数据</param>
        /// <param name="data">等待新增的数据</param>
        /// <param name="max">原数据的最大值</param>
        public static void AddArrayData<T>(ref T[] array, T[] data, int max)
        {
            if (data == null || data.Length == 0)
                return;
            if (array.Length == max)
            {
                Array.Copy(array, data.Length, array, 0, array.Length - data.Length);
                Array.Copy(data, 0, array, array.Length - data.Length, data.Length);
            }
            else if (array.Length + data.Length > max)
            {
                T[] objArray = new T[max];
                for (int index = 0; index < max - data.Length; ++index)
                    objArray[index] = array[index + (array.Length - max + data.Length)];
                for (int index = 0; index < data.Length; ++index)
                    objArray[objArray.Length - data.Length + index] = data[index];
                array = objArray;
            }
            else
            {
                T[] objArray = new T[array.Length + data.Length];
                for (int index = 0; index < array.Length; ++index)
                    objArray[index] = array[index];
                for (int index = 0; index < data.Length; ++index)
                    objArray[objArray.Length - data.Length + index] = data[index];
                array = objArray;
            }
        }

        /// <summary>
        /// 将一个数组进行扩充到指定长度，或是缩短到指定长度<br />
        /// Extend an array to a specified length, or shorten to a specified length or fill
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="data">原先数据的数据</param>
        /// <param name="length">新数组的长度</param>
        /// <returns>新数组长度信息</returns>
        public static T[] ArrayExpandToLength<T>(T[] data, int length)
        {
            if (data == null)
                return new T[length];
            if (data.Length == length)
                return data;
            T[] destinationArray = new T[length];
            Array.Copy(data, destinationArray, Math.Min(data.Length, destinationArray.Length));
            return destinationArray;
        }

        /// <summary>
        /// 将一个数组进行扩充到偶数长度<br />
        /// Extend an array to even lengths
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="data">原先数据的数据</param>
        /// <returns>新数组长度信息</returns>
        public static T[] ArrayExpandToLengthEven<T>(T[] data)
        {
            if (data == null)
                return new T[0];
            return data.Length % 2 == 1 ? SoftBasic.ArrayExpandToLength(data, data.Length + 1) : data;
        }

        /// <summary>
        /// 将指定的数据按照指定长度进行分割，例如int[10]，指定长度4，就分割成int[4],int[4],int[2]，然后拼接list<br />
        /// Divide the specified data according to the specified length, such as int [10], and specify the length of 4 to divide into int [4], int [4], int [2], and then concatenate the list
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="array">等待分割的数组</param>
        /// <param name="length">指定的长度信息</param>
        /// <returns>分割后结果内容</returns>
        public static List<T[]> ArraySplitByLength<T>(T[] array, int length)
        {
            if (array == null)
                return new List<T[]>();
            List<T[]> objArrayList = new List<T[]>();
            int sourceIndex = 0;
            while (sourceIndex < array.Length)
            {
                if (sourceIndex + length < array.Length)
                {
                    T[] destinationArray = new T[length];
                    Array.Copy(array, sourceIndex, destinationArray, 0, length);
                    sourceIndex += length;
                    objArrayList.Add(destinationArray);
                }
                else
                {
                    T[] destinationArray = new T[array.Length - sourceIndex];
                    Array.Copy(array, sourceIndex, destinationArray, 0, destinationArray.Length);
                    sourceIndex += length;
                    objArrayList.Add(destinationArray);
                }
            }
            return objArrayList;
        }

        /// <summary>
        /// 将整数进行有效的拆分成数组，指定每个元素的最大值<br />
        /// Effectively split integers into arrays, specifying the maximum value for each element
        /// </summary>
        /// <param name="integer">整数信息</param>
        /// <param name="everyLength">单个的数组长度</param>
        /// <returns>拆分后的数组长度</returns>
        public static int[] SplitIntegerToArray(int integer, int everyLength)
        {
            int[] array = new int[integer / everyLength + (integer % everyLength == 0 ? 0 : 1)];
            for (int index = 0; index < array.Length; ++index)
                array[index] = index != array.Length - 1 ? everyLength : (integer % everyLength == 0 ? everyLength : integer % everyLength);
            return array;
        }

        /// <summary>
        /// 判断两个字节的指定部分是否相同<br />
        /// Determines whether the specified portion of a two-byte is the same
        /// </summary>
        /// <param name="b1">第一个字节</param>
        /// <param name="start1">第一个字节的起始位置</param>
        /// <param name="b2">第二个字节</param>
        /// <param name="start2">第二个字节的起始位置</param>
        /// <param name="length">校验的长度</param>
        /// <returns>返回是否相等</returns>
        /// <exception cref="T:System.IndexOutOfRangeException"></exception>
        public static bool IsTwoBytesEqual(byte[] b1, int start1, byte[] b2, int start2, int length)
        {
            if (b1 == null || b2 == null)
                return false;
            for (int index = 0; index < length; ++index)
            {
                if (b1[index + start1] != b2[index + start2])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 判断两个字节的指定部分是否相同<br />
        /// Determines whether the specified portion of a two-byte is the same
        /// </summary>
        /// <param name="b1">第一个字节</param>
        /// <param name="b2">第二个字节</param>
        /// <returns>返回是否相等</returns>
        public static bool IsTwoBytesEqual(byte[] b1, byte[] b2)
        {
            return b1 != null && b2 != null && b1.Length == b2.Length && SoftBasic.IsTwoBytesEqual(b1, 0, b2, 0, b1.Length);
        }

        /// <summary>
        /// 判断两个数据的令牌是否相等<br />
        /// Determines whether the tokens of two data are equal
        /// </summary>
        /// <param name="head">字节数据</param>
        /// <param name="token">GUID数据</param>
        /// <returns>返回是否相等</returns>
        public static bool IsByteTokenEqual(byte[] head, Guid token)
        {
            return SoftBasic.IsTwoBytesEqual(head, 12, token.ToByteArray(), 0, 16);
        }

        /// <summary>
        /// 判断两个数据的令牌是否相等<br />
        /// Determines whether the tokens of two data are equal
        /// </summary>
        /// <param name="token1">第一个令牌</param>
        /// <param name="token2">第二个令牌</param>
        /// <returns>返回是否相等</returns>
        public static bool IsTwoTokenEqual(Guid token1, Guid token2)
        {
            return SoftBasic.IsTwoBytesEqual(token1.ToByteArray(), 0, token2.ToByteArray(), 0, 16);
        }

        /// <summary>
        /// 获取一个枚举类型的所有枚举值，可直接应用于组合框数据<br />
        /// Gets all the enumeration values of an enumeration type that can be applied directly to the combo box data
        /// </summary>
        /// <typeparam name="TEnum">枚举的类型值</typeparam>
        /// <returns>枚举值数组</returns>
        public static TEnum[] GetEnumValues<TEnum>() where TEnum : struct
        {
            return (TEnum[])Enum.GetValues(typeof(TEnum));
        }

        /// <summary>
        /// 从字符串的枚举值数据转换成真实的枚举值数据<br />
        /// Convert enumeration value data from strings to real enumeration value data
        /// </summary>
        /// <typeparam name="TEnum">枚举的类型值</typeparam>
        /// <param name="value">枚举的字符串的数据值</param>
        /// <returns>真实的枚举值</returns>
        public static TEnum GetEnumFromString<TEnum>(string value) where TEnum : struct
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }

        /// <summary>
        /// 一个泛型方法，提供json对象的数据读取<br />
        /// A generic method that provides data read for a JSON object
        /// </summary>
        /// <typeparam name="T">读取的泛型</typeparam>
        /// <param name="json">json对象</param>
        /// <param name="name">值名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值对象</returns>
        public static T GetValueFromJsonObject<T>(JObject json, string name, T defaultValue)
        {
            return json.Property(name) != null ? json.Property(name)!.Value.Value<T>() : defaultValue;
        }

        /// <summary>
        /// 一个泛型方法，提供json对象的数据写入<br />
        /// A generic method that provides data writing to a JSON object
        /// </summary>
        /// <typeparam name="T">写入的泛型</typeparam>
        /// <param name="json">json对象</param>
        /// <param name="property">值名称</param>
        /// <param name="value">值数据</param>
        public static void JsonSetValue<T>(JObject json, string property, T value)
        {
            if (json.Property(property) != null)
                json.Property(property)!.Value = new JValue(value);
            else
                json.Add(property, new JValue(value));
        }

        /// <summary>
        /// 字节数据转化成16进制表示的字符串<br />
        /// Byte data into a string of 16 binary representations
        /// </summary>
        /// <param name="inBytes">字节数组</param>
        /// <returns>返回的字符串</returns>
        public static string ByteToHexString(byte[] inBytes)
        {
            return SoftBasic.ByteToHexString(inBytes, char.MinValue);
        }

        /// <summary>
        /// 字节数据转化成16进制表示的字符串<br />
        /// Byte data into a string of 16 binary representations
        /// </summary>
        /// <param name="inBytes">字节数组</param>
        /// <param name="segment">分割符</param>
        /// <returns>返回的字符串</returns>
        /// <example>
        /// <code lang="cs" source="HslCommunication_Net45.Test\Documentation\Samples\BasicFramework\SoftBasicExample.cs" region="ByteToHexStringExample2" title="ByteToHexString示例" />
        /// </example>
        public static string ByteToHexString(byte[] inBytes, char segment)
        {
            return SoftBasic.ByteToHexString(inBytes, segment, 0);
        }

        /// <summary>
        /// 字节数据转化成16进制表示的字符串<br />
        /// Byte data into a string of 16 binary representations
        /// </summary>
        /// <param name="inBytes">字节数组</param>
        /// <param name="segment">分割符，如果设置为0，则没有分隔符信息</param>
        /// <param name="newLineCount">每隔指定数量的时候进行换行，如果小于等于0，则不进行分行显示</param>
        /// <param name="format">格式信息，默认为{0:X2}</param>
        /// <returns>返回的字符串</returns>
        public static string ByteToHexString(
          byte[] inBytes,
          char segment,
          int newLineCount,
          string format = "{0:X2}")
        {
            if (inBytes == null)
                return string.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            long num = 0;
            foreach (byte inByte in inBytes)
            {
                if (segment == char.MinValue)
                    stringBuilder.Append(string.Format(format, inByte));
                else
                    stringBuilder.Append(string.Format(format + "{1}", inByte, segment));
                ++num;
                if (newLineCount > 0 && num >= newLineCount)
                {
                    stringBuilder.Append(Environment.NewLine);
                    num = 0L;
                }
            }
            if (segment != char.MinValue && stringBuilder.Length > 1 && stringBuilder[^1] == segment)
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 字符串数据转化成16进制表示的字符串<br />
        /// String data into a string of 16 binary representations
        /// </summary>
        /// <param name="inString">输入的字符串数据</param>
        /// <returns>返回的字符串</returns>
        /// <exception cref="T:System.NullReferenceException"></exception>
        public static string ByteToHexString(string inString)
        {
            return SoftBasic.ByteToHexString(Encoding.Unicode.GetBytes(inString));
        }

        private static int GetHexCharIndex(char ch)
        {
            switch (ch)
            {
                case '0':
                    return 0;
                case '1':
                    return 1;
                case '2':
                    return 2;
                case '3':
                    return 3;
                case '4':
                    return 4;
                case '5':
                    return 5;
                case '6':
                    return 6;
                case '7':
                    return 7;
                case '8':
                    return 8;
                case '9':
                    return 9;
                case 'A':
                case 'a':
                    return 10;
                case 'B':
                case 'b':
                    return 11;
                case 'C':
                case 'c':
                    return 12;
                case 'D':
                case 'd':
                    return 13;
                case 'E':
                case 'e':
                    return 14;
                case 'F':
                case 'f':
                    return 15;
                default:
                    return -1;
            }
        }

        /// <summary>
        /// 将16进制的字符串转化成Byte数据，将检测每2个字符转化，也就是说，中间可以是任意字符<br />
        /// Converts a 16-character string into byte data, which will detect every 2 characters converted, that is, the middle can be any character
        /// </summary>
        /// <param name="hex">十六进制的字符串，中间可以是任意的分隔符</param>
        /// <returns>转换后的字节数组</returns>
        /// <remarks>参数举例：AA 01 34 A8</remarks>
        public static byte[] HexStringToBytes(string hex)
        {
            MemoryStream memoryStream = new MemoryStream();
            for (int index = 0; index < hex.Length; ++index)
            {
                if (index + 1 < hex.Length && SoftBasic.GetHexCharIndex(hex[index]) >= 0 && SoftBasic.GetHexCharIndex(hex[index + 1]) >= 0)
                {
                    memoryStream.WriteByte((byte)(SoftBasic.GetHexCharIndex(hex[index]) * 16 + SoftBasic.GetHexCharIndex(hex[index + 1])));
                    ++index;
                }
            }
            byte[] array = memoryStream.ToArray();
            memoryStream.Dispose();
            return array;
        }

        /// <summary>
        /// 将byte数组按照双字节进行反转，如果为单数的情况，则自动补齐<br />
        /// Reverses the byte array by double byte, or if the singular is the case, automatically
        /// </summary>
        /// <remarks>例如传入的字节数据是 01 02 03 04, 那么反转后就是 02 01 04 03</remarks>
        /// <param name="inBytes">输入的字节信息</param>
        /// <returns>反转后的数据</returns>
        public static byte[] BytesReverseByWord(byte[] inBytes)
        {
            if (inBytes == null)
                return null;
            if (inBytes.Length == 0)
                return new byte[0];
            byte[] lengthEven = SoftBasic.ArrayExpandToLengthEven(inBytes.CopyArray());
            for (int index = 0; index < lengthEven.Length / 2; ++index)
            {
                (lengthEven[index * 2], lengthEven[index * 2 + 1]) = (lengthEven[index * 2 + 1], lengthEven[index * 2]);
            }
            return lengthEven;
        }

        /// <summary>
        /// 将字节数组显示为ASCII格式的字符串，当遇到0x20以下及0x7E以上的不可见字符时，使用十六进制的数据显示<br />
        /// Display the byte array as a string in ASCII format, when encountering invisible characters below 0x20 and above 0x7E, use hexadecimal data to display<br />
        /// </summary>
        /// <param name="content">字节数组信息</param>
        /// <returns>ASCII格式的字符串信息</returns>
        public static string GetAsciiStringRender(byte[] content)
        {
            if (content == null)
                return string.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            for (int index = 0; index < content.Length; ++index)
            {
                if (content[index] < 32 || content[index] > 126)
                    stringBuilder.Append($"\\{content[index]:X2}");
                else
                    stringBuilder.Append((char)content[index]);
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 从显示的ASCII格式的字符串，转为原始字节数组，如果遇到 \00 这种表示原始字节的内容，则直接进行转换操作，遇到 \r 直接转换 0x0D, \n 直接转换 0x0A<br />
        /// Convert from the displayed string in ASCII format to the original byte array. If you encounter \00, which represents the original byte content,
        /// the conversion operation is performed directly. When encountering \r, it is directly converted to 0x0D, and \n is directly converted to 0x0A.
        /// </summary>
        /// <param name="render">等待转换的字符串</param>
        /// <returns>原始字节数组</returns>
        public static byte[] GetFromAsciiStringRender(string render)
        {
            if (string.IsNullOrEmpty(render))
                return new byte[0];
            MatchEvaluator evaluator = m => string.Format("{0}", (char)Convert.ToByte(m.Value.Substring(1), 16));
            return Encoding.ASCII.GetBytes(Regex.Replace(render.Replace("\\r", "\r").Replace("\\n", "\n"), "\\\\[0-9A-Fa-f]{2}", evaluator));
        }

        /// <summary>
        /// 将原始的byte数组转换成ascii格式的byte数组<br />
        /// Converts the original byte array to an ASCII-formatted byte array
        /// </summary>
        /// <param name="inBytes">等待转换的byte数组</param>
        /// <returns>转换后的数组</returns>
        public static byte[] BytesToAsciiBytes(byte[] inBytes)
        {
            return Encoding.ASCII.GetBytes(SoftBasic.ByteToHexString(inBytes));
        }

        /// <summary>
        /// 将ascii格式的byte数组转换成原始的byte数组<br />
        /// Converts an ASCII-formatted byte array to the original byte array
        /// </summary>
        /// <param name="inBytes">等待转换的byte数组</param>
        /// <returns>转换后的数组</returns>
        public static byte[] AsciiBytesToBytes(byte[] inBytes)
        {
            return SoftBasic.HexStringToBytes(Encoding.ASCII.GetString(inBytes));
        }

        /// <summary>
        /// 从字节构建一个ASCII格式的数据内容<br />
        /// Build an ASCII-formatted data content from bytes
        /// </summary>
        /// <param name="value">数据</param>
        /// <returns>ASCII格式的字节数组</returns>
        public static byte[] BuildAsciiBytesFrom(byte value)
        {
            return Encoding.ASCII.GetBytes(value.ToString("X2"));
        }

        /// <summary>
        /// 从short构建一个ASCII格式的数据内容<br />
        /// Constructing an ASCII-formatted data content from a short
        /// </summary>
        /// <param name="value">数据</param>
        /// <returns>ASCII格式的字节数组</returns>
        public static byte[] BuildAsciiBytesFrom(short value)
        {
            return Encoding.ASCII.GetBytes(value.ToString("X4"));
        }

        /// <summary>
        /// 从ushort构建一个ASCII格式的数据内容<br />
        /// Constructing an ASCII-formatted data content from ushort
        /// </summary>
        /// <param name="value">数据</param>
        /// <returns>ASCII格式的字节数组</returns>
        public static byte[] BuildAsciiBytesFrom(ushort value)
        {
            return Encoding.ASCII.GetBytes(value.ToString("X4"));
        }

        /// <summary>
        /// 从uint构建一个ASCII格式的数据内容<br />
        /// Constructing an ASCII-formatted data content from uint
        /// </summary>
        /// <param name="value">数据</param>
        /// <returns>ASCII格式的字节数组</returns>
        public static byte[] BuildAsciiBytesFrom(uint value)
        {
            return Encoding.ASCII.GetBytes(value.ToString("X8"));
        }

        /// <summary>
        /// 从字节数组构建一个ASCII格式的数据内容<br />
        /// Byte array to construct an ASCII format data content
        /// </summary>
        /// <param name="value">字节信息</param>
        /// <returns>ASCII格式的地址</returns>
        public static byte[] BuildAsciiBytesFrom(byte[] value)
        {
            byte[] numArray = new byte[value.Length * 2];
            for (int index = 0; index < value.Length; ++index)
                SoftBasic.BuildAsciiBytesFrom(value[index]).CopyTo(numArray, 2 * index);
            return numArray;
        }

        private static byte GetDataByBitIndex(int offset)
        {
            switch (offset)
            {
                case 0:
                    return 1;
                case 1:
                    return 2;
                case 2:
                    return 4;
                case 3:
                    return 8;
                case 4:
                    return 16;
                case 5:
                    return 32;
                case 6:
                    return 64;
                case 7:
                    return 128;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// 获取byte数据类型的第offset位，是否为True<br />
        /// Gets the index bit of the byte data type, whether it is True
        /// </summary>
        /// <param name="value">byte数值</param>
        /// <param name="offset">索引位置</param>
        /// <returns>结果</returns>
        public static bool BoolOnByteIndex(byte value, int offset)
        {
            byte dataByBitIndex = SoftBasic.GetDataByBitIndex(offset);
            return (value & dataByBitIndex) == dataByBitIndex;
        }

        /// <summary>
        /// 设置取byte数据类型的第offset位，是否为True<br />
        /// Set the offset bit of the byte data type, whether it is True
        /// </summary>
        /// <param name="byt">byte数值</param>
        /// <param name="offset">索引位置</param>
        /// <param name="value">写入的结果值</param>
        /// <returns>结果</returns>
        public static byte SetBoolOnByteIndex(byte byt, int offset, bool value)
        {
            byte dataByBitIndex = SoftBasic.GetDataByBitIndex(offset);
            return value ? (byte)(byt | (uint)dataByBitIndex) : (byte)(byt & (uint)~dataByBitIndex);
        }

        /// <summary>
        /// 将bool数组转换到byte数组<br />
        /// Converting a bool array to a byte array
        /// </summary>
        /// <param name="array">bool数组</param>
        /// <returns>转换后的字节数组</returns>
        public static byte[] BoolArrayToByte(bool[] array)
        {
            if (array == null)
                return null;
            byte[] numArray = new byte[array.Length % 8 == 0 ? array.Length / 8 : array.Length / 8 + 1];
            for (int index = 0; index < array.Length; ++index)
            {
                if (array[index])
                    numArray[index / 8] += SoftBasic.GetDataByBitIndex(index % 8);
            }
            return numArray;
        }

        /// <summary>
        /// 将bool数组转换为字符串进行显示，true被转为1，false转换为0<br />
        /// Convert the bool array to a string for display, true is converted to 1, false is converted to 0
        /// </summary>
        /// <param name="array">bool数组</param>
        /// <returns>转换后的字符串</returns>
        public static string BoolArrayToString(bool[] array)
        {
            if (array == null)
                return string.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            
            foreach (var t in array)
                stringBuilder.Append(t ? "1" : "0");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// 从Byte数组中提取位数组，length代表位数，例如数组 03 A1 长度10转为 [1100 0000 10]<br />
        /// Extracts a bit array from a byte array, length represents the number of digits
        /// </summary>
        /// <param name="inBytes">原先的字节数组</param>
        /// <param name="length">想要转换的长度，如果超出自动会缩小到数组最大长度</param>
        /// <returns>转换后的bool数组</returns>
        public static bool[] ByteToBoolArray(byte[] inBytes, int length)
        {
            if (inBytes == null)
                return null;
            if (length > inBytes.Length * 8)
                length = inBytes.Length * 8;
            bool[] boolArray = new bool[length];
            for (int index = 0; index < length; ++index)
                boolArray[index] = SoftBasic.BoolOnByteIndex(inBytes[index / 8], index % 8);
            return boolArray;
        }

        /// <summary>
        /// 从Byte数组中提取位数组，length代表位数，例如数组 01 00 长度10转为 [true,false]<br />
        /// </summary>
        /// <param name="inBytes">原先的字节数组</param>
        /// <param name="length">想要转换的长度，如果超出自动会缩小到数组最大长度</param>
        /// <param name="trueValue">当为true时对应的byte值</param>
        /// <returns>转换后bool数组</returns>
        public static bool[] ByteToBoolArray(byte[] inBytes, int length, byte trueValue)
        {
            if (inBytes == null)
                return null;
            if (length > inBytes.Length)
                length = inBytes.Length;
            bool[] boolArray = new bool[length];
            for (int index = 0; index < length; ++index)
                boolArray[index] = inBytes[index] == trueValue;
            return boolArray;
        }

        /// <summary>
        /// 从Byte数组中提取所有的位数组<br />
        /// Extracts a bit array from a byte array, length represents the number of digits
        /// </summary>
        /// <param name="inBytes">原先的字节数组</param>
        /// <returns>转换后的bool数组</returns>
        public static bool[] ByteToBoolArray(byte[] inBytes)
        {
            return inBytes != null ? SoftBasic.ByteToBoolArray(inBytes, inBytes.Length * 8) : null;
        }

        /// <summary>
        /// 将一个数组的前后移除指定位数，返回新的一个数组<br />
        /// Removes a array before and after the specified number of bits, returning a new array
        /// </summary>
        /// <param name="value">数组</param>
        /// <param name="leftLength">前面的位数</param>
        /// <param name="rightLength">后面的位数</param>
        /// <returns>新的数组</returns>
        /// <exception cref="T:System.RankException"></exception>
        public static T[] ArrayRemoveDouble<T>(T[] value, int leftLength, int rightLength)
        {
            if (value == null)
                return null;
            if (value.Length <= leftLength + rightLength)
                return new T[0];
            T[] destinationArray = new T[value.Length - leftLength - rightLength];
            Array.Copy(value, leftLength, destinationArray, 0, destinationArray.Length);
            return destinationArray;
        }

        /// <summary>
        /// 将一个数组的前面指定位数移除，返回新的一个数组<br />
        /// Removes the preceding specified number of bits in a array, returning a new array
        /// </summary>
        /// <param name="value">数组</param>
        /// <param name="length">等待移除的长度</param>
        /// <returns>新的数组</returns>
        /// <exception cref="T:System.RankException"></exception>
        public static T[] ArrayRemoveBegin<T>(T[] value, int length)
        {
            return SoftBasic.ArrayRemoveDouble(value, length, 0);
        }

        /// <summary>
        /// 将一个数组的后面指定位数移除，返回新的一个数组<br />
        /// Removes the specified number of digits after a array, returning a new array
        /// </summary>
        /// <param name="value">数组</param>
        /// <param name="length">等待移除的长度</param>
        /// <returns>新的数组</returns>
        /// <exception cref="T:System.RankException"></exception>
        public static T[] ArrayRemoveLast<T>(T[] value, int length)
        {
            return SoftBasic.ArrayRemoveDouble(value, 0, length);
        }

        /// <summary>
        /// 获取到数组里面的中间指定长度的数组<br />
        /// Get an array of the specified length in the array
        /// </summary>
        /// <param name="value">数组</param>
        /// <param name="index">起始索引</param>
        /// <param name="length">数据的长度</param>
        /// <returns>新的数组值</returns>
        /// <exception cref="T:System.IndexOutOfRangeException"></exception>
        public static T[] ArraySelectMiddle<T>(T[] value, int index, int length)
        {
            if (value == null)
                return null;
            if (length == 0)
                return new T[0];
            T[] destinationArray = new T[Math.Min(value.Length, length)];
            Array.Copy(value, index, destinationArray, 0, destinationArray.Length);
            return destinationArray;
        }

        /// <summary>
        /// 选择一个数组的前面的几个数据信息<br />
        /// Select the begin few items of data information of a array
        /// </summary>
        /// <param name="value">数组</param>
        /// <param name="length">数据的长度</param>
        /// <returns>新的数组</returns>
        public static T[] ArraySelectBegin<T>(T[] value, int length)
        {
            if (length == 0)
                return new T[0];
            T[] destinationArray = new T[Math.Min(value.Length, length)];
            if (destinationArray.Length != 0)
                Array.Copy(value, 0, destinationArray, 0, destinationArray.Length);
            return destinationArray;
        }

        /// <summary>
        /// 选择一个数组的后面的几个数据信息<br />
        /// Select the last few items of data information of a array
        /// </summary>
        /// <param name="value">数组</param>
        /// <param name="length">数据的长度</param>
        /// <returns>新的数组信息</returns>
        public static T[] ArraySelectLast<T>(T[] value, int length)
        {
            if (length == 0)
                return new T[0];
            T[] destinationArray = new T[Math.Min(value.Length, length)];
            Array.Copy(value, value.Length - length, destinationArray, 0, destinationArray.Length);
            return destinationArray;
        }

        /// <summary>
        /// 拼接任意个泛型数组为一个总的泛型数组对象，采用深度拷贝实现。<br />
        /// Splicing any number of generic arrays into a total generic array object is implemented using deep copy.
        /// </summary>
        /// <typeparam name="T">数组的类型信息</typeparam>
        /// <param name="arrays">任意个长度的数组</param>
        /// <returns>拼接之后的最终的结果对象</returns>
        public static T[] SpliceArray<T>(params T[][] arrays)
        {
            int length = 0;
            for (int index = 0; index < arrays.Length; ++index)
            {
                T[] array = arrays[index];
                if (array != null && array.Length != 0)
                    length += arrays[index].Length;
            }
            int index1 = 0;
            T[] objArray = new T[length];
            for (int index2 = 0; index2 < arrays.Length; ++index2)
            {
                T[] array = arrays[index2];
                if (array != null && array.Length != 0)
                {
                    arrays[index2].CopyTo(objArray, index1);
                    index1 += arrays[index2].Length;
                }
            }
            return objArray;
        }

        /// <summary>
        /// 将一个<see cref="T:System.String" />的数组和多个<see cref="T:System.String" /> 类型的对象整合成一个数组<br />
        /// Combine an array of <see cref="T:System.String" /> and multiple objects of type <see cref="T:System.String" /> into an array
        /// </summary>
        /// <param name="first">第一个数组对象</param>
        /// <param name="array">字符串数组信息</param>
        /// <returns>总的数组对象</returns>
        public static string[] SpliceStringArray(string first, string[] array)
        {
            List<string> stringList = new List<string>();
            stringList.Add(first);
            stringList.AddRange(array);
            return stringList.ToArray();
        }

        /// <summary>
        /// 将两个<see cref="T:System.String" />的数组和多个<see cref="T:System.String" /> 类型的对象整合成一个数组<br />
        /// Combine two arrays of <see cref="T:System.String" /> and multiple objects of type <see cref="T:System.String" /> into one array
        /// </summary>
        /// <param name="first">第一个数据对象</param>
        /// <param name="second">第二个数据对象</param>
        /// <param name="array">字符串数组信息</param>
        /// <returns>总的数组对象</returns>
        public static string[] SpliceStringArray(string first, string second, string[] array)
        {
            List<string> stringList = new List<string>();
            stringList.Add(first);
            stringList.Add(second);
            stringList.AddRange(array);
            return stringList.ToArray();
        }

        /// <summary>
        /// 将两个<see cref="T:System.String" />的数组和多个<see cref="T:System.String" /> 类型的对象整合成一个数组<br />
        /// Combine two arrays of <see cref="T:System.String" /> and multiple objects of type <see cref="T:System.String" /> into one array
        /// </summary>
        /// <param name="first">第一个数据对象</param>
        /// <param name="second">第二个数据对象</param>
        /// <param name="third">第三个数据对象</param>
        /// <param name="array">字符串数组信息</param>
        /// <returns>总的数组对象</returns>
        public static string[] SpliceStringArray(
          string first,
          string second,
          string third,
          string[] array)
        {
            List<string> stringList = new List<string>();
            stringList.Add(first);
            stringList.Add(second);
            stringList.Add(third);
            stringList.AddRange(array);
            return stringList.ToArray();
        }

        private static bool ValidateUrlEncodingParameters(byte[] bytes, int offset, int count)
        {
            if (bytes == null && count == 0)
                return false;
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));
            if (offset < 0 || offset > bytes.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0 || offset + count > bytes.Length)
                throw new ArgumentOutOfRangeException(nameof(count));
            return true;
        }

        private static bool IsUrlSafeChar(char ch)
        {
            if (ch >= 'a' && ch <= 'z' || ch >= 'A' && ch <= 'Z' || ch >= '0' && ch <= '9')
                return true;
            switch (ch)
            {
                case '!':
                case '(':
                case ')':
                case '*':
                case '-':
                case '.':
                case '_':
                    return true;
                default:
                    return false;
            }
        }

        private static char IntToHex(int n)
        {
            Debug.Assert(n < 16);
            return n <= 9 ? (char)(n + 48) : (char)(n - 10 + 65);
        }

        private static byte[] UrlEncodeToBytes(byte[] bytes)
        {
            int num1 = 0;
            int length = bytes.Length;
            if (!ValidateUrlEncodingParameters(bytes, num1, length))
                return null;
            int num2 = 0;
            int num3 = 0;
            for (int index = 0; index < length; ++index)
            {
                char ch = (char)bytes[num1 + index];
                if (ch == ' ')
                    ++num2;
                else if (!IsUrlSafeChar(ch))
                    ++num3;
            }
            if (num2 == 0 && num3 == 0)
            {
                if (bytes.Length == length)
                    return bytes;
                byte[] dst = new byte[length];
                Buffer.BlockCopy(bytes, num1, dst, 0, length);
                return dst;
            }
            byte[] bytes1 = new byte[length + num3 * 2];
            int num4 = 0;
            for (int index1 = 0; index1 < length; ++index1)
            {
                byte num5 = bytes[num1 + index1];
                char ch = (char)num5;
                if (IsUrlSafeChar(ch))
                    bytes1[num4++] = num5;
                else if (ch == ' ')
                {
                    bytes1[num4++] = 43;
                }
                else
                {
                    byte[] numArray1 = bytes1;
                    int index2 = num4;
                    int num6 = index2 + 1;
                    numArray1[index2] = 37;
                    byte[] numArray2 = bytes1;
                    int index3 = num6;
                    int num7 = index3 + 1;
                    int hex1 = (byte)IntToHex(num5 >> 4 & 15);
                    numArray2[index3] = (byte)hex1;
                    byte[] numArray3 = bytes1;
                    int index4 = num7;
                    num4 = index4 + 1;
                    int hex2 = (byte)IntToHex(num5 & 15);
                    numArray3[index4] = (byte)hex2;
                }
            }
            return bytes1;
        }

        private static byte[] UrlEncode(byte[] bytes, bool alwaysCreateNewReturnValue)
        {
            byte[] bytes1 = SoftBasic.UrlEncodeToBytes(bytes);
            return !alwaysCreateNewReturnValue || bytes1 == null || bytes1 != bytes ? bytes1 : (byte[])bytes1.Clone();
        }

        /// <summary>
        /// 将字符串编码为URL可以识别的字符串，中文会被编码为%开头的数据，例如 中文 转义为 %2F%E4%B8%AD%E6%96%87 <br />
        /// Encoding a string as a URL-recognizable string Chinese encoded as data that begins with %, such as 中文 escaped as %2F%E4%B8%AD%E6%96%87
        /// </summary>
        /// <param name="str">等待转换的字符串数据</param>
        /// <param name="e">编码信息，一般为 UTF8 </param>
        /// <returns>编码之后的结果</returns>
        public static string UrlEncode(string str, Encoding e)
        {
            return str == null ? null : Encoding.ASCII.GetString(UrlEncode(e.GetBytes(str), true));
        }

        /// <summary>
        /// 使用序列化反序列化深度克隆一个对象，该对象需要支持序列化特性<br />
        /// Cloning an object with serialization deserialization depth that requires support for serialization attributes
        /// </summary>
        /// <param name="original">源对象，支持序列化</param>
        /// <returns>新的一个实例化的对象</returns>
        /// <exception cref="T:System.NullReferenceException"></exception>
        /// <exception cref="T:System.NonSerializedAttribute"></exception>
        /// <remarks>
        /// <note type="warning">
        /// <paramref name="original" /> 参数必须实现序列化的特性
        /// </note>
        /// </remarks>
        public static T DeepClone<T>(T original)
        {
            using MemoryStream memoryStream = new MemoryStream();
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            serializer.WriteObject(memoryStream, original);
            memoryStream.Position = 0;
            return (T)serializer.ReadObject(memoryStream);
        }
    }
}
