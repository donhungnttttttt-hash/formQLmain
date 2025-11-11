using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formQLmain
{
    class ModifyDanhSachDoAn
    {
        SqlDataAdapter _da;
        SqlCommand _cmd;

        // Đọc toàn bộ
        public DataTable getAllDoAn() // thêm dữ liệu này
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM DOAN join TLBC";
            using (SqlConnection conn = Connection.getConnection())
            {
                _da = new SqlDataAdapter(sql, conn);
                _da.Fill(dt);
            }
            return dt;
        }

        // ================Thêm==========================
        public bool insert(DanhSachDoAn da, out string error)
        {
            error = "";
            string sql = @"INSERT INTO DOAN
                           (MADOAN, TENDETAI, MASINHVIEN, GVHD, NAMBAOVE, TOMTAT, MATAILIEUBC)
                           VALUES (@maDA, @tenDeTai, @maSV, @gvhd, @namBaoVe, @tomTat, @maTLBC)";

            SqlConnection conn = Connection.getConnection();
            try
            {
                conn.Open();
                _cmd = new SqlCommand(sql, conn);

                _cmd.Parameters.Add("@maDA", SqlDbType.VarChar).Value = da.MaDoAn;
                _cmd.Parameters.Add("@tenDeTai", SqlDbType.NVarChar).Value = (object)da.TenDeTai ?? DBNull.Value;
                _cmd.Parameters.Add("@maSV", SqlDbType.VarChar).Value = (object)da.MaSinhVien ?? DBNull.Value;
                _cmd.Parameters.Add("@gvhd", SqlDbType.NVarChar).Value = (object)da.GVHD ?? DBNull.Value;

                // DateTime? -> DBNull nếu null
                var pNgay = _cmd.Parameters.Add("@namBaoVe", SqlDbType.Date); // dùng Date (hoặc DateTime)
                pNgay.Value = (object)da.NamBaoVe ?? DBNull.Value;

                _cmd.Parameters.Add("@tomTat", SqlDbType.NVarChar).Value = (object)da.TomTat ?? DBNull.Value;
                _cmd.Parameters.Add("@maTLBC", SqlDbType.VarChar).Value = (object)da.MaTaiLieuBC ?? DBNull.Value;

                _cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) { error = ex.Message; return false; }
            finally { conn.Close(); }
        }

        // Cập nhật (cho phép đổi MADOAN thông qua maDoAnCu)
        public bool update(DanhSachDoAn da, string maDoAnCu, out string error)
        {
            error = "";
            string sql = @"UPDATE DOAN
                           SET MADOAN = @maDA,
                               TENDETAI = @tenDeTai,
                               MASINHVIEN = @maSV,
                               GVHD = @gvhd,
                               NAMBAOVE = @namBaoVe,
                               TOMTAT = @tomTat,
                               MATAILIEUBC = @maTLBC
                           WHERE MADOAN = @maDACu";

            SqlConnection conn = Connection.getConnection();
            try
            {
                conn.Open();
                _cmd = new SqlCommand(sql, conn);

                _cmd.Parameters.Add("@maDACu", SqlDbType.VarChar).Value = maDoAnCu;

                _cmd.Parameters.Add("@maDA", SqlDbType.VarChar).Value = da.MaDoAn;
                _cmd.Parameters.Add("@tenDeTai", SqlDbType.NVarChar).Value = (object)da.TenDeTai ?? DBNull.Value;
                _cmd.Parameters.Add("@maSV", SqlDbType.VarChar).Value = (object)da.MaSinhVien ?? DBNull.Value;
                _cmd.Parameters.Add("@gvhd", SqlDbType.NVarChar).Value = (object)da.GVHD ?? DBNull.Value;

                var pNgay = _cmd.Parameters.Add("@namBaoVe", SqlDbType.Date);
                pNgay.Value = (object)da.NamBaoVe ?? DBNull.Value;

                _cmd.Parameters.Add("@tomTat", SqlDbType.NVarChar).Value = (object)da.TomTat ?? DBNull.Value;
                _cmd.Parameters.Add("@maTLBC", SqlDbType.VarChar).Value = (object)da.MaTaiLieuBC ?? DBNull.Value;

                int affected = _cmd.ExecuteNonQuery();
                return affected > 0;
            }
            catch (Exception ex) { error = ex.Message; return false; }
            finally { conn.Close(); }
        }

        // Xóa
        public bool delete(string maDoAn, out string error)
        {
            error = "";
            string sql = "DELETE FROM DOAN WHERE MADOAN = @maDA";
            SqlConnection conn = Connection.getConnection();
            try
            {
                conn.Open();
                _cmd = new SqlCommand(sql, conn);
                _cmd.Parameters.Add("@maDA", SqlDbType.VarChar).Value = maDoAn;

                int affected = _cmd.ExecuteNonQuery();
                return affected > 0;
            }
            catch (Exception ex) { error = ex.Message; return false; }
            finally { conn.Close(); }
        }

        // (tuỳ chọn) kiểm tra trùng mã
        public bool exists(string maDoAn)
        {
            string sql = "SELECT COUNT(1) FROM DOAN WHERE MADOAN = @maDA";
            using (SqlConnection conn = Connection.getConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@maDA", SqlDbType.VarChar).Value = maDoAn;
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }
        // Tìm theo mọi cột chính + ngày bảo vệ (yyyy-MM-dd)
        public DataTable searchDoAn(string keyword)
        {
            DataTable dt = new DataTable();
            string sql = @"
                SELECT * FROM DOAN
                WHERE MADOAN      LIKE @kw
                   OR TENDETAI    LIKE @kw
                   OR MASINHVIEN  LIKE @kw
                   OR GVHD        LIKE @kw
                   OR TOMTAT      LIKE @kw
                   OR MATAILIEUBC LIKE @kw
                   OR CONVERT(varchar(10), NAMBAOVE, 120) LIKE @kw";

            using (SqlConnection conn = Connection.getConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@kw", SqlDbType.NVarChar).Value = "%" + keyword + "%";
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }
    }
}
