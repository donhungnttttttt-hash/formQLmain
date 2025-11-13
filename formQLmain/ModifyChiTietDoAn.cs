using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formQLmain
{
    class ModifyChiTietDoAn
    {
        SqlDataAdapter _da;
        SqlCommand _cmd;
        //
        public DataTable getAllDoAn()
        {
            DataTable dt = new DataTable();
            string sql = @"SELECT DA.MADOAN,DA.TENDETAI, SV.HOTEN, SV.CHUYENNGANH,SV.MASINHVIEN ,SV.KHOA, DA.GVHD, DA.NAMBAOVE, DA.TOMTAT,TLBC.FILEBC,TLBC.MATAILIEUBC, TLBC.SLIDE,TLBC.LY_LICH,SV.LOP, TK.MATUKHOA,TK.TUKHOA  FROM DOAN DA JOIN SINHVIEN SV ON DA.MASINHVIEN = SV.MASINHVIEN
            JOIN TAILIEUBC TLBC ON DA.MATAILIEUBC=TLBC.MATAILIEUBC JOIN  TUKHOA_DOAN TKDA ON TKDA.MADOAN=DA.MADOAN JOIN TUKHOA TK ON TK.MATUKHOA=TKDA.MATUKHOA  ;";
            using (SqlConnection conn = Connection.getConnection())
            {
                _da = new SqlDataAdapter(sql, conn);
                _da.Fill(dt);
            }
            return dt;
        }
        // Thêm

        
        // =========================cập nhật =====================================
        
    }
}
