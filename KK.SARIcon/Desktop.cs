using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
namespace KK.SARIcon
{
    /// <summary>
    /// 桌面
    /// </summary>
    public class Desktop
    {
        /// <summary>桌面窗口句柄</summary>
        private IntPtr m_DesktopPtr = IntPtr.Zero;
        /// <summary>ListView控件句柄</summary>
        private IntPtr m_ListViewPtr = IntPtr.Zero;
        /// <summary>桌面ListView所属的进程ID</summary>
        private Int32 m_ProcessID = 0;

        #region 属性

        /// <summary>
        /// 获取桌面句柄
        /// </summary>
        public IntPtr DesktopPtr
        {
            get
            {
                return m_DesktopPtr;
            }
        }
        /// <summary>
        /// 获取桌面进程ID
        /// </summary>
        public Int32 DesktopProcessID
        {
            get
            {
                return m_ProcessID;
            }
        }
        /// <summary>
        /// 获取桌面ListView控件句柄
        /// </summary>
        public IntPtr DesktopListViewPtr
        {
            get
            {
                return m_ListViewPtr;
            }
        }


        #endregion

        #region 构造

        /// <summary>
        /// 通过指定的句柄构造
        /// </summary>
        /// <param name="hWnd"></param>
        public Desktop(IntPtr hWnd)
        {
            this.m_DesktopPtr = hWnd;
            if (hWnd != IntPtr.Zero)
            {
                // 根据桌面句柄获取桌面ListView控件句柄
                m_ListViewPtr = API.GetWindow(m_DesktopPtr, API.GetWindowTypeEnum.GW_CHILD);
                m_ListViewPtr = API.GetWindow(m_ListViewPtr, API.GetWindowTypeEnum.GW_CHILD);

                // 根据ListView控件句柄获取其所在的进程的ID
                API.GetWindowThreadProcessId(m_ListViewPtr, out m_ProcessID);
            }
        }

        /// <summary>
        /// 自动监测桌面句柄构造一个新的桌面操作
        /// </summary>
        public Desktop()
        {
            // 先尝试通过Progman来查找，如果找不到SysListView32，就通过WorkerW来找
            m_DesktopPtr = API.FindWindow("Progman", null);
            if (m_DesktopPtr != IntPtr.Zero)
            {
                m_ListViewPtr = API.GetWindow(m_DesktopPtr, API.GetWindowTypeEnum.GW_CHILD);
                m_ListViewPtr = API.GetWindow(m_ListViewPtr, API.GetWindowTypeEnum.GW_CHILD);
            }

            // 尝试通过WorkerW来查找
            IntPtr workerWPtr = IntPtr.Zero;
            while (m_ListViewPtr == IntPtr.Zero)
            {
                workerWPtr = API.FindWindowEx(IntPtr.Zero, workerWPtr, "WorkerW", null);
                if (workerWPtr == IntPtr.Zero) break;
                m_DesktopPtr = workerWPtr;
                IntPtr shellDLLPtr = API.FindWindowEx(workerWPtr, IntPtr.Zero, "SHELLDLL_DefView", null);
                if (shellDLLPtr == IntPtr.Zero) continue;
                m_ListViewPtr = API.FindWindowEx(shellDLLPtr, IntPtr.Zero, "SysListView32", null);
            }

            if (m_ListViewPtr == IntPtr.Zero)
            {
                throw new ApplicationException("未能找到桌面句柄，无法完成初始化。");
            }

            // 根据ListView控件句柄获取其所在的进程的ID
            API.GetWindowThreadProcessId(m_ListViewPtr, out m_ProcessID);
        }

        #endregion


        /// <summary>
        /// 获取桌面上的选择的数量
        /// </summary>
        /// <returns></returns>
        public Int32 GetSelectedCount()
        {
            Int32 count = API.SendMessage(m_ListViewPtr, SystemDefinedMessages.CommonControl.LVM_GETSELECTEDCOUNT, 0, 0);
            return count;
        }

        /// <summary>
        /// 获取桌面上的图标数量
        /// </summary>
        /// <returns></returns>
        public Int32 GetItemsCount()
        {
            Int32 count = API.SendMessage(m_ListViewPtr, SystemDefinedMessages.CommonControl.LVM_GETITEMCOUNT, 0, 0);
            return count;
        }

        /// <summary>
        /// 获取制定图标的文本
        /// </summary>
        /// <returns></returns>
        public String GetItemText(Int32 itemIndex)
        {
            String result = String.Empty;
            UInt32 pref = 0;
            WinCtrlAPI.ListView.LVITEM _lv = new WinCtrlAPI.ListView.LVITEM();

            // 打开桌面进程
            IntPtr proPtr = API.OpenProcess(API.ProcessSecurityAccessRight.PROCESS_VM_OPERATION | API.ProcessSecurityAccessRight.PROCESS_VM_READ | API.ProcessSecurityAccessRight.PROCESS_VM_WRITE, false, (UInt32)m_ProcessID);

            // 在桌面进程给文本分配一段内存
            IntPtr textPtr = API.VirtualAllocEx(proPtr, IntPtr.Zero, 260, API.MemoryAllocationType.MEM_COMMIT, API.MemoryProtectionConstants.PAGE_EXECUTE_READWRITE);
            _lv.pszText = textPtr;

            _lv.mask = SystemDefinedMessages.CommonControl.LVIF_TEXT;
            _lv.iSubItem = 0;
            _lv.cchTextMax = 260;

            // 给LVItem分配一段内存
            IntPtr lvPtr = API.VirtualAllocEx(proPtr, IntPtr.Zero, Marshal.SizeOf(_lv), API.MemoryAllocationType.MEM_COMMIT, API.MemoryProtectionConstants.PAGE_EXECUTE_READWRITE);

            // 在当前进程分配一段内存，将ListView对象转化为结构体写入当前进程内存，
            IntPtr tmpPtr = Marshal.AllocHGlobal(Marshal.SizeOf(_lv));
            Marshal.StructureToPtr(_lv, tmpPtr, false);

            // 将当前进程内存中的结构体写入到桌面进程内存中。
            Boolean writeResult = API.WriteProcessMemory(proPtr, lvPtr, tmpPtr, Marshal.SizeOf(_lv), ref pref);
            if (!writeResult)
                throw new ApplicationException("写入内存错误：" + API.GetLastError().ToString());

            Marshal.FreeHGlobal(tmpPtr);

            // 发送消息，接收文本
            Int32 msgResult = API.SendMessage(this.m_ListViewPtr, SystemDefinedMessages.CommonControl.LVM_GETITEMTEXT, itemIndex, lvPtr);
            if (msgResult == 0)
                throw new ApplicationException(API.GetLastError().ToString());

            // 在本程序分配一段内存空间，将桌面进程的内存读取到本进程，然后在转换为文本。
            IntPtr tmpPtr2 = Marshal.AllocHGlobal(260);
            Boolean readResult = API.ReadProcessMemory(proPtr, textPtr, tmpPtr2, 260, ref pref);
            if (readResult)
            {
                result = Marshal.PtrToStringAuto(tmpPtr2);
            }
            Marshal.FreeHGlobal(tmpPtr2);

            // 释放桌面进程中开辟的内存
            API.VirtualFreeEx(proPtr, lvPtr, 0, API.MemoryFreeType.MEM_RELEASE);
            API.VirtualFreeEx(proPtr, textPtr, 0, API.MemoryFreeType.MEM_RELEASE);

            // 关闭桌面进程
            API.CloseHandle(proPtr);

            return result;
        }

        /// <summary>
        /// 获取指定图标的坐标
        /// </summary>
        /// <param name="itemIndex"></param>
        /// <returns></returns>
        public System.Drawing.Point GetItemLocation(Int32 itemIndex)
        {
            System.Drawing.Point location = new System.Drawing.Point();
            // 打开进程
            IntPtr proPtr = API.OpenProcess(API.ProcessSecurityAccessRight.PROCESS_VM_OPERATION | API.ProcessSecurityAccessRight.PROCESS_VM_READ | API.ProcessSecurityAccessRight.PROCESS_VM_WRITE, false, (UInt32)m_ProcessID);

            // 在进程中分配内存空间，用来存储坐标数据
            IntPtr locationPtr = IntPtr.Zero;
            locationPtr = API.VirtualAllocEx(proPtr, IntPtr.Zero, Marshal.SizeOf(location), API.MemoryAllocationType.MEM_COMMIT, API.MemoryProtectionConstants.PAGE_EXECUTE_READWRITE);

            // 发送系统消息，将图标坐标数据写入分配的内存中
            Int32 msgResult = API.SendMessage(m_ListViewPtr, SystemDefinedMessages.CommonControl.LVM_GETITEMPOSITION, itemIndex, locationPtr);

            if (msgResult == 0)
                throw new ApplicationException("发送消息执行失败。返回结果：0");

            // 在当前进程中开辟一段非托管内存空间，并把结构体写入到非托管内存中。
            IntPtr tmpPtr = Marshal.AllocHGlobal(Marshal.SizeOf(location));
            Marshal.StructureToPtr(location, tmpPtr, false);

            // 将桌面进程内存数据读取到开辟的内存空间中
            UInt32 pref = 0;
            Boolean readResult = API.ReadProcessMemory(proPtr, locationPtr, tmpPtr, Marshal.SizeOf(location), ref pref);
            if (!readResult)
                throw new ApplicationException("读取内存失败");

            // 将内存中的结构体转化出来
            System.Drawing.Point result = (System.Drawing.Point)Marshal.PtrToStructure(tmpPtr, typeof(System.Drawing.Point));

            // 释放内存空间
            Marshal.FreeHGlobal(tmpPtr);
            API.VirtualFreeEx(proPtr, locationPtr, 0, API.MemoryFreeType.MEM_RELEASE);

            // 关闭进程
            API.CloseHandle(proPtr);

            return result;
        }

        /// <summary>
        /// 设置图标坐标
        /// </summary>
        /// <param name="itemIndex"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public Boolean SetItemLocation(Int32 itemIndex, System.Drawing.Point location)
        {
            API.SendMessage(m_ListViewPtr, SystemDefinedMessages.CommonControl.LVM_SETITEMPOSITION, itemIndex, MakeLParam(location.X, location.Y));
            return true;
        }

        public IntPtr MakeLParam(int wLow, int wHigh)
        {
            return (IntPtr)(((short)wHigh << 16) | (wLow & 0xffff));
        }

        /// <summary>
        /// 选择指定项
        /// </summary>
        /// <param name="itemIndex"></param>
        public void SelectItem(Int32 itemIndex)
        {
            WinCtrlAPI.ListView.LVITEM listView = new WinCtrlAPI.ListView.LVITEM();
            //listView.iItem = itemIndex;
            listView.mask = SystemDefinedMessages.CommonControl.LVIF_STATE;
            listView.stateMask = SystemDefinedMessages.CommonControl.LVIS_FOCUSED | SystemDefinedMessages.CommonControl.LVIS_SELECTED;
            listView.state = SystemDefinedMessages.CommonControl.LVIS_FOCUSED | SystemDefinedMessages.CommonControl.LVIS_SELECTED;
            listView.cchTextMax = 0;
            listView.iImage = 0;

            IntPtr listViewPtr = Marshal.AllocHGlobal(Marshal.SizeOf(listView));
            Marshal.StructureToPtr(listView, listViewPtr, false);
            Int32 re = API.SendMessage(m_ListViewPtr, SystemDefinedMessages.CommonControl.LVM_SETITEMSTATE, itemIndex, listViewPtr.ToInt32());
            Marshal.FreeHGlobal(listViewPtr);
        }

        /// <summary>
        /// 获取默认桌面句柄
        /// </summary>
        /// <returns></returns>
        public static IntPtr GetDefaultIntptr()
        {
            IntPtr hwnd = API.FindWindow("ProgMan", null);
            return hwnd;
        }

    }
}
