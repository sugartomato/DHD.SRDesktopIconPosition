using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
namespace KK.SARIcon
{
    public class API
    {
        [DllImport("User32.dll")]
        public static extern Int32 SendMessage(IntPtr hwnd, Int32 Msg, Int32 wParam, Int32 lParam);
        [DllImport("User32.dll")]
        public static extern Int32 SendMessage(IntPtr hwnd, Int32 Msg, Int32 wParam, System.Text.StringBuilder lParam);

        [DllImport("User32.dll")]
        public static extern Int32 SendMessage(IntPtr hwnd, Int32 Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("User32.dll")]
        public static extern Int32 SendMessage(IntPtr hwnd, Int32 Msg, Int32 wParam, IntPtr lParam);

        [DllImport("User32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, GetWindowTypeEnum uCmd);
        [DllImport("User32.dll")]
        public static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out Int32 lpdwProcessId);
        [DllImport("kernel32.dll")]
        public extern static IntPtr OpenProcess(UInt32 dwDesiredAccess, Boolean bInheritHandle, UInt32 dwProcessId);

        [DllImport("Kernel32.dll")]
        public extern static Boolean ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, Int32 nSize, ref UInt32 lpNumberOfBytesRead);
        [DllImport("Kernel32.dll")]
        public extern static Boolean ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, StringBuilder lpBuffer, Int32 nSize, ref UInt32 lpNumberOfBytesRead);
        [DllImport("Kernel32.dll")]
        public extern static Boolean WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, Int32 nSize, ref UInt32 lpNumberOfBytesWritten);
        [DllImport("Kernel32.dll")]
        public static extern Int32 GetLastError();
        [DllImport("Kernel32.dll")]
        public extern static IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, Int32 dwSize, UInt32 flAllocationType, UInt32 flProtect);
        [DllImport("Kernel32.dll")]
        public extern static IntPtr VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, Int32 dwSize, Int32 dwFreeType);
        [DllImport("Kernel32.dll")]
        public extern static Boolean CloseHandle(IntPtr hObject);
        [DllImport("User32.dll")]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);
        [DllImport("User32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParnet, IntPtr hwndChildAfter, String lpszClass, String lpszWindow);


        public const Int32 LVM_FIRST = 0x1000;
        public const Int32 LVM_GETITEMCOUNT = LVM_FIRST + 4;

        public enum GetWindowTypeEnum
        {
            /// <summary>
            /// The retrieved handle identifies the child window at the top of the Z order, if the specified window is a parent window; otherwise, the retrieved handle is NULL. The function examines only child windows of the specified window. It does not examine descendant windows.
            /// </summary>
            GW_CHILD = 5,
            /// <summary>
            /// The retrieved handle identifies the enabled popup window owned by the specified window (the search uses the first such window found using GW_HWNDNEXT); otherwise, if there are no enabled popup windows, the retrieved handle is that of the specified window.
            /// </summary>
            GW_ENABLEDPOPUP = 6,
            /// <summary>
            /// The retrieved handle identifies the window of the same type that is highest in the Z order.
            /// If the specified window is a topmost window, the handle identifies a topmost window. If the specified window is a top-level window, the handle identifies a top-level window. If the specified window is a child window, the handle identifies a sibling window.
            /// </summary>
            GW_HWNDFIRST = 0,
            /// <summary>
            /// The retrieved handle identifies the window of the same type that is lowest in the Z order.
            /// If the specified window is a topmost window, the handle identifies a topmost window. If the specified window is a top-level window, the handle identifies a top-level window. If the specified window is a child window, the handle identifies a sibling window.
            /// </summary>
            GW_HWNDLAST = 1,
            /// <summary>
            /// The retrieved handle identifies the window below the specified window in the Z order.
            /// If the specified window is a topmost window, the handle identifies a topmost window. If the specified window is a top-level window, the handle identifies a top-level window. If the specified window is a child window, the handle identifies a sibling window.
            /// </summary>
            GW_HWNDNEXT = 2,
            /// <summary>
            /// The retrieved handle identifies the window above the specified window in the Z order.
            /// If the specified window is a topmost window, the handle identifies a topmost window. If the specified window is a top-level window, the handle identifies a top-level window. If the specified window is a child window, the handle identifies a sibling window.
            /// </summary>
            GW_HWNDPREV = 3,
            /// <summary>
            /// The retrieved handle identifies the specified window's owner window, if any. For more information, see Owned Windows.
            /// </summary>
            GW_OWNER = 4
        }

        public class ProcessSecurityAccessRight
        {
            /// <summary>
            /// Required to create a process.
            /// </summary>
            public const UInt32 PROCESS_CREATE_PROCESS = 0x0080;
            /// <summary>
            /// Required to create a thread.
            /// </summary>
            public const UInt32 PROCESS_CREATE_THREAD = 0x0002;
            /// <summary>
            /// Required to duplicate a handle using DuplicateHandle.
            /// </summary>
            public const UInt32 PROCESS_DUP_HANDLE = 0x0040;
            /// <summary>
            /// Required to retrieve certain information about a process, such as its token, exit code, and priority class (see OpenProcessToken).
            /// </summary>
            public const UInt32 PROCESS_QUERY_INFORMATION = 0x0400;
            public const UInt32 PROCESS_QUERY_LIMITED_INFORMATION = 0x1000;
            public const UInt32 PROCESS_SET_INFORMATION = 0x0200;
            /// <summary>
            /// Required to set memory limits using SetProcessWorkingSetSize.
            /// </summary>
            public const UInt32 PROCESS_SET_QUOTA = 0x0100;
            /// <summary>
            /// Required to suspend or resume a process.
            /// </summary>
            public const UInt32 PROCESS_SUSPEND_RESUME = 0x0800;
            /// <summary>
            /// Required to terminate a process using TerminateProcess.
            /// </summary>
            public const UInt32 PROCESS_TERMINATE = 0x0001;
            /// <summary>
            /// Required to perform an operation on the address space of a process (see VirtualProtectEx and WriteProcessMemory).
            /// </summary>
            public const UInt32 PROCESS_VM_OPERATION = 0x0008;
            /// <summary>
            /// Required to read memory in a process using ReadProcessMemory.
            /// </summary>
            public const UInt32 PROCESS_VM_READ = 0x0010;
            /// <summary>
            /// Required to write to memory in a process using WriteProcessMemory.
            /// </summary>
            public const UInt32 PROCESS_VM_WRITE = 0x0020;
            /// <summary>
            /// Required to wait for the process to terminate using the wait functions.
            /// </summary>
            public const UInt64 SYNCHRONIZE = 0x00100000L;
        }

        public class MemoryAllocationType
        {
            /// <summary>
            /// Allocates memory charges (from the overall size of memory and the paging files on disk) for the specified reserved memory pages. The function also guarantees that when the caller later initially accesses the memory, the contents will be zero. Actual physical pages are not allocated unless/until the virtual addresses are actually accessed.
            /// To reserve and commit pages in one step, call VirtualAllocEx with MEM_COMMIT | MEM_RESERVE.
            /// Attempting to commit a specific address range by specifying MEM_COMMIT without MEM_RESERVE and a non-NULL lpAddress fails unless the entire range has already been reserved. The resulting error code is ERROR_INVALID_ADDRESS.
            /// An attempt to commit a page that is already committed does not cause the function to fail. This means that you can commit pages without first determining the current commitment state of each page.
            /// If lpAddress specifies an address within an enclave, flAllocationType must be MEM_COMMIT.
            /// </summary>
            public const Int32 MEM_COMMIT = 0x00001000;

            /// <summary>
            /// Reserves a range of the process's virtual address space without allocating any actual physical storage in memory or in the paging file on disk.
            /// You commit reserved pages by calling VirtualAllocEx again with MEM_COMMIT. To reserve and commit pages in one step, call VirtualAllocEx with MEM_COMMIT | MEM_RESERVE.
            /// Other memory allocation functions, such as malloc and LocalAlloc, cannot use reserved memory until it has been released.
            /// </summary>
            public const Int32 MEM_RESERVE = 0x00002000;

            /// <summary>
            /// Indicates that data in the memory range specified by lpAddress and dwSize is no longer of interest. The pages should not be read from or written to the paging file. However, the memory block will be used again later, so it should not be decommitted. This value cannot be used with any other value.
            /// Using this value does not guarantee that the range operated on with MEM_RESET will contain zeros. If you want the range to contain zeros, decommit the memory and then recommit it.
            /// When you use MEM_RESET, the VirtualAllocEx function ignores the value of fProtect. However, you must still set fProtect to a valid protection value, such as PAGE_NOACCESS.
            /// VirtualAllocEx returns an error if you use MEM_RESET and the range of memory is mapped to a file. A shared view is only acceptable if it is mapped to a paging file.
            /// </summary>
            public const Int32 MEM_RESET = 0x00080000;

            /// <summary>
            /// MEM_RESET_UNDO should only be called on an address range to which MEM_RESET was successfully applied earlier. It indicates that the data in the specified memory range specified by lpAddress and dwSize is of interest to the caller and attempts to reverse the effects of MEM_RESET. If the function succeeds, that means all data in the specified address range is intact. If the function fails, at least some of the data in the address range has been replaced with zeroes.
            /// This value cannot be used with any other value. If MEM_RESET_UNDO is called on an address range which was not MEM_RESET earlier, the behavior is undefined. When you specify MEM_RESET, the VirtualAllocEx function ignores the value of flProtect. However, you must still set flProtect to a valid protection value, such as PAGE_NOACCESS.
            /// Windows Server 2008 R2, Windows 7, Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP:  The MEM_RESET_UNDO flag is not supported until Windows 8 and Windows Server 2012.
            /// </summary>
            public const Int32 MEM_RESET_UNDO = 0x1000000;

            /// <summary>
            /// Allocates memory using large page support.
            /// The size and alignment must be a multiple of the large-page minimum. To obtain this value, use the GetLargePageMinimum function.
            /// If you specify this value, you must also specify MEM_RESERVE and MEM_COMMIT.
            /// </summary>
            public const Int32 MEM_LARGE_PAGES = 0x20000000;

            /// <summary>
            /// Reserves an address range that can be used to map Address Windowing Extensions (AWE) pages.
            /// This value must be used with MEM_RESERVE and no other values.
            /// </summary>
            public const Int32 MEM_PHYSICAL = 0x00400000;

            /// <summary>
            /// Allocates memory at the highest possible address. This can be slower than regular allocations, especially when there are many allocations.
            /// </summary>
            public const Int32 MEM_TOP_DOWN = 0x00100000;

        }

        public class MemoryProtectionConstants
        {
            /// <summary>
            /// Enables execute access to the committed region of pages. An attempt to write to the committed region results in an access violation.
            /// This flag is not supported by the CreateFileMapping function.
            /// </summary>
            public const Int32 PAGE_EXECUTE = 0x10;
            public const Int32 PAGE_EXECUTE_READ = 0x20;
            public const Int32 PAGE_EXECUTE_READWRITE = 0x40;
            public const Int32 PAGE_EXECUTE_WRITECOPY = 0x80;
            public const Int32 PAGE_NOACCESS = 0x01;
            public const Int32 PAGE_READONLY = 0x02;
            public const Int32 PAGE_READWRITE = 0x04;
            public const Int32 PAGE_WRITECOPY = 0x08;
            public const Int32 PAGE_TARGETS_INVALID = 0x40000000;
            public const Int32 PAGE_TARGETS_NO_UPDATE = 0x40000000;
        }

        public class MemoryFreeType
        {
            /// <summary>
            /// Decommits the specified region of committed pages. After the operation, the pages are in the reserved state.
            /// The function does not fail if you attempt to decommit an uncommitted page. This means that you can decommit a range of pages without first determining their current commitment state.
            /// Do not use this value with MEM_RELEASE.
            /// The MEM_DECOMMIT value is not supported when the lpAddress parameter provides the base address for an enclave.
            /// </summary>
            public const Int32 MEM_DECOMMIT = 0x4000;
            /// <summary>
            /// Releases the specified region of pages. After the operation, the pages are in the free state.
            /// If you specify this value, dwSize must be 0 (zero), and lpAddress must point to the base address returned by the VirtualAllocEx function when the region is reserved. The function fails if either of these conditions is not met.
            /// If any pages in the region are committed currently, the function first decommits, and then releases them.
            /// The function does not fail if you attempt to release pages that are in different states, some reserved and some committed. This means that you can release a range of pages without first determining the current commitment state.
            /// Do not use this value with MEM_DECOMMIT.
            /// </summary>
            public const Int32 MEM_RELEASE = 0x8000;
        }

    }
}
