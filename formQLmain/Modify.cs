using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace formQLmain
{
    class Modify
    {
        SqlDataAdapter dataAdapter; // sẽ truy xuất dữ liệu vào bảng 
        SqlCommand sqlCommand; // dùng để truy vấn và cập nhật tới CSDk
        public Modify()
        {
        }
        public DataTable getAllSinhVien() 
        { 
            DataTable dataTable= new DataTable();
            string query = "SELECT * FROM SINHVIEN";
            using (SqlConnection sqlConnection = Connection.getConnection())
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter(query, sqlConnection);
                dataAdapter.Fill(dataTable);
                sqlConnection.Close(); // hình nhưu adapter chỉ dùng cho sellect hay sao ý 
            }
            return dataTable;
        }
        //-------------------------------------------------------------------------------------------------------------
        // hàm để thêm sửa xóa dữ liệu 
        public bool insert(SinhVien sinhVien, out string error) // insert dữ liệu vàp 
        {
            error = "";
            SqlConnection sqlConnection = Connection.getConnection();
            string query = "INSERT INTO SINHVIEN (MASINHVIEN, MATK, HOTEN, CHUYENNGANH, LOP, KHOA) " +
               "VALUES (@maSV, @maTK, @hoTen, @chuyenNganh, @lop, @khoa)";

            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.Parameters.Add("@maSV", SqlDbType.VarChar).Value = sinhVien.MaSV;
                sqlCommand.Parameters.Add("@maTK", SqlDbType.VarChar).Value = sinhVien.MaTK;
                sqlCommand.Parameters.Add("@hoTen", SqlDbType.NVarChar).Value = sinhVien.HoTen;
                sqlCommand.Parameters.Add("@chuyenNganh", SqlDbType.NVarChar).Value = sinhVien.ChuyenNganh;
                sqlCommand.Parameters.Add("@lop", SqlDbType.NVarChar).Value = sinhVien.Lop;
                sqlCommand.Parameters.Add("@khoa", SqlDbType.NVarChar).Value = sinhVien.Khoa;
                sqlCommand.ExecuteNonQuery(); // thực thi câu lệnh truy vấn
            }
            catch (Exception ex)
            { // nếu lỗi thì trả về false
                error = ex.Message;
                return false;

            }
            finally
            {
                sqlConnection.Close();
            }
            return true; // nếu thành công trả về true
        }
        public bool update(SinhVien sinhVien, string maSVCu, out string error)
        {
            error = "";
            string query = @"
        UPDATE SINHVIEN
        SET MASINHVIEN = @maSV,
            MATK        = @maTK,
            HOTEN       = @hoTen,
            CHUYENNGANH = @chuyenNganh,
            LOP         = @lop,
            KHOA        = @khoa
        WHERE MASINHVIEN = @maSVCu";   // dùng mã cũ để xác định dòng cần sửa

            using (SqlConnection sqlConnection = Connection.getConnection())
            using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
            {
                sqlCommand.Parameters.Add("@maSVCu", SqlDbType.VarChar).Value = maSVCu;
                sqlCommand.Parameters.Add("@maSV", SqlDbType.VarChar).Value = sinhVien.MaSV;
                sqlCommand.Parameters.Add("@maTK", SqlDbType.VarChar).Value = sinhVien.MaTK;
                sqlCommand.Parameters.Add("@hoTen", SqlDbType.NVarChar).Value = sinhVien.HoTen;
                sqlCommand.Parameters.Add("@chuyenNganh", SqlDbType.NVarChar).Value = sinhVien.ChuyenNganh;
                sqlCommand.Parameters.Add("@lop", SqlDbType.NVarChar).Value = sinhVien.Lop;
                sqlCommand.Parameters.Add("@khoa", SqlDbType.NVarChar).Value = sinhVien.Khoa;

                try
                {
                    sqlConnection.Open();
                    int affected = sqlCommand.ExecuteNonQuery();
                    return affected > 0;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    return false;
                }
            }
        }
        //---------------------Hàm để xóa dữ liệu -------------------------------------------------
        public bool delete(string maSV, out string error) // xóa dữ liệu bảng sinh viên dựa vào mã sinh viên
        {
            error = "";
            string query = "DELETE FROM SINHVIEN WHERE MASINHVIEN = @maSV";

            using (SqlConnection sqlConnection = Connection.getConnection())
            using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
            {
                cmd.Parameters.Add("@maSV", SqlDbType.VarChar).Value = maSV;

                try
                {
                    sqlConnection.Open();
                    int affected = cmd.ExecuteNonQuery();
                    return affected > 0;   // true nếu xóa được >= 1 dòng
                }
                catch (Exception ex)
                {
                    error = ex.Message;    // ví dụ lỗi FK nếu còn tham chiếu
                    return false;
                }
            }
        }
        // ================= hàm tìm kiếm sinh viên ==================
        public DataTable searchSinhVien(string keyword)
        {
            DataTable dt = new DataTable();
            string sql = @"
        SELECT * FROM SINHVIEN
        WHERE MASINHVIEN   LIKE @kw
           OR MATK         LIKE @kw
           OR HOTEN        LIKE @kw
           OR CHUYENNGANH  LIKE @kw
           OR LOP          LIKE @kw
           OR KHOA         LIKE @kw";

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
        // ================hàm lọc 
        public DataTable filterSinhVien(string chuyenNganh, string lop, string khoa)
        {
            DataTable dt = new DataTable();
            string sql = @"
        SELECT * FROM SINHVIEN
        WHERE (@chuyenNganh = '' OR CHUYENNGANH = @chuyenNganh)
          AND (@lop = '' OR LOP = @lop)
          AND (@khoa = '' OR KHOA = @khoa)";

            using (SqlConnection conn = Connection.getConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@chuyenNganh", chuyenNganh ?? "");
                cmd.Parameters.AddWithValue("@lop", lop ?? "");
                cmd.Parameters.AddWithValue("@khoa", khoa ?? "");

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }


    }
}
