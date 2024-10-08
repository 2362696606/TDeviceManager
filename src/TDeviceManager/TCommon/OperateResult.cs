﻿using System.Diagnostics.CodeAnalysis;

namespace TCommon
{
    public class OperateResult
    {
        private bool _isSuccess;

        public OperateResult()
        {
        }

        /// <summary>
        /// 使用指定的消息实例化一个默认的结果对象
        /// </summary>
        /// <param name="msg">错误消息</param>
        public OperateResult(string msg)
        {
            Message = msg;
        }

        /// <summary>
        /// 使用错误代码，消息文本来实例化对象
        /// </summary>
        /// <param name="err">错误代码</param>
        /// <param name="msg">错误消息</param>
        public OperateResult(int err, string msg)
        {
            ErrorCode = err;
            Message = msg;
        }

        /// <summary>
        /// 操作是否成功标识
        /// </summary>
        public bool IsSuccess
        {
            get => _isSuccess;
            set
            {
                _isSuccess = value;
                if (_isSuccess)
                {
                    if (string.IsNullOrEmpty(Message))
                    {
                        Message = "成功";
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(Message))
                    {
                        Message = "失败";
                    }
                }
            }
        }

        /// <summary>
        /// 成功时，该值为1，失败时该值为0
        /// </summary>
        public int ResultCode => IsSuccess ? 1 : 0;
        /// <summary>
        /// 是否失败
        /// </summary>
        public bool IsFailed => !IsSuccess;

        /// <summary>
        /// 操作返回信息
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 操作错误代码
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 是否有Content属性，泛型结果有，非泛型结果中为null
        /// </summary>
        public bool HasContent
        {
            get
            {
                var type = this.GetType();
                var typeGenericTypeArguments = type.GenericTypeArguments;
                if (typeGenericTypeArguments.Length > 0)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// 获取泛型Result中的Content,如果为非泛型，结果为null,
        /// 如：OperateResult.CreateSuccessResult(true)  此结果为 true
        ///  OperateResult.CreateSuccessResult()中为null
        /// </summary>
        /// <returns></returns>
        public object? GetContent()
        {
            if (HasContent)
            {
                var type = this.GetType();
                var propertyInfo = type.GetProperty("Content");
                if (propertyInfo != null)
                {
                    var value = propertyInfo.GetValue(this);
                    return value;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///  结果返回
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"错误代码：{ErrorCode}   错误信息：{Message}";
        }

        /// <summary>
        /// 创建并返回一个失败的结果对象，该对象复制另一个结果对象的错误信息
        /// </summary>
        /// <typeparam name="T">目标数据类型</typeparam>
        /// <param name="result">之前的结果对象</param>
        /// <returns>带默认泛型对象的失败结果类</returns>
        public static OperateResult<T> CreateFailedResult<T>(OperateResult result)
        {
            return new OperateResult<T>()
            {
                ErrorCode = result.ErrorCode,
                Message = result.Message,
            };
        }

        public static OperateResult<T> CreateFailedResult<T>(string msg)
        {
            return new OperateResult<T>(msg)
            {
                ErrorCode = 1,
                Message = msg,
            };
        }

        public static OperateResult<T> CreateFailedResult<T>(string msg, int errorCode)
        {
            return new OperateResult<T>(msg)
            {
                ErrorCode = errorCode,
                Message = msg,
            };
        }

        /// <summary>
        /// 创建失败的信息返回对象
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static OperateResult CreateFailedResult(string msg, int errorCode)
        {
            return new OperateResult()
            {
                IsSuccess = false,
                ErrorCode = errorCode,
                Message = msg,
            };
        }

        public static string ConvertException(Exception ex)
        {
            return ex.Message + " " + ex.InnerException + " " + Environment.NewLine + ex.StackTrace;
        }

        public static string ConvertExMessage(Exception ex)
        {
            return ex.Message;
        }

        /// <summary>
        /// 创建并返回一个失败的结果对象
        /// </summary>
        /// <returns>成功的结果对象</returns>
        public static OperateResult CreateFailedResult()
        {
            return new OperateResult()
            {
                IsSuccess = false,
                ErrorCode = 1,
            };
        }

        /// <summary>
        /// 创建失败结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static OperateResult CreateFailedResult(string msg)
        {
            return new OperateResult()
            {
                IsSuccess = false,
                ErrorCode = 1,
                Message = msg,
            };
        }

        /// <summary>
        /// 创建一个带成功信息的成功对象
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static OperateResult CreateSuccessResult(string msg)
        {
            return new OperateResult()
            {
                IsSuccess = true,
                ErrorCode = 0,
                Message = msg,
            };
        }

        /// <summary>
        /// 创建并返回一个成功的结果对象
        /// </summary>
        /// <returns>成功的结果对象</returns>
        public static OperateResult CreateSuccessResult()
        {
            return new OperateResult()
            {
                IsSuccess = true,
                ErrorCode = 0,
            };
        }

        /// <summary>
        /// 创建并返回一个成功的结果对象，并带有一个参数对象
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="value">类型的值对象</param>
        /// <returns>成功的结果对象</returns>
        public static OperateResult<T> CreateSuccessResult<T>(T value)
        {
            return new OperateResult<T>()
            {
                IsSuccess = true,
                ErrorCode = 0,
                Content = value
            };
        }

        /// <summary>
        /// 失败的返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static OperateResult<T> CreateFailedResult<T>(T value, string msg)
        {
            return new OperateResult<T>()
            {
                IsSuccess = false,
                ErrorCode = 0,
                Message = msg,
                Content = value
            };
        }

        /// <summary>
        /// 失败的返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static OperateResult<T> CreateSuccessResult<T>(T value, string msg)
        {
            return new OperateResult<T>()
            {
                IsSuccess = true,
                ErrorCode = 0,
                Message = msg,
                Content = value
            };
        }

        /// <summary>
        /// 基本描述
        /// </summary>
        /// <returns></returns>
        public string Description()
        {
            return $"{(IsSuccess ? "成功" : "失败")}：{IsSuccess} 消息：{Message}";
        }
        /// <summary>
        /// 基本描述
        /// </summary>
        /// <returns></returns>
        public string ContentDescription()
        {
            if (HasContent)
            {
                return $"{(IsSuccess ? "成功" : "失败")}：{IsSuccess}，返回结果：{GetContent()} 消息：{Message}";
            }
            else
            {
                return Description();
            }
        }
    }

    /// <summary>
    /// 操作结果的泛型类，允许带一个用户自定义的泛型对象，推荐使用这个类
    /// </summary>
    /// <typeparam name="T">泛型类</typeparam>
    public class OperateResult<T> : OperateResult
    {
        #region Constructor

        /// <summary>
        /// 实例化一个默认的结果对象
        /// </summary>
        public OperateResult()
        {

        }

        /// <summary>
        /// 使用指定的消息实例化一个默认的结果对象
        /// </summary>
        /// <param name="msg">错误消息</param>
        public OperateResult(string msg)
            : base(msg)
        {
        }

        /// <summary>
        /// 使用错误代码，消息文本来实例化对象
        /// </summary>
        /// <param name="err">错误代码</param>
        /// <param name="msg">错误消息</param>
        public OperateResult(int err, string msg)
            : base(err, msg)
        {
        }

        #endregion Constructor

        /// <summary>
        /// 用户自定义的泛型数据
        /// </summary>
        [NotNull]
        public T? Content { get; set; }
    }
}
