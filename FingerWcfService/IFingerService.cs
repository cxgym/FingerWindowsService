using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace FingerWcfService
{
    /// <summary>
    /// WCF服务接口
    /// </summary>
    [ServiceContract]
    public interface IFingerService
    {
        /// <summary>
        /// 测试Post消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "TestPost/{message}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string TestPost(string message);

        //  Message：http://localhost:22889/Hello
        /// <summary>
        /// 测试Get消息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "Hello", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string TestGet();

        //  Message：1、"PostMessage?message={message}&type={type}&level={level}" 调用成功但是参数都为空
        //  Message：2、PostMessage?{message}&{type}&{level} 调用失败  格式必须为name=value
        //  Message：3、PostMessage?{message=message}&{type=type}&{level=level} 报错 格式无效
        //  Message：4、PostMessage?message={message}/type={type}/level={level} 报错 格式无效
        /// <summary>
        /// Post传递消息字符串
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "PostMessage?message={message}&type={type}&level={level}", ResponseFormat = WebMessageFormat.Json)]
        ResultMessage PostMessage(string message, string type, string level);

        /// <summary>
        /// Post方式传递实体
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ResultMessage PostEntity(MessageEntity message);

        /// <summary>
        /// Post方式传递单一字符串
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "PostMMM/{message=message}/", BodyStyle = WebMessageBodyStyle.Bare)]
        string PostMMM(string message);

        [OperationContract]
        [WebGet(UriTemplate = "Fingerprint", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ResultMessage FingerprintGet();
    }

    /// <summary>
    /// 消息实体
    /// </summary>
    public class MessageEntity
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public string level { get; set; }
    }

    /// <summary>
    /// 返回消息
    /// </summary>
    public class ResultMessage
    {
        /// <summary>
        /// 消息代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 消息信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 消息数据
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// 创建消息
        /// </summary>
        /// <returns></returns>
        public static ResultMessage Create()
        {
            ResultMessage message = new ResultMessage();

            message.Code = "201";
            //message.Data = DateTime.Now.ToString();
            message.Message = "调用成功";

            return message;
        }

    }
}
