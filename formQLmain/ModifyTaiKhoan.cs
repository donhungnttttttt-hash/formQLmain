using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formQLmain
{
    class ModifyTaiKhoan
    {
        SqlDataAdapter _da;
        SqlCommand _cmd;

        // Lấy tất cả tài khoản
        public DataTable getAllTaiKhoan()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM TAIKHOAN";
            using (SqlConnection conn = Connection.getConnection())
            {
                _da = new SqlDataAdapter(sql, conn);
                _da.Fill(dt);
            }
            return dt;
        }

        // Thêm tài khoản
        public bool insert(TaiKhoan tk, out string error)
        {
            error = "";
            string sql = @"INSERT INTO TAIKHOAN(MATK, EMAIL, MATKHAU, NGAYCAP, NGAYCAPNHAT, VAITRO)
                           VALUES(@maTK, @email, @matKhau, @ngayCap, @ngayCapNhat, @vaiTro)";
            SqlConnection conn = Connection.getConnection();

            try
            {
                conn.Open();
                _cmd = new SqlCommand(sql, conn);

                _cmd.Parameters.Add("@maTK", SqlDbType.VarChar).Value = tk.MaTK;
                _cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = (object)tk.Email ?? DBNull.Value;
                _cmd.Parameters.Add("@matKhau", SqlDbType.VarChar).Value = (object)tk.MatKhau ?? DBNull.Value;

                // nullable DateTime -> DBNull.Value nếu null
                _cmd.Parameters.Add("@ngayCap", SqlDbType.DateTime).Value = (object)tk.NgayCap ?? DBNull.Value;
                _cmd.Parameters.Add("@ngayCapNhat", SqlDbType.DateTime).Value = (object)tk.NgayCapNhat ?? DBNull.Value;

                _cmd.Parameters.Add("@vaiTro", SqlDbType.VarChar).Value = (object)tk.VaiTro ?? DBNull.Value;

                _cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
            finally { conn.Close(); }
        }

        // Cập nhật tài khoản (cho phép đổi luôn MATK bằng maTKCu)
        public bool update(TaiKhoan tk, string maTKCu, out string error)
        {
            error = "";
            string sql = @"
                UPDATE TAIKHOAN
                SET MATK = @maTK,
                    EMAIL = @email,
                    MATKHAU = @matKhau,
                    NGAYCAP = @ngayCap,
                    NGAYCAPNHAT = @ngayCapNhat,
                    VAITRO = @vaiTro
                WHERE MATK = @maTKCu";

            SqlConnection conn = Connection.getConnection();

            try
            {
                conn.Open();
                _cmd = new SqlCommand(sql, conn);

                _cmd.Parameters.Add("@maTKCu", SqlDbType.VarChar).Value = maTKCu;

                _cmd.Parameters.Add("@maTK", SqlDbType.VarChar).Value = tk.MaTK;
                _cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = (object)tk.Email ?? DBNull.Value;
                _cmd.Parameters.Add("@matKhau", SqlDbType.VarChar).Value = (object)tk.MatKhau ?? DBNull.Value;
                _cmd.Parameters.Add("@ngayCap", SqlDbType.DateTime).Value = (object)tk.NgayCap ?? DBNull.Value;
                _cmd.Parameters.Add("@ngayCapNhat", SqlDbType.DateTime).Value = (object)tk.NgayCapNhat ?? DBNull.Value;
                _cmd.Parameters.Add("@vaiTro", SqlDbType.VarChar).Value = (object)tk.VaiTro ?? DBNull.Value;

                int affected = _cmd.ExecuteNonQuery();
                return affected > 0;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
            finally { conn.Close(); }
        }

        // Xóa tài khoản
        public bool delete(string maTK, out string error)
        {
            error = "";
            string sql = "DELETE FROM TAIKHOAN WHERE MATK = @maTK";
            SqlConnection conn = Connection.getConnection();

            try
            {
                conn.Open();
                _cmd = new SqlCommand(sql, conn);
                _cmd.Parameters.Add("@maTK", SqlDbType.VarChar).Value = maTK;

                int affected = _cmd.ExecuteNonQuery();
                return affected > 0;
            }
            catch (Exception ex)
            {
                error = ex.Message; // ví dụ lỗi FK nếu còn ràng buộc
                return false;
            }
            finally { conn.Close(); }
        }

        // (Tùy chọn) Kiểm tra trùng mã tài khoản trước khi Insert/đổi khóa
        public bool exists(string maTK)
        {
            string sql = "SELECT COUNT(1) FROM TAIKHOAN WHERE MATK = @maTK";
            using (SqlConnection conn = Connection.getConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@maTK", SqlDbType.VarChar).Value = maTK;
                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }
        //==== tìm kiếm với lọc 
        // Tìm kiếm theo từ khóa trên MATK/EMAIL/VAITRO và cả ngày (yyyy-MM-dd)
        public DataTable searchTaiKhoan(string keyword)
        {
            DataTable dt = new DataTable();
            string sql = @"
        SELECT * FROM TAIKHOAN
        WHERE MATK   LIKE @kw
           OR EMAIL  LIKE @kw
           OR VAITRO LIKE @kw
           OR CONVERT(varchar(10), NGAYCAP, 120)     LIKE @kw
           OR CONVERT(varchar(10), NGAYCAPNHAT, 120) LIKE @kw";

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
