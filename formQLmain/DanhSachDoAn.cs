using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formQLmain
{
    class DanhSachDoAn
    {
        private string _maDoAn;
        private string _tenDeTai;
        private string _maSinhVien;
        private string _gvhd;
        private DateTime? _namBaoVe;   // đổi sang DateTime?
        private string _maTaiLieuBC;
        private string _tomTat;

        public DanhSachDoAn() { }

        public DanhSachDoAn(string maDoAn, string tenDeTai, string maSinhVien,
                            string gvhd, DateTime? namBaoVe, string maTaiLieuBC, string tomTat)
        {
            _maDoAn = maDoAn;
            _tenDeTai = tenDeTai;
            _maSinhVien = maSinhVien;
            _gvhd = gvhd;
            _namBaoVe = namBaoVe;
            _maTaiLieuBC = maTaiLieuBC;
            _tomTat = tomTat;
        }

        public string MaDoAn { get => _maDoAn; set => _maDoAn = value; }
        public string TenDeTai { get => _tenDeTai; set => _tenDeTai = value; }
        public string MaSinhVien { get => _maSinhVien; set => _maSinhVien = value; }
        public string GVHD { get => _gvhd; set => _gvhd = value; }
        public DateTime? NamBaoVe { get => _namBaoVe; set => _namBaoVe = value; }
        public string MaTaiLieuBC { get => _maTaiLieuBC; set => _maTaiLieuBC = value; }
        public string TomTat { get => _tomTat; set => _tomTat = value; }
    }
}