using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumLib.Enums
{
    /// <summary>
    /// 錯誤狀態碼
    /// </summary>
    public enum StatusCodeEnum
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,

        /// <summary>
        /// 已存在使用者帳號
        /// </summary>
        UserExist = 0,

        /// <summary>
        /// 使用者帳號不符規範
        /// </summary>
        UserNameInValid = -1,

        /// <summary>
        /// 暱稱不符規範
        /// </summary>
        NicknameInvalid = -2,

        /// <summary>
        /// 密碼不符規範
        /// </summary>
        PwdInvalid = -3,

        /// <summary>
        /// 使用者帳號不存在
        /// </summary>
        UserNotExist = -4,

        /// <summary>
        /// 密碼錯誤
        /// </summary>
        PwdFail = -5,

        /// <summary>
        /// 無 token
        /// </summary>
        TokenNotExist = -6,

        /// <summary>
        /// jwt token 時效過期
        /// </summary>
        TokenExpired = -7,

        /// <summary>
        /// 發文內容不符規範
        /// </summary>
        Postinvalid = -8
    }
}
