/// <summary>
/// เก็บข้อมูล Message ของระบบ
/// </summary>
public class Cls_HC_Message
{
    //การตั้งชื่อ ขอให้ใช้ Format ดังนี้ ControllerName_Status
    // เช่น
    //public static string Inform_Save_Success = "บันทึกข้อมูลเรียบร้อย";
    //public static string Inform_Save_Error = "บันทึกข้อมูลไม่สำเร็จ";

    // Inform Controller
    public static string Inform_Save_Success = "บันทึกข้อมูลเรียบร้อย";
    public static string Inform_Save_Error = "บันทึกข้อมูลไม่สำเร็จ";

    // Receive Controller
    public static string Receive_Save_Success = "บันทึกข้อมูลเรียบร้อย";
    public static string Receive_Save_Error = "บันทึกข้อมูลไม่สำเร็จ";
    public static string Admin
    {
        get
        {
            return "Administrator";
        }
    }

    public static string SendMail_Success = "ส่ง E-Mail เรียบร้อยแล้ว";
    public static string SendMail_Filed = "เกิดข้อผิดพลาดในการส่ง E-Mail";
}
