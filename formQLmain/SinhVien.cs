using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formQLmain
{
    class SinhVien
    {

        private string _maSV;
        private string _maTK;
        private string _hoTen;
        private string _chuyenNganh;
        private string _lop;
        private string _khoa;

        public SinhVien()
        {
        }

        public SinhVien(string maSV, string maTK, string hoTen, string chuyenNganh, string lop, string khoa)
        {
            _maSV = maSV;
            _maTK = maTK;
            _hoTen = hoTen;
            _chuyenNganh = chuyenNganh;
            _lop = lop;
            _khoa = khoa;
        }

        public string MaSV { get => _maSV; set => _maSV = value; }
        public string MaTK { get => _maTK; set => _maTK = value; }
        public string HoTen { get => _hoTen; set => _hoTen = value; }
        public string ChuyenNganh { get => _chuyenNganh; set => _chuyenNganh = value; }
        public string Lop { get => _lop; set => _lop = value; }
        public string Khoa { get => _khoa; set => _khoa = value; }
    }
}
