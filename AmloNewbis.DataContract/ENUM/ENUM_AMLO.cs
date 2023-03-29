using System;
using System.Collections.Generic;
using System.Text;

namespace AmloNewbis.DataContract.ENUM
{
    public class ENUM_INFO_SOURCE
    {
        private ENUM_INFO_SOURCE(string value, string code) { Value = value; Code = code; }

        public string Code { get; private set; }
        public string Value { get; private set; }


        public static ENUM_INFO_SOURCE FREEZE { get { return new ENUM_INFO_SOURCE("บุคคลที่ถูกกำหนด (Thailand list/UN Sanction List)", "FREEZE"); } }
        public static ENUM_INFO_SOURCE PEP_OUT { get { return new ENUM_INFO_SOURCE("บุคคลที่มีสถานภาพทางการเมืองต่างประเทศ (PEP_OUT)", "PEP"); } }
        public static ENUM_INFO_SOURCE PEP_IN { get { return new ENUM_INFO_SOURCE("บุคคลที่มีสถานภาพทางการเมืองในประเทศ (PEP_IN)", "PEP"); } }
        public static ENUM_INFO_SOURCE RCA_IN { get { return new ENUM_INFO_SOURCE("บุคคลที่ใกล้ชิดกับบุคคลที่มีสถานภาพทางการเมืองในประเทศ (RCA )", "RCA"); } }
        public static ENUM_INFO_SOURCE RCA_OUT { get { return new ENUM_INFO_SOURCE("บุคคลที่ใกล้ชิดกับบุคคลที่มีสถานภาพทางการเมืองต่างประเทศ (RCA )", "RCA"); } }
        public static ENUM_INFO_SOURCE STR { get { return new ENUM_INFO_SOURCE("บุคคลที่มีการรายงานเป็นธุรกรรมที่มีเหตุอันควรสงสัย (STR)", "STR"); } }
        public static ENUM_INFO_SOURCE HR_08 { get { return new ENUM_INFO_SOURCE("บุคคลที่แจ้งให้ทราบว่ามีความเสี่ยงต่อการฟอกเงิน (HR-08)", "HR-08"); } }
        public static ENUM_INFO_SOURCE HR_02 { get { return new ENUM_INFO_SOURCE("บุคคลที่ถูกยึด/อายัด ทรัพย์สินตามคำสั่งศาล (HR-02)", "HR-02"); } }
    }
    public class ENUM_OTHER_RISK
    {
        private ENUM_OTHER_RISK(string value, string code) { Value = value; Code = code; }

        public string Code { get; private set; }
        public string Value { get; private set; }


        public static ENUM_OTHER_RISK NT { get { return new ENUM_OTHER_RISK("สัญชาติอิหร่าน/เกาหลีเหนือ", "NT"); } }
        public static ENUM_OTHER_RISK ONT { get { return new ENUM_OTHER_RISK("สัญชาติเสี่ยงอื่นๆ (ที่ไม่ใช่อิหร่าน&เกาหลีเหนือ)", "ONT"); } }
        public static ENUM_OTHER_RISK OCC { get { return new ENUM_OTHER_RISK("อาชีพเสี่ยง", "OCC"); } }
        public static ENUM_OTHER_RISK PRD { get { return new ENUM_OTHER_RISK("ผลิตภัณฑ์เสี่ยง", "PRD"); } }
    }
    public class ENUM_VERIFY
    {
        private ENUM_VERIFY(string code, string value) { Value = value; Code = code; }

        public string Code { get; private set; }
        public string Value { get; private set; }


        public static ENUM_VERIFY VERIFY_PRESON_DESC { get { return new ENUM_VERIFY("1", "ใช่บุคคลเดียวกัน"); } }
        public static ENUM_VERIFY VERIFY_NOT_PRESON_DESC { get { return new ENUM_VERIFY("2", "ไม่ใช่บุคคลเดียวกัน"); } }
    }

    public class ENUM_MATRIX
    {
        private ENUM_MATRIX(string code,string value ) { Value = value; Code = code; }

        public string Code { get; private set; }
        public string Value { get; private set; }


        public static ENUM_MATRIX FORBIDDEN { get { return new ENUM_MATRIX("4", "ต้องห้ามทำธุรกรรม"); } }
        public static ENUM_MATRIX HIGH_RISK { get { return new ENUM_MATRIX("3", "ความเสี่ยงสูง"); } }
        public static ENUM_MATRIX MIDDLE_RISK { get { return new ENUM_MATRIX("2", "ความเสี่ยงกลาง"); } }
        public static ENUM_MATRIX LOW_RISK { get { return new ENUM_MATRIX("1", "ความเสี่ยงต่ำ"); } }
    }
    public class ENUM_UNDERWRITING_CONSIDERATION
    {
        private ENUM_UNDERWRITING_CONSIDERATION(string code, string value) { Value = value; Code = code; }

        public string Code { get; private set; }
        public string Value { get; private set; }


        public static ENUM_UNDERWRITING_CONSIDERATION DESC_1 { get { return new ENUM_UNDERWRITING_CONSIDERATION("1", "พบข้อมูลข่าวสาธารณะเกี่ยวกับการกระทำผิดมูลฐาน ตามกฎหมายฟอกเงิน (โปรดะระบุ)"); } }
        public static ENUM_UNDERWRITING_CONSIDERATION DESC_2 { get { return new ENUM_UNDERWRITING_CONSIDERATION("2", "การชำระเบี้ยประกันภัยมีความสอดคล้องกับรายได้"); } }
        public static ENUM_UNDERWRITING_CONSIDERATION DESC_3 { get { return new ENUM_UNDERWRITING_CONSIDERATION("3", "มีข้อมูลเกี่ยวกับการประกอบกิจการของลูกค้าเพื่อทราบแหล่งที่มาของรายได้/มีแหล่งข้อมูลเพิ่มเติมที่น่าเชื่อถือเกี่ยวกับแหล่งที่มาของรายได้ที่นำมาชำระเบี้ยประกันภัย"); } }
        public static ENUM_UNDERWRITING_CONSIDERATION DESC_4 { get { return new ENUM_UNDERWRITING_CONSIDERATION("4", "การแสดงตนในใบคำขอฯ มีลายมือชื่อผู้ทำธุรกรรม"); } }
        public static ENUM_UNDERWRITING_CONSIDERATION DESC_5 { get { return new ENUM_UNDERWRITING_CONSIDERATION("5", "คนไทย – มีสำเนาบัตรประจำตัวประชาชน"); } }
        public static ENUM_UNDERWRITING_CONSIDERATION DESC_6 { get { return new ENUM_UNDERWRITING_CONSIDERATION("6", "คนไม่มีสัญชาติไทย - สำเนาหนังสือเดินทางหรือบัตรประจำตัวที่รัฐบาลหรือหน่วยงานของรัฐเจ้าของสัญชาติออกให้ หรือสำเนาบัตรประจำตัวในเอกสารสำคัญที่รัฐบาลไทยออกให้"); } }
        public static ENUM_UNDERWRITING_CONSIDERATION DESC_7 { get { return new ENUM_UNDERWRITING_CONSIDERATION("7", "มีการดำเนินการระบุและพิสูจน์ทราบตัวตนเป็นไปตามที่กฎหมายกำหนด"); } }
        public static ENUM_UNDERWRITING_CONSIDERATION SUMMARY_DESC_1 { get { return new ENUM_UNDERWRITING_CONSIDERATION("8", "อนุมัติรับประกันภัย"); } }
        public static ENUM_UNDERWRITING_CONSIDERATION SUMMARY_DESC_2 { get { return new ENUM_UNDERWRITING_CONSIDERATION("9", "ไม่อนุมัติรับประกันภัย(โปรดระบุเหตุผล)"); } }

    }
    public class ENUM_APPROVER
    {
        private ENUM_APPROVER(string code, string value) { Value = value; Code = code; }

        public string Code { get; private set; }
        public string Value { get; private set; }


        public static ENUM_APPROVER S { get { return new ENUM_APPROVER("S", "ต้องผ่านการอนุมัติจากผู้บริหารระดับฝ่ายขึ้นไปของหน่วยงานที่มีหน้าที่รับผิดชอบโดยตรง"); } }
        public static ENUM_APPROVER D { get { return new ENUM_APPROVER("D", "ต้องผ่านการอนุมัติจากผู้บริหารระดับฝ่ายขึ้นไปของหน่วยงานที่มีหน้าที่รับผิดชอบโดยตรง"); } }
        public static ENUM_APPROVER B { get { return new ENUM_APPROVER("B", "ต้องผ่านการอนุมัติจากผู้บริหารระดับฝ่ายขึ้นไปของหน่วยงานที่มีหน้าที่รับผิดชอบโดยตรงและต้องผ่านการพิจารณาโดยผู้บริหารสายงานของหน่วยงานที่มีหน้าที่รับผิดชอบโดยตรงด้วย"); } }



    }
}
