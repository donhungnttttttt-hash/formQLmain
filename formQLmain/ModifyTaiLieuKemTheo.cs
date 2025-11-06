using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formQLmain
{
    class ModifyTaiLieuKemTheo
    {
        SqlDataAdapter _da;
        SqlCommand _cmd;

        // Lấy tất cả tài liệu kèm theo
        public DataTable getAllTaiLieu()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM TAILIEUBC";
            using (SqlConnection conn = Connection.getConnection())
            {
                _da = new SqlDataAdapter(sql, conn);
                _da.Fill(dt);
            }
            return dt;
        }

        // Thêm
        public bool insert(TaiLieuKemTheo tl, out string error)
        {
            error = "";
            string sql = @"INSERT INTO TAILIEUBC(MATAILIEUBC, FILEBC, SLIDE, LY_LICH)
                           VALUES(@ma, @file, @slide, @lylich)";
            SqlConnection conn = Connection.getConnection();

            try
            {
                conn.Open();
                _cmd = new SqlCommand(sql, conn);
                _cmd.Parameters.Add("@ma", SqlDbType.VarChar).Value = tl.MaTLBC;
                _cmd.Parameters.Add("@file", SqlDbType.NVarChar).Value = (object)tl.FileBC ?? DBNull.Value;
                _cmd.Parameters.Add("@slide", SqlDbType.NVarChar).Value = (object)tl.Slide ?? DBNull.Value;
                _cmd.Parameters.Add("@lylich", SqlDbType.NVarChar).Value = (object)tl.LyLich ?? DBNull.Value;

                _cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) { error = ex.Message; return false; }
            finally { conn.Close(); }
        }

        // Cập nhật (cho phép đổi MATAILIEUBC qua maCu)
        public bool update(TaiLieuKemTheo tl, string maCu, out string error)
        {
            error = "";
            string sql = @"UPDATE TAILIEUBC
                           SET MATAILIEUBC = @ma,
                               FILEBC = @file,
                               SLIDE = @slide,
                               LY_LICH = @lylich
                           WHERE MATAILIEUBC = @maCu";
            SqlConnection conn = Connection.getConnection();

            try
            {
                conn.Open();
                _cmd = new SqlCommand(sql, conn);
                _cmd.Parameters.Add("@maCu", SqlDbType.VarChar).Value = maCu;

                _cmd.Parameters.Add("@ma", SqlDbType.VarChar).Value = tl.MaTLBC;
                _cmd.Parameters.Add("@file", SqlDbType.NVarChar).Value = (object)tl.FileBC ?? DBNull.Value;
                _cmd.Parameters.Add("@slide", SqlDbType.NVarChar).Value = (object)tl.Slide ?? DBNull.Value;
                _cmd.Parameters.Add("@lylich", SqlDbType.NVarChar).Value = (object)tl.LyLich ?? DBNull.Value;

                int affected = _cmd.ExecuteNonQuery();
                return affected > 0;
            }
            catch (Exception ex) { error = ex.Message; return false; }
            finally { conn.Close(); }
        }

        // Xóa
        public bool delete(string maTLBC, out string error)
        {
            error = "";
            string sql = "DELETE FROM TAILIEUBC WHERE MATAILIEUBC = @ma";
            SqlConnection conn = Connection.getConnection();

            try
            {
                conn.Open();
                _cmd = new SqlCommand(sql, conn);
                _cmd.Parameters.Add("@ma", SqlDbType.VarChar).Value = maTLBC;

                int affected = _cmd.ExecuteNonQuery();
                return affected > 0;
            }
            catch (Exception ex) { error = ex.Message; return false; }
            finally { conn.Close(); }
        }

        // (Tuỳ chọn) kiểm tra trùng mã
        public bool exists(string maTLBC)
        {
            string sql = "SELECT COUNT(1) FROM TAILIEUBC WHERE MATAILIEUBC = @ma";
            using (SqlConnection conn = Connection.getConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@ma", SqlDbType.VarChar).Value = maTLBC;
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }
        // ============================= tìm kiếm đó 

        public DataTable searchTaiLieu(string keyword)
        {
            DataTable dt = new DataTable();
            string sql = @"
                SELECT * FROM TAILIEUBC
                WHERE MATAILIEUBC LIKE @kw
                   OR FILEBC      LIKE @kw
                   OR SLIDE       LIKE @kw
                   OR LY_LICH     LIKE @kw";

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
