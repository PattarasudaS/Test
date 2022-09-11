/// <summary>
/// เก็บค่าคงที่ เพื่อใช้ในระบบ Home Care
/// </summary>
public class Cls_HC_Constance
{
    // Code ประเภท Transection 
    public static string HC_Job_Code_Stage_Inform_Receive = "R";
    public static string HC_Job_Code_Stage_WorkSheet = "W";
    
    /// <summary>
    /// HC รับเรื่อง 24 ชม
    /// </summary>
    public static long HC_Main_Process_01 = 1;
    /// <summary>
    /// HC เปิดใบงาน
    /// </summary>
    public static long HC_Main_Process_02 = 2;
    /// <summary>
    /// รออนุมัติ
    /// </summary>
    public static long HC_Main_Process_03 = 3;
    /// <summary>
    /// อนุมัติแล้ว
    /// </summary>
    public static long HC_Main_Process_04 = 4;
    /// <summary>
    /// อยู่ระหว่างดำเนินการ
    /// </summary>
    public static long HC_Main_Process_05 = 5;
    /// <summary>
    /// ซ่อมแล้วเสร็จ
    /// </summary>
    public static long HC_Main_Process_06 = 6;
    /// <summary>
    /// รอแนบเอกสาร
    /// </summary>
    public static long HC_Main_Process_07 = 7;
    /// <summary>
    /// ปิดใบงาน
    /// </summary>
    public static long HC_Main_Process_08 = 8;
    /// <summary>
    /// รอส่งวัสดุ
    /// </summary>
    public static long HC_Main_Process_09 = 9;
    /// <summary>
    /// สถานะ Pending
    /// </summary>
    public static long HC_Main_Process_10 = 10;
    /// <summary>
    /// HC-F5
    /// </summary>
    public static long HC_Main_Process_11 = 11;
    /// <summary>
    /// HC_F9
    /// </summary>
    public static long HC_Main_Process_12 = 12;

    // Menu [กำหนดการมองเห็น Main Menu , Sub Menu]
    public static string HC_Show_Main_Menu = "1";
    public static string HC_Show_Sub_Menu = "1";
    public static string HC_Hidden_Main_Menu = "0";
    public static string HC_Hidden_Sub_Menu = "0";

    // Sub Menu Item [เป็นตัวที่ใช้เทียบว่า เมนูไหน Active อยู่]
    public static int HC_Sub_Menu_1 = 1;
    public static int HC_Sub_Menu_2 = 2;
    public static int HC_Sub_Menu_3 = 3;
    public static int HC_Sub_Menu_4 = 4;
    public static int HC_Sub_Menu_5 = 5;
    public static int HC_Sub_Menu_6 = 6;
    public static int HC_Sub_Menu_7 = 7;

    // Menu for Home Care
    public static string HC_Show_Sub_Menu_HomeCare = "1";
    public static string HC_Hidden_Sub_Menu_HomeCare = "0";

    // Menu For Callcenter
    public static string HC_Show_Sub_Menu_Callcenter = "1";
    public static string HC_Hidden_Sub_Menu_Callcenter = "0";

    // DropDown
    public static string HC_DDL_Project_Authen = "HC_DDl_Project_Authen";
    public static string HC_DDL_Unit = "HC_DDL_Unit";

    // Error
    public static string HC_Error_404 = "404";
    public static string HC_Error_NULL = "";
    public static string HC_Error_404_Message_EN = "Not Found Data";
    public static string HC_Error_404_Message_TH = "ไม่พบข้อมูล";

    public static string HC_Error_500 = "500";

}
