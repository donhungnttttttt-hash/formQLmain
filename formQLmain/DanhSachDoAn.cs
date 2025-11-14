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
        private string _maTLBC;
        private string _fileBC;
        private string _slide;
        private string _lyLich;
        private string _maTuKhoa;
        private string _tuKhoa;

        public DanhSachDoAn() { }

        public DanhSachDoAn(string maDoAn, string tenDeTai, string maSinhVien,
                            string gvhd, DateTime? namBaoVe, string maTaiLieuBC,
                            string tomTat, string maTLBC, string fileBC, string slide, string lyLich, string maTuKhoa, string tuKhoa)
        {
            _maDoAn = maDoAn;
            _tenDeTai = tenDeTai;
            _maSinhVien = maSinhVien;
            _gvhd = gvhd;
            _namBaoVe = namBaoVe;
            _maTaiLieuBC = maTaiLieuBC;
            _tomTat = tomTat;
            _maTLBC = maTLBC;
            _fileBC = fileBC;
            _slide = slide;
            _lyLich = lyLich;
            _maTuKhoa = maTuKhoa;
            _tuKhoa = tuKhoa;
        }

        public string MaDoAn { get => _maDoAn; set => _maDoAn = value; }
        public string TenDeTai { get => _tenDeTai; set => _tenDeTai = value; }
        public string MaSinhVien { get => _maSinhVien; set => _maSinhVien = value; }
        public string GVHD { get => _gvhd; set => _gvhd = value; }
        public DateTime? NamBaoVe { get => _namBaoVe; set => _namBaoVe = value; }
        public string MaTaiLieuBC { get => _maTaiLieuBC; set => _maTaiLieuBC = value; }
        public string TomTat { get => _tomTat; set => _tomTat = value; }
        // của tailieukemtheo 
        public string MaTLBC { get => _maTLBC; set => _maTLBC = value; }
        public string FileBC { get => _fileBC; set => _fileBC = value; }
        public string Slide { get => _slide; set => _slide = value; }
        public string LyLich { get => _lyLich; set => _lyLich = value; }
        public string MaTuKhoa { get => _maTuKhoa; set => _maTuKhoa = value; }

        public string TuKhoa { get => _tuKhoa; set => _tuKhoa = value; }
    }
}
