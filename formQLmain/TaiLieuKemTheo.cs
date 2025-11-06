using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formQLmain
{
    class TaiLieuKemTheo
    {
        private string _maTLBC;
        private string _fileBC;
        private string _slide;
        private string _lyLich;

        public TaiLieuKemTheo() { }

        public TaiLieuKemTheo(string maTLBC, string fileBC, string slide, string lyLich)
        {
            _maTLBC = maTLBC;
            _fileBC = fileBC;
            _slide = slide;
            _lyLich = lyLich;
        }

        public string MaTLBC { get => _maTLBC; set => _maTLBC = value; }
        public string FileBC { get => _fileBC; set => _fileBC = value; }
        public string Slide { get => _slide; set => _slide = value; }
        public string LyLich { get => _lyLich; set => _lyLich = value; }
    }
}
