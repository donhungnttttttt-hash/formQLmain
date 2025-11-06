using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formQLmain
{
    class TaiKhoan
    {
        private string _maTK;
        private string _email;
        private string _matKhau;
        private DateTime? _ngayCap;
        private DateTime? _ngayCapNhat;
        private string _vaiTro;

        public TaiKhoan()
        {
        }

        public TaiKhoan(string maTK, string email, string matKhau, DateTime? ngayCap, DateTime? ngayCapNhat, string vaiTro)
        {
            _maTK = maTK;
            _email = email;
            _matKhau = matKhau;
            _ngayCap = ngayCap;
            _ngayCapNhat = ngayCapNhat;
            _vaiTro = vaiTro;
        }

        public string MaTK
        {
            get => _maTK;
            set => _maTK = value;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public string MatKhau
        {
            get => _matKhau;
            set => _matKhau = value;
        }

        public DateTime? NgayCap
        {
            get => _ngayCap;
            set => _ngayCap = value;
        }

        public DateTime? NgayCapNhat
        {
            get => _ngayCapNhat;
            set => _ngayCapNhat = value;
        }

        public string VaiTro
        {
            get => _vaiTro;
            set => _vaiTro = value;
        }
    }
}
