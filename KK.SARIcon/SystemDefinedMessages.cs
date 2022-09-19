using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KK.SARIcon
{
    /// <summary>
    /// 系统定义的消息
    /// </summary>
    /// <remarks>
    /// The system sends or posts a system-defined message when it communicates with an application. It uses these messages to control the operations of applications and to provide input and other information for applications to process. An application can also send or post system-defined messages. Applications generally use these messages to control the operation of control windows created by using preregistered window classes.
    /// Each system-defined message has a unique message identifier and a corresponding symbolic constant (defined in the software development kit (SDK) header files) that states the purpose of the message. For example, the WM_PAINT constant requests that a window paint its contents.
    /// Symbolic constants specify the category to which system-defined messages belong. The prefix of the constant identifies the type of window that can interpret and process the message. Following are the prefixes and their related message categories.
    /// 
    /// </remarks>
    /// <link>
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/ms644927(v=vs.85).aspx#app_defined
    /// </link>
    public class SystemDefinedMessages
    {


        #region Mouse Input Notifications

        /// <summary>
        /// Posted when the user presses the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        public const Int32 WM_LBUTTONDOWN = 0x0201;
        /// <summary>
        /// Posted when the user releases the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// </summary>
        public const Int32 WM_LBUTTONUP = 0x0202;
        /// <summary>
        /// Posted to a window when the cursor moves. If the mouse is not captured, the message is posted to the window that contains the cursor. Otherwise, the message is posted to the window that has captured the mouse.
        /// Parameters
        ///     wParam
        ///         Indicates whether various virtual keys are down. This parameter can be one or more of the following values.
        ///     lParam
        ///         The low-order word specifies the x-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area.
        ///         The high-order word specifies the y-coordinate of the cursor. The coordinate is relative to the upper-left corner of the client area.
        /// Return value
        ///     Type: LRESULT
        ///     The return value is TRUE if the text is set. It is FALSE (for an edit control), LB_ERRSPACE (for a list box), or CB_ERRSPACE (for a combo box) if insufficient space is available to set the text in the edit control. It is CB_ERR if this message is sent to a combo box without an edit control.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public const Int32 WM_MOUSEMOVE = 0x0200;



        #endregion


        #region Scroll Bar Notifications



        #endregion

        #region Window Messages

        /// <summary>
        /// Sets the text of a window.
        /// 设置窗口文本
        /// Parameters
        ///     wParam
        ///         This parameter is not used.
        ///     lParam
        ///         A pointer to a null-terminated string that is the window text.
        /// Return value
        ///     Type: LRESULT
        ///     The return value is TRUE if the text is set. It is FALSE (for an edit control), LB_ERRSPACE (for a list box), or CB_ERRSPACE (for a combo box) if insufficient space is available to set the text in the edit control. It is CB_ERR if this message is sent to a combo box without an edit control.
        /// </summary>
        /// <remarks>
        /// The DefWindowProc function sets and displays the window text. For an edit control, the text is the contents of the edit control. For a combo box, the text is the contents of the edit-control portion of the combo box. For a button, the text is the button name. For other windows, the text is the window title.
        /// This message does not change the current selection in the list box of a combo box. An application should use the CB_SELECTSTRING message to select the item in a list box that matches the text in the edit control.
        /// </remarks>
        public const Int32 WM_SETTEXT = 0x000C;

        /// <summary>
        /// 获取窗口文本
        /// Copies the text that corresponds to a window into a buffer provided by the caller.
        /// Parameters
        ///     wParam
        ///         The maximum number of characters to be copied, including the terminating null character.
        ///         ANSI applications may have the string in the buffer reduced in size (to a minimum of half that of the wParam value) due to conversion from ANSI to Unicode.
        ///     lParam
        ///         A pointer to the buffer that is to receive the text.
        /// Return value
        ///     Type: LRESULT
        ///     The return value is the number of characters copied, not including the terminating null character.
        /// </summary>
        /// <remarks>
        /// The DefWindowProc function copies the text associated with the window into the specified buffer and returns the number of characters copied. Note, for non-text static controls this gives you the text with which the control was originally created, that is, the ID number. However, it gives you the ID of the non-text static control as originally created. That is, if you subsequently used a STM_SETIMAGE to change it the original ID would still be returned.
        /// For an edit control, the text to be copied is the content of the edit control. For a combo box, the text is the content of the edit control (or static-text) portion of the combo box. For a button, the text is the button name. For other windows, the text is the window title. To copy the text of an item in a list box, an application can use the LB_GETTEXT message.
        /// When the WM_GETTEXT message is sent to a static control with the SS_ICON style, a handle to the icon will be returned in the first four bytes of the buffer pointed to by lParam. This is true only if the WM_SETTEXT message has been used to set the icon.
        /// Rich Edit: If the text to be copied exceeds 64K, use either the EM_STREAMOUT or EM_GETSELTEXT message.
        /// Sending a WM_GETTEXT message to a non-text static control, such as a static bitmap or static icon control, does not return a string value. Instead, it returns zero. In addition, in early versions of Windows, applications could send a WM_GETTEXT message to a non-text static control to retrieve the control's ID. To retrieve a control's ID, applications can use GetWindowLong passing GWL_ID as the index value or GetWindowLongPtr using GWLP_ID.
        /// </remarks>
        public const Int32 WM_GETTEXT = 0x000D;

        /// <summary>
        /// 获取窗口文本长度
        /// Determines the length, in characters, of the text associated with a window.
        /// Parameters
        ///     wParam
        ///         This parameter is not used and must be zero.
        ///     lParam
        ///         This parameter is not used and must be zero.
        /// Return value
        ///     Type: LRESULT
        ///     The return value is the length of the text in characters, not including the terminating null character.
        /// </summary>
        /// <remarks>
        /// For an edit control, the text to be copied is the content of the edit control. For a combo box, the text is the content of the edit control (or static-text) portion of the combo box. For a button, the text is the button name. For other windows, the text is the window title. To determine the length of an item in a list box, an application can use the LB_GETTEXTLEN message.
        /// When the WM_GETTEXTLENGTH message is sent, the DefWindowProc function returns the length, in characters, of the text. Under certain conditions, the DefWindowProc function returns a value that is larger than the actual length of the text. This occurs with certain mixtures of ANSI and Unicode, and is due to the system allowing for the possible existence of double-byte character set (DBCS) characters within the text. The return value, however, will always be at least as large as the actual length of the text; you can thus always use it to guide buffer allocation. This behavior can occur when an application uses both ANSI functions and common dialogs, which use Unicode.
        /// To obtain the exact length of the text, use the WM_GETTEXT, LB_GETTEXT, or CB_GETLBTEXT messages, or the GetWindowText function.
        /// Sending a WM_GETTEXTLENGTH message to a non-text static control, such as a static bitmap or static icon controlc, does not return a string value. Instead, it returns zero.
        /// </remarks>
        public const Int32 WM_GETTEXTLENGTH = 0x000E;


        #endregion

        #region 控件消息

        /// <summary>
        /// 系统公共控件消息
        /// </summary>
        public class CommonControl
        {
            #region ListView
            /// <summary>
            ///  ListView messages
            /// </summary>
            public const Int32 LVM_FIRST = 0x1000;
            /// <summary>
            /// Retrieves the number of items in a list-view control. You can send this message explicitly or by using the ListView_GetItemCount macro.
            /// [wParam]
            ///     Must be zero.
            /// [lParam]
            ///     Must be zero.
            /// [Return value]
            ///     Returns the number of items.
            /// </summary>
            public const Int32 LVM_GETITEMCOUNT = LVM_FIRST + 4;

            /// <summary>
            /// Retrieves the text of a list-view item or subitem. You can send this message explicitly or by using the ListView_GetItemText macro.
            /// [wParam]
            ///     Index of the list-view item.
            /// [lParam]
            ///     Pointer to an LVITEM structure. To retrive the item text, set iSubItem to zero. To retrieve the text of a subitem, set iSubItem to the subitem's index. The pszText member points to a buffer that receives the text. The cchTextMax member specifies the number of characters in the buffer.
            /// [Return value]
            ///     If you send this message explicitly, it returns the number of characters in the pszText member of the LVITEM structure.
            /// </summary>
            public const Int32 LVM_GETITEMTEXT = LVM_FIRST + 115;
            public const Int32 LVM_GETITEMTEXTW = LVM_FIRST + 115;
            public const Int32 LVM_GETITEMTEXTA = LVM_FIRST + 45;

            /// <summary>
            /// Retrieves some or all of a list-view item's attributes. You can send this message explicitly or by using the ListView_GetItem macro.
            /// [wParam]
            ///     Must be zero.
            /// [lParam]
            ///     Pointer to an LVITEM structure that specifies the information to retrieve and receives information about the list-view item.
            /// [Return value]
            ///     Returns TRUE if successful, or FALSE otherwise.
            /// [Remarks]   
            ///     When the LVM_GETITEM message is sent, the iItem and iSubItem members identify the item or subitem to retrieve information about and the mask member specifies which attributes to retrieve. For a list of possible values, see the description of the LVITEM structure.
            ///     If the LVIF_TEXT flag is set in the mask member of the LVITEM structure, the pszText member must point to a valid buffer and the cchTextMax member must be set to the number of characters in that buffer. Applications should not assume that the text will necessarily be placed in the specified buffer. The control may instead change the pszText member of the structure to point to the new text, rather than place it in the buffer.
            ///     If the mask member specifies the LVIF_STATE value, the stateMask member must specify the item state bits to retrieve. On output, the state member contains the values of the specified state bits.
            /// </summary>
            public const Int32 LVM_GETITEM = LVM_FIRST + 75;
            public const Int32 LVM_GETITEMW = LVM_FIRST + 75;
            public const Int32 LVM_GETITEMA = LVM_FIRST + 5;
            /// <summary>
            /// Moves an item to a specified position in a list-view control (must be in icon or small icon view). You can send this message explicitly or by using the ListView_SetItemPosition macro.
            /// [wParam]
            ///     Index of the list-view item.
            /// [lParam]
            ///     The LOWORD specifies the new x-position of the item's upper-left corner, in view coordinates. The HIWORD specifies the new y-position of the item's upper-left corner, in view coordinates.
            /// [Return value]
            ///     Returns TRUE if successful, or FALSE otherwise.
            /// [Remarks]
            ///     If the list-view control has the LVS_AUTOARRANGE style, the items in the list-view control are arranged after the position of the item is set.
            ///     On Windows Vista, sending this message to a list-view control with the LVS_AUTOARRANGE style does nothing, and the return value is FALSE.
            /// </summary>
            /// <link>https://msdn.microsoft.com/en-us/library/windows/desktop/bb761192%28v=vs.85%29.aspx?f=255&MSPPError=-2147217396</link>
            public const Int32 LVM_SETITEMPOSITION = LVM_FIRST + 15;

            /// <summary>
            /// Retrieves the position of a list-view item. You can send this message explicitly or by using the ListView_GetItemPosition macro.
            /// [wParam]
            ///     Index of the list-view item.
            /// [lParam]
            ///     Pointer to a POINT structure that receives the position of the item's upper-left corner, in view coordinates.
            /// [Return value]
            ///     Returns TRUE if successful, or FALSE otherwise.
            /// </summary>
            public const Int32 LVM_GETITEMPOSITION = LVM_FIRST + 16;

            /// <summary>
            /// Determines the number of selected items in a list-view control. You can send this message explicitly or by using the ListView_GetSelectedCount macro.
            /// [wParam]
            ///     Must be zero.
            /// [lParam]
            ///     Must be zero.
            /// [Return value]
            ///     Returns the number of selected items.
            /// </summary>
            public const Int32 LVM_GETSELECTEDCOUNT = LVM_FIRST + 50;

            /// <summary>
            /// Changes the state of an item in a list-view control. You can send this message explicitly or by using the ListView_SetItemState macro.
            /// [wParam]
            ///     Index of the list-view item. If this parameter is -1, then the state change is applied to all items..
            /// [lParam]
            ///     Pointer to an LVITEM structure. The stateMask member specifies which state bits to change, and the state member contains the new values for those bits. The other members are ignored.
            /// [Return value]
            ///     If you send this message explicitly, it returns TRUE if successful or FALSE otherwise.
            /// [Remarks]
            ///     An item's state value includes a set of bit flags that indicate the item's state. The state value can also include image list indexes that indicate the item's state image and overlay image.
            /// </summary>
            public const Int32 LVM_SETITEMSTATE = LVM_FIRST + 43;

            #region ListView state MASK

            /// <summary>
            /// The pszText member is valid or must be set.
            /// </summary>
            public const Int32 LVIF_TEXT = 0x0001;
            /// <summary>
            /// The iImage member is valid or must be set.
            /// </summary>
            public const Int32 LVIF_IMAGE = 0x0002;
            /// <summary>
            /// The lParam member is valid or must be set.
            /// </summary>
            public const Int32 LVIF_PARAM = 0x0004;
            /// <summary>
            /// The state member is valid or must be set.
            /// </summary>
            public const Int32 LVIF_STATE = 0x0008;

            #endregion

            #region MyRegion

            public const Int32 LVIS_FOCUSED = 0x0001;
            public const Int32 LVIS_SELECTED = 0x0002;
            public const Int32 LVIS_CUT = 0x0004;
            public const Int32 LVIS_DROPHILITED = 0x0008;
            public const Int32 LVIS_ACTIVATING = 0x0020;
            public const Int32 LVIS_OVERLAYMASK = 0x0F00;
            public const Int32 LVIS_STATEIMAGEMASK = 0xF000;



            #endregion



            #endregion
        }


        #endregion
    }
}
