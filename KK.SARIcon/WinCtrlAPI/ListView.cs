using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace KK.SARIcon.WinCtrlAPI.ListView
{
    /// <summary>
    /// Specifies or receives the attributes of a list-view item. This structure has been updated to support a new mask value (LVIF_INDENT) that enables item indenting. This structure supersedes the LV_ITEM structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct LVITEM
    {
        /// <summary>
        /// Set of flags that specify which members of this structure contain data to be set or which members are being requested. This member can have one or more of the following flags set:
        /// LVIF_COLFMT,LVIF_COLUMNS,LVIF_DI_SETITEM,LVIF_GROUPID,LVIF_IMAGE,LVIF_INDENT,LVIF_NORECOMPUTE,LVIF_PARAM,LVIF_STATE,LVIF_TEXT
        /// 
        /// </summary>
        public UInt32 mask;
        /// <summary>
        /// Zero-based index of the item to which this structure refers.
        /// </summary>
        public Int32 iItem;
        public Int32 iSubItem;
        public UInt32 state;
        public UInt32 stateMask;
        /// <summary>
        /// If the structure specifies item attributes, pszText is a pointer to a null-terminated string containing the item text. When responding to an LVN_GETDISPINFO notification, be sure that this pointer remains valid until after the next notification has been received.
        /// </summary>
        public IntPtr pszText;
        /// <summary>
        /// Number of TCHARs in the buffer pointed to by pszText, including the terminating NULL.
        /// This member is only used when the structure receives item attributes. It is ignored when the structure specifies item attributes. For example, cchTextMax is ignored during LVM_SETITEM and LVM_INSERTITEM. It is read-only during LVN_GETDISPINFO and other LVN_ notifications.
        /// </summary>
        public Int32 cchTextMax;
        public Int32 iImage;
        /// <summary>
        /// Value specific to the item. If you use the LVM_SORTITEMS message, the list-view control passes this value to the application-defined comparison function. You can also use the LVM_FINDITEM message to search a list-view control for an item with a specified lParam value.
        /// </summary>
        public IntPtr lParam;

        /// <summary>
        /// Version 4.70. Number of image widths to indent the item. A single indentation equals the width of an item image. Therefore, the value 1 indents the item by the width of one image, the value 2 indents by two images, and so on. Note that this field is supported only for items. Attempting to set subitem indentation will cause the calling function to fail.
        /// </summary>
        public Int32 iIndent;
        public Int32 iGroupId;
        public Int32 cColumns;
        public IntPtr puColumns;
        public IntPtr piColFmt;
        public int iGroup;

    }
}
